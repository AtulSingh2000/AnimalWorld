using System;
[Serializable]

public class MarketOrderModel
{
    public string id;
    public string type;
    public IngModel[] products;
    public IngModel reward;
    public IngModel xp_boost;
    public string level_boost;
    public string xp_level;
}
