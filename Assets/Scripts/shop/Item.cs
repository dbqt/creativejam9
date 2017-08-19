using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTag
{

    public string name;
    public float price;

    public static ItemTag CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ItemTag>(jsonString);
    }

    public BaseItem getGameObjectFromItem()
    {
        switch (name.ToLower())
        {
            case "bag" :    return new Bag();
            case "shield":  return new Shield();
            case "tnt":     return new TNT();
            case "pickaxe": return new PickAxe();
            case "shovel":  return new Shovel();

            default:
                Debug.Log("probleme classe ItemTag : nom " + name.ToLower() + " inexistant parmi les noms d'items");
                return null;
        }
    }
}
