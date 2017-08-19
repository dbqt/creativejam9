using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTag
{

    public string name;
    public int price;

    public static ItemTag CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<ItemTag>(jsonString);
    }
}
