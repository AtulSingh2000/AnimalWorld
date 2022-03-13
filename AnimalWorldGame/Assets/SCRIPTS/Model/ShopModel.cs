using System;
[Serializable]

public class ShopModel
{
    public string id;
    public string type;
    public string template_id;
    public string schema;
    public string available;
    public string max;
    public IngModel price;
    public IngModel resource;
    public string req_level;
}
