using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryView : BaseView
{

    public Transform inventoryParent;
        [Header("Left Panel")]
    [Space]
    public Transform a1;
    public Transform b1;
    public Transform c1;
    public Transform d1;
    public Transform focus1;

        [Header("Top Panel")]
    [Space]
    public Transform a;
    public Transform b;
    public Transform c;
    public Transform d;
    public Transform focus;

    public GameObject prefab;


    public string current_type = "all";
    public string current_producer = "all";
    public IngModel[] balances= new IngModel[1];
    [Header("Details Panel")]
    [Space]
    public GameObject detail_view_panel;
    public Image detail_view_img;
    public TMP_Text item_type;
    public TMP_Text item_name;
    public TMP_Text descripton_text;
    public TMP_Text avaialable_text;
    public Button burn_btn;
    public AbbvHelper helper;

    public BurnModel[] burn_ids;
    
    public List<InfoDataModel> infos = new List<InfoDataModel>();
    public  string infojson;

             protected override void Start()
    {
        base.Start();
                        string jsonData = JsonHelper.fixJson(infojson);
        infos.AddRange(JsonHelper.FromJson<InfoDataModel>(jsonData));
            Debug.Log(infos[1]);
        current_producer="all";
        current_type="all";
        Populate();
        MessageHandler.Server_GetBurnids();
        MessageHandler.OnBurnData+=OnBurnData;
        //MessageHandler.Server_UpdateUserBalance();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnBurnData-=OnBurnData;
    }

    public void switchtype(string type)
    {
        current_type = type;
            {
        a.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        b.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        c.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        d.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        switch (type)
        {
            case "all":
                focus.SetParent(a);
                a.GetComponent<TMP_Text>().color = new Color32(255, 255, 255,255);
                break;
            case "resources":
                focus.SetParent(b);
                b.GetComponent<TMP_Text>().color = new Color32(255, 255, 255,255);
                break;
            case "products":
                focus.SetParent(c);
                c.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                break;
            case "boosters":
                focus.SetParent(d);
                d.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                break;
        }
            focus.localPosition = new Vector3(0f, -25f, 0f);
            focus.localScale = new Vector3(1f, 1f, 1f);
    }
        Populate();
    }

    public void switchproducer(string type)
    {
        current_producer = type;
                a1.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        b1.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        c1.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        d1.GetComponent<TMP_Text>().color = new Color32(74, 172, 247, 255);
        switch (type)
        {
            case "all":
                focus1.SetParent(a1);
                a1.GetComponent<TMP_Text>().color = new Color32(255, 255, 255,255);
                break;
            case "trees":
                focus1.SetParent(b1);
                b1.GetComponent<TMP_Text>().color = new Color32(255, 255, 255,255);
                break;
            case "cropfields":
                focus1.SetParent(c1);
                c1.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                break;
            case "animals":
                focus1.SetParent(d1);
                d1.GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
                break;
        }
            focus1.localPosition = new Vector3(0f, -25f, 0f);
            focus1.localScale = new Vector3(1f, 1f, 1f);
        Populate();
    }

    public void ongameBalance()

{
    LoadingPanel.SetActive(false);
    //Populate();
}

    public void OnBurnData(BurnModel[] burnModel)

{
    LoadingPanel.SetActive(false);
    burn_ids=burnModel;
    Populate();
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
    public void ClosePopup()
    {
        detail_view_panel.SetActive(false);
    }

    public void Populate()
    {
        clearChildObjs(inventoryParent);

        foreach(IngModel balance in MessageHandler.userModel.user_balance)
        {
            if(balance.in_name!="AWXP"&&balance.in_name!="AWC")
            {
            InfoDataModel data= new InfoDataModel();
            string type="";
            string producer="";
            string description="";
            foreach(InfoDataModel temp in MessageHandler.infos)
            {
                if(temp.id==balance.in_name)
                {
                    Debug.Log("found");
                    data=temp;
                    type=data.type;
                    producer=data.producer;
                    description=data.description;
                }
            }

            if( (current_type==type || current_type=="all") && (current_producer==producer || current_producer=="all"))
            {
                var ins = Instantiate(prefab);
                ins.transform.SetParent(inventoryParent);
                var child = ins.gameObject.GetComponent<InventoryCall>();
                ins.transform.localScale=new Vector3(1,1,1);
                child.item_name = balance.in_name;
                child.count=balance.in_qty;
                child.type=type;
                child.producer=producer;
                child.description=description;
                child.detail_view_panel=detail_view_panel;
                child.burn_ids=filterids(balance.in_name);
                child.SetData();
            }
        }
    }}

    public string[] filterids(string symbol)
    {
        foreach(BurnModel d in burn_ids)
        {
            if(d.symbol==symbol)
            {
                return d.ids;
            }
        }
        return new string[0];
    }

}
