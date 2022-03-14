using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropAssetCall : MonoBehaviour
{
    public string asset_id;
    public string asset_name;
    public string slot_size;
    public string land_id;
    public string level;
    public string cooldown;
    public On_RecipeDataModel[] on_recip;
    public string prod_sec;
    public string harvest;
    public string max_harvest;
    public TMP_Text asset_id_text;
    public TMP_Text level_text;
    public GameObject register_btn;
    public GameObject unregister_btn;
    public GameObject select_btn;
    public GameObject details_btn;
    public GameObject LoadingPanel;

    protected virtual void Start()
    {

        if (cooldown == "1")
        {
            Image machine_image = this.gameObject.transform.Find("NFT_Image").gameObject.GetComponent<Image>();
            UnityEngine.Color alpha = machine_image.color;
            alpha.a = 0.5f;
            machine_image.color = alpha;
            details_btn.GetComponent<Button>().interactable = false;
        }
        else if (!string.IsNullOrEmpty(harvest) && !string.IsNullOrEmpty(max_harvest) && double.Parse(harvest) == double.Parse(max_harvest))
        {
            Image machine_image = this.gameObject.transform.Find("NFT_Image").gameObject.GetComponent<Image>();
            UnityEngine.Color alpha = machine_image.color;
            alpha.a = 0.5f;
            machine_image.color = alpha;
            details_btn.GetComponent<Button>().interactable = false;
        }
    }
    public void RegisterAsset()
    {
        Debug.Log("Register_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_RegisterAsset(asset_id, asset_name,MessageHandler.userModel.land_id, "cropfield");
        }
        else
            SSTools.ShowMessage("CropField Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void DeRegisterAsset()
    {
        Debug.Log("DeRegister_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_DeRegisterAsset(asset_id, asset_name, "cropfield");
        }
        else
            SSTools.ShowMessage("CropField Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }
}
