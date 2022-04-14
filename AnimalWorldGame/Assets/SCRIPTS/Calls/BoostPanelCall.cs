using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPanelCall : MonoBehaviour
{
    public GameObject oil_obj;
    public GameObject fertilizer_obj;
    public GameObject animal_obj;
    public GameObject LoadingPanel;

    public string asset_id;
    public string type;
    public string symbol;


    public void use_boost()
    {
        LoadingPanel.SetActive(true);
        MessageHandler.Server_Boost(asset_id, type);
    }

    
}
