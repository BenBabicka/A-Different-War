using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[System.Serializable]
public class floorObjectPlacement : MonoBehaviour
{

    public GameObject prefabPlacementObject;


    public float grid = 2.0f;

    // Store which spaces are in use
    int[,] usedSpace;

    GameObject placementObject = null;
    GameObject areaObject = null;

    bool mouseClick = false;
    Vector3 lastPos;

    public bool canBuild;
    public bool onHover;
    private Vector3 curPos;
    bool ableToBuild;
    public String BuildingName;

    public GameObject lastplaced;
    public GameObject storage;


    public bool tent;
    public bool huntingLodge;
    public bool farm;

    public Text objectiveText;

    bool displaytext;

    public Toggle toggle;
    Ray ray;


    float ID;
   public bool placeOnce;
   public bool storageBuilding;
    public bool placeOnceHasBeenPlaced;

    public GameObject buildingManager;
    public List<floorObjectPlacement> otherBuildingScripts;
    bool cantBuild;


    public Sprite failedSprite;
    Sprite orginalSprite;


    // Use this for initialization
    void Start()
    {
       /* Vector3 slots = GetComponent<Renderer>().bounds.size / grid;
        usedSpace = new int[Mathf.CeilToInt(slots.x), Mathf.CeilToInt(slots.z)];
        for (var x = 0; x < Mathf.CeilToInt(slots.x); x++)
        {
            for (var z = 0; z < Mathf.CeilToInt(slots.z); z++)
            {
                usedSpace[x, z] = 0;
            }
        }*/
        otherBuildingScripts.AddRange(GameObject.FindObjectsOfType<floorObjectPlacement>());
        if(otherBuildingScripts.Contains(this))
        {
            otherBuildingScripts.Remove(this);
        }
        if (prefabPlacementObject.GetComponent<Building>().FinshedSpriteLoaction)
        {
            orginalSprite = prefabPlacementObject.GetComponent<Building>().FinshedSpriteLoaction.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            orginalSprite = prefabPlacementObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }

    }



    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        canBuild = toggle.isOn;
        if (placementObject)
        { 
            if (cantBuild)
            {
                if (placementObject.GetComponent<Building>().FinshedSpriteLoaction)
                {
                    placementObject.GetComponent<Building>().FinshedSpriteLoaction.GetComponent<SpriteRenderer>().sprite = failedSprite;
                }
                else
                {
                    placementObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = failedSprite;
                }
            }
            else
            {
                if (placementObject.GetComponent<Building>().FinshedSpriteLoaction)
                {
                    placementObject.GetComponent<Building>().FinshedSpriteLoaction.GetComponent<SpriteRenderer>().sprite = orginalSprite;
                }
                else
                {
                    placementObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = orginalSprite;
                }

            }
        }
        toggle.onValueChanged.AddListener(delegate {
            foreach (var item in otherBuildingScripts)
            {
                item.toggle.isOn = false;

                item.canBuild = false;

            }
        });
     
        if (storage == null)
        {
            if (!storageBuilding)
            {
                toggle.interactable = false;
            }
        }
        else
        {
            toggle.interactable = true;
        }

        if (GameObject.FindWithTag("Storage"))
        {
            storage = GameObject.FindWithTag("Storage");
        }
        if (canBuild == false)
        {
            Destroy(placementObject);
        }


        if(placeOnce)
        {
            if(placeOnceHasBeenPlaced)
            {
            toggle.interactable = false;
                toggle.isOn = false;
                return; 
            }
        }
        



           
                Vector3 point;

                // point = Input.mousePosition;
                if (onHover)
                {
                    if (canBuild)
                {
                if (prefabPlacementObject.GetComponent<Building>().materials.Count != 0)
                {
                    for (int i = 0; i < prefabPlacementObject.GetComponent<Building>().materials.Count; i++)
                    {

                        if (storage.GetComponent<StorageInventory>().dictionary.ContainsKey(prefabPlacementObject.GetComponent<Building>().materials[i]) && storage.GetComponent<StorageInventory>().dictionary[prefabPlacementObject.GetComponent<Building>().materials[i]] >= prefabPlacementObject.GetComponent<Building>().materialAmount[i])
                        {

                            Debug.Log("can build");



                        }
                        else
                        {
                            return;
                        }
                    }
                }

                    // Check for mouse ray collision with this object
                    if (getTargetLocation(out point))
                            {
                                //I'm lazy and use the object size from the renderer..
                            //    Vector3 halfSlots = GetComponent<Renderer>().bounds.size / 2.0f;

                                // Transform position is the center point of this object, x and z are grid slots from 0..slots-1
                             //   int x = (int)Math.Round(Math.Round(point.x - transform.position.x + halfSlots.x - grid / 2.0f) / grid);
                             //   int z = (int)Math.Round(Math.Round(point.z - transform.position.z + halfSlots.z - grid / 2.0f) / grid);

                                // Calculate the quantized world coordinates on where to actually place the object
                             //   point.x = (float)(x) * grid - halfSlots.x + transform.position.x + grid / 2.0f;
                              //  point.z = (float)(z) * grid - halfSlots.z + transform.position.z + grid / 2.0f;

                                // Create an object to show if this area is available for building
                                // Re-instantiate only when the slot has changed or the object not instantiated at all



                                // Create or move the object
                                if (!placementObject)
                                {
                                    placementObject = (GameObject)Instantiate(prefabPlacementObject, point, Quaternion.identity);
                                    placementObject.tag = "Ground";
                                }
                                else
                                {
                                    placementObject.transform.position = point;
                                }
                                if (GameObject.Find("Manager").GetComponent<GameManager>().canMove == true)
                                {
                                    // On left click, insert the object to the area and mark it as "used"
                                    if (Input.GetMouseButtonDown(0) && mouseClick == false && !Input.GetKeyDown(KeyCode.LeftShift))
                                    {
                                        // Place the object
                                        Destroy(placementObject);

                                    //    Debug.Log("Placement Position: " + x + ", " + z);
                                        ID += 1;
                            // ToDo: place the result somewhere..
                            if (canBuild)
                            {
                                GameObject building = Instantiate(prefabPlacementObject, point, Quaternion.identity) as GameObject;
                                      building.transform.position = new Vector3(point.x, 0, point.z);


                                        lastplaced = building;
                                        building.GetComponent<BuildingStartUp>().hasPlaced = true;
                                        building.name = BuildingName;

                                        building.GetComponent<BuildingID>().ID = ID;
                                        gameObject.GetComponent<BuildingSave>().Buildings.Add(building);
                                StartCoroutine( gameObject.GetComponent<BuildingSave>().UpdateInfomation(.2f));
                            placeOnceHasBeenPlaced = true;
                                if(storageBuilding)
                                {
                                    GameObject.Find("SaveManager").GetComponent<SaveManager>().UpdateStorageInformation();
                                }
                                }
                                    }


                                }
                                if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.LeftShift) || !Input.GetMouseButtonDown(2))
                                {
                                    mouseClick = false;
                                }

                                if (Input.GetMouseButtonDown(1))
                                {
                                    Destroy(placementObject);
                                    mouseClick = true;
                                    toggle.isOn = false;
                                    canBuild = false;
                                }

                            }
                            else
                            {
                                if (placementObject)
                                {
                                    Destroy(placementObject);
                                    placementObject = null;
                                }
                                if (areaObject)
                                {
                                    Destroy(areaObject);
                                    areaObject = null;
                                }
                            }
                 

                }
              



            
        }
        else
        {
            Destroy(placementObject);
           //    toggle.isOn = false;

            canBuild = false;
        }
    }

    bool getTargetLocation(out Vector3 point)
    {

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo, 10000))
        {
            if (!hitInfo.collider.gameObject.GetComponent<Building>())
            {
                point = hitInfo.point;
                cantBuild = false;
                return true;

            }
            else
            {
                point = hitInfo.point;

                cantBuild = true;
                return true;


            }

        }
        point = Vector3.zero;
        return false;
    }
}
