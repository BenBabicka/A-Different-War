using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class PlayerSave : MonoBehaviour {

    public Vector3 position;
    public Vector3 rotation;
    public Vector3 waypoint;
    public int headIndex;
    public int bodyIndex;
    public int hairIndex;
    public string unitName;

    public bool pickedUp;
    public int weaponID;

    public int playerID;

    [HideInInspector]
    public List<GameObject> allWeaponsSpawnedIn;

    //Players Jobs
    public List<bool> jobBools;
    public WeaponSpawner weaponSpawner;

    public float amountOfSlots;


    public List<playersInventory> player_Inventories;
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
        weaponSpawner = FindObjectOfType<WeaponSpawner>();
    }
    void Update()
    {
        if(!weaponSpawner)
        {
            weaponSpawner = FindObjectOfType<WeaponSpawner>();
        }
        amountOfSlots = gameObject.GetComponent<Inventory>().amountOfSlots;

    }

    public void UpdateInformation()
    {
        player_Inventories.Clear();
        position = transform.position;
        rotation = transform.eulerAngles;
        waypoint = gameObject.GetComponent<NavMeshAgent>().destination;
        headIndex = transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<SpritePicker>().spriteindex;
        bodyIndex = transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<SpritePicker>().spriteindex;
        hairIndex = transform.GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<SpritePicker>().spriteindex;
        pickedUp = gameObject.GetComponent<ItemPickUp>().pickedUp;
        amountOfSlots = gameObject.GetComponent<Inventory>().amountOfSlots;
        foreach (var slot in gameObject.GetComponent<Inventory>().slots)
        {
            player_Inventories.Add(new playersInventory(slot.slot_id, slot.slot.GetComponent<InventorySlot>().amount, slot.slot.GetComponent<InventorySlot>().occupied_Name, slot.slotFull));
        }
        if (jobBools.Count < 10)
        {
            jobBools.Add(gameObject.GetComponent<JobManager>().builder);
            jobBools.Add(gameObject.GetComponent<JobManager>().Medic);
            jobBools.Add(gameObject.GetComponent<JobManager>().Farmer);
            jobBools.Add(gameObject.GetComponent<JobManager>().Lumber);
            jobBools.Add(gameObject.GetComponent<JobManager>().Miner);
            jobBools.Add(gameObject.GetComponent<JobManager>().hunter);
            jobBools.Add(gameObject.GetComponent<JobManager>().cooker);
            jobBools.Add(gameObject.GetComponent<JobManager>().weaver);
            jobBools.Add(gameObject.GetComponent<JobManager>().researcher);
            jobBools.Add(gameObject.GetComponent<JobManager>().crafter);
        }
        else
        { 
            jobBools[0] = gameObject.GetComponent<JobManager>().builder;
            jobBools[1] = gameObject.GetComponent<JobManager>().Medic;
            jobBools[2] = gameObject.GetComponent<JobManager>().Farmer;
            jobBools[3] = gameObject.GetComponent<JobManager>().Lumber;
            jobBools[4] = gameObject.GetComponent<JobManager>().Miner;
            jobBools[5] = gameObject.GetComponent<JobManager>().hunter;
            jobBools[6] = gameObject.GetComponent<JobManager>().cooker;
            jobBools[7] = gameObject.GetComponent<JobManager>().weaver;
            jobBools[8] = gameObject.GetComponent<JobManager>().researcher;
            jobBools[9] = gameObject.GetComponent<JobManager>().crafter;
        }
        if (gameObject.GetComponent<ItemPickUp>().pickedUp)
        {
            weaponID = gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponData>().spawn_ID;
        }
        else
        {
            weaponID = 0;
        }
        unitName = transform.name;
    }

    public IEnumerator LoadInformantion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = position;
        transform.eulerAngles = rotation;
        gameObject.GetComponent<NavMeshAgent>().destination = waypoint ;

        transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<SpritePicker>().spriteindex = headIndex;
        transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<SpritePicker>().spriteindex = bodyIndex;
        transform.GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<SpritePicker>().spriteindex = hairIndex;
       
            transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<SpritePicker>().load = true;
            transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<SpritePicker>().loaded = false;
            StartCoroutine( transform.GetChild(0).GetChild(2).GetChild(1).GetChild(1).GetComponent<SpritePicker>().loadSprite(.15f));
        
       
            transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<SpritePicker>().load = true;
            transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<SpritePicker>().loaded = false;
            StartCoroutine( transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetComponent<SpritePicker>().loadSprite(.15f));
      
       
            transform.GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<SpritePicker>().load = true;
            transform.GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<SpritePicker>().loaded = false;
            StartCoroutine( transform.GetChild(0).GetChild(2).GetChild(1).GetChild(2).GetComponent<SpritePicker>().loadSprite(.15f));
        

        transform.name = unitName;
        for (int i = 0; i < player_Inventories.Count; i++)
        {
            gameObject.GetComponent<Inventory>().amountOfSlots = (int)amountOfSlots;
            foreach (var slot in gameObject.GetComponent<Inventory>().slots)
            {

                if (slot.slot_id == player_Inventories[i].slot_id)
                {
                    slot.slot.GetComponent<InventorySlot>().amount = player_Inventories[i].amountInInventorySlot;
                    slot.slot.GetComponent<InventorySlot>().occupied_Name = player_Inventories[i].itemInInventorySlot;
                    slot.slot.GetComponent<InventorySlot>().slotIsFull = player_Inventories[i].slotIsFull;
                }
            }
        }
        gameObject.GetComponent<IdelManager>().idleTime = UnityEngine.Random.Range(10,20);           

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().BuilderToggle.isOn = jobBools[0];
        gameObject.GetComponent<JobManager>().builder = jobBools[0];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().MedicToggle.isOn = jobBools[1];
        gameObject.GetComponent<JobManager>().Medic = jobBools[1];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().FarmToggle.isOn = jobBools[2];
        gameObject.GetComponent<JobManager>().Farmer = jobBools[2];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().LumberToggle.isOn = jobBools[3];
        gameObject.GetComponent<JobManager>().Lumber = jobBools[3];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().MinerToggle.isOn = jobBools[4];
        gameObject.GetComponent<JobManager>().Miner = jobBools[4];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().HunterToggle.isOn = jobBools[5];
        gameObject.GetComponent<JobManager>().hunter = jobBools[5];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().cookerToggle.isOn = jobBools[6];
        gameObject.GetComponent<JobManager>().cooker = jobBools[6];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().weaverToggle.isOn = jobBools[7];
        gameObject.GetComponent<JobManager>().weaver = jobBools[7];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().researchToggle.isOn = jobBools[8];
        gameObject.GetComponent<JobManager>().researcher = jobBools[8];

        gameObject.GetComponent<UnitInfomation>().spawnedInJobPanel.GetComponent<Jobs>().crafterToggle.isOn = jobBools[9];
        gameObject.GetComponent<JobManager>().crafter = jobBools[9];

        
        yield return new WaitForSeconds(.75f);

        allWeaponsSpawnedIn = weaponSpawner.spawnedInWeapons;

        gameObject.GetComponent<ItemPickUp>().pickedUp = pickedUp;
        if (pickedUp)
        {
            for (int i = 0; i < allWeaponsSpawnedIn.Count; i++)
            {
                if (allWeaponsSpawnedIn[i].GetComponent<WeaponData>().spawn_ID == weaponID)
                {
                    gameObject.GetComponent<ItemPickUp>().weapon = allWeaponsSpawnedIn[i];
                }
            }
        }
    
    }

}
