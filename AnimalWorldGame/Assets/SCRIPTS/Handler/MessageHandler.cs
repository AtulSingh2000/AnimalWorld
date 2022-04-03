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
    private static extern void getUserB();

    [DllImport("__Internal")]
    private static extern void login(string type);

    [DllImport("__Internal")]
    private static extern void logout();

    [DllImport("__Internal")]
    private static extern void getdmodata();
    [DllImport("__Internal")]
    private static extern void getburnids();
    [DllImport("__Internal")]
    private static extern void getshopdata();
        [DllImport("__Internal")]
    private static extern void burnid(string id);
    [DllImport("__Internal")]
    private static extern void depositawc(string amount);

    [DllImport("__Internal")]
    private static extern void withdrawawc(string amount);

    [DllImport("__Internal")]
    private static extern void filldmo(string id,string amount);

    [DllImport("__Internal")]
    private static extern void buyshopl(string id,string amount);

    [DllImport("__Internal")]
    private static extern void register_asset(string asset_id,string name,string land_id,string type);

    [DllImport("__Internal")]
    private static extern void deregister_asset(string asset_id, string name,string type);

    [DllImport("__Internal")]
    private static extern void machine_start(string asset_id, string recipe_Id,string type);

    [DllImport("__Internal")]
    private static extern void boost(string asset_id, string type);

    [DllImport("__Internal")]
    private static extern void machine_claim(string asset_id, string recipe_Id,string type);

    [DllImport("__Internal")]
    private static extern void tree_claim(string symbol);

    [DllImport("__Internal")]
    private static extern void claim_all_assets(string type,string subtype,string land);

    public delegate void LoadingData();
    public static LoadingData onLoadingData;

    public delegate void UserData();
    public static UserData OnUserData;

    public delegate void AssetData(AssetModel[] assetModel);
    public static AssetData OnAssetData;

    public delegate void CallBack(CallBackDataModel[] callback);
    public static CallBack OnCallBackData;
    public static LevelModel[] levelModel = null;
    public static LoadingModel loadingModel = null;
    public static UserModel userModel = null;
    public static AssetModel[] assetModel = null;
    public static MarketOrderModel marketmodel = null;
    public static ShopModel shopmodle = null;

    public static MessageHandler instance;

    public delegate void MessageData(string[] messages);
    public static MessageData OnMessageData;

    public delegate void Shop(ShopModel[] shopmodel);
    public static Shop OnShopData;    

    public delegate void DMO(MarketOrderModel[] DMOmodel);
    public static DMO OnDMOData;

    public delegate void BurnData(BurnModel[] burnModel);
    public static BurnData OnBurnData;

    public static BurnModel[] burn_ids;

    public static List<InfoDataModel> infos = new List<InfoDataModel>();
    public  string infojson;

    public delegate void inv_data(IngModel[] user_balance);
    public static inv_data OnBalanceUpdate;

    public void Start()
    {
        string jsonData = JsonHelper.fixJson(infojson);
        infos.AddRange(JsonHelper.FromJson<InfoDataModel>(jsonData));
    }
    public static void Server_TryAutoLogin()
    {
        autologin();
    }
    public static void Server_GetUserData(string type)
    {
        login(type);
    }

    public static void Server_RegisterAsset(string asset_id,string name,string land_id,string type)
    {
        register_asset(asset_id, name,land_id,type);
    }

    public static void Server_DeRegisterAsset(string asset_id, string name,string type)
    {
        deregister_asset(asset_id, name,type);
    }
    public static void Server_LogoutSession()
    {
        logout();
    }

    public static void Server_StartMachine(string asset_id,string recipe_id,string type)
    {
        machine_start(asset_id, recipe_id,type);
    }

    public static void Server_ClaimMachine(string asset_id,string recipe_id,string type)
    {
        machine_claim(asset_id, recipe_id,type);
    }

    public static void Server_ClaimTree(string symbol)
    {
        tree_claim(symbol);
    }

    public static void Server_GetBurnids()
    {
        getburnids();

    }

    public static void Server_Claim_All_Assets(string type,string subtype)
    {
        claim_all_assets(type, subtype, userModel.land_id);
    }
public static void Server_BurnNFT(string asset_id)
    {
        burnid(asset_id);
    }
    public void Client_SetBurnids(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        burn_ids=JsonHelper.FromJson<BurnModel>(jsonData);
        OnBurnData(burn_ids);
    }
    public static void Server_Boost(string asset_id,string type)
    {
        boost(asset_id, type);
    }

    public void Client_SetUserData(string playerdata)
    {
        userModel = JsonUtility.FromJson<UserModel>(playerdata);
        OnUserData();
    }

    public void Client_FetchingData(string status)
    {
        Debug.Log(status);
        loadingModel.loading = status;
        onLoadingData();
    }

    public void Client_SetCallBackData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        OnCallBackData(JsonHelper.FromJson<CallBackDataModel>(jsonData));
    }

    public void Client_SetTreeData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        userModel.trees = JsonHelper.FromJson<AssetModel>(jsonData);
        Debug.Log("set tree data");
    }

    public void Client_SetMachineData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        userModel.machines = JsonHelper.FromJson<MachineDataModel>(jsonData);
    }

    public void Client_SetLevelData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        levelModel= JsonHelper.FromJson<LevelModel>(jsonData);
    }

    public void Client_SetCropData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        userModel.crops = JsonHelper.FromJson<CropDataModel>(jsonData);
    }

    public void Client_SetLandData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        userModel.lands = JsonHelper.FromJson<AssetModel>(jsonData);
    }
    public void Client_SetUserTreeData(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        userModel.user_data = JsonHelper.FromJson<ProduceDataModel>(jsonData);
        Debug.Log("set user Data");
    }

    public void Client_UpdateUserBalance(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        OnBalanceUpdate(JsonHelper.FromJson<IngModel>(jsonData));
        Debug.Log("in balance update");
    }

    public void Client_UpdateLandAssetsBalance(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        userModel.land_assets = JsonHelper.FromJson<IngModel>(jsonData);
    }
    public void Client_UpdateWalletBalance(string data)
    {
        Debug.Log("Message Handler + " + data);
        userModel.awcBal = data;
        Debug.Log("Updated Wallet Balance");
    }
  
    public static void Server_GetShopdata()
    {
        getshopdata();
    }

    public static void Server_UpdateUserBalance()
    {
        getUserB();
    }

    public static string GetBalanceKey(string key)
    {
        foreach(IngModel balance in userModel.user_balance)
        {
            if(balance.in_name==key)
            {
                return balance.in_qty;
            }
        }
        return "0.00";
    }

    public static LevelModel[] GetUserLevel()
    {
        string xpbalance= GetBalanceKey("AWXP");
        LevelModel[] final_level = new LevelModel[2];
        for(int i=0;i < levelModel.Length;i++)
        {
            if(double.TryParse(xpbalance,out double xp_bal))
            {
                string xp_amount = levelModel[i].xp_amount.Split(' ')[0];
                if (double.TryParse(xp_amount,out double level_amt))
                {
                    if(xp_bal >= level_amt)
                    {
                        final_level[0] = levelModel[i];
                        final_level[1] = levelModel[i + 1];
                    }
                }
            }
        }
        return final_level;
    }


    public void Client_SetShopdata(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        ShopModel[]  listings= JsonHelper.FromJson<ShopModel>(jsonData);
        OnShopData(listings);
    }

    public static void Server_GetDMOdata()
    {
        getdmodata();
    }
    public static void Server_FillDmo(string id,string amount)
    {
        filldmo(id,amount);
    }
    public static void Server_BuyShopL(string id,string amount)
    {
        buyshopl(id,amount);
    }

    public static void Server_DepositAWC(string amount)
    {
        depositawc(amount);
    }
    public static void Server_WithdrawAWC(string amount)
    {
        withdrawawc(amount);
    }

    
    public static string getType(string name)
    {
        return "";
    }
    public static string getProducer(string name)
    {
        return "";
    }

    public void Client_SetDMOdata(string data)
    {
        string jsonData = JsonHelper.fixJson(data);
        Debug.Log(jsonData);
        MarketOrderModel[]  dmos= JsonHelper.FromJson<MarketOrderModel>(jsonData);
        Debug.Log(dmos[0]);
        OnDMOData(dmos);
    }
}