using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopCall : MonoBehaviour
{
    public string id;
    public string type;


    //Shop
    public string template_id;
    public string schema;
    public string available;
    public string max;
    public IngModel price;
    public IngModel resource;
    public string req_level;

    //DMO
    public IngModel[] products;
    public IngModel reward;
    public IngModel xp_boost;
    public string level_boost;
    public string xp_level;
    public string quantity;

    [Header("Config")]
    public TMP_Text description;
    public TMP_Text min_level;
    public TMP_Text quantitytxt;
    public Image template_image;
    public TMP_Text price1txt;
    public Image symbol1_image;
    public TMP_Text price2txt;
    public Image symbol2_image;
    public TMP_Text availabletxt;
    public TMP_Text reqtxt;
    public Button buyButton;
    public Image button_sprite;
    public GameObject LoadingPanel;
    public TMP_InputField buyqty;

    public ShopView view;
    public AbbvHelper helper;

    public void SetData()
    {
        double user_balance = double.Parse(MessageHandler.GetBalanceKey("AWC"));
        Debug.Log(user_balance);
        if (type=="ingame")
        {
            if(helper.recipes_abv.ContainsKey(resource.in_name))
                description.text= "Buy 1 " + helper.recipes_abv[resource.in_name];
            else
                description.text = "Buy 1 " + resource.in_name;
            var sprite_img = Resources.Load<Sprite>("Sprites/" + helper.recipes_abv[resource.in_name]);
            if (sprite_img)
                template_image.sprite = sprite_img;
            price1txt.text = price.in_qty;
            min_level.text = req_level;
            if (double.Parse(price.in_qty) <= user_balance)
                buyButton.onClick.AddListener(delegate { BuyResource("1"); });
            else
            {
                buyButton.interactable = false;
            }
        }
        else if(type=="mint")
        {
            description.text = "Mint 1 " + helper.recipes_abv[price.in_name] + " NFT";
            var sprite_img = Resources.Load<Sprite>("Sprites/" + helper.recipes_abv[price.in_name]);
            if (sprite_img)
                template_image.sprite = sprite_img;
            price1txt.text = price.in_qty;
            min_level.text = req_level;
            string balance = MessageHandler.GetBalanceKey(price.in_name);
            availabletxt.text =balance.Split('.')[0];
            reqtxt.text = price.in_qty.Split('.')[0];
            buyButton.onClick.AddListener(delegate { BuyResource("1"); });
        }
        else if(type=="pack")
        {
            description.text="Buy 1 " + type;
            price1txt.text=price.in_qty;
            min_level.text=req_level;
            if(double.Parse(price.in_qty) <= user_balance)
                buyButton.onClick.AddListener(delegate { BuyResource("1"); });
            else if (double.Parse(price.in_qty) < user_balance)
            {
                buyButton.interactable = false;
            }
        }
        else if(type=="dmo")
        {
            description.text = products[0].in_qty + " " + helper.recipes_abv[products[0].in_name];
            var sprite_img = Resources.Load<Sprite>("Sprites/" + helper.recipes_abv[products[0].in_name]);
            if (sprite_img)
                template_image.sprite = sprite_img;
            price1txt.text = reward.in_qty;
            price2txt.text = xp_boost.in_qty;
            reqtxt.text = products[0].in_qty.Split('.')[0];
            min_level.text = "1";
            string balance = MessageHandler.GetBalanceKey(products[0].in_name);
            float req_db = float.Parse(products[0].in_qty);
            float balance_db = float.Parse(balance);
            availabletxt.text = balance.Split('.')[0];
            if (balance_db >= 0 && balance_db <= (req_db * 0.25))
            {
                Debug.Log("red");
                button_sprite.sprite = Resources.Load<Sprite>("Sprites/red_btn"); //red
            }
            else if (balance_db > (req_db * 0.25) && balance_db <= (req_db * 0.5))
            {
                Debug.Log("orange");
                button_sprite.sprite = Resources.Load<Sprite>("Sprites/orange_btn"); //orange
            }
            else if (balance_db > (req_db * 0.5) && balance_db <= (req_db * 0.75))
            {
                Debug.Log("yellow");
                button_sprite.sprite = Resources.Load<Sprite>("Sprites/yellow_btn"); //yellow
            }
            else if (balance_db > (req_db * 0.75) && balance_db >= req_db)
            {
                Debug.Log("green");
                button_sprite.sprite = Resources.Load<Sprite>("Sprites/green_btn"); //green
            }

            buyButton.onClick.AddListener(delegate { FillDMO(id); });

            if (double.TryParse(reqtxt.text,out double required_amt))
            {
                if (required_amt <= double.Parse(balance))
                    buyButton.onClick.AddListener(delegate { FillDMO(products[0].in_qty); });
                else
                {
                    buyButton.onClick.AddListener(delegate { FillDMO(balance); });
                    buyButton.interactable = false;
                    UnityEngine.Color alpha = buyButton.gameObject.transform.parent.gameObject.GetComponent<Image>().color;
                    alpha.a = 0.7f;
                    buyButton.gameObject.transform.parent.gameObject.GetComponent<Image>().color = alpha;
                }
                    
            }
        }
    }

    public void FillDMO(string quantity)
    {
        if (!string.IsNullOrEmpty(id))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.marketmodel.id = id;
            MessageHandler.marketmodel.products = products;
            MessageHandler.marketmodel.reward.in_qty = reward.in_qty;
            MessageHandler.marketmodel.xp_boost = xp_boost;
            MessageHandler.marketmodel.xp_level = xp_level;
            MessageHandler.Server_FillDmo(id,quantity);
        }
        else
            SSTools.ShowMessage("No Order Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void BuyResource(string quantity)
    {
        if (!string.IsNullOrEmpty(price.in_qty) && !string.IsNullOrEmpty(id)  && !string.IsNullOrEmpty(resource.in_qty))
        {
            LoadingPanel.SetActive(true);
            MessageHandler.shopmodle.id = id;
            MessageHandler.shopmodle.resource = resource;
            MessageHandler.shopmodle.price.in_name = price.in_name;
            MessageHandler.shopmodle.price.in_qty = (double.Parse(quantity) * double.Parse(price.in_qty)).ToString();
            MessageHandler.shopmodle.type = type;
            MessageHandler.Server_BuyShopL(id,quantity);
        }
        else
            SSTools.ShowMessage("No Order Selected", SSTools.Position.bottom, SSTools.Time.twoSecond);
    }

    public void onBuyQtyChange()
    {
        buyButton.onClick.RemoveAllListeners();
        if(Int64.TryParse(buyqty.text,out long qty))
        {
            Debug.Log(qty);
            if (qty != 0)
            {
                if(helper.recipes_abv.ContainsKey(resource.in_name))
                    description.text = "Buy " + qty.ToString() + " " + helper.recipes_abv[resource.in_name];
                else
                    description.text = "Buy " + qty.ToString() + " " + resource.in_name;
                double final_price = (double.Parse(price.in_qty) * qty);
                price1txt.text = final_price.ToString() + ".00";
                if (final_price <= double.Parse(MessageHandler.GetBalanceKey("AWC")))
                { 
                    buyButton.onClick.AddListener(delegate { BuyResource(qty.ToString()); }); 
                    buyButton.interactable = true;
                }
                else
                {
                    SSTools.ShowMessage("Insufficient Balance !", SSTools.Position.bottom, SSTools.Time.twoSecond);
                    buyButton.interactable = false;
                }
            }
            else
                SSTools.ShowMessage("Buy Quantity Cannot be 0", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        else
        {
            if (helper.recipes_abv.ContainsKey(resource.in_name))
                description.text = "Buy 1 " + helper.recipes_abv[resource.in_name];
            else
                description.text = "Buy 1 " + resource.in_name;
            price1txt.text = price.in_qty;
            if (double.Parse(price.in_qty) <= double.Parse(MessageHandler.GetBalanceKey("AWC"))) { 
                buyButton.interactable = true;
                buyButton.onClick.AddListener(delegate { BuyResource("1"); });
            }
            else
            {
                SSTools.ShowMessage("Insufficient Balance !", SSTools.Position.bottom, SSTools.Time.twoSecond);
                buyButton.interactable = false;
            }
        }
    }

}
