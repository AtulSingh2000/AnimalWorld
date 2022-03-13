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
    balance = "0.00 AWC"; //await getAwcBalance();
    userData = await getUserData();
    treeData = await getTreeData();
    recipeData = await getRecipeData();
    machineData = await getMachineData();
    cropfieldData = await getCropsData();
    landData = await getLandData();
    userBalanceData = await getUserBalance();
    let obj = {
      account: userAccount.toString(),
      awcBal: awc_balance,
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
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&owner=" + "anmworldtokn" + "&page=1&limit=1000&order=desc&sort=asset_id";
      const response = await fetch("https://" + "test.wax.api.atomicassets.io/" + path, {
        headers: {
          "Content-Type": "text/plain"
        },
        method: "POST",
      });

      const body = await response.json();
      return body.data;
    } else {
      var path = "atomicassets/v1/assets?collection_name=" + collectionName + "&schema_name=" + schema + "&owner=" + "anmworldtokn" + "&page=1&limit=1000&order=desc&sort=asset_id";
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
          //img: asset.template.immutable_data.img,
          name: asset.template.immutable_data.name,
          schema: asset.schema.schema_name,
          rarity: asset.template.immutable_data.Rarity
        });
      }
      console.log(obj);
      return obj;
    }
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
          in_name: new_string[1] == "COCUNUT" ? "COCONUT" : new_string[1]
        });
      }
    }
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
          price : data.price,	
          resource : data.resource,	
          req_level : data.req_level
        });
      }
    }
    unityInstance.SendMessage(
      "GameController",
      "Client_SetShopData",
      obj === undefined ? JSON.stringify({}) : JSON.stringify(obj)
    );
  } catch (e) {
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
    console.log(body.rows);
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

const getTreeData = async () => {
  try {
    let assetData = await getAssets("tree");
    var tree_data = [];
    const arr = Object.values(assetData);
    console.log(arr);
    if (arr.length != 0) {
      const check_data = await checkAssetIds("dtrees");
      console.log(check_data);
      if (typeof check_data !== 'undefined') {
        const check_ids = check_data[0];
        for (const asset of arr) {
          if (check_ids.includes(asset.asset_id)) {
            const check_body_data = check_data[1];
            for (const bodyData of check_body_data) {
              if (bodyData.id == asset.asset_id) {
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
                  reg: "1"
                });
              }
            }
          } else {
            let rarity = asset.rarity;
            let produce_power = "";
            switch (rarity) {
              case "":
                produce_power = "";
                break;
              case "":
                produce_power = "";
                break;
              case "":
                produce_power = "";
                break;
            }
            tree_data.push({
              name: asset.name,
              type: "tree",
              asset_id: asset.asset_id,
              land_id: "0",
              prod_pwer: "0",
              rarity: asset.rarity,
              cooldown: "0",
              last_claim: "0",
              delay : "0",
              reg: "0"
            });
          }
        }
      } else {
          tree_data.push({
            name: asset.name,
            type: "tree",
            asset_id: asset.asset_id,
            land_id: "0",
            prod_pwer: "0",
            rarity: asset.rarity,
            cooldown: "0",
            last_claim: "0",
            delay : "0",
            reg: "0"
          });
        }
      }
    console.log(tree_data);
    return tree_data;
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const getMachineData = async () => {
  try {
    let assets = getAssets("machines");
    assetData = await assets;
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
              if (bodyData.id == asset.asset_id) {
                machine_data.push({
                  name: asset.name,
                  asset_id: asset.asset_id,
                  template_id: asset.template_id,
                  slots: bodyData.slots,
                  cd_start: bodyData.cooldown,
                  harvests: bodyData.harvests,
                  land_id: bodyData.land_id,
                  level: "Level"+bodyData.level,
                  prod_sec: bodyData.prod_sec,
                  on_recipe: bodyData.on_recipe,
                  rarity: asset.rarity,
                  reg: "1",
                  //img: asset.template.immutable_data.img,
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
              land_id: "0",
              level: "0",
              prod_sec: "0",
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
          land_id: "0",
          level: "0",
          prod_sec: "0",
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
    let assets = getAssets("land");
    assetData = await assets;
    var land_data = [];
    const arr = Object.values(assetData);
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
    var crop_data = [];
    const arr = Object.values(assetData);
    if (arr.length != 0) {
      const check_data = await checkAssetIds("cropfields");
      console.log(check_data);
      if (typeof check_data !== 'undefined') {
        const check_ids = check_data[0];
        for (const asset of arr) {
          if (check_ids.includes(asset.asset_id)) {
            const check_body_data = check_data[1];
            for (const bodyData of check_body_data) {
              if (bodyData.id == asset.asset_id) {
                crop_data.push({
                  name: asset.name,
                  asset_id: asset.asset_id,
                  template_id: asset.template_id,
                  slots: bodyData.slots,
                  cd_start: bodyData.cooldown,
                  harvests: bodyData.harvests,
                  land_id: bodyData.land_id,
                  level: bodyData.level,
                  prod_sec: bodyData.prod_sec,
                  on_recipe: bodyData.on_recipe,
                  reg: "1",
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
              land_id: "0",
              level: "0",
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
          land_id: "0",
          level: "0",
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
    let wallet = "anmworldtokn";
    var path = "/v1/chain/get_table_rows";
    var data = JSON.stringify({
      json: true,
      code: contract,
      scope: contract,
      table: table,
      /*key_type: `i64`,
      index_position: 2,*/
      //lower_bound: eosjsName.nameToUint64('anmworldtokn'),
      limit: 1000,
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
              recipeID: r_data.recipeID
            });
          }
          table_data.push({
            id: data.asset_id,
            slots: data.slots,
            cd_start: data.cooldown,
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
              recipeID: r_data.recipeID
            });
          }
          table_data.push({
            id: data.asset_id,
            slots: data.slots,
            cd_start: data.cooldown,
            harvests: data.harvests,
            land_id: data.land_id,
            level: data.level,
            prod_sec: data.prod_sec,
            on_recipe: recipe_obj,
          });
        }
        else if(table == "lands"){
          land_data.push({
            
          });
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
              recipeID: r_data.recipeID
            });
          }
          callback_obj.push({
            on_recip: recipe_obj,
            prod_sec: arr.prod_sec,
            harvest: arr.harvest,
            type: "start_machine"
          });
          break;
          case ("recipe_claim"):
            let r_obj = [];
            for (const r_data of arr[0].on_recipe) {
              r_obj.push({
                start: r_data.start,
                delay: r_data.delay,
                recipeID: r_data.recipeID
              });
            }
            callback_obj.push({
              on_recip: r_obj,
              prod_sec: arr.prod_sec,
              harvest: arr.harvest,
              type: "recipe_claim"
            });
            break;
        default:
          break;
      }
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

const register_nft = async (asset_id, name,land_id,type) => {
  try {
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
        asset_ids: [asset_id],
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
        asset_ids: [asset_id],
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
          type: "reg"
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
    let action_name = "";
    switch(type){
      case ("tree"):
        action_name = "deregtrees";
        break;
      case ("land"):
        action_name = "deregland";
        break;
      case ("machine"):
        action_name = "deregmch";
        break;
      case ("cropfield"):
        action_name = "deregcf";
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
        asset_ids: [asset_id],
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
    await getMachineD();
    await getCallBack("machines", asset_id, "start_machine");
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const claim_machine = async (asset_id, recipeID) => {
  try {
    const result = await wallet_transact([{
      account: contract,
      name: "claimmch",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        asset_id: asset_id,
        recipe_id: recipeID
      },
    },]);
    await delay(3000);
    await getMachineD();
    await getCallBack("machines", asset_id, "recipe_claim");
  } catch (e) {
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}

const claim_tree = async (symbol) => {
  try {
    const result = await wallet_transact([{
      account: contract,
      name: "claimtree",
      authorization: [{
        actor: wallet_userAccount,
        permission: anchorAuth
      }],
      data: {
        player: wallet_userAccount,
        fruit: symbol,
      },
    },]);
    await delay(2500);
    await getUserD();
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
    console.log(e);
    unityInstance.SendMessage("ErrorHandler", "Client_SetErrorData", e.message);
  }
}