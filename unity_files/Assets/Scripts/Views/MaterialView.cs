using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialView : BaseView
{
    public MaterialDataModel[] mdata;
    /*public TMP_Text refineTitle;
    public Image refineImg;
    public TMP_Text mintTitle;
    public Image mintImg;
    public TMP_Text addText;
    public Image addImg;

    public ImgObjectView[] images;*/

    protected override void Start()
    {
        base.Start();
        SetUI();

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();  
    }

    public void SetUI()
    {
        foreach(InventoryModel idata in MessageHandler.userModel.inventory)
        {
            foreach(MaterialDataModel m_data in mdata)
            {
                if(idata.name == m_data.name)
                {
                    m_data.count.text = idata.count;
                    if(idata.count == "0")
                    {
                        m_data.detail_btn.interactable = false;
                        UnityEngine.Color alpha = m_data.material_img.color;
                        alpha.a = 0.5f;
                        m_data.material_img.color = alpha; 
                    }
                }
            }
        }
    }
}
