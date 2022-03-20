const wax = new waxjs.WaxJS({
  rpcEndpoint: 'https://waxtestnet.greymass.com',
  tryAutoLogin: false
});

var loggedIn = false;
var anchorAuth = "owner";

const dapp = "AnimalWorld";
const endpoint = "testnet.wax.pink.gg";
const contract = "anmworldgame";
const tokenContract = 'tokenanimal1';
const collectionName = 'animaltestin';
const schemaName = '';
let userAccount = "";
const symbol = 'AWC';
const tree_symbols = ["FIG","LEMON","MANGO","ORANGE","COCONUT"];
const crop_symbols = ["CLY","CORN","CRTSE","CLYSE","WHEAT","SBEANE","CORNSE","WHEATSE"];
let awc_balance = "";

const autoLogin = async () => {
  var isAutoLoginAvailable = await wallet_isAutoLoginAvailable();
  if (isAutoLoginAvailable) {
    login();
  }
}

const wallet_isAutoLoginAvailable = async () => {
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

const selectWallet = async (walletType) => {
  wallet_selectWallet(walletType);
  login();
}

const wallet_selectWallet = async (walletType) => {
  useAnchor = walletType == "anchor";
}

const login = async () => {
  try {
    userAccount = await wallet_login();
    sendUserData();
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const logout = async () => {
  console.log("in");
  await wallet_logout();
  loggedIn = false;
  let obj = [];
  obj.push({
    type: "logout"
  });
  unityInstance.SendMessage(
    "GameController",
    "Client_SetCallBackData",
    obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
  );
}

const wallet_login = async () => {
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

const wallet_transact = async (actions) => {
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

async function fetchingData() {
  let status = "true";
  unityInstance.SendMessage(
    "GameController",
    "Client_FetchingData",
    status
  );
}

const delay = async (delayInms) => {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve(2);
    }, delayInms);
  });
}

const sendUserData = async () => {
  try {
    await fetchingData();
    balance = await getAwcBalance();
    userData = await getUserData();
    treeData = await getTreeData();
    recipeData = await getRecipeData();
    machineData = await getMachineData();
    cropfieldData = await getCropsData();
    landData = await getLandData();
    userBalanceData = await getUserBalance();
    let obj = {
      account: userAccount.toString(),
      awcBal: balance,
      trees: treeData,
      lands: landData,
      machines: machineData,
      machine_recipes: recipeData,
      user_balance: userBalanceData,
      crops: cropfieldData,
      user_data: userData,
    }
    console.log(obj);
    unityInstance.SendMessage(
      "GameController",
      "Client_SetUserData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );

  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}


const getAssets = async (schema) => {
  try {
    if (schema == "all") {
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&owner=" + wallet_userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
      const response = await fetch("https://" + "test.wax.api.atomicassets.io/" + path, {
        headers: {
          "Content-Type": "text/plain"
        },
        method: "POST",
      });

      const body = await response.json();
      return body.data;
    } else {
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&schema_name=" + schema + "&owner=" + wallet_userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
      const response = await fetch("https://" + "test.wax.api.atomicassets.io/" + path, {
        headers: {
          "Content-Type": "text/plain"
        },
        method: "POST",
      });

      const body = await response.json();
      const data = Object.values(body.data);
      var obj = [];
      for (const asset of data) {
        obj.push({
          asset_id: asset.asset_id,
          template_id : asset.template.template_id,
          name: asset.template.immutable_data.name,
          schema: asset.schema.schema_name,
          rarity: asset.template.immutable_data.Rarity,
          level: typeof(asset.mutable_data.Level) != 'undefined' ? asset.mutable_data.Level : "1",
          harvests : typeof(asset.mutable_data.harvests) != 'undefined' ? asset.mutable_data.harvests : "0"
        });
      }
      return obj;
    }
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getLevelData = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "lvlconfig",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    let obj = [];
    if (body.rows.length != 0) {
      for (const data of Object.values(body.rows)) {
        obj.push({
          xp_amount: data.xp_amount,
          level : data.level,
          max_machine_level: data.max_machine_level,
          max_cropf_level: data.max_cropf_level,
          max_animal_level: data.max_animal_level
        });
      }
    }
    unityInstance.SendMessage(
      "GameController",
      "Client_SetLevelData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );

  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getUserBalance = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: userAccount,
      table: "balances",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    console.log(body.rows);
    let obj = [];
    if (body.rows.length != 0) {
      for (const data of Object.values(body.rows)) {
        new_string = data.balance.split(' ');
        if(new_string[1] == "AWC")
          awc_balance = new_string[0] + " AWC";

        obj.push({
          in_qty: new_string[0],
          in_name: new_string[1] == "COCUNUT" ? "COCONUT" : new_string[1],
          product_from: "tree"                
        });
      }
    }
    await getLevelData();
    return obj;
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getUserB = async() =>{
  try{
    data = await getUserBalance();
    unityInstance.SendMessage(
      "GameController",
      "Client_UpdateUserBalance",
      data === undefined ? JSON.stringify({}) : JSON.stringify(data)
    );
  }
  catch(e){
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}
const getdmodata = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "dmos",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    console.log(body.rows);
    let obj = [];

    
    if (body.rows.length != 0) {
      for (const data of Object.values(body.rows)) {
        products=[];
        data.products.forEach( x=> {
          products.push({
            in_qty: parseFloat(x.split(' ')[0]).toFixed(0),
            in_name: x.split(' ')[1] == "COCUNUT" ? "COCONUT" : x.split(' ')[1]
          });
        });

        obj.push({
          id : data.id,
          type : data.type,	
          reward :({
            in_qty: parseFloat(data.reward.split(' ')[0]).toFixed(2),
            in_name: data.reward.split(' ')[1] == "COCUNUT" ? "COCONUT" : data.reward.split(' ')[1]
          }),	
          xp_boost : ({
            in_qty: parseFloat(data.xp_boost.split(' ')[0]).toFixed(2),
            in_name: data.xp_boost.split(' ')[1] == "COCUNUT" ? "COCONUT" : data.reward.split(' ')[1]
          }),	
          products : products,	
          level_boost : data.max,	
          xp_level : data.xp_level
        });
      }
    }
    unityInstance.SendMessage(
      "GameController",
      "Client_SetDMOdata",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );

  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getshopdata = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "shop",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    console.log(body.rows);
    let obj = [];
    if (body.rows.length != 0) {
      for (const data of Object.values(body.rows)) {
        obj.push({
          id : data.id,
          type : data.type,	
          template_id : data.template_id,	
          schema : data.schema,	
          available : data.available,	
          max : data.max,	
          price : ({
            in_qty: parseFloat(data.price.split(' ')[0]).toFixed(2),
            in_name: data.price.split(' ')[1] == "COCUNUT" ? "COCONUT" : data.price.split(' ')[1]
          }),	
          resource : ({
            in_qty: parseFloat(data.resource.split(' ')[0]).toFixed(2),
            in_name: data.resource.split(' ')[1] == "COCUNUT" ? "COCONUT" : data.resource.split(' ')[1]
          }),	
          req_level : data.req_level
        });
      }
    }
    unityInstance.SendMessage(
      "GameController",
      "Client_SetShopdata",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );

  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getAwcBalance = async() => {
  var path = "/v1/chain/get_table_rows";
  var data = JSON.stringify({
    json: true,
    code: "anmworldtokn",
    scope: wallet_userAccount,
    table: "accounts",
    limit: 1,
  });

  const response = await fetch("https://" + endpoint + path, {
    headers: {
      "Content-Type": "text/plain"
    },
    body: data,
    method: "POST",
  });

  const body = await response.json();
  balance = parseFloat(0.0000);
  if (body.rows.length != 0) {
    for (j = 0; j < body.rows.length; j++) {
      if (body.rows[j].balance.includes('AWC'))
        balance = parseFloat(body.rows[j].balance).toFixed(4);
      return balance;
    }
  }
  return balance;
}

const getAwcB = async() => {
  try {
    let updatedBal = await getAwcBalance();
    unityInstance.SendMessage(
      "GameController",
      "Client_UpdateWalletBalance",
      updatedBal === undefined ? JSON.stringify({}) : updatedBal
    );
  }
  catch(e){
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getUserData = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: userAccount,
      table: "user",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    let obj = [];
    if (body.rows.length != 0) {
      for (const data of Object.values(body.rows)) {
        new_string = data.power.split(' ');
        obj.push({
          name: new_string[1] == "COCUNUT" ? "COCONUT" : new_string[1],
          value: new_string[0],
          unclaimed: data.unclaimed.split(' ')[0],
          last_claim: data.last_claim,
          cd_start: data.cooldown,
          harvests: data.harvests
        });
      }
    }
    return obj;
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getUserD = async() =>{
  try{
    data = await getUserData();
    unityInstance.SendMessage(
      "GameController",
      "Client_SetUserTreeData",
      data === undefined ? JSON.stringify({}) : JSON.stringify(data)
    );
  }
  catch(e){
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getProduceData = async (userData) => {
  try {
    console.log((userData));
    const user_d = Object.values(userData);
    let obj = [];
    for (const data of user_d) {
      let new_string = data.power.split(' ');
      obj.push({
        name: new_string[1] == "COCUNUT" ? "COCONUT" : new_string[1],
        value: new_string[0]
      });
    }
    console.log(obj);
    return obj;
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

async function getUserInventoryData(data) {
  inv_obj = [];
  if (data) {
    for (i = 0; i < data.length; i++) {
      inv = data[i].split(" ");
      inv_obj.push({
        count: inv[0],
        name: inv[1]
      });
    }
  }
  return inv_obj;
}

const getTreeConfig = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "treeconfigs",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    let obj = [];
    if(body.rows.length != 0){
      for(const data of Object.values(body.rows)){
        obj.push({
          template_id: data.template_id,
          max_harvests: data.max_harvests,
          boost: data.tree_boost,
          level_boost: data.level_boost
        })
      }
    }
    return obj;
  }
  catch(e){
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getMachineConfig = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "mchconfig",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    let obj = [];
    if(body.rows.length != 0){
      for(const data of Object.values(body.rows)){
        obj.push({
          template_id: data.template_id,
          max_harvests: data.cd_harvests,
          cost_level: data.cost_level
        })
      }
    }
    return obj;
  }
  catch(e){
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getTreeData = async () => {
  try {
    let assetData = await getAssets("tree");
    let config = Object.values(await getTreeConfig());
    var tree_data = [];
    const arr = Object.values(assetData);
    console.log(arr);
    if (arr.length != 0) {
      const check_data = await checkAssetIds("dtrees");
      console.log(check_data);
      if (typeof check_data !== 'undefined') {
        const check_ids = check_data[0];
        for (const asset of arr) {
          let harvest,boost,level_boost,cost_level = "";
          if (check_ids.includes(asset.asset_id)) {
            const check_body_data = check_data[1];
            for (const bodyData of check_body_data) {
              if (bodyData.id == asset.asset_id) {
                for(const c_data of config){

                  if(asset.template_id == c_data.template_id){
                    harvest = c_data.max_harvests;
                    boost = c_data.boost;
                    level_boost = c_data.level_boost;
                    //cost_level = c_data.cost_level;
                    break;
                  }
                }
                tree_data.push({
                  name: asset.name,
                  type: "tree",
                  asset_id: asset.asset_id,
                  land_id: bodyData.land_id,
                  prod_pwer: bodyData.prod_power,
                  rarity: asset.rarity,
                  cooldown: bodyData.cooldown,
                  last_claim: bodyData.last_claim,
                  delay : bodyData.delay,
                  max_harvests: harvest,
                  level : "Level"+asset.level,
                  current_harvests: asset.harvests,
                  boost: boost,
                  level_boost: level_boost,
                  reg: "1",
                  //cost_level: cost_level
                });
              }
            }
          } else {
            tree_data.push({
              name: asset.name,
              type: "tree",
              asset_id: asset.asset_id,
              land_id: "null",
              prod_pwer: "0",
              rarity: asset.rarity,
              cooldown: "0",
              last_claim: "0",
              delay : "0",
              reg: "0",
              max_harvests: harvest,
              level : "Level"+asset.level,
              current_harvests: asset.harvests,
              boost: boost,
              level_boost: level_boost,
            });
          }
        }
      } else {
        for(const c_data of config){
              if(asset.template_id == c_data.template_id){
                harvest = c_data.max_harvests;
                boost = c_data.boost;
                level_boost = c_data.level_boost
                break;
              }
            }
          tree_data.push({
            name: asset.name,
            type: "tree",
            asset_id: asset.asset_id,
            land_id: "null",
            prod_pwer: "0",
            rarity: asset.rarity,
            cooldown: "0",
            last_claim: "0",
            delay : "0",
            reg: "0",
            max_harvests: harvest,
            level : "Level"+asset.level,
            current_harvests: asset.harvests,
            boost: boost,
            level_boost: level_boost,
          });
        }
      }
    return tree_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getMachineData = async () => {
  try {
    let assetData = await getAssets("machines");
    let config = Object.values(await getMachineConfig());
    console.log(config);
    var machine_data = [];
    const arr = Object.values(assetData);
    if (arr.length != 0) {
      const check_data = await checkAssetIds("machines");
      console.log(check_data);
      if (typeof check_data !== 'undefined') {
        const check_ids = check_data[0];
        for (const asset of arr) {
          if (check_ids.includes(asset.asset_id)) {
            const check_body_data = check_data[1];
            for (const bodyData of check_body_data) {
              let harvest = "";
              let cost_level = [];
                for(const c_data of config){
                  if(asset.template_id == c_data.template_id){
                    harvest = c_data.max_harvests;
                    for(const cost_data of c_data.cost_level){
                      cost_level.push({
                        in_name: "Level"+cost_data.level,
                        in_qty: cost_data.count
                      });
                    }
                    break;
                  }
                }
              if (bodyData.id == asset.asset_id) {
                machine_data.push({
                  name: asset.name,
                  asset_id: asset.asset_id,
                  template_id: asset.template_id,
                  slots: bodyData.slots,
                  cd_start: bodyData.cooldown,
                  harvests: bodyData.harvests,
                  land_id: bodyData.land_id,
                  level: "Level"+asset.level,
                  on_recipe: bodyData.on_recipe,
                  rarity: asset.rarity,
                  reg: "1",
                  max_harvests: harvest,
                  cost_level: cost_level
                });
              }
            }
          } else {
            machine_data.push({
              name: asset.name,
              asset_id: asset.asset_id,
              template_id: asset.template_id,
              slots: "0",
              cd_start: "0",
              harvests: "0",
              land_id: "null",
              level: "Level"+asset.level,
              on_recipe: "0",
              rarity: asset.rarity,
              reg: "0",
            });
          }
        }
      } else {
        machine_data.push({
          name: asset.name,
          asset_id: asset.asset_id,
          template_id: asset.template_id,
          slots: "0",
          cd_start: "0",
          harvests: "0",
          land_id: "null",
          level: "Level"+asset.level,
          on_recipe: "0",
          rarity: asset.rarity,
          reg: "0",
        });
      }
    }
    return machine_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getMachineD = async () => {
  try {
    machineData = await getMachineData();
    unityInstance.SendMessage(
      "GameController",
      "Client_SetMachineData",
      machineData === undefined ? JSON.stringify({}) : JSON.stringify(machineData)
    );
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getLandData = async () => {
  try {
    let assetData = await getAssets("land");
    var land_data = [];
    const arr = Object.values(assetData);
    console.log(arr);
    if (arr.length != 0) {
      const check_data = await checkAssetIds("lands");
      console.log(check_data);
      if (typeof check_data !== 'undefined') {
        const check_ids = check_data[0];
        for (const asset of arr) {
          if (check_ids.includes(asset.asset_id)) {
            const check_body_data = check_data[1];
            for (const bodyData of check_body_data) {
              if (bodyData.id == asset.asset_id) {
                land_data.push({
                  name: asset.name,
                  asset_id: asset.asset_id,
                  template_id: asset.template_id,
                  reg: "1",
                });
              }
            }
          } else {
            land_data.push({
              name: asset.name,
              asset_id: asset.asset_id,
              template_id: asset.template_id,
              reg: "0",
            });
          }
        }
      } else {
          land_data.push({
            name: asset.name,
            asset_id: asset.asset_id,
            template_id: asset.template_id,
            reg: "0",
          });
      }
    }
    return land_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getLandD = async () => {
  try {
    landData = await getLandData();
    unityInstance.SendMessage(
      "GameController",
      "Client_SetLandData",
      landData === undefined ? JSON.stringify({}) : JSON.stringify(landData)
    );
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getCropsData = async () => {
  try {
    let assets = getAssets("cropfields");
    assetData = await assets;
    let config = Object.values(await getMachineConfig());
    var crop_data = [];
    const arr = Object.values(assetData);
    if (arr.length != 0) {
      const check_data = await checkAssetIds("cropfields");
      if (typeof check_data !== 'undefined') {
        const check_ids = check_data[0];
        for (const asset of arr) {
          if (check_ids.includes(asset.asset_id)) {
            const check_body_data = check_data[1];
            for (const bodyData of check_body_data) {
              let harvest = "";
              let cost_level = [];
                for(const c_data of config){
                  if(asset.template_id == c_data.template_id){
                    harvest = c_data.max_harvests;
                    for(const cost_data of c_data.cost_level){
                      cost_level.push({
                        in_name: "Level"+cost_data.level,
                        in_qty: cost_data.count
                      });
                    }
                    break;
                  }
                }
              if (bodyData.id == asset.asset_id) {
                crop_data.push({
                  name: asset.name,
                  asset_id: asset.asset_id,
                  template_id: asset.template_id,
                  slots: bodyData.slots,
                  cd_start: bodyData.cd_start,
                  harvests: bodyData.harvests,
                  land_id: bodyData.land_id,
                  level: "Level"+asset.level,
                  prod_sec: bodyData.prod_sec,
                  on_recipe: bodyData.on_recipe,
                  reg: "1",
                  max_harvests: harvest,
                  cost_level: cost_level
                });
              }
            }
          } else {
            crop_data.push({
              name: asset.name,
              asset_id: asset.asset_id,
              template_id: asset.template_id,
              slots: "0",
              cd_start: "0",
              harvests: "0",
              land_id: "null",
              level: "Level"+asset.level,
              prod_sec: "0",
              on_recipe: "0",
              reg: "0",
            });
          }
        }
      } else {
        crop_data.push({
          name: asset.name,
          asset_id: asset.asset_id,
          template_id: asset.template_id,
          slots: "0",
          cd_start: "0",
          harvests: "0",
          land_id: "null",
          level: "Level"+asset.level,
          prod_sec: "0",
          on_recipe: "0",
          reg: "0",
        });
      }
    }
    return crop_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getCropsD = async () => {
  try {
    cropData = await getCropsData();
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCropData",
      cropData === undefined ? JSON.stringify({}) : JSON.stringify(cropData)
    );
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getRecipeData = async () => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: "recipes",
      limit: 1000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    if (body.rows.length != 0) {
      console.log(body.rows);
      let obj = [];
      const bodyData = Object.values(body.rows);
      for (const data of bodyData) {
        let products = [];
        for (const p_data of data.in) {
          let in_string = p_data.split(' ');
          products.push({
            in_qty: in_string[0],
            in_name: in_string[1] == "COCUNUT" ? "COCONUT" : in_string[1]
          });
        }
        let out_string;
        for (const o_data of data.out) {
          out_string = o_data.split(' ');
        }
        obj.push({
          id: data.id,
          machine: data.machine,
          products: products,
          out_qty: out_string[0],
          out_name: out_string[1],
          craft_time: data.craft_time
        });
      }
      return obj;
    } else
      return [];
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const checkAssetIds = async (table) => {
  try {
    console.log(table);
    console.log(contract);
    let wallet = wallet_userAccount;
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: table,
      key_type: `i64`,
      index_position: 2,
      lower_bound: eosjsName.nameToUint64(wallet_userAccount),
      limit: 2000
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });

    const body = await response.json();
    console.log(body);
    const arr = Object.values(body.rows);
    const ids = [];
    const table_data = [];
    if (arr.length != 0) {
      for (const data of arr) {
        if(data.owner==wallet_userAccount)
        {
        ids.push(data.asset_id);
        if (table == "dtrees") {
          table_data.push({
            id: data.asset_id,
            land_id: data.land_id,
            prod_power: (parseFloat(data.prod_power.split(' ')[0]).toFixed(2)),
            cooldown: data.cooldown,
            last_claim: data.last_claim,
            delay : data.delay,
          });
        } else if (table == "machines") {
          let recipe_obj = [];
          for (const r_data of data.on_recipe) {
            recipe_obj.push({
              start: r_data.start,
              delay: r_data.delay,
              recipeID: r_data.recipeID,
              orderID: r_data.orderID
            });
          }
          table_data.push({
            id: data.asset_id,
            slots: data.slots,
            cooldown: data.cooldown,
            harvests: data.harvests,
            land_id: data.land_id,
            level: data.level,
            prod_sec: data.prod_sec,
            on_recipe: recipe_obj,
          });
        }
        else if (table == "cropfields"){
          let recipe_obj = [];
          for (const r_data of data.on_recipe) {
            recipe_obj.push({
              start: r_data.start,
              delay: r_data.delay,
              recipeID: r_data.recipeID,
              orderID: r_data.orderID
            });
          }
          table_data.push({
            id: data.asset_id,
            slots: data.slots,
            cd_start: data.cooldown,
            harvests: "0",//data.harvests,
            land_id: data.land_id,
            level: data.level,
            prod_sec: data.prod_sec,
            on_recipe: recipe_obj,
          });
        }
        else if (table == "lands"){
          table_data.push({
            id: data.asset_id
          });
        }
      }
    }
    }
    return [ids, table_data];
  } catch (e) {

  }
}

const getCallBack = async (table, asset_id, action_type) => {
  try {
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: table,
      limit: 1,
      lower_bound: asset_id,
    });
    const response = await fetch("https://" + endpoint + path, {
      headers: {
        "Content-Type": "text/plain"
      },
      body: data,
      method: "POST",
    });
    const body = await response.json();
    console.log(body);
    const arr = Object.values(body.rows);
    console.log(arr);
    let callback_obj = [];
    if (body.rows.length != 0) {
      switch (action_type) {
        case ("start_machine"):
          let recipe_obj = [];
          for (const r_data of arr[0].on_recipe) {
            recipe_obj.push({
              start: r_data.start,
              delay: r_data.delay,
              recipeID: r_data.recipeID,
              orderID: r_data.orderID
            });
          }
          callback_obj.push({
            on_recip: recipe_obj,
            cooldown: arr[0].cooldown,
            harvest: table == "cropfields" ? "0" : arr[0].harvests,
            type: "start_machine"
          });
          break;
          case ("recipe_claim"):
            let r_obj = [];
            for (const r_data of arr[0].on_recipe) {
              r_obj.push({
                start: r_data.start,
              delay: r_data.delay,
              recipeID: r_data.recipeID,
              orderID: r_data.orderID
              });
            }
            callback_obj.push({
              on_recip: r_obj,
              cooldown: arr[0].cooldown,
              harvest: table == "cropfields" ? "0" : arr[0].harvests,
              type: "recipe_claim"
            });
            break;
        default:
          break;
      }
      console.log(callback_obj);
      unityInstance.SendMessage(
        "GameController",
        "Client_SetCallBackData",
        callback_obj === undefined ? JSON.stringify({}) : JSON.stringify(callback_obj)
      );
    }
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const register_nft = async (asset_id,name,land_id,type) => {
  try {
    var ids = asset_id.split(",");
    let action_name = "";
    switch(type){
      case ("tree"):
        action_name = "regtrees";
        break;
      case ("land"):
        action_name = "regland";
        break;
      case ("machine"):
        action_name = "regmch";
        break;
      case ("cropfield"):
        action_name = "regcf";
        break;
    }
    const result = type != "land" ? await wallet_transact([{
      account: contract,
      name: action_name,
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_ids: ids,
        land_id: land_id
      },
    },]):await wallet_transact([{
      account: contract,
      name: action_name,
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_ids: ids,
      },
    },]);
    await delay(2000);
    let obj = [];
    switch(type){
      case ("tree"):
        treeData = await getTreeData();
        unityInstance.SendMessage(
          "GameController",
          "Client_SetTreeData",
          treeData === undefined ? JSON.stringify({}) : JSON.stringify(treeData)
        );
        obj.push({
          tree_name: name,
          type: "reg"
        });
        break;
      case ("land"):
        await getLandD();
        obj.push({
          type: "reg"
        });
        break;
      case ("machine"):
        await getMachineD();
        obj.push({
          type: "reg",
          machine_name: name
        });
        break;
      case ("cropfield"):
        await getCropsD();
        unityInstance.SendMessage(
          "GameController",
          "Client_SetCropData",
          cropData === undefined ? JSON.stringify({}) : JSON.stringify(cropData)
        );
        obj.push({
          type: "reg"
        });
        break;
    }
    console.log(obj);
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}
const deregister_nft = async (asset_id, name,type) => {
  try {
    var ids = asset_id.split(",");
    var name="";
    var data={};
    if(type=="land")
    {
      name="deregland";
      data=
      {
        player: wallet_userAccount,
        asset_ids: ids
      };
    }
    else
    {
      name="deregnft";
      data=
      {
        player: wallet_userAccount,
        asset_ids: ids,
        type: type
      }; 
    }
    const result = await wallet_transact([{
      account: contract,
      name: name,
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data:data,
    },]);
    await delay(2000);
    let obj = [];
    switch(type){
      case ("tree"):
        treeData = await getTreeData();
        unityInstance.SendMessage(
          "GameController",
          "Client_SetTreeData",
          treeData === undefined ? JSON.stringify({}) : JSON.stringify(treeData)
        );
        obj.push({
          tree_name: name,
          type: "dereg"
        });
        break;
      case ("land"):
        await getLandD();
        obj.push({
          type: "dereg"
        });
        break;
      case ("machine"):
        await getMachineD();
        obj.push({
          type: "dereg",
          machine_name: "machine"
        });
        break;
      case ("cropfield"):
        cropData = await getCropsD();
        obj.push({
          type: "dereg"
        });
        break;
    }
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const start_machine = async (asset_id, recipeID,type) => {
  try {
    let action_name = type == "machine" ? "startmch" : "startcf";
    const result = await wallet_transact([{
      account: contract,
      name: action_name,
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_id: asset_id,
        recipeID: recipeID
      },
    },]);
    await delay(3000);
    await getUserB();
    if(type == "machine"){
      await getMachineD();
      await getCallBack("machines", asset_id, "start_machine");
    }
    else {
      await getCropsD();
      await getCallBack("cropfields", asset_id, "start_machine");
    }
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const filldmo = async (id,amount) => {
  try {
    await wallet_transact([{
      account: contract,
      name: "filldmo",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        dmoid: id,
      },
    },]);
    await delay(2000);
    await getUserB();
    await getdmodata();
    let callback_obj = [];
    callback_obj.push({
      type: "order fill"
    });
    console.log(callback_obj);
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      callback_obj === undefined ? JSON.stringify({}) : JSON.stringify(callback_obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const buyshopl = async (id,amount) => {
  try {
    const result = await wallet_transact([{
      account: contract,
      name: "buyshopl",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        id: id,
        player: wallet_userAccount,
        quantity: amount
      },
    },]);
    await delay(2000);
    await getUserB();
    await getshopdata();
    let callback_obj = [];
    callback_obj.push({
      type: "buy"
    });
    console.log(callback_obj);
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      callback_obj === undefined ? JSON.stringify({}) : JSON.stringify(callback_obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const depositawc = async (amount) => {
  try {
    var final = parseFloat(amount).toFixed(4);
    const result = await wallet_transact([{
      account: "anmworldtokn",
      name: "transfer",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        from: wallet_userAccount,
        to: "anmworldgame",
        quantity: final+" AWC",
        memo:"deposit"
      },
    },]);
    await delay(1500);
    await getUserB();
    await getAwcB();
    let callback_obj = [];
    callback_obj.push({
      type: "deposit",
      helper: final
    });
    console.log(callback_obj);
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      callback_obj === undefined ? JSON.stringify({}) : JSON.stringify(callback_obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}
const withdrawawc = async (amount) => {
  try {
    var final=parseFloat(amount).toFixed(4);
    const result = await wallet_transact([{
      account: contract,
      name: "withdrawawc",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        quantity: final+" AWC",
      },
    },]);
    await delay(1500);
    await getUserB();
    await getAwcB();
    let callback_obj = [];
    callback_obj.push({
      type: "withdraw",
      helper: final
    });
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      callback_obj === undefined ? JSON.stringify({}) : JSON.stringify(callback_obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}
const claim_machine = async (asset_id, recipeID,type) => {
  try {
    var ids = recipeID.split(",");
    let action_name = "";
    console.log(recipeID);
    switch(type){
      case ("machine"):
        action_name = "claimmch";
        break;
      case ("crop"):
        action_name = "claimcf";
        break;
    }
    const result = await wallet_transact([{
      account: contract,
      name: action_name,
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_id: asset_id,
        orderIDs: ids
      },
    },]);
    await delay(3000);
    await getUserB();
    switch(type){
      case ("machine"):
        await getMachineD();
        await getCallBack("machines", asset_id, "recipe_claim");
        break;
      case ("crop"):
        await getCropsD();
        await getCallBack("cropfields", asset_id, "recipe_claim");
        break;
    }
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const claim_tree = async (asset_id) => {
  try {
    var ids = asset_id.split(",");
    console.log(ids);
    await wallet_transact([{
      account: contract,
      name: "claimtree",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_ids: ids
      },
    },]);
    await delay(2500);
    await getUserB();
    await getUserD();
    treeData = await getTreeData();
    unityInstance.SendMessage(
      "GameController",
      "Client_SetTreeData",
      treeData === undefined ? JSON.stringify({}) : JSON.stringify(treeData)
    );
    await getUserB();
    let callback_obj = [];
    callback_obj.push({
      type: "tree_claim"
    });
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      callback_obj === undefined ? JSON.stringify({}) : JSON.stringify(callback_obj)
    );
  } catch (e) {
    console.log(e.message);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const burnid = async (id) => {
  try {
    const result = await wallet_transact([{
      account: "atomicassets",
      name: "burnasset",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        asset_owner: wallet_userAccount,
        asset_id: id
      },
    },]);
    await getUserB();
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const  getburnids= async () => {
  try {
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&schema_name=" + "resourcepack" + "&owner=" + wallet_userAccount + "&page=1&limit=1000&order=desc&sort=asset_id";
      const response = await fetch("https://" + "test.wax.api.atomicassets.io/" + path, {
        headers: {
          "Content-Type": "text/plain"
        },
        method: "POST",
      });
      const body = await response.json();
      const data = Object.values(body.data);
      var obj=[];
      console.log(data);
      for (const asset of data) {
        var symbol= asset.template.immutable_data.Symbol;
        console.log(symbol);
      var id=asset.asset_id;
        var ids=[];
        var exists=false;
        for(let i=0;i<obj.length;i++) 
        {
          if(obj[i].symbol==symbol) {
          ids= obj[i].ids;
          exists=true;
          obj[i].ids.push(id);
        }
      }
      if(!exists)
      {
        ids.push(id);
        obj.push(
          {
            symbol:symbol,
            ids:ids
          }
        );
      }
        
      }
      console.log(obj);
      unityInstance.SendMessage(
        "GameController",
        "Client_SetBurnids",
        obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
      );
  } catch (e) {
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const use_boost = async (asset_id,type) => {
  try {
    let action_name = "";
    const result = await wallet_transact([{
      account: contract,
      name: "useboost",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_ids: [asset_id],
        type : type
      },
    },]);
    await delay(2000);
    await getUserB();
    let obj = [];
    switch(type){
      case ("tree"):
        treeData = await getTreeData();
        unityInstance.SendMessage(
          "GameController",
          "Client_SetTreeData",
          treeData === undefined ? JSON.stringify({}) : JSON.stringify(treeData)
        );
        obj.push({
          helper: "tree",
          type: "boost"
        });
        break;
      case ("machine"):
        await getMachineD();
        obj.push({
          helper: "machine",
          type: "boost"
        });
        break;
      case ("crop"):
        await getCropsD();
        obj.push({
          helper: "crop",
          type: "boost"
        });
        break;
    }
    unityInstance.SendMessage(
      "GameController",
      "Client_SetCallBackData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}