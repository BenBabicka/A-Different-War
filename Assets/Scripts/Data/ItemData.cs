using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
    {
        Head, Chest, Legs, Feet, Vest, Belt, forhand, inventorySlot
    }


public class ItemData : MonoBehaviour {
 
    public State Slot;
    public Sprite sprite;
    public string itemName;

    public int item_ID;
    [HideInInspector]
    public bool isPickedUp;
}
