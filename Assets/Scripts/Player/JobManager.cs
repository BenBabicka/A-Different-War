using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class JobManager : MonoBehaviour
{
    public bool sleeping;
    public List<GameObject> slots;
    int whatslot;
    int allSlots;
    int whatmaterial;
    bool isBuilding;
    [Space]
    public bool builder;
    public bool Medic;
    public bool Farmer;
    public bool Lumber;
    public bool Miner;
    public bool hunter;
    public bool cooker;
    public bool weaver;
    public bool researcher;
    public bool crafter;
    [Space]
    public Vector3 center;
    public Vector3 size;
    public Vector3 pos;

    [Space]
    UnityEngine.AI.NavMeshAgent nav;
    SelectableUnitComponent unit;
    public float Wood;
    public float Textiles;
    public float Food;
    public float Metal;

    [Space]
    public float distance2;

    [Space]
    public GameObject closeBuilding;
    public GameObject closeFarm;
    public GameObject closeTree;
    public GameObject closeAnimal;
    public GameObject closeFood;
    public GameObject closeRock;
    public GameObject closeDeadAnimal;
    public GameObject closeCookingStation;
    public GameObject closeHuntingLodge;
    public GameObject closeResearchBench;
    public GameObject closeCraftingBench;
    public GameObject closeCottonPlant;
    public bool one;

    public float HealAmount;

    [Space]
    public List<GameObject> allUnits;
    public List<GameObject> deadAnimals;
    public List<GameObject> CookedFood;
    public List<GameObject> Foods;

    [Space]
    bool pickupAnimal;
    bool dropAnimal;
    bool canBuild;
    bool getingHealed;
    bool cooked;
    bool findNewIdlelpos;

    public bool injob;

    [Space]
    float dist = 2f;
    float time = 30;
    float time3 = 5;
    float tim;
    float findBuildTime = 5;
    float weavingTime = 7;
    float cutTime = 5;
    float woodCutTime = 5;
    float stoneMineTime = 5;
    float foodharvestTime = 1;
    float plantHarvestTime = 5;
    bool buildOne = true;

    public Sprite log_Sprite;
    public Sprite stone_Sprite;
    public Sprite orgin_Sprite;

    bool notCuttingForTheFirstTime;
    bool notMiningForTheFirstTime;
    bool notBuildingForTheFirstTime;

    public Vector3 tran;

    bool woodcut;
    bool stoneCut;
    bool suppliesGathered;
    bool build;
    bool foodHarvested;

    bool woodDevievled;
    bool stoneDevievled;
    bool suppliesDevievled;
    bool foodDevievled;

    bool ifNewTreeNeeded;
    bool ifNewStoneNeeded;

    float woodDeliverTime;
    float stoneDeliverTime;
    float supplyDeliverTime;
    float foodDeliverTime;

    bool canHarvest;

    float baseSpeed;

    public bool slot_001_full;
    public bool slot_002_full;
    public bool slot_003_full;
    public bool slot_004_full;
    public bool slot_005_full;

    public bool slot_001_filling;
    public bool slot_002_filling;
    public bool slot_003_filling;
    public bool slot_004_filling;
    public bool slot_005_filling;

    float deliverTime = 1;
    UnitXp xp;


    [HideInInspector]
    public List<GameObject> Benchs;

    public int index;
    float removeTime = 1;

    List<Transform> thingsInsideBuilding;
    public List<GameObject> allCottonPlants;


    public GameObject manager;
    // Use this for initialization
    void Start()
    {
        xp = gameObject.GetComponent<UnitXp>();
        nav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        unit = gameObject.GetComponent<SelectableUnitComponent>();
        baseSpeed = nav.speed;
        manager = GameObject.Find("Manager");
        if (closeBuilding == null)
        {
            closeBuilding = null;
        }
        if (closeFarm == null)
        {
            closeFarm = null;
        }
        center = gameObject.transform.position;

        randomPosition();


    }

    void randomPosition()
    {

        center = gameObject.transform.position;
        pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.1f, Random.Range(-size.z / 2, size.z / 2));

        float one111;
        one111 = Random.Range(1, 9);


        if (findNewIdlelpos)
        {
            time = 30;
            time3 = one111;
            findNewIdlelpos = false;
        }
    }

    public void FindBuildings()
    {
        if (manager.GetComponent<PriorityList>().buildingPriority.Count > 0)
        {

            if (manager.GetComponent<PriorityList>().buildingPriority == null)
                manager.GetComponent<PriorityList>().buildingPriority.RemoveAt(0);

            closeBuilding = manager.GetComponent<PriorityList>().buildingPriority[0];


        }
        else
        {
            for (var i = GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist.Count - 1; i > -1; i--)
            {
                if (GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist[i] == null)
                    GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist.RemoveAt(i);
            }
            GameObject closestBuilding = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestBuilding = go;
                    closeBuilding = closestBuilding;
                    distance = curDistance;

                }
            }
        }
    }

    public void FindTree()
    {

        if (manager.GetComponent<PriorityList>().treePriority.Count > 0)
        {

            if (manager.GetComponent<PriorityList>().treePriority == null)
                manager.GetComponent<PriorityList>().treePriority.RemoveAt(0);

            closeTree = manager.GetComponent<PriorityList>().treePriority[0];


        }
        else
        {
            GameObject[] Trees;
            Trees = GameObject.FindGameObjectsWithTag("Tree");
            GameObject closestBuilding = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in Trees)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestBuilding = go;
                    closeTree = closestBuilding;
                    distance = curDistance;

                }
            }
        }
    }
    public void FindCottonPlant()
    {


        allCottonPlants.AddRange(GameObject.FindGameObjectsWithTag("Cotton"));

        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in allCottonPlants)
        {
            if (go.GetComponent<PlantManager>().harvasted)
            {
                allCottonPlants.Remove(go);
            }
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestBuilding = go;
                closeCottonPlant = closestBuilding;
                distance = curDistance;

            }
        }
    }
    public void FindFarm()
    {


        GameObject[] Farms;
        Farms = GameObject.FindGameObjectsWithTag("Farm");
        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Farms)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestBuilding = go;
                closeFarm = closestBuilding;
                distance = curDistance;

            }
        }
    }
    public void FindCraftingBench()
    {


        GameObject[] craftingBnech;
        craftingBnech = GameObject.FindGameObjectsWithTag("CraftingBench");
        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in craftingBnech)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestBuilding = go;
                closeCraftingBench = closestBuilding;
                distance = curDistance;

            }
        }
    }
    public void FindResearchBench()
    {


        if (!Benchs.Contains(GameObject.FindGameObjectWithTag("Research")))
        {
            if (GameObject.FindGameObjectWithTag("Research").GetComponent<ResearchManager>().player == null)
            {
                Benchs.Add(GameObject.FindGameObjectWithTag("Research"));
            }
        }
        GameObject closestResearchBench = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Benchs)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestResearchBench = go;
                closeResearchBench = closestResearchBench;
                distance = curDistance;

            }
        }
    }
    public void FindAnimals()
    {


        GameObject[] Animals;
        Animals = GameObject.FindGameObjectsWithTag("Animal");
        GameObject closestAnimal = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Animals)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestAnimal = go;
                closeAnimal = closestAnimal;
                distance = curDistance;

            }
        }
    }
    public void FindCookingStation()
    {
        GameObject[] Trees;
        Trees = GameObject.FindGameObjectsWithTag("CookingStation");
        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Trees)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestBuilding = go;
                closeCookingStation = closestBuilding;
                distance = curDistance;

            }
        }
    }
    public void FindHuntingLog()
    {
        GameObject[] Trees;
        Trees = GameObject.FindGameObjectsWithTag("HuntingLodge");
        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Trees)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestBuilding = go;
                closeHuntingLodge = closestBuilding;
                distance = curDistance;

            }
        }
    }
    public void FindDeadAnimals()
    {


        GameObject closestAnimal = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in deadAnimals)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestAnimal = go;
                closeDeadAnimal = closestAnimal;
                distance = curDistance;
                cooked = false;

            }
        }
    }
    public void FindFood()
    {


        GameObject[] Trees;
        Trees = GameObject.FindGameObjectsWithTag("Food");
        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Trees)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestBuilding = go;
                closeTree = closestBuilding;
                distance = curDistance;
                NavMeshPath path = new NavMeshPath();

                NavMesh.CalculatePath(transform.position, closeFood.transform.position, NavMesh.AllAreas, path);
                nav.path = path;
            }
        }
    }

    public void FindRock()
    {
        if (manager.GetComponent<PriorityList>().minerPriority.Count > 0)
        {
            if (manager.GetComponent<PriorityList>().minerPriority == null)
                manager.GetComponent<PriorityList>().minerPriority.RemoveAt(0);

            closeRock = manager.GetComponent<PriorityList>().minerPriority[0];


        }
        else { 
        GameObject[] Rocks;
        Rocks = GameObject.FindGameObjectsWithTag("Rock");
        GameObject closestBuilding = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
            foreach (GameObject go in Rocks)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestBuilding = go;
                    closeRock = closestBuilding;
                    distance = curDistance;

                }
            }
        }
    }
    // Update is called once per frame

    void OnCollisonEnter(Collider col)
    {
        if (col.tag == "Path")
        {
            nav.speed = col.GetComponent<PathManager>().speed;
        }

    }
    void OnCollisonExit(Collider col)
    {
        if (col.tag == "Path")
        {
            nav.speed = baseSpeed;
        }

    }
    void Update()
    {
        if (sleeping)
        {
            return;
        }



        NavMeshPath path = new NavMeshPath();

        for (int i = 0; i < gameObject.GetComponent<Inventory>().slots.Count; i++)
        {
            if (!slots.Contains(gameObject.GetComponent<Inventory>().slots[i].slot))
            {
                slots.Add(gameObject.GetComponent<Inventory>().slots[i].slot);
            }
        }

        randomPosition();
        tran = gameObject.transform.position;
        if (builder)
        {
            Medic = false;
            Farmer = false;
            Lumber = false;
            Miner = false;

            if (gameObject.transform.position == tran)
            {
                FindBuildings();
            }
        }
        if (Medic)
        {
            Farmer = false;
            builder = false;
            Lumber = false;
            Miner = false;

        }
        if (Farmer)
        {
            builder = false;
            Medic = false;
            Miner = false;
            Lumber = false;

        }
        if (Lumber)
        {
            Farmer = false;
            Medic = false;
            builder = false;
            Miner = false;

        }
        if (Miner)
        {
            Farmer = false;
            builder = false;
            Medic = false;
            Lumber = false;

        }
        if (builder || Medic || Farmer || Lumber || Miner || hunter || cooker || weaver || researcher || crafter)
        {
            injob = true;
            gameObject.GetComponent<SelectableUnitComponent>().inJob = true;

        }
        else
        {
            injob = false;
            gameObject.GetComponent<SelectableUnitComponent>().inJob = false;

        }



        if (gameObject.GetComponent<CombatEnable>().Drafted == false)
        {

            #region mining
            if (Miner)
            {

                GameObject storage = GameObject.Find("Storage");
                if (!closeRock)
                {
                    FindRock();
                }

                if (closeRock)
                {

                    if (allSlots == 0)
                    {
                        suppliesDevievled = false;


                    }
                    if (gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().amount == 0 && suppliesDevievled == false)
                    {
                        allSlots = 0;
                        whatslot = 0;

                    }
                    if (allSlots == 0)
                    {


                        NavMesh.CalculatePath(transform.position, closeRock.transform.GetChild(1).position, NavMesh.AllAreas, path);
                        nav.path = path;


                    }



                    if (Vector3.Distance(closeRock.transform.GetChild(1).position, transform.position) < 3)
                    {
                        stoneMineTime -= Time.deltaTime * (1 + (xp.level / 100));
                        if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount < gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().capacity)
                        {
                            if (stoneMineTime <= 0)
                            {
                                closeRock.GetComponent<StoneManager>().cutProgress += 0.5f;

                                if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == null)
                                {
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name = closeRock.GetComponent<StoneManager>().WhatMat;
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;

                                }
                                else if(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == closeRock.GetComponent<StoneManager>().WhatMat)
                                {
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;

                                }
                                stoneMineTime = 5;
                            }
                        }
                        else
                        {
                            whatslot += 1;
                        }
                    }

                    if (gameObject.GetComponent<Inventory>().slots[gameObject.GetComponent<Inventory>().slots.Count - 1].slot.GetComponent<InventorySlot>().amount == gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().capacity)
                    {
                        suppliesDevievled = true;
                    }
                    if (suppliesDevievled)
                    {


                        NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                        if (gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().amount == gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().capacity)
                        {
                            whatslot = 0;
                        }


                        if (Vector3.Distance(gameObject.transform.position, storage.transform.position) < 3)
                        {
                            deliverTime -= Time.deltaTime;

                            if (deliverTime <= 0)
                            {
                                if (whatslot < gameObject.GetComponent<Inventory>().slots.Count)
                                {
                                    if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount > 0)
                                    {
                                        if (!storage.GetComponent<StorageInventory>().dictionary.ContainsKey(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name))
                                        {
                                            storage.GetComponent<StorageInventory>().dictionary.Add(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name, 1);
                                            gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount -= 1;
                                            allSlots -= 1;
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount -= 1;
                                            storage.GetComponent<StorageInventory>().dictionary[gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name] += 1;
                                            allSlots -= 1;

                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name = null;
                                        whatslot += 1;

                                    }
                                    deliverTime += 1;
                                }
                                else
                                {
                                    suppliesDevievled = false;
                                }
                            }
                        }


                    }
                }

               

            }
            else
            {
                notMiningForTheFirstTime = false;
                ifNewStoneNeeded = false;
                stoneDeliverTime = 1;
            }
            #endregion

            #region Cooking
            if (cooker)
            {

                GameObject storage = GameObject.Find("Storage");

                FindCookingStation();
                if (closeDeadAnimal == null)
                {
                    FindDeadAnimals();
                }
                if (closeCookingStation != null)
                {
                    if (closeDeadAnimal != null)
                    {

                        if (pickupAnimal == false)
                        {


                            NavMesh.CalculatePath(transform.position, closeDeadAnimal.transform.position, NavMesh.AllAreas, path);
                            nav.path = path;

                            if (Vector3.Distance(transform.position, closeDeadAnimal.transform.position) < 1)
                            {

                                pickupAnimal = true;
                                dropAnimal = false;

                            }
                        }

                        if (Vector3.Distance(transform.position, closeCookingStation.transform.position) < 2 && closeDeadAnimal.GetComponent<AnimalCooking>().progress <= 0)
                        {
                            closeDeadAnimal.transform.position = closeCookingStation.transform.position;
                            dropAnimal = true;
                            pickupAnimal = false;

                        }

                        if (closeDeadAnimal.GetComponent<AnimalCooking>().progress >= 100)
                        {
                            if (!cooked)
                            {
                                closeDeadAnimal.GetComponent<AnimalCooking>().progress = 100;


                                NavMesh.CalculatePath(transform.position, closeDeadAnimal.transform.position, NavMesh.AllAreas, path);
                                nav.path = path;
                                Debug.Log("Moving To Food");
                                if (Vector3.Distance(transform.position, closeDeadAnimal.transform.position) < 2)
                                {
                                    Debug.Log("Picking Up");
                                    pickupAnimal = true;
                                    dropAnimal = false;
                                }

                            }

                        }

                        if (Vector3.Distance(transform.position, storage.transform.position) < 2)
                        {
                            if (!cooked)
                            {
                                if (closeDeadAnimal.GetComponent<AnimalCooking>().progress >= 100 && pickupAnimal == true)
                                {
                                    Debug.Log("droping off food");
                                    dropAnimal = false;
                                    pickupAnimal = false;
                                    closeDeadAnimal.transform.position = storage.transform.GetChild(0).position;
                                    closeDeadAnimal.transform.rotation = storage.transform.GetChild(0).rotation;
                                    closeDeadAnimal.tag = "Food";
                                    deadAnimals.Remove(closeDeadAnimal);
                                    FindDeadAnimals();
                                    cooked = true;

                                }
                            }
                        }

                        if (pickupAnimal)
                        {
                            if (dropAnimal == false)
                            {
                                closeDeadAnimal.transform.position = gameObject.transform.GetChild(1).position;
                                closeDeadAnimal.transform.rotation = gameObject.transform.GetChild(1).rotation;
                                closeDeadAnimal.GetComponent<AnimalCooking>().cookingStation = closeCookingStation;
                                if (closeDeadAnimal.GetComponent<AnimalCooking>().progress <= 0)
                                {


                                    NavMesh.CalculatePath(transform.position, closeCookingStation.transform.position, NavMesh.AllAreas, path);
                                    nav.path = path;
                                }
                                if (closeDeadAnimal.GetComponent<AnimalCooking>().progress >= 100)
                                {


                                    NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                                    nav.path = path;
                                }
                            }
                        }
                    }

                }
            }
            #endregion

            #region Weaver
            if (weaver)
            {

                GameObject storage = GameObject.Find("Storage");
                if (!closeCottonPlant)
                {
                    FindCottonPlant();
                }
                if (closeCottonPlant.GetComponent<PlantManager>().harvasted)
                {
                    if (allSlots > 0)
                    {
                        suppliesDevievled = true;
                    }
                    if (allSlots <= 0 && suppliesDevievled == false)
                    {
                        closeCottonPlant = null;
                        FindCottonPlant();
                    }
                }
                if (closeCottonPlant)
                {

                    if (allSlots == 0)
                    {
                        suppliesDevievled = false;


                    }
                    if (gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().amount == 0 && suppliesDevievled == false)
                    {
                        allSlots = 0;
                        whatslot = 0;

                    }
                    if (allSlots == 0)
                    {


                        NavMesh.CalculatePath(transform.position, closeCottonPlant.transform.GetChild(1).position, NavMesh.AllAreas, path);
                        nav.path = path;


                    }



                    if (Vector3.Distance(closeCottonPlant.transform.GetChild(1).position, transform.position) < 3)
                    {
                        plantHarvestTime -= Time.deltaTime * (1 + (xp.level / 100));
                        if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount < gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().capacity)
                        {
                            if (plantHarvestTime <= 0)
                            {
                                closeCottonPlant.GetComponent<PlantManager>().cutProgress += 0.5f;

                                if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == null)
                                {
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name = "Cotton";
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;

                                }
                                else if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == "Cotton")
                                {
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;

                                }
                                plantHarvestTime = 5;
                            }
                        }
                        else
                        {
                            whatslot += 1;
                        }
                    }

                    if (gameObject.GetComponent<Inventory>().slots[gameObject.GetComponent<Inventory>().slots.Count - 1].slot.GetComponent<InventorySlot>().amount == gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().capacity)
                    {
                        suppliesDevievled = true;
                    }
                    if (suppliesDevievled)
                    {


                        NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                        if (gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().amount == gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().capacity)
                        {
                            whatslot = 0;
                        }


                        if (Vector3.Distance(gameObject.transform.position, storage.transform.position) < 3)
                        {
                            deliverTime -= Time.deltaTime;

                            if (deliverTime <= 0)
                            {
                                if (whatslot < gameObject.GetComponent<Inventory>().slots.Count)
                                {
                                    if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount > 0)
                                    {
                                        if (!storage.GetComponent<StorageInventory>().dictionary.ContainsKey(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name))
                                        {
                                            storage.GetComponent<StorageInventory>().dictionary.Add(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name, 1);
                                            gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount -= 1;
                                            allSlots -= 1;
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount -= 1;
                                            storage.GetComponent<StorageInventory>().dictionary[gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name] += 1;
                                            allSlots -= 1;

                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name = null;
                                        whatslot += 1;

                                    }
                                    deliverTime += 1;
                                }
                                else
                                {
                                    suppliesDevievled = false;
                                }
                            }
                        }


                    }



                }
            }
            #endregion

            #region researcher
            if (researcher)
            {

                if (closeResearchBench)
                {

                    NavMesh.CalculatePath(transform.position, closeResearchBench.GetComponent<ResearchManager>().researchPosition.position, NavMesh.AllAreas, path);
                    nav.path = path;

                    closeResearchBench.GetComponent<ResearchManager>().player = gameObject;

                }
                else
                {
                    FindResearchBench();
                }
            }
            #endregion

            #region Crafter
            if (crafter)
            {
                if (closeCraftingBench == null)
                {
                    FindCraftingBench();
                }
               else
                {
                    closeCraftingBench.GetComponent<craftingBench>().player = gameObject;
                }

            }
            else
            {
                if(closeCraftingBench)
                {
                    closeCraftingBench.GetComponent<craftingBench>().player = null;
                    closeCraftingBench = null;

                }

            }
            #endregion

            #region hunter
            if (hunter)
            {

                GameObject storage = GameObject.Find("Storage");

                if (gameObject.GetComponent<ItemPickUp>().weapon != null)
                {
                    if (closeAnimal == null)
                    {
                        FindAnimals();
                    }
                    if (closeHuntingLodge == null)
                    {
                        FindHuntingLog();
                    }
                    if (closeAnimal != null && closeHuntingLodge != null)
                    {


                        if (closeAnimal.GetComponent<Health>().health > 0)
                        {


                            NavMesh.CalculatePath(transform.position, closeAnimal.transform.position, NavMesh.AllAreas, path);
                            nav.path = path;
                            if (Vector3.Distance(transform.position, closeAnimal.transform.position) < gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponData>().range)
                            {
                                if (closeAnimal.GetComponent<Health>().health >= 1)
                                {
                                    nav.Stop();
                                    gameObject.GetComponent<Shooting>().AnimalAim();
                                }

                            }
                            else
                            {
                                nav.Resume();
                            }
                        }
                        if (closeAnimal.GetComponent<Health>().health <= 0)
                        {


                            NavMesh.CalculatePath(transform.position, closeHuntingLodge.transform.GetChild(0).position, NavMesh.AllAreas, path);
                            nav.path = path;


                            closeAnimal.transform.position = gameObject.transform.GetChild(1).position;
                            closeAnimal.transform.rotation = gameObject.transform.GetChild(1).rotation;


                            if (Vector3.Distance(transform.position, closeHuntingLodge.transform.GetChild(0).transform.position) <= 4)
                            {
                                closeAnimal.transform.position = closeHuntingLodge.transform.GetChild(1).position;
                                closeAnimal.transform.rotation = closeHuntingLodge.transform.GetChild(1).rotation;
                                cutTime -= Time.deltaTime;
                                if (cutTime <= 0)
                                {
                                    closeAnimal.transform.position = closeHuntingLodge.transform.GetChild(2).position;
                                    closeAnimal.transform.rotation = closeHuntingLodge.transform.GetChild(2).rotation;
                                    FindAnimals();
                                    cutTime = 5;

                                }
                            }
                        }
                    }
                }
                if (gameObject.GetComponent<ItemPickUp>().weapon == null)
                {
                    gameObject.GetComponent<ItemPickUp>().FindWeapons();
                }
            }
            #endregion

            #region lumber
            if (Lumber)
            {


                GameObject storage = GameObject.Find("Storage");
                if (!closeTree)
                {
                    FindTree();
                }
                if (closeTree.GetComponent<TreeManager>().cutProgress >= 100)
                {
                    if (allSlots > 0)
                    {
                        suppliesDevievled = true;
                    }
                    else
                    {
                        /* closeTree.transform.position = new Vector3(Random.Range(-closeTree.GetComponent<TreeManager>().Randomize.x / 2, closeTree.GetComponent<TreeManager>().Randomize.x / 2), 0.5f, Random.Range(-closeTree.GetComponent<TreeManager>().Randomize.z / 2, closeTree.GetComponent<TreeManager>().Randomize.z / 2));
                         closeTree.GetComponent<TreeManager>().size = 0.5f;
                         closeTree.GetComponent<TreeManager>().cutProgress = 0;*/
                        closeTree.SetActive(false);
                        FindTree();
                    }
                }
                if (closeTree)
                {

                    if (allSlots == 0)
                    {
                        suppliesDevievled = false;


                    }
                    if (gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().amount == 0 && suppliesDevievled == false)
                    {
                        allSlots = 0;
                        whatslot = 0;

                    }
                    if (allSlots == 0)
                    {


                        NavMesh.CalculatePath(transform.position, closeTree.transform.GetChild(1).position, NavMesh.AllAreas, path);
                        nav.path = path;


                    }



                    if (Vector3.Distance(closeTree.transform.GetChild(1).position, transform.position) < 3)
                    {
                        woodCutTime -= Time.deltaTime * (1 + (xp.level / 100));
                        if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount < gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().capacity)
                        {
                            if (woodCutTime <= 0)
                            {
                                closeTree.GetComponent<TreeManager>().cutProgress += 0.5f;

                                if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == null)
                                {
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name = "Wood";
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;

                                }
                                else if(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == "Wood")
                                {
                                    gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;

                                }
                                else
                                {
                                    whatslot += 1;
                                }
                                woodCutTime = 5;
                            }
                        }
                        else
                        {
                            whatslot += 1;
                        }
                    }

                    if (gameObject.GetComponent<Inventory>().slots[gameObject.GetComponent<Inventory>().slots.Count - 1].slot.GetComponent<InventorySlot>().amount == gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().capacity)
                    {
                        suppliesDevievled = true;
                    }
                    if (suppliesDevievled)
                    {


                        NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                        if (gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().amount == gameObject.GetComponent<Inventory>().slots[0].slot.GetComponent<InventorySlot>().capacity)
                        {
                            whatslot = 0;
                        }


                        if (Vector3.Distance(gameObject.transform.position, storage.transform.position) < 3)
                        {
                            deliverTime -= Time.deltaTime;

                            if (deliverTime <= 0)
                            {
                                if (whatslot < gameObject.GetComponent<Inventory>().slots.Count)
                                {
                                    if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount > 0)
                                    {
                                        if (!storage.GetComponent<StorageInventory>().dictionary.ContainsKey(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name))
                                        {
                                            storage.GetComponent<StorageInventory>().dictionary.Add(gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name, 1);
                                            gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount -= 1;
                                            allSlots -= 1;
                                        }
                                        else
                                        {
                                            gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount -= 1;
                                            storage.GetComponent<StorageInventory>().dictionary[gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name] += 1;
                                            allSlots -= 1;

                                        }
                                    }
                                    else
                                    {
                                        gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name = null;
                                        whatslot += 1;

                                    }
                                    deliverTime += 1;
                                }
                                else
                                {
                                    suppliesDevievled = false;
                                }
                            }
                        }


                    }



                }
               
            }
            else
            {
                notCuttingForTheFirstTime = false;
                ifNewTreeNeeded = false;
                woodDeliverTime = 1;
            }
            #endregion

            #region Find Objects
            findMedic();
            FindBuildings();
            if (Farmer)
            {
                float distance = Mathf.Infinity;
                Vector3 position = transform.position;
                GameObject[] Farm;
                Farm = GameObject.FindGameObjectsWithTag("Farm");
                GameObject closestFarm = null;

                foreach (GameObject go in Farm)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closestFarm = go;
                        closeFarm = closestFarm;
                        distance = curDistance;

                    }
                }
            }
            if (!allUnits.Contains(GameObject.FindGameObjectWithTag("Player")))
            {
                allUnits.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            }
            #endregion

            #region Builder
            if (builder)
            {

                if (closeBuilding == null)
                {
                    FindBuildings();
                    index = 0;
                    if (nav.destination != GameObject.Find("Storage").transform.position)
                    {
                        NavMesh.CalculatePath(transform.position, GameObject.Find("Storage").transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                    }
                }


                if (closeBuilding != null)
                {
                    thingsInsideBuilding = closeBuilding.GetComponent<Building>().insideGameobject;
                    if (closeBuilding.GetComponent<Building>().hasCheckForObjectsInside == true)
                    {
                        if (thingsInsideBuilding.Count > 0)
                        {
                            Debug.Log("BuilderTing");
                            if (index < thingsInsideBuilding.Count)
                            {
                                if (thingsInsideBuilding[index].gameObject.active == true)
                                {
                                    if (thingsInsideBuilding[index].GetComponent<Nature>().Name == "Grass" || thingsInsideBuilding[index].GetComponent<Nature>().Name == "SticksAndLeaves")
                                    {

                                        NavMesh.CalculatePath(transform.position, thingsInsideBuilding[index].position, NavMesh.AllAreas, path);
                                        nav.path = path;

                                        if (Vector3.Distance(transform.position, thingsInsideBuilding[index].transform.position) < 4)
                                        {
                                            removeTime -= Time.deltaTime;

                                            if (removeTime <= 0)
                                            {
                                                thingsInsideBuilding[index].gameObject.SetActive(false);
                                                index += 1;
                                                removeTime = 1;

                                            }
                                        }


                                    }
                                    else
                                    {
                                        if (thingsInsideBuilding[index].GetComponent<Nature>().Name == "Tree")
                                        {

                                            GameObject storage = GameObject.Find("Storage");

                                            if (thingsInsideBuilding[index].GetComponent<TreeManager>().cutProgress >= 100)
                                            {
                                                /*thingsInsideBuilding[index].transform.position = new Vector3(Random.Range(-thingsInsideBuilding[index].GetComponent<TreeManager>().Randomize.x / 2, thingsInsideBuilding[index].GetComponent<TreeManager>().Randomize.x / 2), 0.5f, Random.Range(-thingsInsideBuilding[index].GetComponent<TreeManager>().Randomize.z / 2, thingsInsideBuilding[index].GetComponent<TreeManager>().Randomize.z / 2));
                                                thingsInsideBuilding[index].GetComponent<TreeManager>().size = 0.5f;
                                                thingsInsideBuilding[index].GetComponent<TreeManager>().cutProgress = 0;*/
                                                closeTree.SetActive(false);

                                                index += 1;
                                            }
                                            else
                                            {

                                                if (slots[4].GetComponent<InventorySlot>().amount == 0)
                                                {
                                                    suppliesDevievled = false;


                                                }
                                                if (slots[0].GetComponent<InventorySlot>().amount == 0 && suppliesDevievled == false)
                                                {
                                                    allSlots = 0;
                                                    whatslot = 0;



                                                }
                                                if (suppliesDevievled == false && allSlots == 0)
                                                {

                                                    NavMesh.CalculatePath(transform.position, thingsInsideBuilding[index].GetChild(1).transform.position, NavMesh.AllAreas, path);
                                                    nav.path = path;
                                                }
                                                if (Vector3.Distance(thingsInsideBuilding[index].GetChild(1).transform.position, transform.position) < 4)
                                                {
                                                    woodCutTime -= Time.deltaTime * (1 + (xp.level / 5));
                                                    if (slots[whatslot].GetComponent<InventorySlot>().amount < slots[whatslot].GetComponent<InventorySlot>().capacity)
                                                    {
                                                        if (woodCutTime <= 0)
                                                        {
                                                            thingsInsideBuilding[index].GetComponent<TreeManager>().cutProgress += 0.5f;

                                                            if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name == null)
                                                            {
                                                                slots[whatslot].GetComponent<InventorySlot>().occupied_Name = "Wood";
                                                                slots[whatslot].GetComponent<InventorySlot>().amount += 1;
                                                                allSlots += 1;

                                                            }
                                                            else if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == "Wood")
                                                            {
                                                                gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                                                allSlots += 1;

                                                            }
                                                            else
                                                            {
                                                                whatslot += 1;
                                                            }
                                                            woodCutTime = 5;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        whatslot += 1;
                                                    }
                                                }

                                                if (slots[slots.Count - 1].GetComponent<InventorySlot>().amount == slots[slots.Count - 1].GetComponent<InventorySlot>().capacity)
                                                {
                                                    suppliesDevievled = true;
                                                }
                                                if (suppliesDevievled)
                                                {

                                                    Debug.Log("Go to Storage");
                                                    if (nav.destination != storage.transform.position)
                                                    {
                                                        NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                                                        nav.path = path;
                                                    }
                                                    if (slots[0].GetComponent<InventorySlot>().amount == slots[0].GetComponent<InventorySlot>().capacity)
                                                    {
                                                        whatslot = 0;
                                                    }


                                                    if (Vector3.Distance(gameObject.transform.position, storage.transform.position) < 3)
                                                    {
                                                        deliverTime -= Time.deltaTime;

                                                        if (deliverTime <= 0)
                                                        {
                                                            if (slots[whatslot].GetComponent<InventorySlot>().amount > 0)
                                                            {
                                                                if (!storage.GetComponent<StorageInventory>().dictionary.ContainsKey(slots[whatslot].GetComponent<InventorySlot>().occupied_Name))
                                                                {
                                                                    storage.GetComponent<StorageInventory>().dictionary.Add(slots[whatslot].GetComponent<InventorySlot>().occupied_Name, 1);
                                                                    slots[whatslot].GetComponent<InventorySlot>().amount -= 1;
                                                                }
                                                                else
                                                                {
                                                                    slots[whatslot].GetComponent<InventorySlot>().amount -= 1;
                                                                    storage.GetComponent<StorageInventory>().dictionary[slots[whatslot].GetComponent<InventorySlot>().occupied_Name] += 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                slots[whatslot].GetComponent<InventorySlot>().occupied_Name = null;
                                                                whatslot += 1;

                                                            }
                                                            deliverTime += 1;
                                                        }
                                                    }


                                                }


                                            }
                                        }

                                        if (thingsInsideBuilding[index].GetComponent<Nature>().Name == "Stone" || thingsInsideBuilding[index].GetComponent<Nature>().Name == "Coal")
                                        {

                                            GameObject storage = GameObject.Find("Storage");

                                            if (thingsInsideBuilding[index].GetComponent<StoneManager>().cutProgress >= 100)
                                            {
                                                index += 1;
                                            }

                                            foreach (var slot in slots)
                                            {
                                                if (slots[slots.Count - 1].GetComponent<InventorySlot>().amount == slots[slots.Count - 1].GetComponent<InventorySlot>().capacity)
                                                {
                                                    suppliesDevievled = false;


                                                }
                                                if (slots[0].GetComponent<InventorySlot>().amount == 0 && suppliesDevievled == false)
                                                {
                                                    allSlots = 0;
                                                    whatslot = 0;

                                                }
                                                if (allSlots == 0)
                                                {


                                                    NavMesh.CalculatePath(transform.position, thingsInsideBuilding[index].transform.GetChild(1).position, NavMesh.AllAreas, path);
                                                    nav.path = path;


                                                }


                                                if (Vector3.Distance(thingsInsideBuilding[index].transform.GetChild(1).position, transform.position) < 3)
                                                {
                                                    stoneMineTime -= Time.deltaTime * (1 + (xp.level / 100));
                                                    if (slots[whatslot].GetComponent<InventorySlot>().amount < slots[whatslot].GetComponent<InventorySlot>().capacity)
                                                    {
                                                        if (stoneMineTime <= 0)
                                                        {
                                                            thingsInsideBuilding[index].GetComponent<StoneManager>().cutProgress += 0.5f;

                                                            if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name == null)
                                                            {
                                                                slots[whatslot].GetComponent<InventorySlot>().occupied_Name = thingsInsideBuilding[index].GetComponent<StoneManager>().WhatMat;
                                                                slots[whatslot].GetComponent<InventorySlot>().amount += 1;
                                                                allSlots += 1;

                                                            }
                                                            else if (gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().occupied_Name == thingsInsideBuilding[index].GetComponent<StoneManager>().WhatMat)
                                                            {
                                                                gameObject.GetComponent<Inventory>().slots[whatslot].slot.GetComponent<InventorySlot>().amount += 1;
                                                                allSlots += 1;

                                                            }
                                                            else
                                                            {
                                                                whatslot += 1;
                                                            }
                                                            stoneMineTime = 25;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        whatslot += 1;
                                                    }
                                                }

                                                if (slots[slots.Count - 1].GetComponent<InventorySlot>().amount == slots[slots.Count - 1].GetComponent<InventorySlot>().capacity || thingsInsideBuilding[index].GetComponent<StoneManager>().cutProgress == 100)
                                                {
                                                    suppliesDevievled = true;
                                                }
                                                if (suppliesDevievled)
                                                {


                                                    NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                                                    nav.path = path;
                                                    if (slots[0].GetComponent<InventorySlot>().amount == slots[0].GetComponent<InventorySlot>().capacity)
                                                    {
                                                        whatslot = 0;
                                                    }


                                                    if (Vector3.Distance(gameObject.transform.position, storage.transform.position) < 3)
                                                    {
                                                        deliverTime -= Time.deltaTime;

                                                        if (deliverTime <= 0)
                                                        {
                                                            if (slots[whatslot].GetComponent<InventorySlot>().amount > 0)
                                                            {
                                                                if (!storage.GetComponent<StorageInventory>().dictionary.ContainsKey(slots[whatslot].GetComponent<InventorySlot>().occupied_Name))
                                                                {
                                                                    storage.GetComponent<StorageInventory>().dictionary.Add(slots[whatslot].GetComponent<InventorySlot>().occupied_Name, 1);
                                                                    slots[whatslot].GetComponent<InventorySlot>().amount -= 1;
                                                                }
                                                                else
                                                                {
                                                                    slots[whatslot].GetComponent<InventorySlot>().amount -= 1;
                                                                    storage.GetComponent<StorageInventory>().dictionary[slots[whatslot].GetComponent<InventorySlot>().occupied_Name] += 1;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                slots[whatslot].GetComponent<InventorySlot>().occupied_Name = null;
                                                                whatslot += 1;

                                                            }
                                                            deliverTime += 1;
                                                        }
                                                    }


                                                }
                                            }
                                        }

                                        if (thingsInsideBuilding[index].GetComponent<Nature>().Name == "Cotton")
                                        {

                                            if (thingsInsideBuilding[index].GetComponent<PlantManager>().cutProgress >= 100)
                                            {
                                                index += 1;
                                            }
                                            NavMesh.CalculatePath(transform.position, thingsInsideBuilding[index].transform.position, NavMesh.AllAreas, path);
                                            nav.path = path;


                                            if (Vector3.Distance(thingsInsideBuilding[index].transform.position, transform.position) < 3)
                                            {
                                                plantHarvestTime -= Time.deltaTime * (1 + (xp.level / 100));

                                                if (plantHarvestTime <= 0)
                                                {
                                                    thingsInsideBuilding[index].GetComponent<PlantManager>().cutProgress += 20f;


                                                    plantHarvestTime = 5;
                                                }

                                            }



                                        }
                                    }
                                }
                                else
                                {
                                    index += 1;
                                }
                            }



                        }
                        if (index >= closeBuilding.GetComponent<Building>().insideGameobject.Count)
                        {
                            if (closeBuilding.GetComponent<Building>().currentMaterialsUsedAmount.SequenceEqual(closeBuilding.GetComponent<Building>().materialAmount))
                            {

                                if (closeBuilding.GetComponent<Building>().buildProgress < 100)
                                {
                                    whatmaterial = 0;
                                    whatslot = 0;
                                    allSlots = 0;
                                    closeBuilding.GetComponent<Building>().build = true;
                                    closeBuilding.GetComponent<Building>().player = gameObject;
                                    suppliesDevievled = false;
                                   

                                    NavMesh.CalculatePath(transform.position, closeBuilding.transform.position, NavMesh.AllAreas, path);
                                    if (nav.path == path)
                                    {
                                        nav.path = path;
                                    }
                                }

                            }
                            else
                            {
                                GetSupplys();

                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            #endregion
            #region Farmer
            if (Farmer)
            {

                if (closeFarm)
                {
                    closeFarm.GetComponent<FarmManager>().player = gameObject;

                    if (closeFarm.GetComponent<FarmManager>().cropList.Count == 40)
                    {
                        canHarvest = true;

                    }

                    for (int i = 0; i < gameObject.GetComponent<Inventory>().slots.Count; i++)
                    {
                        if (gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().amount == 10)
                        {
                            gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().slotIsFull = true;
                        }

                    }
                   
                    if (slot_001_full && slot_002_full && slot_003_full && slot_004_full && slot_005_full)
                    {
                        canHarvest = false;
                        foodDevievled = false;
                        foodHarvested = true;


                        NavMesh.CalculatePath(transform.position, GameObject.Find("Storage").transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                    }
                        foreach (var item in closeFarm.GetComponent<FarmManager>().cropList)
                        {
                            if (item.GetComponent<Crop>().harvest == true)
                            {
                                foodharvestTime -= Time.deltaTime;
                                if (foodharvestTime <= 0)
                                {
                                    if (canHarvest)
                                    {
                                        for (int i = 0; i < gameObject.GetComponent<Inventory>().slots.Count;)
                                        {
                                            if (gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().occupied_Name == ((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString() || gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().occupied_Name == null)
                                            {
                                                NavMesh.CalculatePath(transform.position, item.transform.position, NavMesh.AllAreas, path);
                                                nav.path = path;


                                                if (gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().amount < gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().capacity)
                                                {

                                                    item.GetComponent<Crop>().harvested = true;
                                                    gameObject.GetComponent<Inventory>().slots[i].slot_sprite.GetComponent<Image>().sprite = log_Sprite;
                                                    gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().amount += 1;
                                                    gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().occupied_Name = ((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString();
                                                    xp.xp += 2;

                                                    foodharvestTime = 1;

                                                }
                                                else if (gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().occupied_Name == ((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString())
                                                {
                                                    gameObject.GetComponent<Inventory>().slots[i].slotFull = true;
                                                    i++;
                                                }


                                            }
                                        }



                                    }



                                }
                            }

                        }


                    }
                    if (foodHarvested)
                    {
                        if (!foodDevievled)
                        {


                            NavMesh.CalculatePath(transform.position, GameObject.Find("Storage").transform.position, NavMesh.AllAreas, path);
                            nav.path = path;
                        }
                        if (Vector3.Distance(transform.position, GameObject.Find("Storage").transform.position) < 2)
                        {

                            foodDeliverTime -= Time.deltaTime;

                            for (int i = 0; i < gameObject.GetComponent<Inventory>().slots.Count; i++)
                            {
                                if(gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().amount == 0)
                                {
                                    gameObject.GetComponent<Inventory>().slots[i].slotFull = false;
                                }

                                if(foodDeliverTime <= 0)
                                {
                                    foodDeliverTime = 1;
                                    if(gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().occupied_Name == ((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString())
                                    {
                                        if (GameObject.Find("Storage").GetComponent<StorageInventory>().dictionary.ContainsKey(((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString()))
                                        {

                                            GameObject.Find("Storage").GetComponent<StorageInventory>().dictionary[((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString()] += 1;
                                        }
                                        else
                                        {
                                            GameObject.Find("Storage").GetComponent<StorageInventory>().dictionary.Add(((crop)closeFarm.GetComponent<FarmManager>().CropType).ToString(), 1);
                                        }
                                        gameObject.GetComponent<Inventory>().gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().amount -= 1;
                                        if (gameObject.GetComponent<Inventory>().gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().amount <= 0)
                                        {
                                            gameObject.GetComponent<Inventory>().gameObject.GetComponent<Inventory>().slots[i].slot_sprite.GetComponent<Image>().sprite = orgin_Sprite;
                                            gameObject.GetComponent<Inventory>().gameObject.GetComponent<Inventory>().slots[i].slot.GetComponent<InventorySlot>().occupied_Name = "";
                                            gameObject.GetComponent<Inventory>().slots[i].slotFull = false;
                                            i++;
                                            //                        notCuttingForTheFirstTime = false;
                                        }
                                    }
                                }

                            }
                         
                        
                    }



                }
                else
                {
                    FindFarm();
                }
            }
            #endregion

        }
    }

    void findMedic()
    {
        if (Medic)
        {
            for (int i = 0; i < allUnits.Count; i++)
            {
                if (allUnits[i])
                {
                    if (allUnits[i].GetComponent<Health>().health <= 99.99f)
                    {
                        NavMeshPath path = new NavMeshPath();

                        NavMesh.CalculatePath(transform.position, allUnits[i].transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                        if (Vector3.Distance(transform.position, allUnits[i].transform.position) < dist)
                        {
                            allUnits[i].GetComponent<Health>().health += HealAmount / 10;
                            allUnits[i].GetComponent<JobManager>().getingHealed = true;
                            if (allUnits[i].GetComponent<Health>().health >= 99.99f)
                            {
                                allUnits[i].GetComponent<Health>().health = 100;
                                allUnits[i].GetComponent<JobManager>().getingHealed = false;
                                findMedic();
                            }
                        }
                    }
                }
            }
        }

    }

    public void GetSupplys()
    {
        GameObject storage = GameObject.Find("Storage");

        if (closeBuilding.GetComponent<Building>().currentMaterialsUsedAmount.SequenceEqual(closeBuilding.GetComponent<Building>().materialAmount))
        {
            if (closeBuilding.GetComponent<Building>().buildProgress < 100)
            {
               
                NavMeshPath path = new NavMeshPath();

                NavMesh.CalculatePath(transform.position, closeBuilding.transform.position, NavMesh.AllAreas, path);
                nav.path = path;
                closeBuilding.GetComponent<Building>().build = true;
                closeBuilding.GetComponent<Building>().player = gameObject;
                // whatmaterial = 0;
           
            }
            
        }
        else
        {





            if (suppliesDevievled == false)
            {
                if (allSlots + closeBuilding.GetComponent<Building>().currentMaterialsUsedAmount[whatmaterial] >= closeBuilding.GetComponent<Building>().materialAmount[whatmaterial])
                {
                    if (whatmaterial >= closeBuilding.GetComponent<Building>().materialAmount.Count - 1)
                    {
                        suppliesDevievled = true;
                        whatmaterial = 0;

                    }

                }
                if (slots[0].GetComponent<InventorySlot>().occupied_Name != closeBuilding.GetComponent<Building>().materials[whatmaterial])
                {
                    NavMeshPath path = new NavMeshPath();

                    NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                    nav.path = path;


                }



                if (allSlots + closeBuilding.GetComponent<Building>().currentMaterialsUsedAmount[whatmaterial] < closeBuilding.GetComponent<Building>().materialAmount[whatmaterial])
                {

                    if (Vector3.Distance(storage.transform.position, transform.position) < 2)
                    {
                        deliverTime -= Time.deltaTime;
                        if (deliverTime <= 0)
                        {


                            if (slots[whatslot].GetComponent<InventorySlot>().amount < slots[whatslot].GetComponent<InventorySlot>().capacity)
                            {
                                if (slots[whatslot].GetComponent<InventorySlot>().amount == 0)
                                {
                                    slots[whatslot].GetComponent<InventorySlot>().occupied_Name = closeBuilding.GetComponent<Building>().materials[whatmaterial];
                                    slots[whatslot].GetComponent<InventorySlot>().amount += 1;
                                    storage.GetComponent<StorageInventory>().dictionary[closeBuilding.GetComponent<Building>().materials[whatmaterial]] -= 1;
                                    allSlots += 1;
                                    deliverTime += 1;

                                }
                                else if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name == closeBuilding.GetComponent<Building>().materials[whatmaterial])
                                {
                                    slots[whatslot].GetComponent<InventorySlot>().amount += 1;
                                    allSlots += 1;
                                    storage.GetComponent<StorageInventory>().dictionary[closeBuilding.GetComponent<Building>().materials[whatmaterial]] -= 1;
                                    deliverTime += 1;

                                }
                                else if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name != closeBuilding.GetComponent<Building>().materials[whatmaterial] && slots[whatslot].GetComponent<InventorySlot>().occupied_Name != "" && slots[whatslot].GetComponent<InventorySlot>().amount > 0)

                                {

                                    whatslot += 1;
                                }
                                else if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name != closeBuilding.GetComponent<Building>().materials[whatmaterial] && slots[whatslot].GetComponent<InventorySlot>().occupied_Name != null && slots[whatslot].GetComponent<InventorySlot>().amount > 0)
                                {

                                    whatslot += 1;

                                }
                            }

                            else
                            {

                                whatslot += 1;
                            }
                        }
                    }


                }
                else if (allSlots + closeBuilding.GetComponent<Building>().currentMaterialsUsedAmount[whatmaterial] >= closeBuilding.GetComponent<Building>().materialAmount[whatmaterial])
                {

                    if (whatmaterial < closeBuilding.GetComponent<Building>().materialAmount.Count - 1)
                    {

                        whatmaterial += 1;
                        whatslot += 1;
                        allSlots = 0;
                    }
                }
                else
                {


                    suppliesDevievled = true;

                }




                if (slots[slots.Count - 1].GetComponent<InventorySlot>().slotIsFull == true)
                {
                    suppliesDevievled = true;

                }

            }
                if (suppliesDevievled)
                    {
                    if(slots[0].GetComponent<InventorySlot>().amount == slots[0].GetComponent<InventorySlot>().capacity)
                    {
                        whatslot = 0;
                        whatmaterial = 0;
                        deliverTime = 0;
                    }
                        NavMeshPath path = new NavMeshPath();

                        NavMesh.CalculatePath(transform.position, closeBuilding.transform.position, NavMesh.AllAreas, path);
                        nav.path = path;
                    if (Vector3.Distance(gameObject.transform.position, closeBuilding.transform.position) < 2)
                    {
                        if (whatslot < slots.Count)
                        {
                            deliverTime -= Time.deltaTime;
                            if (deliverTime <= 0)
                            {
                            if (slots[whatslot].GetComponent<InventorySlot>().amount > 0)
                            {
                                if (!closeBuilding.GetComponent<Building>().currentMaterialsUsed.Contains(closeBuilding.GetComponent<Building>().materials[whatmaterial]))
                                {
                                    closeBuilding.GetComponent<Building>().currentMaterialsUsed.Add(closeBuilding.GetComponent<Building>().materials[whatmaterial]);
                                }
                                if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name == closeBuilding.GetComponent<Building>().materials[whatmaterial] && slots[whatslot].GetComponent<InventorySlot>().occupied_Name != "")
                                {
                                    slots[whatslot].GetComponent<InventorySlot>().amount -= 1;
                                    closeBuilding.GetComponent<Building>().currentMaterialsUsedAmount[whatmaterial] += 1;
                                    allSlots -= 1;
                                    deliverTime += 1;
                                }
                                else if (slots[whatslot].GetComponent<InventorySlot>().occupied_Name != "" && slots[whatslot].GetComponent<InventorySlot>().occupied_Name != null)
                                    {
                                    whatmaterial += 1;
                                    }
                                    else
                                {
                                    whatslot += 1;

                                }
                            }
                                else
                                {
                                    slots[whatslot].GetComponent<InventorySlot>().occupied_Name = null;
                                    whatslot += 1;
                                    return;
                                }
                            
                        }
                    }
                        if(whatslot >= slots.Count)
                    {
                        suppliesDevievled = false;
                    }
                    




                }




            }
        }




    }



}