using System.Collections;
using System;

[Serializable]
public class UserModel
{
    public string account;
    public string awcBal;
    public AssetModel[] lands;
    public AssetModel[] trees;
    public MachineDataModel[] machines;
    public AnimalDataModel[] animals;
    public ShelterDataModel[] shelters;
    public CropDataModel[] crops;
    public RecipesModel[] machine_recipes;
    public ProduceDataModel[] user_data;
    public IngModel[] user_balance;
    public IngModel[] land_assets;
    public IngModel[] cost_level;
    public string land_id;
}
