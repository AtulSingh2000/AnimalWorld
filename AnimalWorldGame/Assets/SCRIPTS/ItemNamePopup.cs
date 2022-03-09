using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNamePopup : MonoBehaviour
{
    public GameObject juicer;
    public GameObject feeder;
    public GameObject icecream;
    public GameObject bbq;
    public GameObject milk;
    public GameObject house;
    public GameObject inventory;
    public GameObject shop;
    public GameObject tokenexchange;

    // Start is called before the first frame update
    void Start()
    {
        juicer.SetActive(false);
        bbq.SetActive(false);
        icecream.SetActive(false);
        milk.SetActive(false);
        feeder.SetActive(false);
        house.SetActive(false);
        inventory.SetActive(false);
        shop.SetActive(false);
        tokenexchange.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnMouseEnter()
    {
     if(gameObject.name == "Juicer")
     {
         juicer.SetActive(true);
     }
     if(gameObject.name == "BBQ")
     {
         bbq.SetActive(true);
     }
     if(gameObject.name == "IceCream")
     {
         icecream.SetActive(true);
     }
     if(gameObject.name == "MilkFactory")
     {
         milk.SetActive(true);
     }
     if(gameObject.name == "Feeder")
     {
         feeder.SetActive(true);
     }
     if(gameObject.name == "TokenExchange")
     {
         tokenexchange.SetActive(true);
     }
     if(gameObject.name == "Shop")
     {
         shop.SetActive(true);
     }
     if(gameObject.name == "Inventory")
     {
         inventory.SetActive(true);
     }
     if(gameObject.name == "House")
     {
         house.SetActive(true);
     }
    }

     private void OnMouseExit()
    {
        juicer.SetActive(false);
        bbq.SetActive(false);
        icecream.SetActive(false);
        milk.SetActive(false);
        feeder.SetActive(false);
        house.SetActive(false);
        inventory.SetActive(false);
        shop.SetActive(false);
        tokenexchange.SetActive(false);
    }
}
