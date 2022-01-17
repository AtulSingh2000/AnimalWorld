using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CitizenView : BaseView
{
    public GameObject PermissionPanel;
    public GameObject DonePanel;
    public GameObject DonePanel_Obj;
    public GameObject Loader;
    public GameObject MintText;
    public GameObject BurnText;
    public Button SellBtn;
    public TextMeshProUGUI Text;

    public TMP_Text username;
    public TMP_Text citizens;
    public TMP_Text professions;
    public TMP_Text materials;
    public TMP_Text ninjas;
    public TMP_Text done_panel_text;
    protected override void Start()
    {
        Debug.Log("In");
        base.Start();
        Debug.Log("After base start");
        MessageHandler.OnTransactionData += OnTransactionData;
        Debug.Log("After TransactionData");
        SetUIElements();
        Debug.Log("After SetUI");
        AssetModel[] assets = MessageHandler.assetModel;
        Debug.Log("Line 34 - " + assets[2].name);
        for (int i = 0; i < assets.Length; i++)
        {
            if (assets[i].schema == "citizens" && assets[i].name == "Citizens - 10x")
            {
                SellBtn.interactable = true;
                break;
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnTransactionData -= OnTransactionData;
    }

    private void SetUIElements()
    {
        if (MessageHandler.userModel.account != null)
        {
            Debug.Log("in setui");
            username.text = MessageHandler.userModel.account;
            citizens.text = MessageHandler.userModel.citizens;
            professions.text = MessageHandler.userModel.professions.Length.ToString();
            materials.text = MessageHandler.userModel.items.Length.ToString();
            ninjas.text = MessageHandler.userModel.ninjas.Length.ToString();
            Debug.Log("SetUI done");
        }
    }

    public void MintButton()
    {
        PermissionPanel.SetActive(true);
        if(BurnText.activeInHierarchy)
            BurnText.SetActive(false);
        MintText.SetActive(true);
    }

    public void SellButton()
    {
        Application.OpenURL("https://wax-test.atomichub.io/market?collection_name=laxewneftyyy&schema_name=citizens&template_id=263183");
    }

    public void AddButton()
    {
        PermissionPanel.SetActive(true);
        if (MintText.activeInHierarchy)
            MintText.SetActive(false);
        BurnText.SetActive(true);
    }

    public void BuyButton()
    {
        Application.OpenURL("https://wax-test.atomichub.io/market?collection_name=laxewneftyyy&schema_name=citizens&template_id=263183");
    }

    public void YesButton()
    {
        if (MintText.activeInHierarchy)
        {
            PermissionPanel.SetActive(false);
            DonePanel.SetActive(true);
            Loader.SetActive(true);
            MessageHandler.Server_MintCitizenPack();
        }
        else if (BurnText.activeInHierarchy)
        {
            PermissionPanel.SetActive(false);
            DonePanel.SetActive(true);
            Loader.SetActive(true);
            MessageHandler.Server_BurnCitizenPack();
        }
    }

    public void NoButton()
    {
        PermissionPanel.SetActive(false);
    }

    public void OkayButton()
    {
       DonePanel.SetActive(false);
    }

    public void SelectProfession_Btn()
    {
        SceneManager.LoadScene("ProfessionScene");
    }

    private void OnTransactionData()
    {
        if (MessageHandler.transactionModel.transactionid != "")
        {
            Loader.SetActive(false);
            DonePanel_Obj.SetActive(true);
            
            if(MessageHandler.transactionModel.transactionid == "Mint")done_panel_text.text = "The Citizens - 10x NFT was added to your wallet";
            if (MessageHandler.transactionModel.transactionid == "Burn")done_panel_text.text = "10 Citizens have been added to your account";
        }

    }
}
