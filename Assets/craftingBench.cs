using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class craftingBench : MonoBehaviour
{
    public int craftingBenchId;

    public GameObject player;

    public int levelWorkBench;

    public Item item;
    [System.Serializable]
    public class Item
    {
        public string itemName;

        public int amountProduced;
        public float craftTime;
        public List<string> itemsConsumed;
        public List<int> amountConsumed;

        public int stackAmount;

        public Item(string iN, int aP, float cT, List<string> iC, List<int> aC, int sA)
        {
            itemName = iN;
            amountProduced = aP;
            craftTime = cT;
            itemsConsumed = iC;
            amountConsumed = aC;
            stackAmount = sA;
        }
    }

    public bool craft;
    public Transform craftingPosition;

    GameObject storage;
    Inventory inventory;
    public int whatSlot;
    public int allSlots;
    public bool deliever;
   public bool craftingItem;
    public List<int> amountCheck;
  public   int amountProduced;
    public bool craftButtonBool;



    public List<string> materialsNeeded;
    public List<int> materialAmountNeed;

    float deliverTime = 1;


    int slot;
    bool delivering;
    public int materialCheckAmount;
    public int devlierMaterialCheckAmount;

    public float craftingTime;

    public int overLimit;


    public List<Item> queue;

    craftingBenchSave craftingBenchSave;

    void Start()
    {
        storage = GameObject.Find("Storage");
        GameObject.Find("Crafting Panel").GetComponent<CraftingSelectPanel>().craftingBenches.Add(this);
        craftingBenchId = (int)gameObject.GetComponent<BuildingID>().ID;
        craftingBenchSave = FindObjectOfType<craftingBenchSave>();
        craftingBenchSave.craftingBenches.Add(this);
    }

    void Update()
    {
      
        if (craftButtonBool)
        {
            if (player)
            {
                Debug.Log("CraftItem");

                inventory = player.GetComponent<Inventory>();

              

                if (item.itemName != "")
                {
                    if (item.stackAmount > 0)
                    {
                        if (deliever == false)
                        {
                            if (!craftingItem)
                            {
                                craft = true;
                            }
                        }
                    }
                    else
                    {
                        craft = false;
                    }

                }
                else
                {
                    craft = false;
                }
                if (!deliever)
                {
                    if (craft)
                    {


                        Debug.Log("Collecting Supplies");
                        collectSupplies(new List<string>(item.itemsConsumed), new List<int>(item.amountConsumed));

                    }
                }

                if (inventory.slots[inventory.slots.Count - 1].slot.GetComponent<InventorySlot>().amount == inventory.slots[inventory.slots.Count - 1].slot.GetComponent<InventorySlot>().capacity)
                {
                    deliever = true;
                }


            }
        }

        if (deliever)
        {

            if (Vector3.Distance(player.transform.position, storage.transform.position) < 3)
            {

                if (slot < inventory.slots.Count)
                {

                    if (inventory.slots[slot].slot.GetComponent<InventorySlot>().amount > 0)
                    {
                        Debug.Log("adding " + item.itemName + " to the stockpile");

                        if (storage.GetComponent<StorageInventory>().dictionary.ContainsKey(inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name))
                        {
                            allSlots -= 1;
                            inventory.slots[slot].slot.GetComponent<InventorySlot>().amount -= 1;
                            storage.GetComponent<StorageInventory>().dictionary[inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name] += 1;
                        }
                        else
                        {
                            allSlots -= 1;
                            inventory.slots[slot].slot.GetComponent<InventorySlot>().amount -= 1;
                            storage.GetComponent<StorageInventory>().dictionary.Add(inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name, 1);
                        }
                    }
                    else
                    {
                        slot += 1;
                    }

                }
                else
                {
                    slot = 0;
                }


            }
            else
            {
                slot = 0;
            }
            if (allSlots <= 0 && amountProduced == item.stackAmount)
            {
                Debug.Log("Clear crafting");
                slot = 0;
                item.amountConsumed = null;
                item.amountProduced = 0;
                item.craftTime = 0;
                item.itemName = null;
                item.itemsConsumed = null;
                item.stackAmount = 0;
               
                whatSlot = 0;
                materialCheckAmount = 0;
                devlierMaterialCheckAmount = 0;
                whatSlot = 0;
                materialsNeeded.Clear();
                materialAmountNeed.Clear();
                deliverTime = 1;
                amountProduced = 0;
                craftButtonBool = false;
                craft = false;
                deliever = false;
                if (queue.Count > 0)
                {
                    queueNextItem();
                }
            }
        }
    }
   public void queueNextItem()
    {
        
            Debug.Log("queueItems");

            item = new Item(queue[0].itemName, queue[0].amountProduced, queue[0].craftTime, queue[0].itemsConsumed, queue[0].amountConsumed, queue[0].stackAmount);

            CraftItem();

            queue.RemoveAt(0);

        
    }
    public void CraftItem()
    {
        amountCheck = new List<int>(new int[item.amountConsumed.Count]);

        craftButtonBool = true;
    }

    void collectSupplies(List<string> materials, List<int> materialAmount)
    {

        materialsNeeded = materials;
        if (amountCheck[materialCheckAmount] < materialAmount[materialCheckAmount] && !delivering)
        {
            materialAmountNeed = new List<int>(new int[materialAmount.Count]);

            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(player.transform.position, storage.transform.position, NavMesh.AllAreas, path);
            player.GetComponent<NavMeshAgent>().path = path;
         
        }
        if (delivering)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(player.transform.position, craftingPosition.position, NavMesh.AllAreas, path);
            player.GetComponent<NavMeshAgent>().path = path;
        }
        if (!delivering && materialAmountNeed[materialAmountNeed.Count - 1] < (item.amountConsumed[item.amountConsumed.Count - 1] * item.stackAmount))
        {
            craftingTime = item.craftTime;
            deliverTime -= Time.deltaTime;
            if (deliverTime <= 0)
            {
                if (Vector3.Distance(player.transform.position, storage.transform.position) < 3)
                {
                    if (storage.GetComponent<StorageInventory>().dictionary[materials[materialCheckAmount]] >= materialAmount[materialCheckAmount])
                    {
                        if (slot < inventory.slots.Count)
                        {
                            if (materialCheckAmount < materialAmount.Count)
                            {
                                if (amountCheck[materialCheckAmount] < (materialAmount[materialCheckAmount] * item.stackAmount) && inventory.slots[inventory.slots.Count - 1].slotFull == false)
                                {
                                    if (inventory.slots[slot].slot.GetComponent<InventorySlot>().amount < inventory.slots[slot].slot.GetComponent<InventorySlot>().capacity)
                                    {
                                        if (inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name == "" || inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name == null)
                                        {
                                            inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name = materials[materialCheckAmount];
                                            inventory.slots[slot].slot.GetComponent<InventorySlot>().amount += 1;
                                            storage.GetComponent<StorageInventory>().dictionary[materials[materialCheckAmount]] -= 1;
                                            allSlots += 1;
                                            amountCheck[materialCheckAmount] += 1;
                                            deliverTime += 1;
                                        }
                                        else if (inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name == materials[materialCheckAmount])
                                        {
                                            inventory.slots[slot].slot.GetComponent<InventorySlot>().amount += 1;
                                            storage.GetComponent<StorageInventory>().dictionary[materials[materialCheckAmount]] -= 1;
                                            allSlots += 1;
                                            amountCheck[materialCheckAmount] += 1;
                                            deliverTime += 1;
                                        }
                                        else
                                        {
                                            slot += 1;
                                        }
                                    }
                                    else if (inventory.slots[slot].slot.GetComponent<InventorySlot>().amount >= inventory.slots[slot].slot.GetComponent<InventorySlot>().capacity)
                                    {
                                        slot += 1;
                                    }
                                }
                                if (amountCheck[materialCheckAmount] == (materialAmount[materialCheckAmount] * item.stackAmount) || inventory.slots[inventory.slots.Count - 1].slotFull == true)
                                {
                                    slot = 0;
                                    delivering = true;
                                }

                            }

                        }
                        else if (inventory.slots[inventory.slots.Count - 1].slotFull == false)
                        {
                            slot = 0;
                        }
                    }
                }
            }
        }
        if (delivering)
        {
            deliverTime -= Time.deltaTime;
            if (deliverTime <= 0)
            {
                if (Vector3.Distance(player.transform.position, craftingPosition.transform.position) < 3)
                {

                    if (slot < inventory.slots.Count)
                    {
                        if (devlierMaterialCheckAmount < materialAmount.Count)
                        {
                            if (inventory.slots[slot].slot.GetComponent<InventorySlot>().amount > 0)
                            {

                                Debug.Log("Deilvering " + item.itemsConsumed[devlierMaterialCheckAmount]);

                                if (inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name == materials[devlierMaterialCheckAmount])
                                {
                                    allSlots -= 1;
                                    materialAmountNeed[devlierMaterialCheckAmount] += 1;
                                    inventory.slots[slot].slot.GetComponent<InventorySlot>().amount -= 1;
                                    deliverTime += 1;
                                }
                                else
                                {
                                    slot += 1;
                                }

                            }
                            else
                            {
                                inventory.slots[slot].slot.GetComponent<InventorySlot>().occupied_Name = null;
                                slot += 1;
                            }

                        }
                        else
                        {
                            devlierMaterialCheckAmount = 0;
                        }


                    }
                    else
                    {
                        slot = 0;
                    }
                }
            }
        }
        
        if(allSlots == 0 && materialAmountNeed[materialCheckAmount] == (item.amountConsumed[materialCheckAmount] * item.stackAmount) &&  materialCheckAmount < materialAmountNeed.Count - 1)
        {
            delivering = false;
            devlierMaterialCheckAmount += 1;
            materialCheckAmount += 1;
        }

        if (materialAmountNeed[materialAmountNeed.Count - 1] == (item.amountConsumed[item.amountConsumed.Count - 1] * item.stackAmount))
        {
            delivering = false;
            Craft(item.craftTime);
        }

    }

    void Craft(float craftTime)
    {

        if (Vector3.Distance(player.transform.position, craftingPosition.position) < 3)
        {
            craftingTime -= Time.deltaTime;
            if (amountProduced < item.stackAmount)
            {
                slot = 0;
                if (craftingTime <= 0)
                {
                    if (whatSlot < inventory.slots.Count)
                    {

                        if (allSlots < item.amountProduced * item.stackAmount)
                        {
                            if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount < inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                            {

                                if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == "" || inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == null)
                                {
                                    Debug.Log("New slot");
                                    overLimit = 0;
                                    inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name = item.itemName;
                                    inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += item.amountProduced;
                                    allSlots += item.amountProduced;
                                    craftingTime = craftTime;
                                    amountProduced += 1;

                                }
                                else if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == item.itemName && inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount + item.amountProduced <= inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                                {
                                    Debug.Log("filling existing slot");
                                    overLimit = 0;
                                    inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += item.amountProduced;
                                    allSlots += item.amountProduced;
                                    craftingTime = craftTime;
                                    amountProduced += 1;

                                }
                                else 
                                {
                                    if (whatSlot < inventory.slots.Count - 1)
                                    {
                                        if (overLimit < item.amountProduced)
                                        {
                                            Debug.Log("overflow");
                                            allSlots += 1;

                                            inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += 1;
                                            overLimit += 1;
                                            if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount >= inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                                            {

                                                whatSlot += 1;
                                                inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name = item.itemName;
                                                inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += item.amountProduced - overLimit;
                                                allSlots += item.amountProduced - overLimit;
                                                amountProduced += 1;
                                                craftingTime = craftTime;


                                            }
                                        }
                                        else
                                        {
                                            overLimit = 0;
                                        }
                                    }
                                    else
                                    {
                                        deliever = true;
                                    }


                                }
                                
                            }
                            else if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount >= inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                            {
                                Debug.Log("changing Slot after slot is full");
                                whatSlot += 1;
                            }
                        }
                    }
                    else if (inventory.slots[inventory.slots.Count - 1].slotFull == false)
                    {
                        whatSlot = 0;
                    }

                }


            }
        }
       
        if (amountProduced >= item.stackAmount)
        {

            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(player.transform.position, storage.transform.position, NavMesh.AllAreas, path);
            player.GetComponent<NavMeshAgent>().path = path;
            deliever = true;
            
        }
       
           
        
        /* if (!deliever)
         {
             craftingItem = true;
             if(!collectedItems)
             {


                     if (storage.GetComponent<StorageInventory>().dictionary[item.itemsConsumed[whatMat]] >= item.amountConsumed[whatMat])
                     {

                         if (Vector3.Distance(player.transform.position, storage.transform.position) < 3)
                         {
                             if (whatSlot < inventory.slots.Count - 1)
                             {
                                 if (whatMat <= item.itemsConsumed.Count - 1)
                                 {
                                     if (amountCheck[whatMat] < item.amountConsumed[whatMat])
                                     {
                                     Debug.Log("Amount Check is less then " + item.amountConsumed[whatMat]);
                                     if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount < inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                                         {
                                         Debug.Log(item.itemsConsumed[whatMat]);

                                         if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == "")
                                             {
                                                 inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name = item.itemsConsumed[whatMat];
                                                 inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += 1;
                                                 storage.GetComponent<StorageInventory>().dictionary[item.itemsConsumed[whatMat]] -= 1;
                                                 allSlots += 1;
                                                 amountCheck[whatMat] += 1;
                                                 craft = false;
                                             }
                                             else if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == item.itemsConsumed[whatMat])
                                             {
                                                 inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += 1;
                                                 storage.GetComponent<StorageInventory>().dictionary[item.itemsConsumed[whatMat]] -= 1;
                                                 allSlots += 1;
                                                 amountCheck[whatMat] += 1;
                                                 craft = false;
                                             }
                                             else
                                             {
                                                 whatSlot += 1;
                                             }
                                         }
                                         else
                                         {
                                             whatSlot += 1;
                                         }
                                     }
                                     else
                                     {
                                     deliverCollectedItems = true;

                                     whatMat += 1;
                                     }
                                 }
                                 else
                                 {
                                     whatSlot = 0;
                                 }
                             }
                             else
                             {
                             deliverCollectedItems = true;

                             craftingItem = false;
                                 craft = false;
                                 deliever = true;

                             }
                         }

                 }

                 if (deliverCollectedItems)
                 {
                     NavMeshPath path = new NavMeshPath();
                     NavMesh.CalculatePath(player.transform.position, craftingPosition.position, NavMesh.AllAreas, path);
                     player.GetComponent<NavMeshAgent>().path = path;
                     if (Vector3.Distance(player.transform.position, craftingPosition.position) < 1)
                     {
                         if (whatSlot < player.GetComponent<Inventory>().slots.Count)
                         {

                             if (player.GetComponent<Inventory>().slots[whatSlot].slot.GetComponent<InventorySlot>().amount > 0)
                             {
                                 if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == item.itemsConsumed[whatMat])
                                 {

                                     player.GetComponent<Inventory>().slots[whatSlot].slot.GetComponent<InventorySlot>().amount -= 1;
                                     allSlots -= 1;
                                 }

                             }
                             else
                             {
                                 player.GetComponent<Inventory>().slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name = null;
                                 whatSlot += 1;

                             }

                         }

                         if (allSlots <= 0)
                         {
                             deliverCollectedItems = false;
                             collectedItems = true;
                         }
                     }

                 }
             }



             if (item.stackAmount > 0)
             {

                 if (Vector3.Distance(player.transform.position, craftingPosition.position) < 1)
                 {
                     if (whatSlot < inventory.slots.Count - 1)
                     {
                         if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount < inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                         {


                             if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == "" || inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == null)
                             {
                                 amountProduced = 0;
                                 inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name = item.itemName;
                                 inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += item.amountProduced;
                                 allSlots += item.amountProduced;
                                 item.stackAmount -= 1;
                             }
                             else if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().occupied_Name == item.itemName && inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount + item.amountProduced <= inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                             {
                                 inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += item.amountProduced;
                                 allSlots += item.amountProduced;
                                 item.stackAmount -= 1;
                             }
                             else if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount < inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)
                             {
                                 if (amountProduced < item.amountProduced)
                                 {
                                     inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount += 1;
                                     amountProduced += 1;
                                     if (inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().amount >= inventory.slots[whatSlot].slot.GetComponent<InventorySlot>().capacity)

                                     {
                                         whatSlot += 1;
                                     }
                                 }


                             }
                             else
                             {
                                 whatSlot += 1;
                             }
                         }
                         else
                         {
                             whatSlot += 1;
                         }
                     }

                 }

             }
         }*/
    }
}
