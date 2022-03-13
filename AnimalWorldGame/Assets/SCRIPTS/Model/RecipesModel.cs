using System;
[Serializable]
public class RecipesModel
{
    public string id;
    public string machine;
    public IngModel[] products;
    public string out_qty;
    public string out_name;
    public string craft_time;
    public string orderID;
}
