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
        machines.Add("Ice Cream Maker", "icecreammaker");
        machines.Add("icecreammaker", "Ice Cream Maker");
        machines.Add("BBQ", "bbq");
        machines.Add("Feeder", "feeder");
        machines.Add("445189", "Premium Machine Crate V.2");
        machines.Add("445188", "Standard Machine Crate V.2");
        machines.Add("445662", "CropField");
        machines.Add("485528", "Legendary Animal Crate");
        machines.Add("485500", "Standard Animal Crate");
        machines.Add("485501", "Standard Shelter Crate");


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
        recipes.Add("CRJUICE", "Carrot Juice");

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
        recipes.Add("BFEED", "Bee Feed");
        recipes.Add("CFEED", "Cow Feed");
        recipes.Add("PFEED", "Pig Feed");
        recipes.Add("HFEED", "Chicken Feed");
        recipes.Add("GFEED", "Goat Feed");

        recipes.Add("HONEY", "Honey");
        recipes.Add("PORK", "Pork");
        recipes.Add("EGG", "Egg");
        recipes.Add("GMILK", "Goat Milk");
        recipes.Add("CMILK", "Cow Milk");
        recipes.Add("CREAM", "Cream");
        recipes.Add("HICE", "Honey Ice Cream");
        recipes.Add("MICE", "Mango Ice Cream");
        recipes.Add("OICE", "Orange Ice Cream");
        recipes.Add("CICE", "Coconut Ice Cream");
        recipes.Add("FICE", "Fig Ice Cream");
        recipes.Add("LICE", "Lemon Ice Cream");
        recipes.Add("MFICE", "Mixed Fruit Ice Cream");
        recipes.Add("HPBBQ", "Honey Pork BBQ");
        recipes.Add("LPBBQ", "Lemon Pork BBQ");
        recipes.Add("CPBBQ", "Cheesy Chilly Pork BBQ");
        recipes.Add("SBBQ", "Creamy Soy Lemon Grill");
        recipes.Add("CLOM", "Chilly Omelette");
        recipes.Add("CHOM", "Cheese Omelette");
        recipes.Add("GVEG", "Cheesy Grilled Veggies");
        recipes.Add("GFRUIT", "Creamy Grilled Fruits");
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
