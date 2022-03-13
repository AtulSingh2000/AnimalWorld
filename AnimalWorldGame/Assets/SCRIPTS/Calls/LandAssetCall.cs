using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandAssetCall : MonoBehaviour
{
    public string asset_id;
    public string asset_name;
    public TMP_Text asset_id_text;
    public TMP_Text registered_assets_text;
    public GameObject register_btn;
    public GameObject unregister_btn;
    public GameObject select_btn;
    public GameObject login_select_btn;
    public GameObject LoadingPanel;

    public void RegisterAsset()
    {
        Debug.Log("Register_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_RegisterAsset(asset_id, asset_name,MessageHandler.userModel.land_id,"land");
        }
        else
            SSTools.ShowMessage("Land Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void DeRegisterAsset()
    {
        Debug.Log("DeRegister_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_DeRegisterAsset(asset_id, asset_name,"land");
        }
        else
            SSTools.ShowMessage("Land Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }
}
