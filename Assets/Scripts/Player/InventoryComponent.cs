using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryComponent : MonoBehaviour {

    public string itemName;
    public Sprite itemIcon;


  public  List<Sprite>Icons;
  public List<GameObject> Items;

    bool takeItem;


    void Update()
    {
        Debug.Log("Finding item icon");

        foreach (var item in Resources.LoadAll<Sprite>("ItemIcons"))
        {
             if (!Icons.Contains(item))
             { 
                Icons.AddRange(Resources.LoadAll<Sprite>("ItemIcons"));
             }
        }
        foreach (var item in Resources.LoadAll<Sprite>("ItemIcons//Farm"))
        {
            if (!Icons.Contains(item))
            {
                Icons.AddRange(Resources.LoadAll<Sprite>("ItemIcons//Farm"));
            }
        }

        foreach (var item in Resources.LoadAll<GameObject>("Items"))
        {
            if (!Items.Contains(item))
            {
                Items.AddRange(Resources.LoadAll<GameObject>("Items"));
            }
        }
        foreach (var item in Resources.LoadAll<GameObject>("Items//Food"))
        {
            if (!Items.Contains(item))
            {
                Items.AddRange(Resources.LoadAll<GameObject>("Items//Food"));
            }
        }

        foreach (var item in Icons)
        {
            if (item.name == itemName)
            {
                itemIcon = item;
            }
        }


        transform.GetChild(2).GetComponent<Image>().sprite = itemIcon;
        transform.GetChild(2).GetComponent<Image>().preserveAspect = true;



    }

    
        public void take()
    {

        foreach (var item in Items)
        {
            Debug.Log(item.name);
            if (item.name == itemName)
            {
                Debug.Log("Selecting Items");
                    
                GameObject i = Instantiate(item) as GameObject;
                i.transform.position = new Vector3(GameObject.Find("Storage").transform.position.x, GameObject.Find("Storage").transform.position.y + .5f, GameObject.Find("Storage").transform.position.z);
                
                foreach (var key in GameObject.Find("Storage").GetComponent<StorageInventory>().dictionary.Keys)
                {
                    if(key == itemName)
                    {
                        GameObject.Find("Storage").GetComponent<StorageInventory>().dictionary[key] -= 1;
                    }
                }
                if (i.GetComponent<Crop>())
                {
                    i.GetComponent<Crop>().ifPickedUp = true;
                }
            }
        }

    }

    
}
