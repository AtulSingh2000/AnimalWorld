using System;
[Serializable]

public class MachineDataModel
{
    public string name;
    public string asset_id;
    public string template_id;
    public string slots;
    public string cd_start;
    public string harvests;
    public string max_harvests;
    public string land_id;
    public string level;
    public string prod_sec;
    public On_RecipeDataModel[] on_recipe;
    public string reg;
    public string rarity;
    public string img;
}
