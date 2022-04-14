using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCall : MonoBehaviour
{
    public string asset_id;
    public string asset_name;
    public string time;
    public string delayValue;
    public string cooldown;
    public string level;
    public string shelter_id;
    public TMP_Text asset_id_text;
    public TMP_Text time_to_claim;
    public TMP_Text current_harvest;
    public TMP_Text level_text;
    public GameObject register_btn;
    public GameObject unregister_btn;
    public GameObject claim_btn;
    public GameObject LoadingPanel;
    public GameObject boost_btn;
    public bool start_timer;
    public bool maxed_harvests;
    public Image asset_image;
    public bool can_claim = false;
    public string type = "null";

    protected virtual void Start()
    {
        asset_image = this.gameObject.transform.Find("NFT_Image").gameObject.GetComponent<Image>();
        if (type != "addAnimal")
        {
            claim_btn.GetComponent<Button>().interactable = false;
            if (start_timer)
            {
                StartCoroutine(StartCountdown(time, delayValue));
            }
            else if (maxed_harvests)
            {
                claim_btn.gameObject.SetActive(true);
                time_to_claim.gameObject.SetActive(true);
                time_to_claim.text = "Maxed Harvests";
                UnityEngine.Color alpha = asset_image.color;
                alpha.a = 0.7f;
                asset_image.color = alpha;
            }
            else if (cooldown == "1")
            {
                time_to_claim.gameObject.SetActive(true);
                claim_btn.gameObject.SetActive(true);
                time_to_claim.text = "In Cooldown!";
                UnityEngine.Color alpha = asset_image.color;
                alpha.a = 0.7f;
                asset_image.color = alpha;
            }
        }
    }

    public void RegisterAsset()
    {
        Debug.Log("Register_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(shelter_id))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_RegisterAsset(asset_id, asset_name, shelter_id, "animal");
        }
        else
            SSTools.ShowMessage("Animal Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void DeRegisterAsset()
    {
        Debug.Log("DeRegister_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_DeRegisterAsset(asset_id, asset_name, "animal");
        }
        else
            SSTools.ShowMessage("Animal Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    private IEnumerator StartCountdown(string time, string delayValue)
    {
        claim_btn.gameObject.SetActive(false);
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        double delay_seconds = Convert.ToDouble(delayValue);
        double final_epoch_time = Convert.ToDouble(time) + delay_seconds;
        double currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        double diff = final_epoch_time - currentEpochTime;
        Debug.Log("Difference is  - " + diff);
        time_to_claim.gameObject.transform.parent.gameObject.SetActive(true);
        if (diff > 0)
        {
            int temp = 0;
            while (temp != 1)
            {
                TimeSpan Ntime = TimeSpan.FromSeconds(diff);
                time_to_claim.text = Ntime.ToString();
                yield return new WaitForSeconds(1f);
                diff -= 1;
                if (diff == 0) temp = 1;
            }
        }
        time_to_claim.gameObject.transform.parent.gameObject.SetActive(false);
        claim_btn.gameObject.SetActive(true);
        claim_btn.GetComponent<Button>().interactable = true;
        can_claim = true;
        claim_btn.GetComponent<Button>().onClick.AddListener(delegate { Claim_Produce(); });
    }

    public void Claim_Produce()
    {
        LoadingPanel.SetActive(true);
        MessageHandler.Server_ClaimAnimal(asset_id);
    }
}
