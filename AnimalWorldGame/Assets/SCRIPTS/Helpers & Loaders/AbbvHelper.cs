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
        machines.Add("Ice Cream Maker", "icecream");

        //Popcorn Maker
        recipes.Add("CPOPCORN", "Cheese Popcorn");
        recipes.Add("SPCORN", "Spicy Popcorn");
        recipes.Add("CORN", "Corn");
        recipes.Add("CHILLY", "Chilly");
        recipes.Add("CHEESE", "Cheese");
        recipes.Add("CPCORN", "Cheesy Popcorn");
        recipes.Add("MANGO", "Mango");
        recipes.Add("ORANGE", "Orange");
        recipes.Add("COCONUT", "Coconut");
        recipes.Add("FIG", "Fig");
        recipes.Add("LEMON", "Lemon");
        recipes.Add("FPCORN", "Fruity Popcorn");
        recipes.Add("PCORN", "Popcorn");

        //Juicer
        recipes.Add("MFJUICE", "Mixed Fruit Juice");
        recipes.Add("LJUICE", "Lemon Juice");
        recipes.Add("FJUICE", "Fig Juice");
        recipes.Add("CJUICE", "Coconut Juice");
        recipes.Add("OJUICE", "Orange Juice");
        recipes.Add("MJUICE", "Mango Juice");

        //Crop Field
        recipes.Add("WHEATSE", "Wheat Seeds");
        recipes.Add("CLYSE", "Chilly Seeds");
        recipes.Add("SBEANSE", "Soybean Seeds");
        recipes.Add("CORNSE", "Corn Seeds");
        recipes.Add("CRTSE", "Carrot Seeds");
        recipes.Add("WHEAT", "Wheat");
        recipes.Add("CRT", "Carrot");
        recipes.Add("CLY", "Chilly");
        recipes.Add("SBEAN", "Soybean");

        recipes.Add("FLZR", "Fertilizer");
        recipes.Add("OIL", "Oil");

    }

    public Dictionary<string, string> machines_abv
    {
        get { return machines; }
    }

    public Dictionary<string, string> recipes_abv
    {
        get { return recipes; }
    }
}
