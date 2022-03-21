using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopView : BaseView
{

    private ShopModel[] listings = new ShopModel[0];
    private MarketOrderModel[] dmos = new MarketOrderModel[0];
   // private List<ConfigDataModel> juicer = new List<MachineDataModel>();

    public Transform dmoPanel;
    public Transform resourcePanel;
    public Transform mintPanel;
    public Transform packPanel;

    public Transform scrollview;
    public GameObject shop_scroll;

    public GameObject dmoPrefab;
    public GameObject resourcePrefab;
    public GameObject mintPrefab;
    public GameObject packPrefab;

    public RectTransform focus;

    public AbbvHelper helper;

    private bool pack = false;
    private bool dmo = false;
    private bool resource = false;
    private bool mint = false;
    private string type = "";

 protected override void Start()
    {
        base.Start();
        LoadingPanel.SetActive(true);
        MessageHandler.OnShopData += OnShopData;
        MessageHandler.OnDMOData += OnDMOData;
        MessageHandler.OnCallBackData += OnCallBackData;
        MessageHandler.Server_GetShopdata();
        MessageHandler.Server_GetDMOdata();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnShopData -= OnShopData;
        MessageHandler.OnDMOData -= OnDMOData;
        MessageHandler.OnCallBackData -= OnCallBackData;
    }


     public void OnShopData(ShopModel[] data)
     {
        listings=data;
        PopulateShop("dmo");
     }
    public void OnDMOData(MarketOrderModel[] data)
    {
        dmos = data;
        PopulateDMOs();
    }

    public void switchtype(string type)
    {
        LoadingPanel.SetActive(true);
        pack=false;
        dmo=false;
        resource=false;
        mint=false;
        dmoPanel.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        resourcePanel.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        mintPanel.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        packPanel.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);

        switch (type)
        {
            case "dmo":
                dmo=true;
                focus.SetParent(dmoPanel);
                dmoPanel.GetComponent<TMP_Text>().color = new Color32(255, 255, 255,255);
                PopulateDMOs();
                break;
            case "resource":
                resource=true;
                focus.SetParent(resourcePanel);
                resourcePanel.GetComponent<TMP_Text>().color = new Color32(255, 255, 255,255);
                PopulateShop("ingame");
                break;
            case "mint":
                mint=true;
                focus.SetParent(mintPanel);
                mintPanel.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                PopulateShop("mint");
                break;
            case "pack":
                pack=true;
                focus.SetParent(packPanel);
                packPanel.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                PopulateShop("pack");
                break;
        }
            focus.localPosition = new Vector3(0f, -25f, 0f);
            focus.localScale = new Vector3(1f, 1f, 1f);
    }
    
    public void PopulateDMOs()
    {
        LoadingPanel.SetActive(false);
        if (!shop_scroll.activeInHierarchy) shop_scroll.gameObject.SetActive(true);
        clearChildObjs(scrollview);

        foreach(MarketOrderModel dmo in dmos)
        {
            var ins = Instantiate(dmoPrefab);
            ins.transform.SetParent(scrollview);
            ins.transform.localScale=new Vector3(1f,1f,1f);
            var child = ins.gameObject.GetComponent<ShopCall>();
            child.type="dmo";
            child.id = dmo.id;
            child.type = dmo.type;
            child.products = dmo.products;
            child.reward = dmo.reward;
            child.xp_boost = dmo.xp_boost;
            child.level_boost = dmo.level_boost;
            child.xp_level = dmo.xp_level;
            child.LoadingPanel = LoadingPanel;
            child.helper = helper;
            child.SetData();
        }
    }

    public void OnCallBackData(CallBackDataModel[] callback)
    {
        CallBackDataModel callBack = callback[0];
        Debug.Log("in callBAck");
        if (!string.IsNullOrEmpty(callBack.type))
        {
            if (callBack.type == "order fill")
            {
                
            }
        }
    }

    public void PopulateShop(string type)
    {
        LoadingPanel.SetActive(false);
        if (!shop_scroll.activeInHierarchy) shop_scroll.gameObject.SetActive(true);
        clearChildObjs(scrollview);

        GameObject prefab = new GameObject();

        if (type == "ingame")
        {
            Debug.Log("set prefab");
            prefab = resourcePrefab;
        }
        else if (type == "mint")
        {
            prefab = mintPrefab;
        }
        else if (type == "pack")
        {
            prefab = packPrefab;
        }

        foreach (ShopModel listing in listings)
        {
            if(listing.type == type)
            {
                var ins = Instantiate(prefab);
                ins.transform.SetParent(scrollview);
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<ShopCall>();
                Debug.Log(listing.id);
                child.id = listing.id;
                child.type = listing.type;
                child.template_id = listing.template_id;
                child.schema = listing.schema;
                child.available = listing.available;
                child.max = listing.max;
                child.price = listing.price;
                child.resource = listing.resource;
                child.req_level = listing.req_level;
                child.LoadingPanel = LoadingPanel;
                child.helper = helper;
                child.SetData();
            }
        }
    }

    private void clearChildObjs(Transform transform)
    {
        if (transform.childCount >= 1)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
