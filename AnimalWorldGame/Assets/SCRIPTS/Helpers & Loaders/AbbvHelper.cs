using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbbvHelper : MonoBehaviour
{
    private Dictionary<string, string> machines = new Dictionary<string, string>();
    private Dictionary<string, string> recipes = new Dictionary<string, string>();

    void Start()
    {
        machines.Add("Juicer", "juicer");
        machines.Add("Popcorn Maker", "popcornmaker");
        machines.Add("Crop Field", "cropfield");
        machines.Add("Dairy", "dairy");
        machines.Add("Ice Cream Maker","icecream");

        //Popcorn Maker
        recipes.Add("CPOPCORN", "Cheese Popcorn");
        recipes.Add("SPCORN", "Spicy Popcorn");
        recipes.Add("CORN", "Corn");
        recipes.Add("CHILLY", "Chilly");
        recipes.Add("CHEESE", "Cheese");
        recipes.Add("CPCORN", "Cheese Popcorn");
        recipes.Add("MANGO", "Mango");
        recipes.Add("ORANGE", "Orange");
        recipes.Add("COCUNUT", "Coconut");
        recipes.Add("COCONUT", "Coconut");
        recipes.Add("FIG", "Fig");
        recipes.Add("LEMON", "Lemon");
        recipes.Add("FPCORN", "Fruit Popcorn");
        recipes.Add("PCORN", "Popcorn");

        //Juicer
        recipes.Add("MFJUICE", "Mixed Fruit Juice");
        recipes.Add("LJUICE", "Lime Juice");
        recipes.Add("FJUICE", "Fig Juice");
        recipes.Add("CJUICE", "Coconut");
        recipes.Add("OJUICE", "Orange Juice");
        recipes.Add("MJUICE", "Mango Juice");

        //Crop Field
        recipes.Add("WHEATSE", "WHEATSE");
        recipes.Add("CLYSE", "CLYSE");
        recipes.Add("SBEANE", "SBEANE");
        recipes.Add("CORNSE", "CORNSE");
        recipes.Add("CRTSE", "CRTSE");
        recipes.Add("WHEAT", "Wheat");
        recipes.Add("CRT", "CRT");
        recipes.Add("CLY", "CLY");
        recipes.Add("SBEAN", "Soya Bean");

        recipes.Add("FLZR", "Fertilizer");
        recipes.Add("OIL", "Oil");

    }

    public Dictionary<string,string> machines_abv
    {
        get { return machines; }
    }

    public Dictionary<string,string> recipes_abv
    {
        get { return recipes; }
    }
}
