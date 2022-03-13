using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeAssetCall : MonoBehaviour
{
    public string asset_id;
    public string asset_name;
    public string time;
    public string delayValue;
    public TMP_Text asset_id_text;
    public TMP_Text time_to_claim;
    public GameObject register_btn;
    public GameObject unregister_btn;
    public GameObject select_btn;
    public GameObject claim_btn;
    public GameObject LoadingPanel;
    public bool start_timer;

    // Start is called before the first frame update
    /*void Start()
    {
        if (start_timer)
        {
            Debug.Log("Timer Started");
            //StartCoroutine(StartCountdown(time, delayValue));
        }
    }*/

    public void RegisterAsset()
    {
        Debug.Log("Register_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_RegisterAsset(asset_id, asset_name, MessageHandler.userModel.land_id, "tree");
        }
        else
            SSTools.ShowMessage("Tree Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void DeRegisterAsset()
    {
        Debug.Log("DeRegister_Asset");
        if (!string.IsNullOrEmpty(asset_id) && !string.IsNullOrEmpty(asset_name))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.Server_DeRegisterAsset(asset_id, asset_name, "tree");
        }
        else
            SSTools.ShowMessage("Tree Not Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    private IEnumerator StartCountdown(string time, string delayValue)
    {
        Debug.Log("In Ienumerator");
        claim_btn.gameObject.SetActive(true);
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        double delay_seconds = Convert.ToDouble(delayValue);
        Debug.Log(delay_seconds);
        double final_epoch_time = Convert.ToDouble(time) + delay_seconds;
        Debug.Log(final_epoch_time);
        double currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        Debug.Log(currentEpochTime);
        double diff = final_epoch_time - currentEpochTime;
        Debug.Log(diff);
        if (diff > 0)
        {
            int temp = 0;
            time_to_claim.gameObject.SetActive(true);
            while (temp != 1)
            {
                TimeSpan Ntime = TimeSpan.FromSeconds(diff);
                time_to_claim.text = Ntime.ToString();
                Debug.Log(Ntime.ToString());
                yield return new WaitForSeconds(1f);
                diff -= 1;
                if (diff == 0) temp = 1;
            }
        }

        time_to_claim.text = "Claim";
        claim_btn.gameObject.GetComponent<Button>().interactable = true;
        claim_btn.GetComponent<Button>().onClick.AddListener(delegate { Claim_Tree_Produce(asset_name.ToUpper()); });
    }

    public void Claim_Tree_Produce(string symbol)
    {
        LoadingPanel.SetActive(true);
        string claim = "0.00 " + symbol;
        MessageHandler.Server_ClaimTree(claim);
    }
}
