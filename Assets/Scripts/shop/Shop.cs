using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Shop : MonoBehaviour
{

    public ShopItemUI[] ShopItems;
    public List<ItemTag> items;

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

        //Debug.Log(items.Count);

        int i = 0;
        foreach (ItemTag it in items)
        {
            //Debug.Log(it.name +" "+it.price);
            ShopItems[i].Name.text = it.name;
            ShopItems[i].Price.text = ""+it.price;

            i++;
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

    public bool buyItem(string itemName, bool isPlayerOne)
    {

        int usableGold = 0;
        int shield = 0;
        int pickaxe = 0;
        int shovel = 0;
        int bag = 0;
        int tnt = 0;
        
        if (isPlayerOne) {
            usableGold = PlayerPrefs.GetInt("UsableGold1");
            shield = PlayerPrefs.GetInt("Shield1");
            pickaxe = PlayerPrefs.GetInt("Pickaxe1");
            shovel = PlayerPrefs.GetInt("Shovel1");
            bag = PlayerPrefs.GetInt("Bag1");
            tnt = PlayerPrefs.GetInt("Tnt1");
        } else {
            usableGold = PlayerPrefs.GetInt("UsableGold2");
            shield = PlayerPrefs.GetInt("Shield2");
            pickaxe = PlayerPrefs.GetInt("Pickaxe2");
            shovel = PlayerPrefs.GetInt("Shovel2");
            bag = PlayerPrefs.GetInt("Bag2");
            tnt = PlayerPrefs.GetInt("Tnt2");
        }

        bool result = false;
        switch (itemName.ToLower())
        {
           case "shield":
                if (getItemPrice("shield") <= usableGold)
                {
                    usableGold -= getItemPrice("shield");
                    shield = 1;
                    result = true;
                }
                break;

            case "tnt":
                if (getItemPrice("tnt") <= usableGold)
                {
                    usableGold -= getItemPrice("tnt");
                    tnt = 1;
                    result = true;
                }
                break;

            case "pickaxe":
                if (getItemPrice("pickaxe") <= usableGold)
                {
                    usableGold -= getItemPrice("pickaxe");
                    pickaxe = 1;
                    result = true;
                }
                break;

            case "shovel":
                if (getItemPrice("shovel") <= usableGold)
                {
                    usableGold -= getItemPrice("shovel");
                    shovel = 1;
                    result = true;
                }
                break;

             case "bag":
                if (getItemPrice("bag") <= usableGold)
                {
                    usableGold -= getItemPrice("bag");
                    bag = 1;
                    result = true;
                }
                break;

            default:
                Debug.Log("probleme classe Shop : nom " + itemName.ToLower() + " inexistant parmi les noms d'items");
                break;
        }

        if (isPlayerOne) {
            PlayerPrefs.SetInt("UsableGold1", usableGold);
            PlayerPrefs.SetInt("Shield1", shield);
            PlayerPrefs.SetInt("Pickaxe1", pickaxe);
            PlayerPrefs.SetInt("Shovel1", shovel);
            PlayerPrefs.SetInt("Bag1", bag);
            PlayerPrefs.SetInt("Tnt1", tnt);
        } else {
            PlayerPrefs.SetInt("UsableGold2", usableGold);
            PlayerPrefs.SetInt("Shield2", shield);
            PlayerPrefs.SetInt("Pickaxe2", pickaxe);
            PlayerPrefs.SetInt("Shovel2", shovel);
            PlayerPrefs.SetInt("Bag2", bag);
            PlayerPrefs.SetInt("Tnt2", tnt);
        }

        return result;
    }

    
}

[System.Serializable]
public struct ShopItemUI {
        public Text Name;
        public Text Price;
    }
