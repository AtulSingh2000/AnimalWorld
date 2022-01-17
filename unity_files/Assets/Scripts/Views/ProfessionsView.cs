using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfessionsView : BaseView
{
    public TMP_Text username;
    public TMP_Text citizens;
    public TMP_Text professions;
    public TMP_Text materials;
    public TMP_Text ninjas;
    public TMP_Text miner_count;
    public TMP_Text farmer_count;
    public TMP_Text engineer_count;
    public TMP_Text lumberjack_count;
    public TMP_Text tailor_count;
    public TMP_Text blacksmith_count;
    public TMP_Text carpenter_count;
    public TMP_Text profession_text;
    public TMP_Text details_text;
    public TMP_Text types_text;
    public TMP_Text citizens_text;
    public TMP_Text uses_text;
    public TMP_Text books_text;
    public TMP_Text name_text;


    public GameObject Text_Panel;
    public GameObject ProfessionTitle_Panel;
    public GameObject Profession_Panel;
    public GameObject Profession_Prefab;
    public Transform Profession_Parent_Obj;
    public GameObject BlendingPanel;
    public RawImage BlendingPanel_img;
    public List<BlendingModel> BlendingData = new List<BlendingModel>();

    public ImgObjectView[] images;
    private List<ProfessionDataModel> Engineer = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Miners = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Farmers = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Tailor = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Carpenter = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> LumberJack = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Blacksmith = new List<ProfessionDataModel>();

    protected override void Start()
    {
        base.Start();
        SetModels();
        SetUIElements();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void SetModels()
    {
        ProfessionDataModel[] professions = MessageHandler.userModel.professions;
        foreach (ProfessionDataModel profess in professions)
        {
            if (profess.name == "Engineer")
            {
                Engineer.Add(profess);
            }
            else if(profess.name == "Tailor")
            {
                Tailor.Add(profess);
            }
            else if (profess.name == "Miner")
            {
                Miners.Add(profess);
            }
            else if (profess.name == "Blacksmith")
            {
                Blacksmith.Add(profess);
            }
            else if (profess.name == "Lumberjack")
            {
                LumberJack.Add(profess);
            }
            else if (profess.name == "Farmer")
            {
                Farmers.Add(profess);
            }
            else if (profess.name == "Carpenter")
            {
                Carpenter.Add(profess);
            }
        }
    }
    private void SetUIElements()
    {
        if (MessageHandler.userModel.account != null)
        {
            username.text = MessageHandler.userModel.account;
            citizens.text = MessageHandler.userModel.citizens;
            professions.text = MessageHandler.userModel.professions.Length.ToString();
            materials.text = MessageHandler.userModel.items.Length.ToString();
            ninjas.text = MessageHandler.userModel.ninjas.Length.ToString();
            miner_count.text = Miners.Count.ToString();
            farmer_count.text = Farmers.Count.ToString();
            engineer_count.text = Engineer.Count.ToString();
            tailor_count.text = Tailor.Count.ToString();
            lumberjack_count.text = LumberJack.Count.ToString();
            carpenter_count.text = Carpenter.Count.ToString();
            blacksmith_count.text = Blacksmith.Count.ToString();
        }
    }
    public void ProfessionNftBtn(string name)
    {
        if(Text_Panel.activeInHierarchy)Text_Panel.SetActive(false);
        if(!ProfessionTitle_Panel.activeInHierarchy)ProfessionTitle_Panel.SetActive(true);
        switch (name)
        {
            case "Miner":
                if (Miners.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(Miners);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData, "Miner");
                }
                break;
            case "Farmer":
                if (Farmers.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(Farmers);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData, "Farmer");
                }
                break;
            case "Engineer":
                if (Engineer.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(Engineer);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData,"Engineer");
                }
                break;
            case "Carpenter":
                if (Carpenter.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(Carpenter);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData,"Carpenter");
                }
                break;
            case "Tailor":
                if (Tailor.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(Tailor);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData,"Tailor");
                }
                break;
            case "Blacksmith":
                if (Blacksmith.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(Blacksmith);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData,"Blacksmith");
                }
                break;
            case "Lumberjack":
                if (LumberJack.Count > 0)
                {
                    if (!Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(true);
                    if (BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(false);
                    SetProfessions(LumberJack);
                }
                else
                {
                    if (Profession_Panel.activeInHierarchy) Profession_Panel.SetActive(false);
                    if (!BlendingPanel.activeInHierarchy) BlendingPanel.SetActive(true);
                    SetBlendingData(BlendingData,"Lumberjack");
                }
                break;
            default:
                break;
        }

    }

    public void SetBlendingData(List<BlendingModel> blendingModel,string name)
    {
        string maxCount = "";
        foreach (MaxNftDataModel nftData in MessageHandler.userModel.nft_count)
        {
            if (nftData.name == name)
            {
                maxCount = nftData.count;
                Debug.Log("Line 233 "+nftData.count);
                break;
            }
        }
        if(maxCount!="")profession_text.text = name + "   " + "0" + "/" + maxCount;else profession_text.text = name + "   " + "0" + "/" + 10;
        foreach (BlendingModel bmodel in blendingModel)
        {
            if(bmodel.profession == name)
            {
                int temp = 0;
                for (int j = 0; j < images.Length; j++)
                {
                    if (images[j].name == name)
                    {
                        temp = j;
                        break;
                    }
                }
                BlendingPanel_img.texture = images[temp].img;
                name_text.text = name;
                details_text.text = bmodel.details;
                uses_text.text = bmodel.uses;
                types_text.text = bmodel.types;
                citizens_text.text = bmodel.citizens;
                books_text.text = bmodel.books;
                break;
            }
        }
    }

    public void SetProfessions(List<ProfessionDataModel> professionModel)
    {
        
        string name = professionModel[0].name;
        string maxCount = "";
        foreach (MaxNftDataModel nftData in MessageHandler.userModel.nft_count)
        {
            if (nftData.name == name)
            {
                maxCount = nftData.count;
            }
        }
        if (maxCount != "") profession_text.text = name + "   " + "0" + "/" + maxCount; else profession_text.text = name + "   " + "0" + "/" + 10;
        int temp = 0;
        for (int j = 0; j < images.Length; j++)
        {
            if (images[j].name == name)
            {
                temp = j;
                break;
            }
        }
        if (Profession_Parent_Obj.childCount >= 1)
        {
            foreach (Transform child in Profession_Parent_Obj)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < professionModel.Count; i++)
        {
            var ins = Instantiate(Profession_Prefab);
            ins.transform.SetParent(Profession_Parent_Obj);
            var child = ins.gameObject.GetComponent<ProfessionCall>();
            child.asset_ids.text = "#" + professionModel[i].asset_id.ToString();
            child.item_count.text = "";//professionModel[i].items;
            child.uses_left.text = professionModel[i].uses_left.ToString();
            child.img.texture = images[temp].img;

            if (professionModel[i].status != "Idle")
            {
                DateTime search_datetime = DateTime.Parse(professionModel[i].last_material_search);
                DateTime search_datetime_local = search_datetime.ToLocalTime();
                int compare_result = DateTime.Compare(DateTime.Now, search_datetime_local);
                if (compare_result >= 0)
                {
                    child.Check.SetActive(true);
                }
                else if (compare_result < 0)
                {
                    child.Timer.SetActive(true);
                    child.timer.text = search_datetime.ToString();
                }
            }
            else if (professionModel[i].status == "Idle")
            {
                if (professionModel[i].reg == "0") child.UnRegistered.SetActive(true);
                else if (professionModel[i].reg == "1") child.Registered.SetActive(true);
            }
        }
        
    }
}
