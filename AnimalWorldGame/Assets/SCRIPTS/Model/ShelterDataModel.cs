using System;
[Serializable]

public class ShelterDataModel
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
    public IngModel[] animals;
    public string reg;
    public string rarity;
    public string img;
    public IngModel[] cost_level;
}
