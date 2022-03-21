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
    public string[] burn_ids=new string[0];
    public AbbvHelper helper;

    public GameObject DetailButton;

    public void SetData()
    {
        counttxt.text= count;
        nametxt.text= helper.recipes_abv[item_name];
        var sprite_img = Resources.Load<Sprite>("Sprites/" + helper.recipes_abv[item_name]);
        if (sprite_img)
            item_image.sprite = sprite_img;
    }

    public void OpenPopup()
    {
        IPopupCall popup= detail_view_panel.GetComponent<IPopupCall>();
        popup.name=item_name;
        popup.count=count;
        popup.type=type;
        popup.producer=producer;
        popup.description = description;
        popup.burn_ids=burn_ids;
        popup.item_image.sprite = item_image.sprite;
        popup.SetData();
        detail_view_panel.SetActive(true);
    }


}
