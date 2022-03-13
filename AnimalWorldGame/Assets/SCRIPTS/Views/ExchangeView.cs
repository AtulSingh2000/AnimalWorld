using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExchangeView : BaseView
{

    public TMP_InputField deposit_input;
    public TMP_InputField withdraw_input;
    public TMP_Text available_waxw_balance;
    public TMP_Text game_balance;
    public TMP_Text deposit_amount;
    public TMP_Text withdrawal_fee;
    public TMP_Text withdraw_amount;
    public TMP_Text received_amount;

    public double withdrawal_fee_config;
    protected override void Start()
    {
        base.Start();
        available_waxw_balance.text=MessageHandler.userModel!=null?MessageHandler.userModel.awcBal:"0.0000 AWC";
        game_balance.text = MessageHandler.GetBalanceKey("AWC");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void deposit()
    {
        if (!string.IsNullOrEmpty(deposit_input.text))
        {
            if(double.Parse(deposit_input.text) <= double.Parse(MessageHandler.userModel.awcBal))
            {
                LoadingPanel.SetActive(true);
                MessageHandler.Server_DepositAWC(deposit_input.text);
            }
            else if (double.Parse(deposit_input.text) > double.Parse(MessageHandler.userModel.awcBal))
                SSTools.ShowMessage("Insufficient Balance", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else
            SSTools.ShowMessage("No Amount Entered", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void withdraw()
    {
        if (!string.IsNullOrEmpty(withdraw_input.text))
        {
            Debug.Log("on button click");
            if(double.TryParse(withdraw_input.text,out double withdraw_amount))
            {
                Debug.Log("parsed");
                if(withdraw_amount <= double.Parse(MessageHandler.GetBalanceKey("AWC")))
                {
                    LoadingPanel.SetActive(true);
                    MessageHandler.Server_WithdrawAWC(withdraw_input.text);
                }
                else if (withdraw_amount > double.Parse(game_balance.text))
                    SSTools.ShowMessage("Insufficient Balance", SSTools.Position.bottom, SSTools.Time.twoSecond);
            }
        }
        else
            SSTools.ShowMessage("No Amount Entered", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void ondeposit_input()
    {
        if(deposit_amount.text==null) return;
            deposit_amount.text=deposit_input.text +" AWC";
        Debug.Log("in");
        Debug.Log(deposit_amount.text);
    }

    public void onwithdraw_input()
    {
        withdraw_amount.text = withdraw_input.text + " AWC";
        string temp_amount = withdraw_input.text;
        if (double.TryParse(temp_amount, out double amount))
        {
            var fee = amount * (withdrawal_fee_config / 100);
            withdrawal_fee.text = fee.ToString() + " AWC";
            amount -= fee;
            received_amount.text = amount.ToString() + " AWC";
        }
        else
        {
            withdraw_amount.text = "0.0000 AWC";
            withdrawal_fee.text = "10%";
            received_amount.text = "0.0000 AWC";
        }
    }
}
