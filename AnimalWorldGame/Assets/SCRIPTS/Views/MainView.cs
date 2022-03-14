using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainView : BaseView
{
    [Space]
    [Header("GameObjects")]
    [Space]
    [Header("Main Menu")]
    public GameObject main_menu_panel;
    public GameObject shop_parent_panel;
    public GameObject inv_parent_panel;
    public GameObject exchange_parent_panel;
    [Header("Trees")]
    public GameObject tree_parent_panel;
    public GameObject tree_subheading_text;
    public GameObject tree_subheading_text_type_2;
    public GameObject tree_show_panel;
    public GameObject tree_stats_panel;
    [Header("Machines")]
    public GameObject machine_parent_panel;
    public GameObject machine_subheading_text;
    public GameObject machine_subheading_text_type_2;
    public GameObject machine_show_panel;
    public GameObject machine_stats_panel;
    [Header("Crops")]
    public GameObject crop_parent_panel;
    public GameObject crop_subheading_text;
    public GameObject crop_subheading_text_type_2;
    public GameObject crop_show_panel;
    public GameObject crop_stats_panel;
    [Header("Machine ING")]
    public GameObject add_ing_prefab;
    public GameObject ing_prefab;
    public GameObject new_ing_prefab;
    public GameObject show_machine_details;
    public GameObject choose_ing_panel;
    public GameObject ing_prefab_2;
    public GameObject choose_Panel_scrollView;
    [Header("Crop ING")]
    public GameObject show_crop_details;
    [Header("Land")]
    public GameObject land_parent_panel;
    public GameObject land_subheading_text;
    public GameObject land_subheading_text_type_2;
    public GameObject land_show_panel;
    public GameObject land_stats_panel;
    [Space]
    [Header("Booster")]
    public GameObject boost_panel;
    [Header("Transform")]
    public Transform parent_tree_main_menu_panel;
    public Transform parent_transform_trees_reg;
    public Transform parent_transform_trees_unreg;
    public Transform parent_machines_main_menu_panel;
    public Transform parent_transform_machine_reg;
    public Transform parent_transform_machine_unreg;
    public Transform parent_machine_current_ing;
    public Transform child_machine_show_recipes;
    public Transform child_machine_recipe_ing;
    public Transform parent_transform_crops_reg;
    public Transform parent_transform_crop_unreg;
    public Transform parent_crops_current_ing;
    public Transform parent_transform_land_reg;
    public Transform parent_transform_land_unreg;
    [Space]
    public TMP_Text user_level;
    public TMP_Text user_level_count;
    [Header("UI Text")]
    public TMP_Text username_text;
    public TMP_Text userbalance_text;
    public TMP_Text land_id;
    [Header("Trees")]
    public TMP_Text total_trees;
    public TMP_Text reg_total_trees;
    public TMP_Text un_total_trees;
    public TMP_Text time_to_claim;
    public TMP_Text current_produce;
    [Header("Machines")]
    public TMP_Text total_machines;
    public TMP_Text reg_total_machines;
    public TMP_Text un_total_machines;
    public TMP_Text slots_in_use_detailView;
    public TMP_Text harvests_detailView;
    [Header("Machine Recipes")]
    public TMP_Text final_product_text;
    [Header("Crops")]
    public TMP_Text total_crops;
    public TMP_Text reg_total_crops;
    public TMP_Text un_total_crops;
    public TMP_Text slots_in_use_crops_detailView;
    public TMP_Text harvests_crops_detailView;
    [Header("Lands")]
    public TMP_Text total_land;
    public TMP_Text reg_total_land;
    public TMP_Text un_total_land;
    public TMP_Text reg_trees;
    public TMP_Text reg_crops;
    public TMP_Text reg_machines;
    [Space]
    [Header("Buttons")]
    [Space]
    [Header("Trees")]
    public Button tree_registered_btn;
    public Button tree_unregistered_btn;
    public Button tree_show_btn;
    public Button tree_register_btn;
    public Button tree_unregister_btn;
    public Button tree_register_all_btn;
    public Button tree_deregister_all_btn;
    public Button tree_claim_btn;
    public Button tree_claim_all_btn;
    [Header("Machines")]
    public Button machine_registered_btn;
    public Button machine_unregistered_btn;
    public Button machine_show_btn;
    public Button machine_register_btn;
    public Button machine_unregister_btn;
    public Button machine_register_all_btn;
    public Button machine_deregister_all_btn;
    public Button machine_claim_btn;
    [Header("Recipes")]
    public Button start_machine;
    [Header("Crops")]
    public Button crop_registered_btn;
    public Button crop_unregistered_btn;
    public Button crop_show_btn;
    public Button crop_register_btn;
    public Button crop_unregister_btn;
    public Button crop_register_all_btn;
    public Button crop_deregister_all_btn;
    public Button crop_claim_btn;
    [Header("Land")]
    public Button land_registered_btn;
    public Button land_unregistered_btn;
    public Button land_show_btn;
    public Button land_register_all_btn;
    public Button land_deregister_all_btn;
    [Header("Recipes")]
    public Button start_crop;
    [Space]
    public Camera camera;
    [Space]
    [Header("Machine Dropdown")]
    public TMP_Dropdown machine_rarity_dropdown;
    [Header("Crop Dropdown")]
    public TMP_Dropdown crop_level_dropdown;
    [Space]
    public ImgObjectView[] img;
    [Space]
    [Header("Image Objects")]
    [Space]
    public AbbvHelper helper;
    public Image machine_detail_view_img;
    public Image final_product_img;
    [Space]
    [Header("Success Panel")]
    public GameObject successPanel;
    public TMP_Text success_header_text;
    public TMP_Text success_text;
    public Image final_product_successPanel;
    //Private Variables
    private string asset_id = "0";
    private string back_status = "close";
    private string s_trees_type = "null";
    private string s_machine_type = "null";
    private string level_type = "Level";
    private bool onTrees = false;
    private bool onMachines = false;
    private bool onCrops = false;
    private bool onLand = false;
    private bool can_start_machine = false;
    private bool can_start_crop = false;
    private bool stopTimer = false;
    private bool populated_tree_rarity = false;
    private bool populated_machine_rarity = false;
    private bool populated_crop_level = false;
    private string helper_var = "";
    private bool UI_opened = false;
    private bool from_main_menu = false;
    //Tree Models
    private List<AssetModel> fig = new List<AssetModel>();
    private List<AssetModel> mango = new List<AssetModel>();
    private List<AssetModel> coconut = new List<AssetModel>();
    private List<AssetModel> orange = new List<AssetModel>();
    private List<AssetModel> lemon = new List<AssetModel>();
    //Machine Models
    private List<MachineDataModel> juicer = new List<MachineDataModel>();
    private List<MachineDataModel> popcorn = new List<MachineDataModel>();
    private List<MachineDataModel> feeder = new List<MachineDataModel>();
    private List<MachineDataModel> bbq = new List<MachineDataModel>();
    private List<MachineDataModel> dairy = new List<MachineDataModel>();
    private List<MachineDataModel> icecream = new List<MachineDataModel>();
    //Recipes Models
    private List<RecipesModel> juicer_recipe = new List<RecipesModel>();
    private List<RecipesModel> popcorn_recipe = new List<RecipesModel>();
    private List<RecipesModel> cropfield_recipe = new List<RecipesModel>();
    //Usable Scripts
    private MachineAssetCall machine_child_asset = new MachineAssetCall();
    private CropAssetCall crop_child_asset = new CropAssetCall();

    //Enums
    enum machine_rarities
    {
        Level,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10
    };

    enum crop_level
    {
        Level,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    };

    protected override void Start()
    {
        base.Start();
        MessageHandler.OnCallBackData += OnCallBackData;
        SSTools.ShowMessage("Welcome Farmer " + MessageHandler.userModel.account, SSTools.Position.bottom, SSTools.Time.twoSecond);
        machine_rarity_dropdown.onValueChanged.AddListener(delegate { changeValue(machine_rarity_dropdown); });
        crop_level_dropdown.onValueChanged.AddListener(delegate { changeValue(crop_level_dropdown); });
        SetData();
        SetElements();
        SetLevel();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnCallBackData -= OnCallBackData;
    }

    public void changeValue(TMP_Dropdown dropdown)
    {
        level_type = dropdown.options[dropdown.value].text;
        if (onMachines) ShowElements_Machines(stack_machines_type, level_type);
        else if (onCrops) ShowElements_Crops(level_type);
        if (level_type == "Level") SSTools.ShowMessage("Showing All Levels", SSTools.Position.bottom, SSTools.Time.twoSecond);
        else if (level_type != "Level") SSTools.ShowMessage(level_type, SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void SetData()
    {
        Debug.Log("in set data");
        //Clear Exisiting List Data
        mango.Clear();
        coconut.Clear();
        fig.Clear();
        lemon.Clear();
        orange.Clear();
        juicer.Clear();
        popcorn.Clear();
        feeder.Clear();
        bbq.Clear();
        icecream.Clear();
        dairy.Clear();

        // Add new Data

        //Trees
        int trees = 0;
        int machines = 0;
        int crops = 0;
        foreach (AssetModel assets in MessageHandler.userModel.trees)
        {
            switch (assets.name)
            {
                case ("Mango Tree"):
                    mango.Add(assets);
                    break;
                case ("Coconut Tree"):
                    coconut.Add(assets);
                    break;
                case ("Fig Tree"):
                    fig.Add(assets);
                    break;
                case ("Lemon Tree"):
                    lemon.Add(assets);
                    break;
                case ("Orange Tree"):
                    orange.Add(assets);
                    break;
                default:
                    break;
            }
            if (assets.land_id == MessageHandler.userModel.land_id) trees++;
        }

        //Machines
        foreach (MachineDataModel assets in MessageHandler.userModel.machines)
        {
            switch (assets.name)
            {
                case ("Juicer"):
                    juicer.Add(assets);
                    break;
                case ("Popcorn Maker"):
                    popcorn.Add(assets);
                    break;
                case ("Feeder"):
                    feeder.Add(assets);
                    break;
                case ("BBQ"):
                    bbq.Add(assets);
                    break;
                case ("Ice Cream Maker"):
                    icecream.Add(assets);
                    break;
                case ("Dairy"):
                    dairy.Add(assets);
                    break;
                default:
                    break;
            }
            if (assets.land_id == MessageHandler.userModel.land_id) machines++;
        }

        //Recipes
        foreach (RecipesModel assets in MessageHandler.userModel.machine_recipes)
        {
            switch (assets.machine)
            {
                case ("juicer"):
                    juicer_recipe.Add(assets);
                    break;
                case ("popcornmaker"):
                    popcorn_recipe.Add(assets);
                    break;
                case ("cropfield"):
                    cropfield_recipe.Add(assets);
                    break;
                default:
                    break;
            }
        }

        foreach (CropDataModel asset_data in MessageHandler.userModel.crops)
        {
            if (asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
            {
                crops++;
            }
        }

        reg_trees.text = trees.ToString();
        reg_machines.text = machines.ToString();
        reg_crops.text = crops.ToString();
    }

    private void PopulateMachineRarity()
    {
        string[] rarity_names_machines = Enum.GetNames(typeof(machine_rarities));
        List<string> rarity_list_machine = new List<string>(rarity_names_machines);
        machine_rarity_dropdown.AddOptions(rarity_list_machine);
    }

    private void PopulateCropLevel()
    {
        string[] level_names_crops = Enum.GetNames(typeof(crop_level));
        List<string> level_list_crops = new List<string>(level_names_crops);
        crop_level_dropdown.AddOptions(level_list_crops);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (!UI_opened)
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfor))
                {
                    if (hitInfor.collider.gameObject.CompareTag("trees"))
                        selectType("trees");
                    else if (hitInfor.collider.gameObject.CompareTag("machines"))
                        selectType("machines");
                    else if (hitInfor.collider.gameObject.CompareTag("cropfield"))
                        selectType("crops");
                    else if (hitInfor.collider.gameObject.CompareTag("shop"))
                        selectType("shop");
                    else if (hitInfor.collider.gameObject.CompareTag("tokenexchange"))
                        selectType("exchange");
                    else if (hitInfor.collider.gameObject.CompareTag("menu")) { main_menu_panel.SetActive(true); from_main_menu = true; }
                    else if (hitInfor.collider.gameObject.CompareTag("inventory"))
                        selectType("inventory");
                }
            }
        }
    }

    private void SetLevel()
    {
        string awxpbal = MessageHandler.GetBalanceKey("AWXP");
        LevelModel[] temp = MessageHandler.GetUserLevel();
        LevelModel current = temp[0];
        LevelModel next = temp[1];
        user_level.text = current.level;
        user_level_count.text = awxpbal + " / " + next.xp_amount;
    }


    private void SetElements()
    {
        username_text.text = MessageHandler.userModel.account;
        userbalance_text.text = MessageHandler.GetBalanceKey("AWC");
        string land = MessageHandler.userModel.land_id;
        if (land == "0")
            land_id.text = "Community Land";
        else
            land_id.text = land;
    }

    private void ShowElements_Machines(string name, string show_rarity)
    {
        var reg = parent_transform_machine_reg;
        var unreg = parent_transform_machine_unreg;
        string type = "";

        clearChildObjs(parent_transform_machine_reg);
        clearChildObjs(parent_transform_machine_unreg);
        machine_show_panel.SetActive(true);
        machine_stats_panel.SetActive(false);
        machine_registered_btn.gameObject.SetActive(false);
        machine_unregistered_btn.gameObject.SetActive(false);
        machine_register_btn.gameObject.SetActive(false);
        machine_register_all_btn.gameObject.SetActive(false);
        machine_deregister_all_btn.gameObject.SetActive(false);
        List<string> reg_ids = new List<string>();
        List<string> dereg_ids = new List<string>();

        List<MachineDataModel> data = new List<MachineDataModel>();
        GameObject asset_prefab = new GameObject();
        switch (name)
        {
            case ("Juicer"):
                data = juicer;
                break;
            case ("Popcorn Maker"):
                data = popcorn;
                break;
            case ("Feeder"):
                data = feeder;
                break;
            case ("BBQ"):
                data = bbq;
                break;
            case ("Ice Cream Maker"):
                data = icecream;
                break;
            case ("Dairy"):
                data = dairy;
                break;
            default:
                break;
        }
        foreach (ImgObjectView prefabs in img)
        {
            if (prefabs.name == name)
            {
                Debug.Log("found");
                asset_prefab = prefabs.prefab;
                type = prefabs.type;
                break;
            }
        }
        machine_rarity_dropdown.gameObject.SetActive(true);
        if (data.Count != 0)
        {
            if (show_rarity == "Level")
            {
                foreach (MachineDataModel asset_data in data)
                {
                    Debug.Log(asset_data.asset_id);
                    if (asset_data.reg == "0")
                    {
                        Debug.Log("in reg");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(unreg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<MachineAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_id_text.fontSize = 14;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.register_btn.SetActive(true);
                        child.boost_btn.gameObject.SetActive(false);
                        reg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true);
                        string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }
                    else if (asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
                    {
                        Debug.Log("in dereg");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(reg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<MachineAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_id_text.fontSize = 14;
                        child.asset_name = asset_data.name;
                        child.slot_size = asset_data.slots;
                        child.land_id = asset_data.land_id;
                        child.level = asset_data.level;
                        child.on_recip = asset_data.on_recipe;
                        child.prod_sec = asset_data.prod_sec;
                        child.harvest = asset_data.harvests;
                        child.max_harvest = asset_data.max_harvests;
                        child.LoadingPanel = LoadingPanel;
                        child.cooldown = asset_data.cd_start;
                        child.details_btn.SetActive(true);
                        child.details_btn.GetComponent<Button>().onClick.AddListener(delegate { show_machines_details(child); });
                        child.boost_btn.gameObject.SetActive(true);
                        child.boost_btn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { show_boost("machine",asset_data.asset_id); });
                        dereg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true);
                        string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }
                }
            }
            else
            {
                foreach (MachineDataModel asset_data in data)
                {
                    if (asset_data.level == show_rarity && asset_data.reg == "0")
                    {
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(unreg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<MachineAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_id_text.fontSize = 14;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.register_btn.SetActive(true);
                        child.boost_btn.gameObject.SetActive(false);
                        reg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true); string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }

                    else if (asset_data.level == show_rarity && asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
                    {
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(reg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<MachineAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_id_text.fontSize = 14;
                        child.asset_name = asset_data.name;
                        child.slot_size = asset_data.slots;
                        child.land_id = asset_data.land_id;
                        child.level = asset_data.level;
                        child.on_recip = asset_data.on_recipe;
                        child.prod_sec = asset_data.prod_sec;
                        child.harvest = asset_data.harvests;
                        child.max_harvest = asset_data.max_harvests;
                        child.LoadingPanel = LoadingPanel;
                        child.cooldown = asset_data.cd_start;
                        child.details_btn.SetActive(true);
                        child.details_btn.GetComponent<Button>().onClick.AddListener(delegate { show_machines_details(child); });
                        Debug.Log(child.details_btn.GetComponent<Button>().onClick.ToString());
                        child.boost_btn.gameObject.SetActive(true);
                        child.boost_btn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { show_boost("machine", asset_data.asset_id); });
                        dereg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true);
                        string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }
                }
            }
        }

        var register_ids = string.Join(",", reg_ids.ToArray());
        var deregister_ids = string.Join(",", dereg_ids.ToArray());
        machine_deregister_all_btn.onClick.RemoveAllListeners();
        machine_register_all_btn.onClick.RemoveAllListeners();
        machine_register_all_btn.onClick.AddListener(delegate { Register_All(register_ids); });
        machine_deregister_all_btn.onClick.AddListener(delegate { Deregister_All(deregister_ids); });
    }

    private void ShowElements_Crops(string show_rarity)
    {
        var reg = parent_transform_crops_reg;
        var unreg = parent_transform_crop_unreg;
        string type = "";

        clearChildObjs(parent_transform_crops_reg);
        clearChildObjs(parent_transform_crop_unreg);
        crop_show_panel.SetActive(true);
        crop_stats_panel.SetActive(false);
        crop_registered_btn.gameObject.SetActive(false);
        crop_unregistered_btn.gameObject.SetActive(false);
        crop_register_btn.gameObject.SetActive(false);
        List<string> reg_ids = new List<string>();
        List<string> dereg_ids = new List<string>();

        GameObject asset_prefab = new GameObject();
        CropDataModel[] data = MessageHandler.userModel.crops;
        foreach (ImgObjectView prefabs in img)
        {
            if (prefabs.name == "Crop Field")
            {
                Debug.Log("found");
                asset_prefab = prefabs.prefab;
                type = prefabs.type;
                break;
            }
        }
        crop_level_dropdown.gameObject.SetActive(true);
        if (data.Length != 0)
        {
            if (show_rarity == "Level")
            {
                foreach (CropDataModel asset_data in data)
                {
                    Debug.Log(asset_data.asset_id);
                    if (asset_data.reg == "0")
                    {
                        Debug.Log("in unreg");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(unreg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<CropAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.register_btn.SetActive(true);
                        reg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true); string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }
                    else if (asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
                    {
                        Debug.Log("in reg");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(reg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<CropAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = asset_data.name;
                        child.slot_size = asset_data.slots;
                        child.land_id = asset_data.land_id;
                        child.level = asset_data.level;
                        child.on_recip = asset_data.on_recipe;
                        child.prod_sec = asset_data.prod_sec;
                        child.harvest = asset_data.harvests;
                        child.max_harvest = asset_data.max_harvests;
                        child.LoadingPanel = LoadingPanel;
                        child.cooldown = asset_data.cd_start;
                        child.details_btn.SetActive(true);
                        child.details_btn.GetComponent<Button>().onClick.AddListener(delegate { show_crops_details(child); });
                        dereg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true);
                        string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }
                }
            }
            else
            {
                foreach (CropDataModel asset_data in data)
                {
                    if (asset_data.level == show_rarity && asset_data.reg == "0")
                    {
                        Debug.Log("in unreg");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(unreg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<CropAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.register_btn.SetActive(true);
                        reg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true);
                        string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }

                    else if (asset_data.level == show_rarity && asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
                    {
                        Debug.Log("in reg");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(reg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<CropAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = asset_data.name;
                        child.slot_size = asset_data.slots;
                        child.land_id = asset_data.land_id;
                        child.level = asset_data.level;
                        child.on_recip = asset_data.on_recipe;
                        child.prod_sec = asset_data.prod_sec;
                        child.harvest = asset_data.harvests;
                        child.max_harvest = asset_data.max_harvests;
                        child.LoadingPanel = LoadingPanel;
                        child.cooldown = asset_data.cd_start;
                        child.details_btn.SetActive(true);
                        child.details_btn.GetComponent<Button>().onClick.AddListener(delegate { show_crops_details(child); });
                        dereg_ids.Add(asset_data.asset_id);
                        child.level_text.gameObject.transform.parent.gameObject.SetActive(true);
                        string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                        Debug.Log(nums);
                        child.level_text.text = nums;
                    }
                }
            }
        }
        var register_ids = string.Join(",", reg_ids.ToArray());
        var deregister_ids = string.Join(",", dereg_ids.ToArray());
        crop_deregister_all_btn.onClick.RemoveAllListeners();
        crop_register_all_btn.onClick.RemoveAllListeners();
        crop_register_all_btn.onClick.AddListener(delegate { Register_All(register_ids); });
        crop_deregister_all_btn.onClick.AddListener(delegate { Deregister_All(deregister_ids); });
    }

    private void ShowElements_Lands(string show_level)
    {
        var reg = parent_transform_land_reg;
        var unreg = parent_transform_land_unreg;
        string type = "";

        clearChildObjs(reg);
        clearChildObjs(unreg);
        land_show_panel.SetActive(true);
        land_stats_panel.SetActive(false);
        land_registered_btn.gameObject.SetActive(false);
        land_unregistered_btn.gameObject.SetActive(false);
        land_register_all_btn.gameObject.SetActive(false);
        land_deregister_all_btn.gameObject.SetActive(false);

        AssetModel[] data = MessageHandler.userModel.lands;
        GameObject asset_prefab = new GameObject();
        foreach (ImgObjectView prefabs in img)
        {
            if (prefabs.name == "Land")
            {
                asset_prefab = prefabs.prefab;
                type = prefabs.type;
                break;
            }
        }
        //crop_level_dropdown.gameObject.SetActive(true);
        if (data.Length != 0)
        {
            if (level_type == "Level")
            {
                foreach (AssetModel asset_data in data)
                {
                    Debug.Log(asset_data.asset_id);
                    if (asset_data.reg == "0")
                    {
                        Debug.Log("in");
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(unreg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<LandAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.register_btn.SetActive(true);
                    }
                    else if (asset_data.reg == "1")
                    {
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(reg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<LandAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        string id = asset_data.asset_id;
                        child.unregister_btn.SetActive(true);
                        child.select_btn.SetActive(true);
                        child.select_btn.GetComponent<Button>().onClick.AddListener(delegate { switchLand(id); });
                        if (MessageHandler.userModel.land_id == child.asset_id)
                            child.select_btn.GetComponent<Button>().interactable = false;
                        else
                            child.select_btn.GetComponent<Button>().interactable = true;
                        int total_assets = 0;
                        child.registered_assets_text.gameObject.SetActive(true);
                        foreach(AssetModel trees in MessageHandler.userModel.trees)
                        {
                            if (trees.land_id == child.asset_id)
                                total_assets++;
                        }
                        foreach(CropDataModel crops in MessageHandler.userModel.crops)
                        {
                            if (crops.land_id == child.asset_id)
                                total_assets++;
                        }
                        foreach(MachineDataModel machines in MessageHandler.userModel.machines)
                        {
                            if (machines.land_id == child.asset_id)
                                total_assets++;
                        }
                        child.registered_assets_text.text = "Registered NFTs : " + total_assets.ToString();
                    }
                }
            }
            /*else
            {
                foreach (AssetModel asset_data in data)
                {
                    if (asset_data.level == level_type && asset_data.reg == "0")
                    {
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(unreg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<CropAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.register_btn.SetActive(true);
                    }

                    else if (asset_data.level == level_type && asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
                    {
                        var ins = Instantiate(asset_prefab);
                        ins.transform.SetParent(reg);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<CropAssetCall>();
                        child.asset_id = asset_data.asset_id;
                        child.asset_id_text.text = "#" + asset_data.asset_id;
                        child.asset_name = name;
                        child.LoadingPanel = LoadingPanel;
                        child.unregister_btn.SetActive(true);
                        child.select_btn.SetActive(true);
                        string id = asset_data.asset_id;
                        child.select_btn.GetComponent<Button>().onClick.AddListener(delegate { switchLand(id); });
                    }
                }
            }*/
        }

        var ins_obj = Instantiate(asset_prefab);
        ins_obj.transform.SetParent(reg);
        ins_obj.transform.localScale = new Vector3(1, 1, 1);
        var child_obj = ins_obj.gameObject.GetComponent<LandAssetCall>();
        child_obj.asset_id = "0";
        child_obj.asset_id_text.text = "Community Land";
        child_obj.LoadingPanel = LoadingPanel;
        child_obj.select_btn.SetActive(true);
        if(MessageHandler.userModel.land_id == child_obj.asset_id)
            child_obj.select_btn.GetComponent<Button>().interactable = false;
        else
            child_obj.select_btn.GetComponent<Button>().interactable = true;
        child_obj.select_btn.GetComponent<Button>().onClick.AddListener(delegate { switchLand("0"); });

        int total = 0;
        child_obj.registered_assets_text.gameObject.SetActive(true);
        foreach (AssetModel trees in MessageHandler.userModel.trees)
        {
            if (trees.land_id == "0")
                total++;
        }
        foreach (CropDataModel crops in MessageHandler.userModel.crops)
        {
            if (crops.land_id == "0")
                total++;
        }
        foreach (MachineDataModel machines in MessageHandler.userModel.machines)
        {
            if (machines.land_id == "0")
                total++;
        }
        child_obj.registered_assets_text.text = "Registered NFTs : " + total.ToString();
    }

    public void switchLand(string asset_id)
    {
        MessageHandler.userModel.land_id = asset_id;
        StartCoroutine(LoadingCountdown());
        asset_id = "0";
        back_status = "close";
        stack_trees_type = "null";
        stack_machines_type = "null";
        level_type = "Level";
        onTrees = false;
        onMachines = false;
        onCrops = false;
        onLand = false;
        can_start_machine = false;
        can_start_crop = false;
        stopTimer = false;
    }

    private IEnumerator LoadingCountdown()
    {
        LoadingPanel.SetActive(true);
        Start();
        current_back_status = "close";
        navButton("back");
        yield return new WaitForSeconds(1f);
        LoadingPanel.SetActive(false);
    }

    private void ShowElements_Trees(string tree_name)
    {
        var reg = parent_transform_trees_reg;
        var unreg = parent_transform_trees_unreg;
        string type = "";

        List<string> reg_ids = new List<string>();
        List<string> dereg_ids = new List<string>();
        clearChildObjs(parent_transform_trees_reg);
        clearChildObjs(parent_transform_trees_unreg);
        tree_show_panel.SetActive(true);
        tree_stats_panel.SetActive(false);
        tree_registered_btn.gameObject.SetActive(false);
        tree_unregistered_btn.gameObject.SetActive(false);
        tree_register_btn.gameObject.SetActive(false);
        tree_unregister_btn.gameObject.SetActive(false);
        tree_register_all_btn.gameObject.SetActive(false);
        tree_deregister_all_btn.gameObject.SetActive(false);
        tree_claim_all_btn.gameObject.SetActive(false);


        List<AssetModel> data = new List<AssetModel>();
        GameObject asset_prefab = new GameObject();
        switch (tree_name)
        {
            case ("Fig"):
                Debug.Log("found prefab");
                data = fig;
                break;
            case ("Mango"):
                Debug.Log("found prefab");
                data = mango;
                break;
            case ("Lemon"):
                Debug.Log("found prefab");
                data = lemon;
                break;
            case ("Orange"):
                Debug.Log("found prefab");
                data = orange;
                break;
            case ("Coconut"):
                data = coconut;
                break;
        }
        foreach (ImgObjectView prefabs in img)
        {
            if (prefabs.name == tree_name)
            {
                Debug.Log("found");
                asset_prefab = prefabs.prefab;
                type = prefabs.type;
                break;
            }
        }

        if (data.Count != 0)
        {
            foreach (AssetModel asset_data in data)
            {
                Debug.Log(asset_data.asset_id);
                if (asset_data.reg == "0")
                {
                    Debug.Log("in");
                    var ins = Instantiate(asset_prefab);
                    ins.transform.SetParent(unreg);
                    ins.transform.localScale = new Vector3(1, 1, 1);
                    var child = ins.gameObject.GetComponent<AssetCall>();
                    child.asset_id = asset_data.asset_id;
                    child.asset_id_text.text = "#" + asset_data.asset_id;
                    child.asset_id_text.fontSize = 14;
                    child.asset_name = tree_name;
                    child.LoadingPanel = LoadingPanel;
                    child.register_btn.SetActive(true);
                    child.time = asset_data.last_claim;
                    child.delayValue = asset_data.delay;
                    child.cooldown = asset_data.cooldown;
                    child.boost_btn.gameObject.SetActive(false);
                    child.current_harvest.text = asset_data.current_harvests;
                    reg_ids.Add(asset_data.asset_id);
                    child.start_timer = false;
                    child.level = asset_data.level;
                    string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                    child.level_text.text = nums;
                }
                else if (asset_data.reg == "1" && asset_data.land_id == MessageHandler.userModel.land_id)
                {
                    Debug.Log("in reg");
                    var ins = Instantiate(asset_prefab);
                    ins.transform.SetParent(reg);
                    ins.transform.localScale = new Vector3(1, 1, 1);
                    var child = ins.gameObject.GetComponent<AssetCall>();
                    child.asset_id = asset_data.asset_id;
                    child.asset_id_text.text = "#" + asset_data.asset_id;
                    child.asset_id_text.fontSize = 14;
                    child.asset_name = tree_name;
                    child.LoadingPanel = LoadingPanel;
                    child.unregister_btn.SetActive(true);
                    child.time = asset_data.last_claim;
                    child.delayValue = asset_data.delay;
                    child.cooldown = asset_data.cooldown;
                    child.boost_btn.gameObject.SetActive(true);
                    child.current_harvest.text = asset_data.current_harvests;
                    child.boost_btn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { show_boost("tree", asset_data.asset_id); });
                    if (asset_data.cooldown == "1" || Int64.Parse(asset_data.current_harvests) == Int64.Parse(asset_data.max_harvests))
                        child.maxed_harvests = true;
                    else
                        child.start_timer = true;
                    dereg_ids.Add(asset_data.asset_id);
                    child.level = asset_data.level;
                    string nums = new String(asset_data.level.Where(Char.IsDigit).ToArray());
                    child.level_text.text = nums;
                }
            }
        }
        var register_ids = string.Join(",", reg_ids.ToArray());
        var deregister_ids = string.Join(",", dereg_ids.ToArray());
        tree_register_all_btn.onClick.RemoveAllListeners();
        tree_deregister_all_btn.onClick.RemoveAllListeners();
        tree_register_all_btn.onClick.AddListener(delegate { Register_All(register_ids); });
        tree_deregister_all_btn.onClick.AddListener(delegate { Deregister_All(deregister_ids); });
    }
    public void Claim_All_Tree_Produce(string asset_id)
    {
        LoadingPanel.SetActive(true);
        MessageHandler.Server_ClaimTree(asset_id);
    }

    public void show_machines_details(MachineAssetCall child_obj)
    {
        current_back_status = "on_registered_machines";
        if (double.Parse(child_obj.harvest) >= double.Parse(child_obj.max_harvest))
        {
            navButton("back");
        }
        else
        {
            machine_child_asset = null;
            clearChildObjs(parent_machine_current_ing);
            machine_rarity_dropdown.gameObject.SetActive(false);
            if (machine_show_panel.gameObject.activeInHierarchy) machine_show_panel.gameObject.SetActive(false);
            if (!show_machine_details.gameObject.activeInHierarchy) show_machine_details.gameObject.SetActive(true);
            machine_subheading_text.GetComponent<TMP_Text>().text = " #" + child_obj.asset_id + " Details";
            foreach (ImgObjectView data in img)
            {
                if (data.type == "machine" && data.name.ToUpper() == child_obj.name.ToUpper())
                {
                    Debug.Log("found");
                    //var ins = data.prefab.transform.Find("NFT_Image");
                    break;
                }
            }
            machine_unregister_btn.gameObject.SetActive(true);
            machine_unregister_btn.onClick.RemoveAllListeners();
            machine_unregister_btn.onClick.AddListener(delegate { child_obj.DeRegisterAsset(); });
            float current_filled_slots = child_obj.on_recip.Length;
            float slots_max = float.Parse(child_obj.slot_size);
            slots_in_use_detailView.text = current_filled_slots + "/" + slots_max;
            harvests_detailView.text = child_obj.harvest + "/" + child_obj.max_harvest;
            float current_harvest = float.Parse(child_obj.harvest);
            float max_harvest = float.Parse(child_obj.max_harvest);

            if (current_filled_slots >= 0 && current_filled_slots <= (slots_max * 0.25))
            {
                slots_in_use_detailView.color = new Color32(110, 195, 111, 255); //green
            }
            else if (current_filled_slots > (slots_max * 0.25) && current_filled_slots <= (slots_max * 0.5))
            {
                slots_in_use_detailView.color = new Color32(255, 203, 0, 255); //yellow
            }
            else if (current_filled_slots > (slots_max * 0.5) && current_filled_slots <= (slots_max * 0.75))
            {
                slots_in_use_detailView.color = new Color32(255, 107, 0, 255); //orange
            }
            else if (current_filled_slots > (slots_max * 0.75) && current_filled_slots <= (slots_max))
            {
                slots_in_use_detailView.color = new Color32(255, 0, 0, 255); //red
            }

            if (current_harvest >= 0 && current_harvest <= (max_harvest * 0.25))
            {
                harvests_detailView.color = new Color32(110, 195, 111, 255); //green
            }
            else if (current_harvest > (max_harvest * 0.25) && current_harvest <= (max_harvest * 0.5))
            {
                harvests_detailView.color = new Color32(255, 203, 0, 255); //yellow
            }
            else if (current_harvest > (max_harvest * 0.5) && current_harvest <= (max_harvest * 0.75))
            {
                harvests_detailView.color = new Color32(255, 107, 0, 255); //orange
            }
            else if (current_harvest > (max_harvest * 0.75) && current_harvest <= (max_harvest))
            {
                harvests_detailView.color = new Color32(255, 0, 0, 255); //red
            }

            if (current_filled_slots != 0)
            {
                foreach (On_RecipeDataModel assets in child_obj.on_recip)
                {
                    var gb = Instantiate(ing_prefab);
                    gb.transform.SetParent(parent_machine_current_ing);
                    gb.transform.localScale = new Vector3(1, 1, 1);
                    var child = gb.gameObject.GetComponent<MachineRecipeCall>();
                    foreach (RecipesModel recipes in MessageHandler.userModel.machine_recipes)
                    {
                        if (recipes.id == assets.recipeID)
                        {
                            Debug.Log("in if show current");
                            child.machine_asset_id = child_obj.asset_id;
                            child.machine_name = child_obj.asset_name;
                            child.recipe_name = recipes.out_name;
                            child.recipe_name_text.text = helper.recipes_abv[recipes.out_name];
                            child.recipe_id = assets.recipeID;
                            child.order_id = assets.orderID;
                            child.type = "machine";
                            child.LoadingPanel = LoadingPanel;
                            child.Start_Timer(assets.start, assets.delay);
                            break;
                        }
                    }
                }
            }
            if (current_filled_slots == 0 || current_filled_slots < Int64.Parse(child_obj.slot_size))
            {
                var ins = Instantiate(add_ing_prefab);
                ins.transform.SetParent(parent_machine_current_ing);
                ins.transform.localScale = new Vector3(1, 1, 1);
                ins.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                if (child_obj.cooldown == "1")
                    ins.gameObject.GetComponent<Button>().interactable = false;
                ins.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Open_Add_New_Machine_Ing(child_obj); });
            }
            machine_child_asset = child_obj;
        }
        
    }
    private void Open_Add_New_Machine_Ing(MachineAssetCall machine)
    {
        clearChildObjs(child_machine_show_recipes);
        clearChildObjs(child_machine_recipe_ing);
        start_machine.interactable = false;
        Debug.Log("in open");
        if (!choose_ing_panel.activeInHierarchy) choose_ing_panel.gameObject.SetActive(true);
        if (choose_Panel_scrollView.gameObject.activeInHierarchy) choose_Panel_scrollView.gameObject.SetActive(false);
        Debug.Log("done");
        foreach (RecipesModel recipes in MessageHandler.userModel.machine_recipes)
        {
            if (recipes.machine.ToUpper() == machine.asset_name.ToUpper() || recipes.machine == helper.machines_abv[machine.asset_name])
            {
                Debug.Log("in recipe loop");
                var ins = Instantiate(new_ing_prefab);
                Debug.Log("instantiated");
                ins.transform.SetParent(child_machine_show_recipes);
                Debug.Log("parent_set");
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<Machine_AddIngCall>();
                Debug.Log("set child");
                child.recipe_id = recipes.id;
                child.machine_asset_id = machine.asset_id;
                child.machine_asset_name = machine.asset_name;
                child.recipe_name = recipes.out_name;
                child.recipe_name_text.text = helper.recipes_abv[recipes.out_name];
                child.show_btn.onClick.AddListener(delegate { Show_Recipe_Ing_OnClick(child); });
            }
        }
    }

    public void show_crops_details(CropAssetCall child_obj)
    {
        current_back_status = "on_registered_crops";
        if (double.Parse(child_obj.harvest) >= double.Parse(child_obj.max_harvest))
        {
            navButton("back");
        }
        else
        {
            crop_child_asset = null;
            clearChildObjs(parent_crops_current_ing);
            crop_level_dropdown.gameObject.SetActive(false);
            if (crop_show_panel.gameObject.activeInHierarchy) crop_show_panel.gameObject.SetActive(false);
            if (!show_crop_details.gameObject.activeInHierarchy) show_crop_details.gameObject.SetActive(true);
            crop_subheading_text.GetComponent<TMP_Text>().text = " #" + child_obj.asset_id + " Details";
            foreach (ImgObjectView data in img)
            {
                if (data.type == "crop" && data.name.ToUpper() == child_obj.name.ToUpper())
                {
                    Debug.Log("found");
                    //var ins = data.prefab.transform.Find("NFT_Image");
                    break;
                }
            }
            int current_filled_slots = child_obj.on_recip.Length;
            if (current_filled_slots != 0)
            {
                foreach (On_RecipeDataModel assets in child_obj.on_recip)
                {
                    var gb = Instantiate(ing_prefab);
                    gb.transform.SetParent(parent_crops_current_ing);
                    gb.transform.localScale = new Vector3(1, 1, 1);
                    var child = gb.gameObject.GetComponent<MachineRecipeCall>();
                    foreach (RecipesModel recipes in MessageHandler.userModel.machine_recipes)
                    {
                        if (recipes.id == assets.recipeID)
                        {
                            child.machine_asset_id = child_obj.asset_id;
                            child.machine_name = child_obj.asset_name;
                            child.recipe_name = recipes.out_name;
                            child.recipe_name_text.text = helper.recipes_abv[recipes.out_name];
                            child.recipe_id = assets.recipeID;
                            child.order_id = assets.orderID;
                            child.type = "crop";
                            child.LoadingPanel = LoadingPanel;
                            child.Start_Timer(assets.start, assets.delay);
                            break;
                        }
                    }
                }
            }
            crop_unregister_btn.gameObject.SetActive(true);
            crop_unregister_btn.onClick.RemoveAllListeners();
            crop_unregister_btn.onClick.AddListener(delegate { child_obj.DeRegisterAsset(); });
            float current__slots = child_obj.on_recip.Length;
            float slots_max = float.Parse(child_obj.slot_size);
            slots_in_use_crops_detailView.text = current__slots + "/" + slots_max;
            harvests_crops_detailView.text = child_obj.harvest + "/" + child_obj.max_harvest;
            float current_harvest = float.Parse(child_obj.harvest);
            float max_harvest = float.Parse(child_obj.max_harvest);

            if (current__slots >= 0 && current__slots <= (slots_max * 0.25))
            {
                slots_in_use_crops_detailView.color = new Color32(110, 195, 111, 255); //green
            }
            else if (current__slots > (slots_max * 0.25) && current__slots <= (slots_max * 0.5))
            {
                slots_in_use_crops_detailView.color = new Color32(255, 203, 0, 255); //yellow
            }
            else if (current__slots > (slots_max * 0.5) && current__slots <= (slots_max * 0.75))
            {
                slots_in_use_crops_detailView.color = new Color32(255, 107, 0, 255); //orange
            }
            else if (current__slots > (slots_max * 0.75) && current__slots <= (slots_max))
            {
                slots_in_use_crops_detailView.color = new Color32(255, 0, 0, 255); //red
            }

            if (current_harvest >= 0 && current_harvest <= (max_harvest * 0.25))
            {
                harvests_crops_detailView.color = new Color32(110, 195, 111, 255); //green
            }
            else if (current_harvest > (max_harvest * 0.25) && current_harvest <= (max_harvest * 0.5))
            {
                harvests_crops_detailView.color = new Color32(255, 203, 0, 255); //yellow
            }
            else if (current_harvest > (max_harvest * 0.5) && current_harvest <= (max_harvest * 0.75))
            {
                harvests_crops_detailView.color = new Color32(255, 107, 0, 255); //orange
            }
            else if (current_harvest > (max_harvest * 0.75) && current_harvest <= (max_harvest))
            {
                harvests_crops_detailView.color = new Color32(255, 0, 0, 255); //red
            }
            if (current_filled_slots == 0 || current_filled_slots < Int64.Parse(child_obj.slot_size))
            {
                var ins = Instantiate(add_ing_prefab);
                ins.transform.SetParent(parent_crops_current_ing);
                ins.transform.localScale = new Vector3(1, 1, 1);
                ins.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                ins.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Open_Add_New_Crop_Ing(child_obj); });
            }
            crop_child_asset = child_obj;
        }
    }

    private void Open_Add_New_Crop_Ing(CropAssetCall machine)
    {
        clearChildObjs(child_machine_show_recipes);
        clearChildObjs(child_machine_recipe_ing);
        Debug.Log("in open");
        if (!choose_ing_panel.activeInHierarchy) choose_ing_panel.gameObject.SetActive(true);
        if (choose_Panel_scrollView.gameObject.activeInHierarchy) choose_Panel_scrollView.gameObject.SetActive(false);
        start_machine.interactable = false;
        Debug.Log("done");
        foreach (RecipesModel recipes in MessageHandler.userModel.machine_recipes)
        {
            if (recipes.machine.ToUpper() == machine.asset_name.ToUpper() || recipes.machine == helper.machines_abv[machine.asset_name])
            {
                Debug.Log("in recipe loop");
                var ins = Instantiate(new_ing_prefab);
                Debug.Log("instantiated");
                ins.transform.SetParent(child_machine_show_recipes);
                Debug.Log("parent_set");
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<Machine_AddIngCall>();
                Debug.Log("set child");
                child.recipe_id = recipes.id;
                child.machine_asset_id = machine.asset_id;
                child.machine_asset_name = machine.asset_name;
                child.recipe_name = recipes.out_name;
                child.recipe_name_text.text = helper.recipes_abv[recipes.out_name];
                child.show_btn.onClick.AddListener(delegate { Show_Recipe_Ing_OnClick(child); });
            }
        }
    }

    private void Show_Recipe_Ing_OnClick(Machine_AddIngCall recipe_obj)
    {
        clearChildObjs(child_machine_recipe_ing);
        Debug.Log("in show recipe");
        if (!choose_Panel_scrollView.gameObject.activeInHierarchy) choose_Panel_scrollView.gameObject.SetActive(true);
        foreach (RecipesModel recipes in MessageHandler.userModel.machine_recipes)
        {
            Debug.Log("in for loop");
            if (recipes.id == recipe_obj.recipe_id)
            {
                Debug.Log("found recipe id");
                can_start_machine = true;
                foreach (IngModel ings in recipes.products)
                {
                    Debug.Log("in for each loop");
                    var ins = Instantiate(ing_prefab_2);
                    ins.transform.SetParent(child_machine_recipe_ing);
                    ins.transform.localScale = new Vector3(1, 1, 1);
                    var child = ins.gameObject.GetComponent<Show_ingCall>();
                    child.raw_name_text.text = ings.in_name;
                    bool found = false;
                    foreach (IngModel balance in MessageHandler.userModel.user_balance)
                    {
                        if (balance.in_name == ings.in_name)
                        {
                            found = true;
                            child.qty_text.text = balance.in_qty + "/" + ings.in_qty;
                            if (double.Parse(balance.in_qty) >= double.Parse(ings.in_qty))
                            {
                                child.qty_text.color = new Color32(94, 173, 53, 255); //Greeen
                            }
                            else if (double.Parse(balance.in_qty) < double.Parse(ings.in_qty))
                            {
                                child.qty_text.color = new Color32(212, 27, 29, 255); //Red
                                can_start_machine = false;
                            }
                            break;
                        }
                    }
                    if (!found)
                    {
                        child.qty_text.text = "0/" + ings.in_qty;
                        child.qty_text.color = new Color32(212, 27, 29, 255);
                        can_start_machine = false;
                    }
                }//Max Machines,Max Trees,Max Crops
                start_machine.interactable = can_start_machine;
                final_product_text.text = recipes.out_qty + " " + helper.recipes_abv[recipes.out_name];
                start_machine.onClick.RemoveAllListeners();
                Debug.Log(recipe_obj.machine_asset_id);
                string machine_id = recipe_obj.machine_asset_id;
                start_machine.onClick.AddListener(delegate { Start_machine_trx(recipes.id, machine_id); });
                break;
            }
        }
    }
    public void Start_machine_trx(string recipe_id, string machine_asset_id)
    {
        Debug.Log(recipe_id);
        Debug.Log(machine_asset_id);
        LoadingPanel.SetActive(true);
        if (onMachines)
            MessageHandler.Server_StartMachine(machine_asset_id, recipe_id, "machine");
        else if (onCrops)
            MessageHandler.Server_StartMachine(machine_asset_id, recipe_id, "crop");
    }

    public void showRegisteredAssets(string type)
    {
        level_type = "Level";
        switch (type)
        {
            case ("trees"):
                current_back_status = "on_trees_stats_panel";
                parent_transform_trees_unreg.gameObject.SetActive(false);
                ShowElements_Trees(stack_trees_type);
                if (parent_transform_trees_reg.childCount == 0)
                    SSTools.ShowMessage("No Registered Trees", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_trees_reg.gameObject.SetActive(true);
                    parent_transform_trees_reg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_trees_reg.GetComponent<RectTransform>();
                    tree_subheading_text.GetComponent<TMP_Text>().text = "Registered Trees";
                    tree_claim_all_btn.gameObject.SetActive(true);
                    tree_deregister_all_btn.gameObject.SetActive(true);
                }
                break;
            case ("machines"):
                current_back_status = "on_machine_stats_panel";
                if (!populated_machine_rarity)
                {
                    PopulateMachineRarity();
                    populated_machine_rarity = true;
                }
                parent_transform_machine_unreg.gameObject.SetActive(false);
                ShowElements_Machines(stack_machines_type, level_type);
                if (parent_transform_machine_reg.childCount == 0)
                    SSTools.ShowMessage("No Registered Machine", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_machine_reg.gameObject.SetActive(true);
                    parent_transform_machine_reg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_machine_reg.GetComponent<RectTransform>();
                    machine_subheading_text.GetComponent<TMP_Text>().text = "Registered Machines";
                    machine_deregister_all_btn.gameObject.SetActive(true);
                }
                break;
            case ("crops"):
                if (!populated_crop_level)
                {
                    PopulateCropLevel();
                    populated_crop_level = true;
                }
                ShowElements_Crops(level_type);
                current_back_status = "on_crops_stats_panel";
                parent_transform_crop_unreg.gameObject.SetActive(false);
                if (parent_transform_crops_reg.childCount == 0)
                    SSTools.ShowMessage("No Registered Crop Fields", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_crops_reg.gameObject.SetActive(true);
                    parent_transform_crops_reg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_crops_reg.GetComponent<RectTransform>();
                    crop_subheading_text.GetComponent<TMP_Text>().text = "Registered CropFields";
                    crop_deregister_all_btn.gameObject.SetActive(true);
                }
                break;
            case ("lands"):
                parent_transform_land_unreg.gameObject.SetActive(false);
                current_back_status = "on_lands_stats_panel";
                ShowElements_Lands(level_type);
                if (parent_transform_land_reg.childCount == 0)
                    SSTools.ShowMessage("No Registered Lands", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_land_reg.gameObject.SetActive(true);
                    parent_transform_land_reg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_land_reg.GetComponent<RectTransform>();
                    land_subheading_text.GetComponent<TMP_Text>().text = "Registered Lands";
                    land_deregister_all_btn.gameObject.SetActive(true);
                }
                break;
        }
        Debug.Log(current_back_status);
    }

    public void ShowUnregisteredAssets(string type)
    {
        switch (type)
        {
            case ("trees"):
                current_back_status = "on_trees_stats_panel";
                parent_transform_trees_reg.gameObject.SetActive(false);
                ShowElements_Trees(stack_trees_type);
                if (parent_transform_trees_unreg.childCount == 0)
                    SSTools.ShowMessage("No Un-Registered Trees", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_trees_unreg.gameObject.SetActive(true);
                    parent_transform_trees_unreg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_trees_unreg.GetComponent<RectTransform>();
                    tree_subheading_text.GetComponent<TMP_Text>().text = "Un-Registered Trees";
                    tree_register_all_btn.gameObject.SetActive(true);
                }
                break;
            case ("machines"):
                if (!populated_machine_rarity)
                {
                    PopulateMachineRarity();
                    populated_machine_rarity = true;
                }
                ShowElements_Machines(stack_machines_type, level_type);
                current_back_status = "on_machine_stats_panel";
                parent_transform_machine_reg.gameObject.SetActive(false);
                if (parent_transform_machine_unreg.childCount == 0)
                    SSTools.ShowMessage("No Un-Registered Machine", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_machine_unreg.gameObject.SetActive(true);
                    parent_transform_machine_unreg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_machine_unreg.GetComponent<RectTransform>();
                    machine_subheading_text.GetComponent<TMP_Text>().text = "Un-Registered Machines";
                    machine_register_all_btn.gameObject.SetActive(true);
                }
                break;
            case ("crops"):
                if (!populated_crop_level)
                {
                    PopulateCropLevel();
                    populated_crop_level = true;
                }
                ShowElements_Crops(level_type);
                current_back_status = "on_crops_stats_panel";
                parent_transform_crops_reg.gameObject.SetActive(false);
                if (parent_transform_crop_unreg.childCount == 0)
                    SSTools.ShowMessage("No Un-Registered Crop Fields", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_crop_unreg.gameObject.SetActive(true);
                    parent_transform_crop_unreg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_crop_unreg.GetComponent<RectTransform>();
                    crop_subheading_text.GetComponent<TMP_Text>().text = "Un-Registered CropFields";
                    crop_register_all_btn.gameObject.SetActive(true);
                }
                break;
            case ("lands"):
                current_back_status = "on_lands_stats_panel";
                ShowElements_Lands(level_type);
                parent_transform_land_reg.gameObject.SetActive(false);
                if (parent_transform_land_unreg.childCount == 0)
                    SSTools.ShowMessage("No Un-Registered Lands", SSTools.Position.bottom, SSTools.Time.twoSecond);
                else
                {
                    parent_transform_land_unreg.gameObject.SetActive(true);
                    parent_transform_land_unreg.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_transform_land_unreg.GetComponent<RectTransform>();
                    land_subheading_text.GetComponent<TMP_Text>().text = "Un-Registered Lands";
                    land_register_all_btn.gameObject.SetActive(true);
                }
                break;
        }
    }

    public void show_boost(string type,string asset_id)
    {
        boost_panel.SetActive(true);
        BoostPanelCall boost_child = boost_panel.gameObject.GetComponent<BoostPanelCall>();
        boost_child.LoadingPanel = LoadingPanel;
        boost_child.asset_id = asset_id;
        boost_child.type = type;
        boost_child.symbol = "";
    }

    public void Register_All(string asset_ids)
    {
        Debug.Log(asset_ids);
        LoadingPanel.SetActive(true);
        string type = "";
        string name_type = "empty_name";
        if (onTrees)
        {
            type = "tree";
            name_type = stack_trees_type;
        }
        else if (onMachines)
        {
            name_type = stack_machines_type;
            type = "machine";
        }
        else if (onCrops)
            type = "cropfield";
        else if (onLand)
            type = "land";
        MessageHandler.Server_RegisterAsset(asset_ids, name_type, MessageHandler.userModel.land_id, type);
    }

    public void Deregister_All(string asset_ids)
    {
        Debug.Log(asset_ids);
        LoadingPanel.SetActive(true);
        string type = "";
        string name_type = "empty_name";
        if (onTrees)
        {
            type = "tree";
            name_type = stack_trees_type;
        }
        else if (onMachines)
        {
            type = "machine";
            name_type = stack_machines_type;
        }
        else if (onCrops)
            type = "cropfield";
        else if (onLand)
            type = "land";
        MessageHandler.Server_DeRegisterAsset(asset_ids, name_type, type);
    }

    public void showStats(string type)
    {
        List<AssetModel> data = new List<AssetModel>();
        List<MachineDataModel> data_machine = new List<MachineDataModel>();
        switch (type)
        {
            case ("Fig"):
                data = fig;
                break;
            case ("Mango"):
                data = mango;
                break;
            case ("Lemon"):
                data = lemon;
                break;
            case ("Orange"):
                data = orange;
                break;
            case ("Coconut"):
                data = coconut;
                break;
            case ("Juicer"):
                data_machine = juicer;
                break;
            case ("Popcorn Maker"):
                data_machine = popcorn;
                break;
            case ("Feeder"):
                data_machine = feeder;
                break;
            case ("BBQ"):
                data_machine = bbq;
                break;
            case ("Ice Cream Maker"):
                data_machine = icecream;
                break;
            case ("Dairy"):
                data_machine = dairy;
                break;
            default:
                break;
        }
        int reg = 0;
        int de_reg = 0;
        double current_pro = 0;
        string asset_name_caps = type.ToUpper();
        if (onTrees)
        {
            if (data.Count != 0)
            {
                foreach (AssetModel treeData in data)
                {
                    if (treeData.reg == "0")
                    {
                        de_reg += 1;
                    }
                    else if (treeData.reg == "1")
                    {
                        reg += 1;
                        if (double.TryParse(treeData.prod_pwer, out double prod))
                            current_pro += prod;
                    }
                }
            }
            else
            {
                string message = "No " + type + " NFT Owned!";
                SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.twoSecond);
            }

            if (tree_show_btn.gameObject.activeInHierarchy) tree_show_btn.gameObject.SetActive(false);
            if (parent_tree_main_menu_panel.gameObject.activeInHierarchy) parent_tree_main_menu_panel.gameObject.SetActive(false);
            if (tree_show_panel.activeInHierarchy) tree_show_panel.SetActive(false);
            if (!tree_stats_panel.activeInHierarchy) tree_stats_panel.SetActive(true);
            tree_registered_btn.gameObject.SetActive(true);
            tree_unregistered_btn.gameObject.SetActive(true);
            total_trees.text = data.Count.ToString();
            reg_total_trees.text = reg.ToString();
            un_total_trees.text = de_reg.ToString();
            time_to_claim.text = current_pro.ToString("0.00") + " " + type;
            current_produce.text = MessageHandler.GetBalanceKey(type.ToUpper()) + " " + type;
        }
        else if (onMachines)
        {
            machine_rarity_dropdown.gameObject.SetActive(false);
            int in_cooldown = 0;
            if (data_machine.Count != 0)
            {
                foreach (MachineDataModel machineData in data_machine)
                {
                    if (machineData.reg == "0")
                    {
                        de_reg += 1;
                    }
                    else if (machineData.reg == "1")
                    {
                        reg += 1;
                        if (machineData.cd_start == "1")
                            in_cooldown++;
                    }
                }
            }
            else
            {
                string message = "No " + type + " NFT Owned!";
                SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.twoSecond);
            }

            if (machine_show_btn.gameObject.activeInHierarchy) machine_show_btn.gameObject.SetActive(false);
            if (parent_machines_main_menu_panel.gameObject.activeInHierarchy) parent_machines_main_menu_panel.gameObject.SetActive(false);
            if (machine_show_panel.activeInHierarchy) machine_show_panel.SetActive(false);
            if (!machine_stats_panel.activeInHierarchy) machine_stats_panel.SetActive(true);
            machine_registered_btn.gameObject.SetActive(true);
            machine_unregistered_btn.gameObject.SetActive(true);
            total_machines.text = data.Count.ToString();
            reg_total_machines.text = reg.ToString();
            un_total_machines.text = de_reg.ToString();
            current_produce.text = in_cooldown.ToString();
        }
        else if (onCrops && type == "Crop Fields")
        {
            if (MessageHandler.userModel.crops.Length != 0)
            {
                foreach (CropDataModel crop in MessageHandler.userModel.crops)
                {
                    if (crop.reg == "0")
                    {
                        de_reg += 1;
                    }
                    else if (crop.reg == "1")
                    {
                        reg += 1;
                    }
                }
            }
            else
            {
                string message = "No CropField NFT Owned!";
                SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            if (crop_show_panel.activeInHierarchy) crop_show_panel.SetActive(false);
            if (!crop_stats_panel.activeInHierarchy) crop_stats_panel.SetActive(true);
            crop_registered_btn.gameObject.SetActive(true);
            crop_unregistered_btn.gameObject.SetActive(true);
            total_crops.text = MessageHandler.userModel.crops.Length.ToString();
            reg_total_crops.text = reg.ToString();
            un_total_crops.text = de_reg.ToString();
        }
        else if (onLand && type == "Lands")
        {
            if (MessageHandler.userModel.lands.Length != 0)
            {
                foreach (AssetModel land in MessageHandler.userModel.lands)
                {
                    if (land.reg == "0")
                    {
                        de_reg += 1;
                    }
                    else if (land.reg == "1")
                    {
                        reg += 1;
                    }
                }
            }
            else
            {
                string message = "No Land NFT Owned!";
                SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            if (land_show_panel.activeInHierarchy) land_show_panel.SetActive(false);
            if (!land_stats_panel.activeInHierarchy) land_stats_panel.SetActive(true);
            land_registered_btn.gameObject.SetActive(true);
            land_unregistered_btn.gameObject.SetActive(true);
            total_land.text = MessageHandler.userModel.lands.Length.ToString();
            reg_total_land.text = reg.ToString();
            un_total_land.text = de_reg.ToString();
        }
    }

    public void onStackPanelOpen()
    {
        if (onTrees)
        {
            current_back_status = "main_tree_menu";
            if (stack_trees_type != "null")
            {
                if (tree_subheading_text_type_2.activeInHierarchy) tree_subheading_text_type_2.SetActive(false);
                if (!tree_subheading_text.activeInHierarchy) tree_subheading_text.SetActive(true);
                tree_subheading_text.GetComponent<TMP_Text>().text = stack_trees_type + " Trees";
                clearChildObjs(parent_transform_trees_unreg);
                clearChildObjs(parent_transform_trees_reg);

                switch (stack_trees_type)
                {
                    case ("Fig"):
                        showStats("Fig");
                        break;
                    case ("Mango"):
                        showStats("Mango");
                        break;
                    case ("Lemon"):
                        showStats("Lemon");
                        break;
                    case ("Orange"):
                        showStats("Orange");
                        break;
                    case ("Coconut"):
                        showStats("Coconut");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                SSTools.ShowMessage("Please Select a Tree Type", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
        }
        else if (onMachines)
        {
            current_back_status = "main_machine_menu";
            if (stack_machines_type != "null")
            {

                if (machine_subheading_text_type_2.activeInHierarchy) machine_subheading_text_type_2.SetActive(false);
                if (!machine_subheading_text.activeInHierarchy) machine_subheading_text.SetActive(true);
                machine_subheading_text.GetComponent<TMP_Text>().text = stack_machines_type + " Machine";
                clearChildObjs(parent_transform_machine_reg);
                clearChildObjs(parent_transform_machine_unreg);
                switch (stack_machines_type)
                {
                    case ("Popcorn Maker"):
                        showStats("Popcorn Maker");
                        break;
                    case ("Juicer"):
                        showStats("Juicer");
                        break;
                    case ("Feeder"):
                        showStats("Feeder");
                        break;
                    case ("BBQ"):
                        showStats("BBQ");
                        break;
                    case ("Ice Cream Maker"):
                        showStats("Ice Cream Maker");
                        break;
                    case ("Dairy"):
                        showStats("Dairy");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                SSTools.ShowMessage("Please Select a Machine Type", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
        }
        else if (onCrops)
        {
            if (from_main_menu) current_back_status = "close";
            else current_back_status = "closeall";
            if (crop_subheading_text_type_2.activeInHierarchy) crop_subheading_text_type_2.SetActive(false);
            if (!crop_subheading_text.activeInHierarchy) crop_subheading_text.SetActive(true);
            crop_subheading_text.GetComponent<TMP_Text>().text = "Crop Fields";
            clearChildObjs(parent_transform_crops_reg);
            clearChildObjs(parent_transform_crop_unreg);
            showStats("Crop Fields");
        }
        else if (onLand)
        {
            if (from_main_menu) current_back_status = "close";
            else current_back_status = "closeall";
            if (!land_subheading_text.activeInHierarchy) land_subheading_text.SetActive(true);
            land_subheading_text.GetComponent<TMP_Text>().text = "Lands";
            clearChildObjs(parent_transform_land_reg);
            clearChildObjs(parent_transform_land_unreg);
            showStats("Lands");
        }
    }

    public void SetTreeType()
    {
        UI_opened = true;
        if (onTrees)
        {
            stack_trees_type = EventSystem.current.currentSelectedGameObject.transform.parent.name;
            SSTools.ShowMessage("Selected " + stack_trees_type + " Tree", SSTools.Position.top, SSTools.Time.twoSecond);
            onStackPanelOpen();
        }
        else if (onMachines)
        {
            stack_machines_type = EventSystem.current.currentSelectedGameObject.transform.parent.name;
            SSTools.ShowMessage("Selected " + stack_machines_type + " Machine", SSTools.Position.top, SSTools.Time.twoSecond);
            onStackPanelOpen();
        }
        else if (onCrops)
            onStackPanelOpen();
        else if (onLand)
            onStackPanelOpen();
    }

    private void clearChildObjs(Transform transform)
    {
        if (transform.childCount >= 1)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    public void navButton(string type)
    {
        string nav_type = "";
        if (type == "back")
            nav_type = current_back_status;
        level_type = "Level";
        switch (nav_type)
        {
            case ("main_tree_menu"):
                if (from_main_menu) current_back_status = "close";
                else current_back_status = "closeall";
                tree_claim_btn.gameObject.SetActive(false);
                parent_transform_trees_unreg.gameObject.SetActive(false);
                parent_transform_trees_reg.gameObject.SetActive(false);
                tree_subheading_text_type_2.gameObject.SetActive(true);
                tree_subheading_text.gameObject.SetActive(false);
                tree_subheading_text_type_2.GetComponent<TMP_Text>().text = "Select Tree";
                tree_stats_panel.SetActive(false);
                tree_show_panel.SetActive(true);
                tree_registered_btn.gameObject.SetActive(false);
                tree_unregistered_btn.gameObject.SetActive(false);
                parent_tree_main_menu_panel.gameObject.SetActive(true);
                parent_tree_main_menu_panel.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_tree_main_menu_panel.GetComponent<RectTransform>();
                break;
            case ("on_trees_stats_panel"):
                parent_transform_trees_unreg.gameObject.SetActive(false);
                tree_register_all_btn.gameObject.SetActive(false);
                tree_deregister_all_btn.gameObject.SetActive(false);
                tree_claim_all_btn.gameObject.SetActive(false);
                onStackPanelOpen();
                break;
            case ("on_registered_machines"):
                machine_rarity_dropdown.gameObject.SetActive(true);
                machine_show_panel.gameObject.SetActive(true);
                show_machine_details.gameObject.SetActive(false);
                can_start_machine = false;
                start_machine.interactable = false;
                current_back_status = "on_machine_stats_panel";
                machine_subheading_text.GetComponent<TMP_Text>().text = "Registered Machines";
                break;
            case ("on_unregistered_trees"):
                break;
            case ("main_machine_menu"):
                if (from_main_menu) current_back_status = "close";
                else current_back_status = "closeall";
                parent_transform_machine_unreg.gameObject.SetActive(false);
                parent_transform_machine_reg.gameObject.SetActive(false);
                machine_subheading_text_type_2.gameObject.SetActive(true);
                machine_subheading_text.gameObject.SetActive(false);
                machine_subheading_text_type_2.GetComponent<TMP_Text>().text = "Select Machine";
                machine_stats_panel.SetActive(false);
                machine_show_panel.SetActive(true);
                machine_registered_btn.gameObject.SetActive(false);
                machine_unregistered_btn.gameObject.SetActive(false);
                parent_machines_main_menu_panel.gameObject.SetActive(true);
                parent_machines_main_menu_panel.parent.gameObject.transform.parent.gameObject.GetComponent<ScrollRect>().content = parent_machines_main_menu_panel.GetComponent<RectTransform>();
                break;
            case ("on_machine_stats_panel"):
                parent_transform_machine_unreg.gameObject.SetActive(false);
                machine_rarity_dropdown.gameObject.SetActive(false);
                machine_register_all_btn.gameObject.SetActive(false);
                machine_deregister_all_btn.gameObject.SetActive(false);
                can_start_machine = false;
                onStackPanelOpen();
                break;
            case ("on_crops_stats_panel"):
                parent_transform_crop_unreg.gameObject.SetActive(false);
                crop_level_dropdown.gameObject.SetActive(false);
                crop_register_all_btn.gameObject.SetActive(false);
                crop_deregister_all_btn.gameObject.SetActive(false);
                onStackPanelOpen();
                if (from_main_menu) current_back_status = "close";
                else current_back_status = "closeall";
                break;
            case ("on_registered_crops"):
                crop_level_dropdown.gameObject.SetActive(true);
                crop_show_panel.gameObject.SetActive(true);
                show_crop_details.gameObject.SetActive(false);
                can_start_crop = false;
                start_crop.interactable = false;
                current_back_status = "on_crops_stats_panel";
                machine_subheading_text.GetComponent<TMP_Text>().text = "Registered Crop Fields";
                break;
            case ("on_lands_stats_panel"):
                parent_transform_land_unreg.gameObject.SetActive(false);
                land_deregister_all_btn.gameObject.SetActive(false);
                land_register_all_btn.gameObject.SetActive(false);
                if (from_main_menu) current_back_status = "close";
                else current_back_status = "closeall";
                //machine_rarity_dropdown.gameObject.SetActive(false);
                onStackPanelOpen();
                break;
            case ("close"):
                tree_parent_panel.SetActive(false);
                machine_parent_panel.SetActive(false);
                crop_parent_panel.SetActive(false);
                land_parent_panel.SetActive(false);
                main_menu_panel.SetActive(true);
                shop_parent_panel.SetActive(false);
                exchange_parent_panel.SetActive(false);
                inv_parent_panel.SetActive(false);
                onTrees = false;
                onMachines = false;
                onCrops = false;
                onLand = false;
                stack_machines_type = null;
                stack_trees_type = null;
                current_back_status = "close";
                UI_opened = false;
                from_main_menu = false;
                break;
            case ("closeall"):
                tree_parent_panel.SetActive(false);
                machine_parent_panel.SetActive(false);
                crop_parent_panel.SetActive(false);
                land_parent_panel.SetActive(false);
                main_menu_panel.SetActive(false);
                shop_parent_panel.SetActive(false);
                exchange_parent_panel.SetActive(false);
                inv_parent_panel.SetActive(false);
                onTrees = false;
                onMachines = false;
                onCrops = false;
                onLand = false;
                stack_machines_type = null;
                stack_trees_type = null;
                current_back_status = "closeall";
                UI_opened = false;
                break;
            default:
                break;
        }
    }
    public void logoutButton()
    {
        LoadingPanel.SetActive(true);
        MessageHandler.Server_LogoutSession();
    }

    public void storeId(string id)
    {
        assetId = id;
    }

    public string assetId
    {
        get { return asset_id; }
        set { asset_id = value; }
    }

    public string current_back_status
    {
        get { return back_status; }
        set { back_status = value; }
    }

    public string stack_trees_type
    {
        get { return s_trees_type; }
        set { s_trees_type = value; }
    }

    public string stack_machines_type
    {
        get { return s_machine_type; }
        set { s_machine_type = value; }
    }

    public string set_helper_var
    {
        get { return helper_var; }
        set { helper_var = value; }
    }

    public void selectType(string type)
    {
        UI_opened = true;
        if (from_main_menu) current_back_status = "close";
        else current_back_status = "closeall";
        switch (type)
        {
            case ("trees"):
                if (!onTrees) onTrees = true;
                main_menu_panel.SetActive(false);
                tree_parent_panel.SetActive(true);
                break;
            case ("machines"):
                if (!onMachines) onMachines = true;
                main_menu_panel.SetActive(false);
                machine_parent_panel.SetActive(true);
                break;
            case ("crops"):
                if (!onCrops) onCrops = true;
                main_menu_panel.SetActive(false);
                crop_parent_panel.SetActive(true);
                SetTreeType();
                break;
            case ("lands"):
                if (!onLand) onLand = true;
                main_menu_panel.SetActive(false);
                land_parent_panel.SetActive(true);
                SetTreeType();
                break;
            case ("shop"):
                main_menu_panel.SetActive(false);
                shop_parent_panel.SetActive(true);
                current_back_status = "close";
                break;
            case ("exchange"):
                main_menu_panel.SetActive(false);
                exchange_parent_panel.SetActive(true);
                break;
            case ("inventory"):
                main_menu_panel.SetActive(false);
                inv_parent_panel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void CloseBtn()
    {
        choose_ing_panel.SetActive(false);
        successPanel.SetActive(false);
        boost_panel.SetActive(false);
        main_menu_panel.SetActive(false);
    }

    public void OnCallBackData(CallBackDataModel[] callback)
    {
        CallBackDataModel callBack = callback[0];
        LoadingPanel.SetActive(false);
        Debug.Log("in callBAck");
        if (!string.IsNullOrEmpty(callBack.type))
        {
            if (callBack.type == "reg")
            {
                SetData();
                if (onTrees && !string.IsNullOrEmpty(callBack.tree_name))
                {
                    ShowElements_Trees(callBack.tree_name);
                }
                else if (onMachines && !string.IsNullOrEmpty(callBack.machine_name))
                {
                    ShowElements_Machines(callBack.machine_name, level_type);
                }
                else if (onLand)
                {
                    ShowElements_Lands(level_type);
                }
                else if (onCrops)
                {
                    ShowElements_Crops(level_type);
                }
                SSTools.ShowMessage("Registeration Successfull !", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            else if (callBack.type == "dereg")
            {
                SetData();
                if (onTrees && !string.IsNullOrEmpty(callBack.tree_name))
                {
                    ShowElements_Trees(callBack.tree_name);

                }
                else if (onMachines && !string.IsNullOrEmpty(callBack.machine_name))
                {
                    machine_rarity_dropdown.gameObject.SetActive(true);
                    machine_show_panel.gameObject.SetActive(true);
                    show_machine_details.gameObject.SetActive(false);
                    showRegisteredAssets("machines");
                }
                else if (onLand)
                {
                    ShowElements_Lands(level_type);
                }
                else if (onCrops)
                {
                    crop_level_dropdown.gameObject.SetActive(true);
                    crop_show_panel.gameObject.SetActive(true);
                    show_crop_details.gameObject.SetActive(false);
                    showRegisteredAssets("crops");
                }
                SSTools.ShowMessage("De-Registration Successfull !", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            else if (callBack.type == "logout")
            {
                LoadingPanel.SetActive(true);
                SceneManager.LoadScene("GameLoginScene");
            }
            else if (callBack.type == "start_machine")
            {
                choose_ing_panel.gameObject.SetActive(false);
                if (onMachines)
                {
                    machine_child_asset.on_recip = callBack.on_recip;
                    machine_child_asset.harvest = callBack.harvest;
                    machine_child_asset.cooldown = callBack.cooldown;
                    show_machines_details(machine_child_asset);
                }
                else if (onCrops)
                {
                    crop_child_asset.on_recip = callBack.on_recip;
                    crop_child_asset.harvest = callBack.harvest;
                    machine_child_asset.cooldown = callBack.cooldown;
                    show_crops_details(crop_child_asset);
                }
            }
            else if (callBack.type == "tree_claim")
            {
                SetData();
                showRegisteredAssets("trees");
                SSTools.ShowMessage("Claim Successfull !", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            else if (callBack.type == "boost")
            {
                SetData();
                CloseBtn();
                if (!string.IsNullOrEmpty(callBack.helper))
                {
                    switch (callBack.helper)
                    {
                        case "tree":
                            showRegisteredAssets("trees");
                            break;
                        case "machine":
                            showRegisteredAssets("machines");
                            break;
                        case "crop":
                            showRegisteredAssets("crops");
                            break;
                    }
                }
                SSTools.ShowMessage("Boost Added Successfully !", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            else if (callBack.type == "recipe_claim")
            {
                SetData();
                if (!string.IsNullOrEmpty(set_helper_var))
                {
                    foreach (RecipesModel recipes in MessageHandler.userModel.machine_recipes)
                    {
                        if (helper_var == recipes.id)
                        {
                            SSTools.ShowMessage("Claim Successfull !", SSTools.Position.bottom, SSTools.Time.twoSecond);
                            successPanel.SetActive(true);
                            success_text.text = "SuccessFully Prepared " + helper.recipes_abv[recipes.out_name];
                            success_header_text.text = "Great Success !";
                            set_helper_var = "";
                            //final_product_successPanel Images 
                            break;
                        }
                    }
                }
                if (onMachines)
                {
                    machine_child_asset.on_recip = callBack.on_recip;
                    machine_child_asset.prod_sec = callBack.prod_sec;
                    machine_child_asset.harvest = callBack.harvest;
                    show_machines_details(machine_child_asset);
                }
                else if (onCrops)
                {
                    crop_child_asset.on_recip = callBack.on_recip;
                    crop_child_asset.prod_sec = callBack.prod_sec;
                    crop_child_asset.harvest = callBack.harvest;
                    show_crops_details(crop_child_asset);
                }
                SSTools.ShowMessage("Claim Successfull !", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
            else if (callBack.type == "withdraw")
            {
                Debug.Log("withdraw");
                successPanel.SetActive(true);
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().game_balance.text = MessageHandler.GetBalanceKey("AWC") + " AWC";
                Debug.Log("after balance");
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().available_waxw_balance.text = MessageHandler.userModel.awcBal + " AWC";
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().game_balance.text = MessageHandler.GetBalanceKey("AWC") + " AWC";
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().withdrawal_fee.text = "10%";
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().received_amount.text = "0.0000 AWC";
                success_text.text = "Wallet Debited for AWC " + callBack.helper;
                success_header_text.text = "Withdraw Successfull";
                StartCoroutine(StartTokenTimer(callBack.helper, "withdraw"));
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().withdraw_input.text = "";
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().withdraw_amount.text = "0.0000 AWC";
                final_product_successPanel.sprite = Resources.Load<Sprite>("Sprites/money");
            }
            else if (callBack.type == "deposit")
            {
                successPanel.SetActive(true);
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().available_waxw_balance.text = MessageHandler.userModel.awcBal + " AWC";
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().game_balance.text = MessageHandler.GetBalanceKey("AWC") + " AWC";
                exchange_parent_panel.gameObject.GetComponent<ExchangeView>().deposit_input.text = "";
                success_text.text = "Wallet Credited for AWC " + callBack.helper;
                success_header_text.text = "Deposit Successfull";
                StartCoroutine(StartTokenTimer(callBack.helper, "deposit"));
                final_product_successPanel.sprite = Resources.Load<Sprite>("Sprites/money");
            }
            else if (callBack.type == "order fill")
            {
                successPanel.SetActive(true);
                success_text.text = "Wallet Credited for AWC " + MessageHandler.marketmodel.reward.in_qty + '\n' + "Earned AWXP " + MessageHandler.marketmodel.xp_boost.in_qty; 
                success_header_text.text = "Order Filled Successfully";
                StartCoroutine(StartTokenTimer(MessageHandler.marketmodel.reward.in_qty, "deposit"));
                MessageHandler.marketmodel = null;
                //Img
            }
            else if (callBack.type == "buy")
            {
                successPanel.SetActive(true);
                string action_name = "";
                string item_name = "";
                switch (MessageHandler.shopmodle.type)
                {
                    case ("ingame"):
                        action_name = "Purchased";
                        item_name = MessageHandler.shopmodle.resource.in_name;
                        break;
                    case ("pack"):
                        action_name = "Purchased";
                        item_name = "pack";
                        break;
                    case ("mint"):
                        action_name = "Minted";
                        item_name = MessageHandler.shopmodle.price.in_name;
                        break;
                }
                success_text.text = action_name + " 1 " + item_name + '\n' + "Wallet Debited for AWC " + MessageHandler.shopmodle.price.in_qty;
                success_header_text.text = action_name + " Successfully";
                StartCoroutine(StartTokenTimer(MessageHandler.shopmodle.price.in_qty, "withdraw"));
                MessageHandler.shopmodle = null;
            }
        }
    }

    private IEnumerator StartTokenTimer(string amount,string type)
    {
        Debug.Log("In ienum");
        string[] bal = userbalance_text.text.Split(' ');
        double ingameBal = double.Parse(bal[0]);
        Debug.Log(ingameBal);
        double final_amount = 0;
        double iter = double.Parse(amount) / 50;
        Debug.Log(iter);
        int temp = 0;
        if(type == "deposit")
        {
            final_amount = ingameBal + double.Parse(amount); ;
            while (temp != 50)
            {
                ingameBal += iter;
                userbalance_text.text = Math.Round(ingameBal, 2).ToString() + " AWC";
                yield return new WaitForSeconds(0.05f);
                temp++;
            }
        }
        else
        {
            final_amount = ingameBal - double.Parse(amount);
            while (temp != 50)
            {
                ingameBal -= iter;
                userbalance_text.text = Math.Round(ingameBal, 2).ToString() + " AWC";
                yield return new WaitForSeconds(0.05f);
                temp++;
            }
        }
        Debug.Log(final_amount);
        userbalance_text.text = final_amount.ToString() + " AWC";
    }
}
