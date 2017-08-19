using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Shop : MonoBehaviour
{

    List<ItemTag> items;

    public void readTextFile()
    {
        items = new List<ItemTag>();
        string path = Application.dataPath + "Scripts/shop/ItemsPrice.txt";
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
}
