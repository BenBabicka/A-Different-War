using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Crafting : MonoBehaviour
{

    StorageData storageData;
    StorageInventory storageInventory;

    public CraftingManager manager;

    public int itemLevel;

    public GameObject parent;

    public string itemName;
    public float craftTime;
    public int amount;

    

    public List<string> Materials;
    public List<int> MaterialsAmount;

    public List<string> keys;

    public float stackItem;



    public List<craftingBench> craftingBenches;
    // Use this for initialization
    void Start()
    {
        gameObject.transform.parent.GetChild(0).GetComponent<Text>().text = itemName;
        gameObject.transform.parent.GetChild(1).GetComponent<Text>().text = craftTime + " Seconds";
        manager = transform.GetComponentInParent<CraftingManager>();
    }



 public void Craft(int craftAmount)
    {
        craftingBenches = manager.craftingBenches;

        for (int i = 0; i < craftingBenches.Count; i++)
        {
            if (craftingBenches[i].levelWorkBench >= itemLevel)
            {
                if (craftingBenches[i].item.itemName == "" || craftingBenches[i].item.itemName == null || craftingBenches[i].item == null)
                {
                    Debug.Log("craft new item");
                    craftingBenches[i].item = new craftingBench.Item(itemName, amount, craftTime, Materials, MaterialsAmount, craftAmount);
                    craftingBenches[i].CraftItem();
                }
                else if (craftingBenches[i].item.itemName == itemName)
                {
                    Debug.Log("add to item");

                    craftingBenches[i].item.stackAmount += craftAmount;
                    craftingBenches[i].CraftItem();

                }
                else
                {
                    if(craftingBenches[i].queue.Count > 0)
                    {


                        for (int x = 0; x < craftingBenches[i].queue.Count; x++)
                        {

                            if (craftingBenches[i].queue[x].itemName == itemName)
                            {
                                craftingBenches[i].queue[x].stackAmount += craftAmount;
                                Debug.Log("continue queue item");

                            }
                            else
                            {
                                Debug.Log("add new queue item");

                                craftingBenches[i].queue.Add(new craftingBench.Item(itemName, amount, craftTime, Materials, MaterialsAmount, craftAmount));
                            }
                        }
                    }
                    else
                    {
                        craftingBenches[i].queue.Add(new craftingBench.Item(itemName, amount, craftTime, Materials, MaterialsAmount, craftAmount));

                    }
                }
            }
        }
    }

      
}




