using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : BaseView
{
    public GameObject ual_wax;
    public GameObject login_btn;
    public GameObject fetching_data_panel;
    public GameObject parent_panel;

    //Land Selection Elements
    public GameObject land_panel;
    public GameObject land_prefab;
    public Transform land_parent_obj;

    private string id = "";
    protected override void Start()
    {
        base.Start();
        MessageHandler.Server_TryAutoLogin();
        MessageHandler.onLoadingData += onLoadingData;
        MessageHandler.OnUserData += OnUserData;
        MessageHandler.OnCallBackData += OnCallBackData;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.onLoadingData -= onLoadingData;
        MessageHandler.OnUserData -= OnUserData;
        MessageHandler.OnCallBackData -= OnCallBackData;
    }

    public void OnAnchorButtonClick()
    {
        MessageHandler.Server_GetUserData("anchor");
    }

    public void OnWaxButtonClick()
    {
        MessageHandler.Server_GetUserData("cloud");
    }

    public void onCreateButtonClick()
    {
        Application.OpenURL("https://all-access.wax.io/");
    }
    public void OnLoginButtonClick()
    {
        login_btn.SetActive(false);
        ual_wax.SetActive(true);
    }

    public void onLoadingData()
    {
        Debug.Log("In OnLoadingData");
        if (MessageHandler.loadingModel.loading == "true")
        {
            ual_wax.SetActive(false);
            fetching_data_panel.SetActive(true);
            login_btn.SetActive(false);
        }
    }

    private void OnUserData()
    {
        Debug.Log("In OnUserData");
        LoadingPanel.SetActive(false);
        parent_panel.SetActive(false);
        land_panel.SetActive(true);
        foreach(AssetModel land in MessageHandler.userModel.lands)
        {
            var ins = Instantiate(land_prefab);
            ins.transform.SetParent(land_parent_obj);
            ins.transform.localScale = new Vector3(1, 1, 1);
            var child = ins.gameObject.GetComponent<LandAssetCall>();
            child.asset_id = land.asset_id;
            child.asset_name = land.name;
            child.asset_id_text.text = "#" + land.asset_id;
            child.login_select_btn.gameObject.SetActive(true);
            child.registered_assets_text.gameObject.SetActive(false);
            child.LoadingPanel = LoadingPanel;
            if(land.reg == "0")
            {
                id = land.asset_id;
                child.login_select_btn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { child.RegisterAsset(); });
            }
            else
                child.login_select_btn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Select_Land(land.asset_id); });
        }

    }

    public void OnCallBackData(CallBackDataModel[] callback)
    {
        CallBackDataModel callBack = callback[0];
        if (!string.IsNullOrEmpty(callBack.type))
        {
            if (callBack.type == "reg")
            {
                LoadingPanel.SetActive(true);
                if (id == "community")
                    MessageHandler.userModel.land_id = "0";
                else
                    MessageHandler.userModel.land_id = id;

                Debug.Log(id);
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void Select_Land(string asset_id)
    {
        LoadingPanel.SetActive(true);
        if (asset_id == "community")
            MessageHandler.userModel.land_id = "0";
        else
            MessageHandler.userModel.land_id = asset_id;

        SceneManager.LoadScene("GameScene");
    }

}
