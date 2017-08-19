using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Shop : MonoBehaviour
{

    List<ItemTag> items;

    public bool showPickAxe, showShield, showShovel, showTNT;

    public void readTextFile()
    {
        items = new List<ItemTag>();
        string path = Application.dataPath + "/Scripts/shop/ItemsPrice.txt";
        StreamReader reader = new StreamReader(path);

        while(!reader.EndOfStream)
            items.Add(ItemTag.CreateFromJSON(reader.ReadLine()));
    }

    void Start()
    {
        readTextFile();

        Debug.Log(items.Count);

        foreach (ItemTag it in items)
        {
            Debug.Log(it.name +" "+it.price);
        }
    }

    void showTools(GameObject player)
    {
        PlayerTools playerTools = player.GetComponent<PlayerTools>();
        showPickAxe = playerTools.pickAxe == null;
        showShield  = playerTools.shield == null;
        showShovel  = playerTools.shovel == null;
        showTNT = playerTools.tnt == null;
    }

    private int getItemPrice(string itemName)
    {
        foreach (ItemTag it in items)
            if (it.name == itemName)
                return it.price;

        return 0;
    }

    public bool buyItem(string itemName, GameObject player)
    {
        PlayerGold playerGold = player.GetComponent<PlayerGold>();
        PlayerTools playerTools = player.GetComponent<PlayerTools>();

        switch (itemName.ToLower())
        {
           case "shield":
                if (getItemPrice("shield") <= playerGold.UsableGold)
                {
                    playerGold.SpendGold(getItemPrice("shield"));
                    playerTools.shield = new Shield();
                    return true;
                }
                else return false;

            case "tnt":
                if (getItemPrice("tnt") <= playerGold.UsableGold)
                {
                    playerGold.SpendGold(getItemPrice("tnt"));
                    playerTools.tnt = new TNT();
                    return true;
                }
                else return false;

            case "pickaxe":
                if (getItemPrice("pickaxe") <= playerGold.UsableGold)
                {
                    playerGold.SpendGold(getItemPrice("pickaxe"));
                    playerTools.pickAxe = new PickAxe();
                    return true;
                }
                else return false;

            case "shovel":
                if (getItemPrice("shovel") <= playerGold.UsableGold)
                {
                    playerGold.SpendGold(getItemPrice("shovel"));
                    playerTools.shovel = new Shovel();
                    return true;
                }
                else return false;

            default:
                Debug.Log("probleme classe Shop : nom " + itemName.ToLower() + " inexistant parmi les noms d'items");
                return false;
        }
    }
}
