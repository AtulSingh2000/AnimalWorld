//import axios from 'axios';

//const { response } = require("express");

const wax = new waxjs.WaxJS({
  rpcEndpoint: 'https://waxtestnet.greymass.com',
  tryAutoLogin: false
});

var loggedIn = false;
var anchorAuth = "owner";

const dapp = "WaxelNinjas";
const endpoint = "testnet.wax.pink.gg";
const contract = "waxelworld11";
const tokenContract = 'waxeltokens1';
const collectionName = 'laxewneftyyy';
const schemaName = 'laxewnefty';
let userAccount = "";

async function autoLogin() {
  var isAutoLoginAvailable = await wallet_isAutoLoginAvailable();
  if (isAutoLoginAvailable) {
    login();
  }
}

async function wallet_isAutoLoginAvailable() {
  const transport = new AnchorLinkBrowserTransport();
  const anchorLink = new AnchorLink({
    transport,
    chains: [{
      chainId: 'f16b1833c747c43682f4386fca9cbb327929334a762755ebec17f6f23c9b8a12',
      nodeUrl: 'https://waxtestnet.greymass.com',
    }],
  });
  var sessionList = await anchorLink.listSessions(dapp);
  if (sessionList && sessionList.length > 0) {
    useAnchor = true;
    return true;
  } else {
    useAnchor = false;
    return await wax.isAutoLoginAvailable();
  }
}

async function selectWallet(walletType) {
  wallet_selectWallet(walletType);
  login();
}

async function wallet_selectWallet(walletType) {
  useAnchor = walletType == "anchor";
}

async function login() {
  try {
    userAccount = await wallet_login();
    sendUserData();
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

async function wallet_login() {
  const transport = new AnchorLinkBrowserTransport();
  const anchorLink = new AnchorLink({
    transport,
    chains: [{
      chainId: 'f16b1833c747c43682f4386fca9cbb327929334a762755ebec17f6f23c9b8a12',
      nodeUrl: 'https://waxtestnet.greymass.com',
    }],
  });
  if (useAnchor) {
    var sessionList = await anchorLink.listSessions(dapp);
    if (sessionList && sessionList.length > 0) {
      wallet_session = await anchorLink.restoreSession(dapp);
    } else {
      wallet_session = (await anchorLink.login(dapp)).session;
    }
    wallet_userAccount = String(wallet_session.auth).split("@")[0];
    auth = String(wallet_session.auth).split("@")[1];
    anchorAuth = auth;
  } else {
    wallet_userAccount = await wax.login();
    wallet_session = wax.api;
    anchorAuth = "active";
  }
  return wallet_userAccount;
}

async function wallet_transact(actions) {
  if (useAnchor) {
    var result = await wallet_session.transact({
      actions: actions
    }, {
      blocksBehind: 3,
      expireSeconds: 30
    });
    result = {
      transaction_id: result.processed.id
    };
  } else {
    var result = await wallet_session.transact({
      actions: actions
    }, {
      blocksBehind: 3,
      expireSeconds: 30
    });
  }
  return result;
}

async function fecthingData(){
  let status = "true";
  unityInstance.SendMessage(
    "GameController",
    "Client_FetchingData",
    status 
  );
}

async function sendUserData() {
  try {

    fecthingData();

    userData = await getUserData();

    inventory = await getUserInventoryData(userData.mat_inventory);

    nft_count = await getNftCount(userData.nft_counts);

    ninjaData = await getNinjaData();
  
    professionData = await getProfessionData();

    itemData = await getItemsData();

    assetData = await getAssets("all");

    // configData = await getConfigData();

    let obj = {
      account: userAccount.toString(),
      ninjas: ninjaData,
      professions:professionData,
      items:itemData,
      citizens:userData.citizen_count,
      inventory:inventory,
      assets:assetData,
      nft_count:nft_count
    }

    console.log(obj);

    unityInstance.SendMessage(
      "GameController",
      "Client_SetUserData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );

  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}


async function getAssets(schema) {
  
  try
  {
    if(schema == "all")
    {
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&owner=" + userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
      const response = await fetch("https://" + "test.wax.api.atomicassets.io/" + path, {
        headers: {
          "Content-Type": "text/plain"
        },
        method: "POST",
      });

      const body = await response.json();
      const data = Object.values(body.data);
      var obj = [];
      for(const asset of data){
        obj.push({
          asset_id:asset.asset_id,
          img:asset.template.immutable_data.img,
          name:asset.template.immutable_data.name,
          schema:asset.schema.schema_name
        });
      }
      return obj;
    }
    else
    {
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&schema_name=" + schema + "&owner=" + userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
      const response = await fetch("https://" + "test.wax.api.atomicassets.io/" + path, {
        headers: {
          "Content-Type": "text/plain"
        },
        method: "POST",
      });

      const body = await response.json();
      return body.data;
    }
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}

async function getUserData() {
  var path = "/v1/chain/get_table_rows";

  var data = JSON.stringify({
    json: true,
    code: contract,
    scope: contract,
    table: "user",
    limit: 1,
    lower_bound: userAccount,
    upper_bound: userAccount,
  });

  const response = await fetch("https://" + endpoint + path, {
    headers: {
      "Content-Type": "text/plain"
    },
    body: data,
    method: "POST",
  });

  const body = await response.json();
  if (body.rows.length != 0)
    return body.rows[0];
  else return 0;
}

async function getUserInventoryData(data) {
  inv_obj = [];
  for(i = 0; i < data.length; i++){
    inv = data[i].split(" ");
    inv_obj.push({
      count:inv[0],
      name:inv[1]
    })
  }
  return inv_obj;
}

const getNftCount = async(userdata) =>{
  nft_count = [];
  nft_count.push({
    count:userdata.maxNinja,
    name:"Max Ninja"
  })
  const maxProfess = Object.values(userdata.maxProfessions);
  for (const data of maxProfess){
    nft_count.push({
      count:data.value,
      name:data.key
    })
  }
  
  return nft_count;
}

async function getAssetD() {
  try {

    let assets = getAssets();
    assetdata = await assets;

    console.log(assetdata);

    unityInstance.SendMessage(
      "GameController",
      "Client_SetAssetData",
      assetdata === undefined ? JSON.stringify({}) : JSON.stringify(assetdata)
    );
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}

async function getNinjaData() {
  try {

    let assets = getAssets("laxewnefty");
    assetData = await assets;

    var ninja_data = [];

    const arr = Object.values(assetData);
    for (const asset of arr) {
      const checkasset = await checkAsset("ninjas", asset.asset_id, asset.data.img);
      ninja_data.push(checkasset);
    }
    return ninja_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}

const getConfigData = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "configs",
      limit: 1
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });

    const body = await response.json();
    if (body.rows.length === 0) return false;
    else {
      console.log(body.rows[0]);
    }
  }
  catch(e){
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}

const checkAsset = async (table, assetId, img) => {
  try {

    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: table,
      limit: 1,
      lower_bound: assetId,
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });

    const body = await response.json();
    if (body.rows.length === 0) return false;
    if (body.rows[0].asset_id === assetId && body.rows[0].owner === userAccount) {
      if(table == "ninjas"){
        return {
          asset_id: body.rows[0].asset_id,
          delay_seconds: body.rows[0].delay_seconds,
          last_search: body.rows[0].last_search,
          race: body.rows[0].race,
          img: img,
          reg: "1",
        };
      }
      else if(table == "items"){
        return {
          asset_id: body.rows[0].asset_id,
          name: body.rows[0].name,
          profession:body.rows[0].profession,
          function_name:body.rows[0].function.key,
          function_value:body.rows[0].function.value,
          equipped:body.rows[0].equipped,	
          last_material_search: body.rows[0].last_material_search,
          uses_left: body.rows[0].uses_left,
          status:body.rows[0].status,
          img: img,
          reg: "1",
        };
      }
      else if(table == "professions"){
        return {
          asset_id: body.rows[0].asset_id,
          type:body.rows[0].type,
          name:body.rows[0].name,
          last_material_search: body.rows[0].last_material_search,
          uses_left: body.rows[0].uses_left,
          items:body.rows[0].items,
          status:"Idle",//body.rows[0].status,
          img: img,
          reg: "1",
        };
      }
    } else {
      if(table == "ninjas"){
        return {
          asset_id: body.rows[0].asset_id,
          delay_seconds: body.rows[0].delay_seconds,
          last_search: body.rows[0].last_search,
          race: body.rows[0].race,
          img: img,
          reg: "0",
        };
      }
      else if(table == "items"){
        return {
          asset_id: body.rows[0].asset_id,
          name: body.rows[0].name,
          profession:body.rows[0].profession,
          function_name:body.rows[0].function.key,
          function_value:body.rows[0].function.value,
          equipped:body.rows[0].equipped,	
          last_material_search: body.rows[0].last_material_search,
          uses_left: body.rows[0].uses_left,
          status:body.rows[0].status,
          img: img,
          reg: "0",
        };
      }
      else if(table == "professions"){
        return {
          asset_id: body.rows[0].asset_id,
          type:body.rows[0].type,
          name:body.rows[0].name,
          last_material_search: body.rows[0].last_material_search,
          uses_left: body.rows[0].uses_left,
          items:body.rows[0].items,
          status:"Idle",//body.rows[0].status,
          img: img,
          reg: "0",
        };
      }
    }
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
};

async function getProfessionData() {
  try {
    
    let assets = getAssets("professions");
    assetData = await assets;

    var profession_data = [];

    const arr = Object.values(assetData);
    for (const asset of arr) {
      const checkasset = await checkAsset("professions", asset.asset_id, asset.data.img);
      profession_data.push(checkasset);
    }
    return profession_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}

async function getItemsData() {
  try {

    let assets = getAssets("items");
    assetData = await assets;

    var items_data = [];
    const arr = Object.values(assetData);
    for (const asset of arr) {
      const checkasset = await checkAsset("items", asset.asset_id, asset.data.img);
      items_data.push(checkasset);
    }
    return items_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e);
  }
}

async function getNinjaD() {
  try {
    let ninjas = getNinjaData();
    ninjadata = await ninjas;

    unityInstance.SendMessage(
      "GameController",
      "Client_SetNinjaData",
      ninjadata === undefined ? JSON.stringify({}) : JSON.stringify(ninjadata)
    );
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", "unable to fetch ninja data");
  }
}

/*async function search_citizen(assetid) {
  try {
    console.log(assetid);
    await anchorSession.transact({
      action: {
        account: 'waxelnftgame',
        name: 'searchforcz',
        authorization: [anchorSession.auth],
        data: {
          account: anchorSession.auth.actor,
          ninjaID: assetid,
        }
      },
    })
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

async function registerAsset(assetid) {
  try {
    console.log(assetid);
    await anchorSession.transact({
      action: {
        account: 'waxelnftgame',
        name: 'registernft',
        authorization: [anchorSession.auth],
        data: {
          asset_id: assetid,
          owner: anchorSession.auth.actor,
        }
      },
    })
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }*/

  const mintcitizens = async() => {
    try{
      const result = await wallet_transact([{
        account: contract,
        name: "mintcitizens",
        authorization: [{
          actor: wallet_userAccount,
          permission: anchorAuth
        }],
        data: {
          account: wallet_userAccount,
          amount: 1
        },
      }, ]);
      await result;let arr;
      result.transaction_id?arr="Mint":"";
      unityInstance.SendMessage(
        "GameController",
        "Client_TrxHash",
        result === undefined ? "" : arr
      );
    }
    catch(e){
      unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
    }
  }

  const burncitizennft = async() => {
    try{
      const result = await wallet_transact([{
        account: contract,
        name: "mintcitizens",
        authorization: [{
          actor: wallet_userAccount,
          permission: anchorAuth
        }],
        data: {
          account: wallet_userAccount,
          amount: 1
        },
      }, ]);
      await result;
      let arr;
      result.transaction_id?arr="Burn":"";
      unityInstance.SendMessage(
        "GameController",
        "Client_TrxHash",
        result === undefined ? "" : arr
      );
    }
    catch(e){
      unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
    }
  }