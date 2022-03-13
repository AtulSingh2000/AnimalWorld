using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Dropdown machine_dropdown;
    private string name;
    enum tree_rarities
    {
        Rarity,
        Common,
        Rare,
        Epic,
        Mythic,
        Legendary
    };
    enum machine_rarities
    {
        Rarity,
        Common,
        Rare,
        Legendary
    };
    void Start()
    {
        Populate_Menu();
    }


    private void Populate_Menu()
    {
        string[] rarity_names = Enum.GetNames(typeof(tree_rarities));
        List<string> rarity_list = new List<string>(rarity_names);
        dropdown.AddOptions(rarity_list);

        string[] rarity_names_machines = Enum.GetNames(typeof(machine_rarities));
        List<string> rarity_list_machine = new List<string>(rarity_names_machines);
        machine_dropdown.AddOptions(rarity_list_machine);
    }

    public string set_name
    {
        get { return name; }
        set { name = value; }
    }
    
}
