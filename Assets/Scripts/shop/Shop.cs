using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Shop : MonoBehaviour
{

    const string checkmark = "✔";
    public ShopItemUI[] ShopItems;
    public List<ItemTag> items;

    [Header("Player 1")]
    public Text P1Score;
    public Text P1Gold;
    public Text[] P1Items;

    [Header("Player 2")]
    public Text P2Score;
    public Text P2Gold;
    public Text[] P2Items;

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

        UpdateUI();
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
                    shovel = 0;
                    result = true;
                }
                break;

            case "shovel":
                if (getItemPrice("shovel") <= usableGold)
                {
                    usableGold -= getItemPrice("shovel");
                    shovel = 1;
                    pickaxe = 0;
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

        UpdateUI();

        return result;
    }

    public void UpdateUI() {
        int score1 = 0;
        int usableGold1 = 0;
        int shield1 = 0;
        int pickaxe1 = 0;
        int shovel1 = 0;
        int bag1 = 0;
        int tnt1 = 0;

        int score2 = 0;
        int usableGold2 = 0;
        int shield2 = 0;
        int pickaxe2 = 0;
        int shovel2 = 0;
        int bag2 = 0;
        int tnt2 = 0;
        
        score1 = PlayerPrefs.GetInt("TotalGold1");
        usableGold1 = PlayerPrefs.GetInt("UsableGold1");
        shield1 = PlayerPrefs.GetInt("Shield1");
        pickaxe1 = PlayerPrefs.GetInt("Pickaxe1");
        shovel1 = PlayerPrefs.GetInt("Shovel1");
        bag1 = PlayerPrefs.GetInt("Bag1");
        tnt1 = PlayerPrefs.GetInt("Tnt1");

        this.P1Score.text = "Score: "+score1;
        this.P1Gold.text = "Gold: "+usableGold1;
        P1Items[items.FindIndex(o => o.name.ToLower() == "shield")].text = shield1 == 1 ? checkmark : "x";
        P1Items[items.FindIndex(o => o.name.ToLower() == "tnt")].text = tnt1 == 1 ? checkmark : "x";
        P1Items[items.FindIndex(o => o.name.ToLower() == "pickaxe")].text = pickaxe1 == 1 ? checkmark : "x";
        P1Items[items.FindIndex(o => o.name.ToLower() == "shovel")].text = shovel1 == 1 ? checkmark : "x";
        P1Items[items.FindIndex(o => o.name.ToLower() == "bag")].text = bag1 == 1 ? checkmark : "x";

        score2 = PlayerPrefs.GetInt("TotalGold2");
        usableGold2 = PlayerPrefs.GetInt("UsableGold2");
        shield2 = PlayerPrefs.GetInt("Shield2");
        pickaxe2 = PlayerPrefs.GetInt("Pickaxe2");
        shovel2 = PlayerPrefs.GetInt("Shovel2");
        bag2 = PlayerPrefs.GetInt("Bag2");
        tnt2 = PlayerPrefs.GetInt("Tnt2");

        this.P2Score.text = "Score: "+score2;
        this.P2Gold.text = "Gold: "+usableGold2;
        P2Items[items.FindIndex(o => o.name.ToLower() == "shield")].text = shield2 == 1 ? checkmark : "x";
        P2Items[items.FindIndex(o => o.name.ToLower() == "tnt")].text = tnt2 == 1 ? checkmark : "x";
        P2Items[items.FindIndex(o => o.name.ToLower() == "pickaxe")].text = pickaxe2 == 1 ? checkmark : "x";
        P2Items[items.FindIndex(o => o.name.ToLower() == "shovel")].text = shovel2 == 1 ? checkmark : "x";
        P2Items[items.FindIndex(o => o.name.ToLower() == "bag")].text = bag2 == 1 ? checkmark : "x";  
    }

    
}

[System.Serializable]
public struct ShopItemUI {
        public Text Name;
        public Text Price;
}
