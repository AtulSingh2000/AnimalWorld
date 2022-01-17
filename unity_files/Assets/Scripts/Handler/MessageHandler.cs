using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MessageHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void autologin();

    [DllImport("__Internal")]
    private static extern void login(string type);

    [DllImport("__Internal")]
    private static extern void getAssetData();
    [DllImport("__Internal")]
    private static extern void mintcitizens();

    [DllImport("__Internal")]
    private static extern void burncitizennft();

    [DllImport("__Internal")]
    private static extern void getNinjaData();

    [DllImport("__Internal")]
    private static extern void searchcz(string assetid);

    [DllImport("__Internal")]
    private static extern void registernft(string assetid);

    public delegate void LoadingData();
    public static LoadingData onLoadingData;

    public delegate void UserData();
    public static UserData OnUserData;

    public delegate void AssetData(AssetModel[] assetModel);
    public static AssetData OnAssetData;

    public delegate void InventoryData(InventoryModel[] inventoryData);
    public static InventoryData OnInventoryData;

    public delegate void NinjaData(NinjaDataModel[] ninjaData);
    public static NinjaData OnNinjaData;

    public delegate void ProfessionData(ProfessionDataModel[] professionData);
    public static ProfessionData OnProfessionData;

    public delegate void ItemData(ItemDataModel[] itemData);
    public static ItemData OnItemData;

    public delegate void MaxNfts(MaxNftDataModel[] maxData);
    public static MaxNfts OnMaxNfts;

    public delegate void TransactionData();
    public static TransactionData OnTransactionData;

    public static LoadingModel loadingModel = null;
    public static UserModel userModel = null;
    public static AssetModel[] assetModel = null;
    public static InventoryModel[] inventoryData = null;
    public static NinjaDataModel[] ninjaData = null;
    public static ProfessionDataModel[] professionData = null;
    public static ItemDataModel[] itemData = null;
    public static MaxNftDataModel[] maxData = null;
    public static TransactionModel transactionModel = null;

    public static MessageHandler instance;

    public delegate void MessageData(string[] messages);
    public static MessageData OnMessageData;

    private HomeView homeView;

    public static void Server_TryAutoLogin()
    {
        autologin();
    }
    public static void Server_GetUserData(string type)
    {
        login(type);
    }

    public static void Server_GetAssetData()
    {
        getAssetData();
    }

    public static void Server_GetNinjaData()
    {
        getNinjaData();
    }

    public static void Server_MintCitizenPack()
    {
        mintcitizens();
    }

    /*public void Server_SearchCitizen(string assetid)
    {
        searchcz(assetid);
    }*/

    public static void Server_RegisterNFT(string assetid)
    {
        registernft(assetid);
    }

    public static void Server_BurnCitizenPack()
    {
        burncitizennft();
    }

    public void Client_SetUserData(string playerdata)
    {
        userModel = JsonUtility.FromJson<UserModel>(playerdata);
        ninjaData = userModel.ninjas;
        professionData = userModel.professions;
        itemData = userModel.items;
        inventoryData = userModel.inventory;
        assetModel = userModel.assets;
        maxData = userModel.nft_count;
        OnUserData();
    }

    public void Client_FetchingData(string status)
    {
        loadingModel.loading = status;
        onLoadingData();
    }

    public void Client_SetAssetData(string assetdata)
    {
        string jsonData = JsonHelper.fixJson(assetdata);
        assetModel = JsonHelper.FromJson<AssetModel>(jsonData);
        OnAssetData(assetModel);
    }

    public void Client_SetNinjaData(string ninjadata)
    {
        string jsonData = JsonHelper.fixJson(ninjadata);
        Debug.Log(jsonData);
        ninjaData = JsonHelper.FromJson<NinjaDataModel>(jsonData);
        Debug.Log("line 84 " + ninjaData[0].race);
        OnNinjaData(ninjaData);
    }

    public void Client_SetProfessionData(string professiondata)
    {
        string jsonData = JsonHelper.fixJson(professiondata);
        Debug.Log(jsonData);
        professionData = JsonHelper.FromJson<ProfessionDataModel>(jsonData);
        Debug.Log("line 97 " + professionData[0].type);
        OnProfessionData(professionData);
    }

    public void Client_SetItemData(string itemdata)
    {
        /*string jsonData = JsonHelper.fixJson(itemdata);
        Debug.Log(jsonData);
        itemData = JsonHelper.FromJson<ItemDataModel>(jsonData);
        Debug.Log("line 111 " + itemData);
        OnItemData(itemData);*/
    }

    public void Client_TrxHash(string trx)
    {
        if (!string.IsNullOrEmpty(trx))
        {
            transactionModel.transactionid = trx;
            OnTransactionData();
        }
        
    }

}