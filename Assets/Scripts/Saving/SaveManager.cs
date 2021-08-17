using Bayat.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bayat.SaveSystem.Demos;
using Bayat.Json.Utilities;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : DemoController
{

    float saveTime = 1;

    public string saveFileName;
    public GameObject mainPlayerCamera;
    public List<MapGenerationSave> mapGenerationSaveObjects = new List<MapGenerationSave>();
    public List<RockSaveManager> rockSaveManagers = new List<RockSaveManager>();
    public List<BuildingSave> buildingSave = new List<BuildingSave>();
    public UnitSaver unitSaver;
    public WeaponSave weapons_Save;
    bool loadedTime;
    public string loadFileName;
    bool newgame;
    bool actualload;
    public craftingBenchSave craftingBenchSave;
    public List<craftingBenchSave.CraftingItem> craftingItems = new List<craftingBenchSave.CraftingItem>();

    public bool loading;
    [System.Serializable]
    public class PlayerData
    {
        public terrain Terrain = new terrain();
        public treeManagerSaveClass treeManagerSave;
        public stoneManagers stoneManagers;
        public buildingSaves buildingSaves;
        public storageSaveClass storageSaveClass;
        public storageInformationClass storageInformation;
        public playerInformationClass playerInformation;
        public weaponsSave weaponssave;
        public gameData gamedata;
        public craftingBenchSaveData craftingItemSave;

    }

    [System.Serializable]
    public class gameData
    {
        public float timeOfDay;
        public float timeTillNextFall;
        public float rainTime;
        public gameData(float tod, float tnf, float rt)
        {
            timeOfDay = tod;
            timeTillNextFall = tnf;
            rainTime = rt;
        }
    }
    #region weapons
    [System.Serializable]
    public class weaponsSave
    {
        public float amount;


        public List<int> weapon_ids;

        public List<int> weapon_spawn_id;

        public List<Vector3> positions;
        public List<Vector3> rotations;
        public List<bool> playerTransforms;
        public List<int> playerids;
        public weaponsSave(float amo, List<int> wid, List<int> sid, List<Vector3> pos, List<Vector3> rot, List<bool> pts, List<int> ids)
        {
            amount = amo;
            weapon_ids = wid;
            weapon_spawn_id = sid;
            positions = pos;
            rotations = rot;
            playerTransforms = pts;
            playerids = ids;
        }
    }
    #endregion
    #region Player Stats
    [System.Serializable]
    public class playerInformationClass
    {
        public int amount;
        public List<Vector3> positions;
        public List<Vector3> Rotations;

        public List<Vector3> waypoint;

        public List<int> PlayerSpriteHeadIndex;
        public List<int> PlayerSpriteBodyIndex;
        public List<int> PlayerSpriteHairIndex;

        public List<string> unitNames;

        public List<bool> pickedUp;
        public List<int> weaponId;

        public List<UnitSaver.listofJobBools> listOfJobBools;
        public List<UnitSaver.playerInventories> inventories;
        public playerInformationClass(int amo, List<Vector3> pos, List<Vector3> rot, List<Vector3> way, List<int> Hea, List<int> bod, List<int> hai, List<string> nam, List<bool> pup, List<int> wid, List<UnitSaver.listofJobBools> ljb, List<UnitSaver.playerInventories> inv)
        {
            amount = amo;
            positions = pos;
            Rotations = rot;
            waypoint = way;
            PlayerSpriteHeadIndex = Hea;
            PlayerSpriteBodyIndex = bod;
            PlayerSpriteHairIndex = hai;
            unitNames = nam;
            pickedUp = pup;
            weaponId = wid;
            listOfJobBools = ljb;
            inventories = inv;
        }

    }

    #endregion
    #region buildings


    [System.Serializable]
    public class storageInformationClass
    {
        public Dictionary<string, float> storageInventory;
        public storageInformationClass(Dictionary<string, float> inv)
        {
            storageInventory = inv;
        }
    }

    [System.Serializable]
    public class storageSaveClass
    {
        public bool placedStorage;
        public storageSaveClass(bool pla)
        {
            placedStorage = pla;
        }
    }
    [System.Serializable]
    public class buildingSaves
    {
        public List<buildingSaveClass> buildingSaveClasses = new List<buildingSaveClass>();

    }

    [System.Serializable]
    public class buildingSaveClass
    {
        public string buildingName;
        public int amount;
        public List<Vector3> buildingPositions;
        public List<Vector3> buildingRotations;
        public List<float> buildingProgress;
        public List<bool> hasPlaced;
        public List<BuildingSave.buildingMaterials> usedBuildingMaterials;
        public buildingSaveClass(string nam, int amo, List<Vector3> pos, List<Vector3> rot, List<float> pro, List<bool> has, List<BuildingSave.buildingMaterials> UBM)
        {
            buildingName = nam;
            amount = amo;
            buildingPositions = pos;
            buildingRotations = rot;
            buildingProgress = pro;
            hasPlaced = has;
            usedBuildingMaterials = UBM;
        }
    }



    public List<buildingSaveClass> buildingSaveClasses = new List<buildingSaveClass>();
    #endregion
    #region Stone
    [System.Serializable]
    public class stoneManagers
    {
        public List<stoneManagerSaveClass> stoneManagerSaveClasses;
    }

    [System.Serializable]
    public class stoneManagerSaveClass
    {
        public string itemSavingName;
        public List<float> cutProgress;
        public stoneManagerSaveClass(List<float> cut, string nam)
        {
            cutProgress = cut;
            itemSavingName = nam;
        }
    }
    public List<stoneManagerSaveClass> rockClasses = new List<stoneManagerSaveClass>();
    #endregion
    #region Trees
    [System.Serializable]
    public class treeManagerSaveClass
    {
        public List<float> size;
        public List<float> cutProgress;
        public treeManagerSaveClass(List<float> siz, List<float> cut)
        {
            size = siz;

            cutProgress = cut;
        }
    }
    public TreeManagerSave treeManager;

    #endregion
    #region terrain
    [System.Serializable]
    public class terrain
    {
        public List<TerrainGeneratorClass> terrainGeneratorClass;
    }


    [System.Serializable]
    public class TerrainGeneratorClass
    {
        public string name;

        public int amount = 0;
        public List<int> spriteValue;
        public List<Vector3> itemPosition;
        public List<Vector3> itemSize;
        public List<bool> spriteFlip;
        public List<bool> disable;
        public TerrainGeneratorClass(string nam, int amo, List<int> spr, List<Vector3> tra, List<Vector3> siz, List<bool> fli, List<bool> dis)
        {
            name = nam;
            amount = amo;
            spriteValue = spr;
            itemPosition = tra;
            itemSize = siz;
            spriteFlip = fli;
            disable = dis;
        }
    }


    public List<TerrainGeneratorClass> terrains = new List<TerrainGeneratorClass>();

    #endregion
    #region crafting   
    [SerializeField]
    public List<CraftingSave> cbs;

    [System.Serializable]
    public class CraftingItem
    {
        public string itemName;

        public int amountProduced;
        public float craftTime;
        public List<string> itemsConsumed;
        public List<int> amountConsumed;

        public int stackAmount;

        public CraftingItem(string iN, int aP, float cT, List<string> iC, List<int> aC, int sA)
        {
            itemName = iN;
            amountProduced = aP;
            craftTime = cT;
            itemsConsumed = iC;
            amountConsumed = aC;
            stackAmount = sA;
        }
    }
    [System.Serializable]

    public class CraftingSave
    {
        public int craftBenchId;
        public string player;

        public int levelWorkBench;
        public CraftingItem craftingItem;
        public bool craft;
        public int whatSlot;
        public int allSlots;
        public bool deliever;
        public bool craftingItemBoolean;
        public List<int> amountCheck;
        public int amountProduced;
        public bool craftButtonBool;
        public List<string> materialsNeeded;
        public List<int> materialAmountNeed;
        public int materialCheckAmount;
        public int devlierMaterialCheckAmount;

        public float craftingTime;

        public int overLimit;


        public List<CraftingItem> queue;
        public CraftingSave(int cBI, string ply, int lwb, CraftingItem cI, bool c, int ws, int aS, bool d, bool cIB, List<int> aC, int ap, bool cbb, List<string> mN, List<int> mAN, int mCA, int dMCA, float cT, int oL, List<CraftingItem> lCI)
        {
            craftBenchId = cBI;
            player = ply;

            levelWorkBench = lwb;
            craftingItem = cI;
            craft = c;
            whatSlot = ws;
            allSlots = aS;
            deliever = d;
            craftingItemBoolean = cIB;
            amountCheck = aC;
            amountProduced = ap;
            craftButtonBool = cbb;
            materialsNeeded = mN;
            materialAmountNeed = mAN;
            materialCheckAmount = mCA;
            devlierMaterialCheckAmount = dMCA;

            craftingTime = cT;

            overLimit = oL;
            queue = lCI;
        }
    }
    [System.Serializable]

    public class craftingBenchSaveData
    {
        public List<CraftingSave> craftingSave;
        public craftingBenchSaveData(List<CraftingSave> cS)
        {
            craftingSave = cS;
        }
    }




    #endregion

    [SerializeField]
    protected PlayerData playerData = new PlayerData();
    [SerializeField]
    protected PlayerData defaultPlayerData = new PlayerData();

    [SerializeField]

    public override void Save()
    {
        if (SaveSystemAPI.ExistsAsync(this.saveFileName + ".sav") != null)
        {
            SaveSystemAPI.DeleteAsync(this.saveFileName + ".sav");
        }
        if (File.Exists(Application.persistentDataPath + "/" + this.saveFileName + ".sav"))
        {
            File.Delete(Application.persistentDataPath + "/" + this.saveFileName + ".sav");
        }
        unitSaver.UpdateInformation();
        weapons_Save.updateInformation();
        craftingBenchSave.updateInformation();
        StartCoroutine(treeManager.UpdateInformation(0.1f));
        foreach (var rock in rockSaveManagers)
        {
            rock.UpdateInformation();
        }
        foreach (var item in mapGenerationSaveObjects)
        {
            item.checkIfDisabled();
        }
        for (int i = 0; i < playerData.Terrain.terrainGeneratorClass.Count; i++)
        {


            playerData.Terrain.terrainGeneratorClass[i].disable = mapGenerationSaveObjects[i].disable;
        }

        foreach (var buildingsave in buildingSave)
        {
            buildingsave.UpdateBuildMaterials();
            buildingsave.UpdateBuildProgress();
        }


        if (GameObject.FindWithTag("Storage"))
        {

            playerData.storageInformation.storageInventory = GameObject.FindWithTag("Storage").GetComponent<StorageInventory>().dictionary;

        }
        playerData.gamedata.timeOfDay = mainPlayerCamera.GetComponent<DayNightCycle>().time;
        playerData.gamedata.timeTillNextFall = mainPlayerCamera.GetComponent<DayNightCycle>().timeTillNextFall;
        playerData.gamedata.rainTime = mainPlayerCamera.GetComponent<DayNightCycle>().rainTime;

        while (saveTime > 0)
        {
            saveTime -= Time.fixedDeltaTime;
        }
        if (saveTime < 0)
        {

            SaveSystemAPI.SaveAsync(this.saveFileName + ".sav", this.playerData);
            Debug.Log("Player data saved successfully");
            saveTime = 1;
        }

    }

    public void LoadGame(string fileName)
    {

        Time.timeScale = 1;
        if (GameObject.FindObjectOfType<LevelLoader>())
        {
            GameObject.FindObjectOfType<LevelLoader>().loadLevel(1);
        }
        else
        {

            SceneManager.LoadScene(1);
        }
        loadedTime = true;
        loading = true;
        loadFileName = fileName;



    }
    public IEnumerator loadGameWait()
    {
        Debug.Log("Start Loading");

        StartCoroutine(lateStart(1.5f));
        yield return new WaitForSeconds(3f);
        actualload = true;


    }
    public override async void Load(string fileName)
    {

        loadedTime = false;
        if (actualload)
        {
            if (!await SaveSystemAPI.ExistsAsync(fileName))
            {
                Debug.Log("Player data not found");
                Debug.Log("Using default player data instead");
                this.playerData = this.defaultPlayerData;
                return;
            }
            this.playerData = await SaveSystemAPI.LoadAsync<PlayerData>(fileName);

            unitSaver.amount = playerData.playerInformation.amount;
            unitSaver.positions = playerData.playerInformation.positions;
            unitSaver.rotations = playerData.playerInformation.Rotations;
            unitSaver.waypoints = playerData.playerInformation.waypoint;
            unitSaver.headIndexs = playerData.playerInformation.PlayerSpriteHeadIndex;
            unitSaver.bodyIndexs = playerData.playerInformation.PlayerSpriteBodyIndex;
            unitSaver.hairIndexs = playerData.playerInformation.PlayerSpriteHairIndex;
            unitSaver.unitNames = playerData.playerInformation.unitNames;
            unitSaver.weaponId = playerData.playerInformation.weaponId;
            unitSaver.weaponPickedUp = playerData.playerInformation.pickedUp;
            unitSaver.JobsBoolList = playerData.playerInformation.listOfJobBools;
            unitSaver.player_Inventories = playerData.playerInformation.inventories;
            StartCoroutine(unitSaver.LoadInformation(.1f));

            weapons_Save.amount = playerData.weaponssave.amount;
            weapons_Save.weapon_ids = playerData.weaponssave.weapon_ids;
            weapons_Save.weapon_spawn_id = playerData.weaponssave.weapon_spawn_id;
            weapons_Save.positions = playerData.weaponssave.positions;
            weapons_Save.rotations = playerData.weaponssave.rotations;
            weapons_Save.playersTransforms = playerData.weaponssave.playerTransforms;
            weapons_Save.playerIDs = playerData.weaponssave.playerids;
            weapons_Save.loadInformation();


            foreach (var item in playerData.craftingItemSave.craftingSave)
            {
                craftingItems = new List<craftingBenchSave.CraftingItem>();


                for (int i = 0; i < item.queue.Count; i++)
                {
                    if (!craftingItems.Contains(new craftingBenchSave.CraftingItem(item.queue[i].itemName, item.queue[i].amountProduced, item.queue[i].craftTime, item.queue[i].itemsConsumed, item.queue[i].amountConsumed, item.queue[i].stackAmount)))
                    {
                        craftingItems.Add(new craftingBenchSave.CraftingItem(item.queue[i].itemName, item.queue[i].amountProduced, item.queue[i].craftTime, item.queue[i].itemsConsumed, item.queue[i].amountConsumed, item.queue[i].stackAmount));
                    }
                }

                craftingBenchSave.benchSave.craftingSave = new List<craftingBenchSave.CraftingSave>(playerData.craftingItemSave.craftingSave.Count);

                if (item.queue.Count > 0)
                {
                    if (GameObject.FindObjectOfType<craftingBenchSave>())
                    {


                        if (!GameObject.FindObjectOfType<craftingBenchSave>().benchSave.craftingSave.Contains(new craftingBenchSave.CraftingSave(item.craftBenchId, item.player, item.levelWorkBench, new craftingBenchSave.CraftingItem(item.craftingItem.itemName, item.craftingItem.amountProduced, item.craftingItem.craftTime, item.craftingItem.itemsConsumed, item.craftingItem.amountConsumed, item.craftingItem.stackAmount), item.craft, item.whatSlot, item.allSlots, item.deliever, item.craftingItemBoolean, item.amountCheck, item.amountProduced, item.craftingItemBoolean, item.materialsNeeded, item.materialAmountNeed, item.materialCheckAmount, item.devlierMaterialCheckAmount, item.craftingTime, item.overLimit, craftingItems)))
                        {
                            GameObject.FindObjectOfType<craftingBenchSave>().benchSave.craftingSave.Add(new craftingBenchSave.CraftingSave(item.craftBenchId, item.player, item.levelWorkBench, new craftingBenchSave.CraftingItem(item.craftingItem.itemName, item.craftingItem.amountProduced, item.craftingItem.craftTime, item.craftingItem.itemsConsumed, item.craftingItem.amountConsumed, item.craftingItem.stackAmount), item.craft, item.whatSlot, item.allSlots, item.deliever, item.craftingItemBoolean, item.amountCheck, item.amountProduced, item.craftingItemBoolean, item.materialsNeeded, item.materialAmountNeed, item.materialCheckAmount, item.devlierMaterialCheckAmount, item.craftingTime, item.overLimit, craftingItems));
                        }
                    }
                }
                else
                {
                    if (GameObject.FindObjectOfType<craftingBenchSave>())
                    {
                        if (!GameObject.FindObjectOfType<craftingBenchSave>().benchSave.craftingSave.Contains(new craftingBenchSave.CraftingSave(item.craftBenchId, item.player, item.levelWorkBench, new craftingBenchSave.CraftingItem(item.craftingItem.itemName, item.craftingItem.amountProduced, item.craftingItem.craftTime, item.craftingItem.itemsConsumed, item.craftingItem.amountConsumed, item.craftingItem.stackAmount), item.craft, item.whatSlot, item.allSlots, item.deliever, item.craftingItemBoolean, item.amountCheck, item.amountProduced, item.craftingItemBoolean, item.materialsNeeded, item.materialAmountNeed, item.materialCheckAmount, item.devlierMaterialCheckAmount, item.craftingTime, item.overLimit, new List<craftingBenchSave.CraftingItem>())))
                        {
                            GameObject.FindObjectOfType<craftingBenchSave>().benchSave.craftingSave.Add(new craftingBenchSave.CraftingSave(item.craftBenchId, item.player, item.levelWorkBench, new craftingBenchSave.CraftingItem(item.craftingItem.itemName, item.craftingItem.amountProduced, item.craftingItem.craftTime, item.craftingItem.itemsConsumed, item.craftingItem.amountConsumed, item.craftingItem.stackAmount), item.craft, item.whatSlot, item.allSlots, item.deliever, item.craftingItemBoolean, item.amountCheck, item.amountProduced, item.craftingItemBoolean, item.materialsNeeded, item.materialAmountNeed, item.materialCheckAmount, item.devlierMaterialCheckAmount, item.craftingTime, item.overLimit, new List<craftingBenchSave.CraftingItem>()));
                        }
                    }
                }
            }



            foreach (var savedTarrain in playerData.Terrain.terrainGeneratorClass)
            {


                foreach (var mapGeneration in mapGenerationSaveObjects)
                {
                    if (mapGeneration.ItemingSaving == savedTarrain.name)
                    {
                        mapGeneration.itemAmount = savedTarrain.amount;
                        mapGeneration.spriteValue = savedTarrain.spriteValue;
                        mapGeneration.itemPosition = savedTarrain.itemPosition;
                        mapGeneration.itemSize = savedTarrain.itemSize;
                        mapGeneration.spriteFlip = savedTarrain.spriteFlip;
                        mapGeneration.disable = savedTarrain.disable;
                        mapGeneration.Load();
                    }
                }


            }

            foreach (var stoneManagerSave in playerData.stoneManagers.stoneManagerSaveClasses)
            {
                foreach (var rockSave in rockSaveManagers)
                {
                    if (rockSave.itemSaveName == stoneManagerSave.itemSavingName)
                    {
                        rockSave.cutProgesses = stoneManagerSave.cutProgress;
                        rockSave.LoadStoneData();
                    }
                }
            }
            GameObject.Find("StorageBuildManager").GetComponent<floorObjectPlacement>().placeOnceHasBeenPlaced = playerData.storageSaveClass.placedStorage;

            foreach (var buildingSaveClass1 in playerData.buildingSaves.buildingSaveClasses)
            {
                foreach (var buildingSave1 in buildingSave)
                {
                    if (buildingSave1.ItemingSaving == buildingSaveClass1.buildingName)
                    {

                        buildingSave1.buildingInfomation.amount = buildingSaveClass1.amount;
                        buildingSave1.buildingInfomation.buildingPositions = buildingSaveClass1.buildingPositions;
                        buildingSave1.buildingInfomation.buildingRotations = buildingSaveClass1.buildingRotations;
                        buildingSave1.buildingInfomation.buildProgress = buildingSaveClass1.buildingProgress;
                        buildingSave1.buildingInfomation.hasPlaced = buildingSaveClass1.hasPlaced;
                        StartCoroutine(buildingSave1.LoadBuildings(2));
                    }

                }
            }
            if (GameObject.FindWithTag("Storage"))
            {
                GameObject.FindWithTag("Storage").GetComponent<StorageData>().loading = true;
                GameObject.FindWithTag("Storage").GetComponent<StorageInventory>().dictionary = playerData.storageInformation.storageInventory;
            }

            treeManager.cutProgesses = playerData.treeManagerSave.cutProgress;
            treeManager.size = playerData.treeManagerSave.size;
            treeManager.LoadTreeData();

            mainPlayerCamera.GetComponent<DayNightCycle>().time = playerData.gamedata.timeOfDay;
            mainPlayerCamera.GetComponent<DayNightCycle>().timeTillNextFall = playerData.gamedata.timeTillNextFall;
            mainPlayerCamera.GetComponent<DayNightCycle>().rainTime = playerData.gamedata.rainTime;




            StartCoroutine(craftingBenchSave.load(2));

            Debug.Log("Player data loaded successfully");
            loading = false;
            actualload = false;

        }
    }



    public override void Delete()
    {
        SaveSystemAPI.DeleteAsync(this.saveFileName + ".sav");
        Debug.Log("Player data deleted successfully");
    }




    void Update()
    {
        GameObject.DontDestroyOnLoad(this);
        if (weapons_Save == null)
        {
            if (FindObjectOfType<WeaponSave>())
            {
                mapGenerationSaveObjects.Clear();
                rockSaveManagers.Clear();
                buildingSave.Clear();
                mainPlayerCamera = Camera.main.gameObject;
                unitSaver = FindObjectOfType<UnitSaver>();
                treeManager = FindObjectOfType<TreeManagerSave>();
                mapGenerationSaveObjects.AddRange(FindObjectsOfType<MapGenerationSave>());
                rockSaveManagers.AddRange(FindObjectsOfType<RockSaveManager>());
                buildingSave.AddRange(FindObjectsOfType<BuildingSave>());
                craftingBenchSave = FindObjectOfType<craftingBenchSave>();
                weapons_Save = FindObjectOfType<WeaponSave>();
            }
        }
        if (loadedTime)
        {
            StartCoroutine(loadGameWait());
        }
        if (actualload)
        {
            Load(loadFileName);
        }
        if (weapons_Save)
        {
            if (!newgame)
            {
                StartCoroutine(lateStart(1.5f));
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            newgame = false;
        }

    }
    IEnumerator lateStart(float time)
    {
        newgame = true;

        foreach (var item in mapGenerationSaveObjects)
        {
            item.checkIfDisabled();
        }
        yield return new WaitForSeconds(time);
        for (int i = 0; i < mapGenerationSaveObjects.Count;)
        {


            if (!playerData.Terrain.terrainGeneratorClass.Contains(new TerrainGeneratorClass(mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.name, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.amount, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.spriteValue, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.itemTransforms, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().itemSize, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().spriteFlip, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().disable)))
            {
                terrains.Add(new TerrainGeneratorClass(mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.name, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.amount, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.spriteValue, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().terrainGeneratorClass.itemTransforms, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().itemSize, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().spriteFlip, mapGenerationSaveObjects[i].GetComponent<MapGenerationSave>().disable));
                i++;
            }
        }

        foreach (var item in terrains)
        {
            if (!playerData.Terrain.terrainGeneratorClass.Contains(item))
            {
                defaultPlayerData.Terrain.terrainGeneratorClass.Add(item);
                playerData.Terrain.terrainGeneratorClass.Add(item);
            }
        }


        for (int i = 0; i < rockSaveManagers.Count;)
        {
            if (!playerData.stoneManagers.stoneManagerSaveClasses.Contains(new stoneManagerSaveClass(rockSaveManagers[i].cutProgesses, rockSaveManagers[i].itemSaveName)))
            {
                rockClasses.Add(new stoneManagerSaveClass(rockSaveManagers[i].cutProgesses, rockSaveManagers[i].itemSaveName));
                i++;
            }
        }

        foreach (var item in rockClasses)
        {
            if (!playerData.stoneManagers.stoneManagerSaveClasses.Contains(item))
            {
                defaultPlayerData.stoneManagers.stoneManagerSaveClasses.Add(item);
                playerData.stoneManagers.stoneManagerSaveClasses.Add(item);
            }
        }

        playerData.treeManagerSave.cutProgress = treeManager.cutProgesses;
        playerData.treeManagerSave.size = treeManager.size;

        for (int i = 0; i < buildingSave.Count;)
        {

            if (!playerData.buildingSaves.buildingSaveClasses.Contains(new buildingSaveClass(buildingSave[i].buildingInfomation.buildingName, buildingSave[i].buildingInfomation.amount, buildingSave[i].buildingInfomation.buildingPositions, buildingSave[i].buildingInfomation.buildingRotations, buildingSave[i].buildingProgress, buildingSave[i].hasPlaced, buildingSave[i].usedBuildingMaterials)))
            {
                buildingSaveClasses.Add(new buildingSaveClass(buildingSave[i].buildingInfomation.buildingName, buildingSave[i].buildingInfomation.amount, buildingSave[i].buildingInfomation.buildingPositions, buildingSave[i].buildingInfomation.buildingRotations, buildingSave[i].buildingProgress, buildingSave[i].hasPlaced, buildingSave[i].usedBuildingMaterials));
                i++;
            }



        }


        foreach (var item in buildingSaveClasses)
        {

            if (!playerData.buildingSaves.buildingSaveClasses.Contains(item))
            {
                defaultPlayerData.buildingSaves.buildingSaveClasses.Add(item);
                playerData.buildingSaves.buildingSaveClasses.Add(item);
            }
        }
    }

    public void UpdateRockInformation()
    {
        for (int i = 0; i < rockClasses.Count; i++)
        {


            for (int x = 0; x < rockSaveManagers.Count; x++)
            {
                if (rockSaveManagers[x].itemSaveName == rockClasses[i].itemSavingName)
                {

                    rockClasses[i] = new stoneManagerSaveClass(rockSaveManagers[x].cutProgesses, rockSaveManagers[x].itemSaveName);
                }
            }
        }
    }
    public void UpdateWeaponSave()
    {
        playerData.weaponssave = new weaponsSave(weapons_Save.amount, weapons_Save.weapon_ids, weapons_Save.weapon_spawn_id, weapons_Save.positions, weapons_Save.rotations, weapons_Save.playersTransforms, weapons_Save.playerIDs);
    }
    public void UpdateCraftingBenchInformation()
    {
        cbs = new List<CraftingSave>();
        foreach (var item in craftingBenchSave.benchSave.craftingSave)
        {
            List<CraftingItem> q = new List<CraftingItem>();
            for (int x = 0; x < item.queue.Count; x++)
            {


                if (!q.Contains(new CraftingItem(item.queue[x].itemName, item.queue[x].amountProduced, item.queue[x].craftTime, item.queue[x].itemsConsumed, item.queue[x].amountConsumed, item.queue[x].stackAmount)))
                {
                    q.Add(new CraftingItem(item.queue[x].itemName, item.queue[x].amountProduced, item.queue[x].craftTime, item.queue[x].itemsConsumed, item.queue[x].amountConsumed, item.queue[x].stackAmount));
                }



            }
            if (!cbs.Contains(new CraftingSave(item.craftingBenchId, item.player, item.levelWorkBench, new CraftingItem(item.craftingItem.itemName, item.craftingItem.amountProduced, item.craftingItem.craftTime, item.craftingItem.itemsConsumed, item.craftingItem.amountConsumed, item.craftingItem.stackAmount), item.craft, item.whatSlot, item.allSlots, item.deliever, item.craftingItemBoolean, item.amountCheck, item.amountProduced, item.craftButtonBool, item.materialsNeeded, item.materialAmountNeed, item.materialCheckAmount, item.devlierMaterialCheckAmount, item.craftingTime, item.overLimit, q)))
            {
                cbs.Add(new CraftingSave(item.craftingBenchId, item.player, item.levelWorkBench, new CraftingItem(item.craftingItem.itemName, item.craftingItem.amountProduced, item.craftingItem.craftTime, item.craftingItem.itemsConsumed, item.craftingItem.amountConsumed, item.craftingItem.stackAmount), item.craft, item.whatSlot, item.allSlots, item.deliever, item.craftingItemBoolean, item.amountCheck, item.amountProduced, item.craftButtonBool, item.materialsNeeded, item.materialAmountNeed, item.materialCheckAmount, item.devlierMaterialCheckAmount, item.craftingTime, item.overLimit, q));
            }
        }
        playerData.craftingItemSave = new craftingBenchSaveData(cbs);
    }
    public void UpdateTreeInformation()
    {
        playerData.treeManagerSave = new treeManagerSaveClass(treeManager.size, treeManager.cutProgesses);
    }
    public void UpdateUnitInformation()
    {
        playerData.playerInformation = new playerInformationClass(unitSaver.amount, unitSaver.positions, unitSaver.rotations, unitSaver.waypoints, unitSaver.headIndexs, unitSaver.bodyIndexs, unitSaver.hairIndexs, unitSaver.unitNames, unitSaver.weaponPickedUp, unitSaver.weaponId, unitSaver.JobsBoolList, unitSaver.player_Inventories);

    }

    public void UpdateStorageInformation()
    {
        playerData.storageSaveClass.placedStorage = GameObject.Find("StorageBuildManager").GetComponent<floorObjectPlacement>().placeOnceHasBeenPlaced;
    }

    public void UpdateBuildingInformation()
    {
        foreach (var buildingsave in buildingSave)
        {


            foreach (var item in playerData.buildingSaves.buildingSaveClasses)
            {

                if (item.buildingName == buildingsave.buildingInfomation.buildingName)
                {
                    item.amount = buildingsave.buildingInfomation.amount;
                    item.buildingPositions = buildingsave.buildingInfomation.buildingPositions;
                    item.buildingRotations = buildingsave.buildingInfomation.buildingRotations;
                    item.buildingProgress = buildingsave.buildingInfomation.buildProgress;
                    item.hasPlaced = buildingsave.buildingInfomation.hasPlaced;

                }

            }
            foreach (var item2 in defaultPlayerData.buildingSaves.buildingSaveClasses)
            {
                if (item2.buildingName == buildingsave.buildingInfomation.buildingName)
                {
                    item2.amount = buildingsave.buildingInfomation.amount;
                    item2.buildingPositions = buildingsave.buildingInfomation.buildingPositions;
                    item2.buildingRotations = buildingsave.buildingInfomation.buildingRotations;
                    item2.buildingProgress = buildingsave.buildingInfomation.buildProgress;
                    item2.hasPlaced = buildingsave.buildingInfomation.hasPlaced;


                }
            }
        }
    }



}
