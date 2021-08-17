using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSaver : MonoBehaviour
{

    public int amount;

    public List<Vector3> positions;
    public List<Vector3> rotations;
    public List<Vector3> waypoints;
    public List<int> headIndexs;
    public List<int> bodyIndexs;
    public List<int> hairIndexs;
    public List<string> unitNames;

    public List<bool> weaponPickedUp;
    public List<int> weaponId;

    public List<GameObject> players;


     GameObject unit;

     GameObject unitButton;
     GameObject unitButtonPanel;

    public List<GameObject> unitButtons;
    public bool load;
    [Serializable]
    public class listofJobBools
    {
        public List<bool> jobBools;
        private List<listofJobBools> jobsBoolList;

        public listofJobBools(List<bool> jobBools)
        {
            this.jobBools = jobBools;
        }

       
    }
    public List<listofJobBools> JobsBoolList;

    public List<playerInventories> player_Inventories;
    [Serializable]
    public class playerInventories
    {
        public List<playersInventory> inventory;
        public playerInventories(List<playersInventory> inventories)
        {
            inventory = inventories;
        }
    }
    [Serializable]
    public class playersInventory
    {

        public int slot_id;
        public float amountInInventorySlot;
        public string itemInInventorySlot;
        public bool slotIsFull;
        public playersInventory(int id, float amo, string ite, bool full)
        {

            slot_id = id;
            amountInInventorySlot = amo;
            itemInInventorySlot = ite;
            slotIsFull = full;
        }
    }
    void Start()
    {
        unit = gameObject.GetComponent<UnitSpawner>().unit;
        unitButton = gameObject.GetComponent<UnitSpawner>().unitButton;
        unitButtonPanel = gameObject.GetComponent<UnitSpawner>().unitButtonPanel;
        StartCoroutine(lateStart());
    }
    IEnumerator lateStart()
    {
        yield return new WaitForSeconds(2f);
        UpdateInformation();
    }
    public void UpdateInformation()
    {
        positions.Clear();
        rotations.Clear();
        waypoints.Clear();
        headIndexs.Clear();
        bodyIndexs.Clear();
        hairIndexs.Clear();
        weaponId.Clear();
        weaponPickedUp.Clear();
        JobsBoolList.Clear();
        player_Inventories.Clear();
        foreach (var player in players)
        {
            player.GetComponent<PlayerSave>().UpdateInformation();
            if(!positions.Contains(player.GetComponent<PlayerSave>().position))
            {
                unitNames.Add(player.GetComponent<PlayerSave>().unitName);
                rotations.Add(player.GetComponent<PlayerSave>().rotation);
                waypoints.Add(player.GetComponent<PlayerSave>().waypoint);
                headIndexs.Add(player.GetComponent<PlayerSave>().headIndex);
                bodyIndexs.Add(player.GetComponent<PlayerSave>().bodyIndex);
                hairIndexs.Add(player.GetComponent<PlayerSave>().hairIndex);
                weaponId.Add(player.GetComponent<PlayerSave>().weaponID);
                weaponPickedUp.Add(player.GetComponent<PlayerSave>().pickedUp);
                playerInventories playerinv = new playerInventories(new List<playersInventory>());

                foreach (var slot in player.GetComponent<PlayerSave>().player_Inventories)
                {
                    if (!playerinv.inventory.Contains(new playersInventory(slot.slot_id, slot.amountInInventorySlot, slot.itemInInventorySlot, slot.slotIsFull)))
                    {
                        playerinv.inventory.Add(new playersInventory(slot.slot_id, slot.amountInInventorySlot, slot.itemInInventorySlot, slot.slotIsFull));
                    }
                }
                if(playerinv.inventory.Count == player.GetComponent<PlayerSave>().amountOfSlots)
                {
                    player_Inventories.Add(playerinv);
                }
                JobsBoolList.Add(new listofJobBools( player.GetComponent<PlayerSave>().jobBools));
                positions.Add(player.GetComponent<PlayerSave>().position);


            }
        }
        
        GameObject.Find("SaveManager").GetComponent<SaveManager>().UpdateUnitInformation();
    }
    void Update()
    {
        if(load)
        { 
       
                if (players.Count < amount)
                {
                    GameObject u = Instantiate(unit, new Vector3(0, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
                    u.SetActive(true);
                    u.GetComponent<SelectableUnitComponent>().canMove = true;
                    u.transform.SetParent(GameObject.Find("Player AI").transform, false);
                    GameObject unitbutton = Instantiate(unitButton, transform.position, transform.rotation);
                    players.Add(u);
                    unitButtons.Add(unitbutton);
                    unitbutton.SetActive(true);
                    unitbutton.transform.SetParent(unitButtonPanel.transform, false);
                    unitbutton.GetComponent<UnitFollowButton>().unit = u;
                    if (!GameObject.Find("Manager").GetComponent<UnitManager>().allUnits.Contains(u))
                    {
                        GameObject.Find("Manager").GetComponent<UnitManager>().allUnits.Add(u);
                    }
                    if (!GameObject.Find("Player").GetComponent<UnitSelectionComponent>().seletable.Contains(u))
                    {
                        GameObject.Find("Player").GetComponent<UnitSelectionComponent>().seletable.Add(u);
                    }

                    if (!Camera.main.GetComponent<RestartCamera>().units.Contains(u))
                    {
                        Camera.main.GetComponent<RestartCamera>().units.Add(u);
                    }


                    if (GameObject.Find("Ground"))
                    {
                        if (!GameObject.Find("Ground").GetComponent<NavMeshPathBuilder>().players.Contains(u))
                        {
                            GameObject.Find("Ground").GetComponent<NavMeshPathBuilder>().players.Add(u);
                        }
                    }
                    return;
                }
                else if (players.Count == amount)
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        players[i].GetComponent<PlayerSave>().position = positions[i];
                        players[i].GetComponent<PlayerSave>().rotation = rotations[i];
                        players[i].GetComponent<PlayerSave>().waypoint = waypoints[i];
                        players[i].GetComponent<PlayerSave>().headIndex = headIndexs[i];
                        players[i].GetComponent<PlayerSave>().bodyIndex = bodyIndexs[i];
                        players[i].GetComponent<PlayerSave>().hairIndex = hairIndexs[i];
                        players[i].GetComponent<PlayerSave>().unitName = unitNames[i];
                        players[i].GetComponent<PlayerSave>().weaponID = weaponId[i];
                        players[i].GetComponent<PlayerSave>().pickedUp = weaponPickedUp[i];
                        players[i].GetComponent<PlayerSave>().jobBools = JobsBoolList[i].jobBools;
                        for (int x = 0; x < player_Inventories[i].inventory.Count; x++)
                        {
                            if (!players[i].GetComponent<PlayerSave>().player_Inventories.Contains(new PlayerSave.playersInventory(player_Inventories[i].inventory[x].slot_id, player_Inventories[i].inventory[x].amountInInventorySlot, player_Inventories[i].inventory[x].itemInInventorySlot, player_Inventories[i].inventory[x].slotIsFull)))
                            {
                                if (players[i].GetComponent<PlayerSave>().player_Inventories.Count < players[i].GetComponent<PlayerSave>().amountOfSlots)
                                {
                                    players[i].GetComponent<PlayerSave>().player_Inventories.Add(new PlayerSave.playersInventory(player_Inventories[i].inventory[x].slot_id, player_Inventories[i].inventory[x].amountInInventorySlot, player_Inventories[i].inventory[x].itemInInventorySlot, player_Inventories[i].inventory[x].slotIsFull));
                                }
                            }
                        }
                            StartCoroutine(players[i].GetComponent<PlayerSave>().LoadInformantion(.5f));
                        
                    }
                
            }
        }
    }
    public IEnumerator LoadInformation(float waitTime)
    {
        load = true;
        yield return new WaitForSeconds(waitTime);
        load = false;
        
    }

}
