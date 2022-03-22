using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineRecipeCall : MonoBehaviour
{
    public string machine_asset_id;
    public string machine_name;
    public string recipe_name;
    public string recipe_id;
    public string order_id;
    public string type;
    public GameObject check_btn;
    public GameObject timer_btn;
    public TMP_Text timer_text;
    public TMP_Text recipe_name_text;
    public GameObject LoadingPanel;
    public Button claim_all;

    public void Start_Timer(string last_search, string delayValue)
    {
        Debug.Log("function called");
        StartCoroutine(StartCountdown(last_search, delayValue));
    }
    private IEnumerator StartCountdown(string time, string delayValue)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        double delay_seconds = Convert.ToDouble(delayValue);
        double final_epoch_time = Convert.ToDouble(time) + delay_seconds;
        double currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        double diff = final_epoch_time - currentEpochTime;
        Debug.Log("Difference is  - " + diff);
        if (diff > 0)
        {
            int temp = 0;
            timer_btn.SetActive(true);
            while (temp != 1)
            {
                TimeSpan Ntime = TimeSpan.FromSeconds(diff);
                timer_text.text = Ntime.ToString();
                yield return new WaitForSeconds(1f);
                diff -= 1;
                if (diff == 0) temp = 1;
            }
        }
        if(timer_btn.activeInHierarchy)timer_btn.SetActive(false);
        claim_all.interactable = true;
        check_btn.SetActive(true);
        check_btn.GetComponent<Button>().onClick.RemoveAllListeners();
        check_btn.GetComponent<Button>().onClick.AddListener(delegate { Claim_Machine(); });
    }

    public void Claim_Machine()
    {
        LoadingPanel.SetActive(true);
        MessageHandler.Server_ClaimMachine(machine_asset_id,order_id,type);
        LoadingPanel.transform.parent.GetComponent<MainView>().set_helper_var = recipe_id;
    }
}
