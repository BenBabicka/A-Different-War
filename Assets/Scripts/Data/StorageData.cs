using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StorageData : MonoBehaviour {

    [Serializable]
    public struct ItemEntry
    {
        public string name;
        public int amount;
    }
    public ItemEntry[] itemEntries;
    public bool loading;
   /* public int Wood;
    public int Textiles;
    public int Food;
    public int Stone;
    public int Credits;*/

    public 

    void Start()
    {
        if (!loading)
        {
            foreach (var entry in itemEntries)
            {
                gameObject.GetComponent<StorageInventory>().dictionary.Add(entry.name, entry.amount);
            }
        }
      /*  gameObject.GetComponent<StorageInventory>().dictionary.Add("Wood", Wood);
        gameObject.GetComponent<StorageInventory>().dictionary.Add("Textiles", Textiles);
        gameObject.GetComponent<StorageInventory>().dictionary.Add("Carrot", Food/2);
        gameObject.GetComponent<StorageInventory>().dictionary.Add("Potato", Food/2);
        gameObject.GetComponent<StorageInventory>().dictionary.Add("Stone", Stone);
        gameObject.GetComponent<StorageInventory>().dictionary.Add("Credits", Credits);*/

    }

   


}
