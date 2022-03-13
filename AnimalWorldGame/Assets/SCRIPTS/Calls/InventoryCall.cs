using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryCall : MonoBehaviour
{
    //Shop
    public string item_name;
    public string count;
    public string type;
    public string producer;
    public string description;
    public Image item_image;

    [Header("Config")]
    public TMP_Text counttxt;
    public TMP_Text nametxt;
    public TMP_Text dsctxt;
    public GameObject detail_view_panel;
    public Image icon;
    public string[] burn_ids=new string[0];

    public GameObject DetailButton;

    public void SetData()
    {
        counttxt.text= count;
        nametxt.text= item_name;
        //icon= item_image;
    }

    public void OpenPopup()
    {
                IPopupCall popup= detail_view_panel.GetComponent<IPopupCall>();
        popup.name=item_name;
        popup.count=count;
        popup.type=type;
        popup.producer=producer;
        popup.description=description;
        popup.burn_ids=burn_ids;
        //popup.image=icon;
        popup.SetData();
        detail_view_panel.SetActive(true);
    }


}
