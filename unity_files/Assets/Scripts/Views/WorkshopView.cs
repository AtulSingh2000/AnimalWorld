using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopView : BaseView
{
    //public ItemDataModel[] idata;
    private List<ItemDataModel> CHammer = new List<ItemDataModel>();
    private List<ItemDataModel> CSaw = new List<ItemDataModel>();
    private List<ItemDataModel> CSickle = new List<ItemDataModel>();
    private List<ItemDataModel> CPickAxe = new List<ItemDataModel>();
    private List<ItemDataModel> CAxe = new List<ItemDataModel>();
    private List<ItemDataModel> CHoe = new List<ItemDataModel>();
    private List<ItemDataModel> BCart = new List<ItemDataModel>();
    private List<ItemDataModel> BWheelbarrow = new List<ItemDataModel>();
    private List<ItemDataModel> BWagon = new List<ItemDataModel>();

    private List<ItemDataModel> THammer = new List<ItemDataModel>();
    private List<ItemDataModel> TSaw = new List<ItemDataModel>();
    private List<ItemDataModel> TSickle = new List<ItemDataModel>();
    private List<ItemDataModel> TPickAxe = new List<ItemDataModel>();
    private List<ItemDataModel> TAxe = new List<ItemDataModel>();
    private List<ItemDataModel> THoe = new List<ItemDataModel>();
    private List<ItemDataModel> OCart = new List<ItemDataModel>();
    private List<ItemDataModel> OWheelBarrow = new List<ItemDataModel>();
    private List<ItemDataModel> OWagon = new List<ItemDataModel>();

    private List<ItemDataModel> IHammer = new List<ItemDataModel>();
    private List<ItemDataModel> ISaw = new List<ItemDataModel>();
    private List<ItemDataModel> ISickle = new List<ItemDataModel>();
    private List<ItemDataModel> IPickAxe = new List<ItemDataModel>();
    private List<ItemDataModel> IAxe = new List<ItemDataModel>();
    private List<ItemDataModel> IHoe = new List<ItemDataModel>();
    private List<ItemDataModel> TCart = new List<ItemDataModel>();
    private List<ItemDataModel> TWheelBarrow = new List<ItemDataModel>();
    private List<ItemDataModel> TWagon = new List<ItemDataModel>();
    protected override void Start()
    {
        base.Start();
        SetListData();
        //SetUI();

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void SetListData()
    {
        foreach(ItemDataModel idata in MessageHandler.userModel.items)
        {
            switch (idata.name)
            {

            }
        }
    }


}
