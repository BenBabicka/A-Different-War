using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public float capacity;
    public float amount;
    public Sprite org_sprite;
    public string occupied_Name;
    public bool slotIsFull;
   public List<Sprite> Icons;
    public bool emptyingSlot;

    public int slotId;
    void Update()
    {
        if(amount >= capacity)
        {
            slotIsFull = true;
        }
        else
        {
            slotIsFull = false;
        }

        if(amount == 0)
        {
            org_sprite = null;
            transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
            occupied_Name = null;
        }

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

       

        foreach (var item in Icons)
        {
            if (item.name == occupied_Name)
            {
                org_sprite = item;
            }
            if(occupied_Name == null)
            {
                org_sprite = null;
            }
        }

        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = org_sprite;
        

        if(emptyingSlot)
        {
            gameObject.GetComponentInParent<Inventory>().EmptySlot(slotId);
        if(amount <= 0)
            {
                emptyingSlot = false;
            }
        }

    }

    public void EmptySlot()
    {
        if(amount > 0)
        {
            emptyingSlot = true;
            Debug.Log("empty slot");
        }
    }


}
