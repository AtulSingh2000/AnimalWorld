using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IPopupCall : MonoBehaviour
{
    //Shop
    public string description;
    public string name;
    public string count;
    public string type;
    public string producer;
    public Image item_image;

    [Header("Config")]
    public TMP_Text descriptiontxt;
    public TMP_Text nametxt;
    public TMP_Text typetxt;
    public TMP_Text counttxt;
    public TMP_Text availablenfts;
    public TMP_Text producertxt;
    public GameObject detail_view_panel;

    public string[] burn_ids=new string[0];

    public GameObject DetailButton;

    public void SetData()
    {
        descriptiontxt.text= description;
        nametxt.text= name;
        typetxt.text= type;
        counttxt.text= count;
        producertxt.text= producer;
        availablenfts.text= burn_ids.Length.ToString();
    }



    public void Burn()
    {
          Debug.Log("FillDMO");
        if (!string.IsNullOrEmpty(burn_ids[0]))
        {
            //LoadingPanel.SetActive(true);
            MessageHandler.Server_BurnNFT(burn_ids[0]);
List<string> tmp = new List<string>(burn_ids);
tmp.RemoveAt(0);
burn_ids = tmp.ToArray();
SetData();
        }
        else
            SSTools.ShowMessage("No NFT Available", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }
}
