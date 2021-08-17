using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
[System.Serializable]
public class WallBuilding : MonoBehaviour
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

    public GameObject lastWallPlaced;
    bool lastwall;

    public float zpos;
    public float xpos;

    bool one;
    float timer = 0;


    public GameObject storage;


    public List<GameObject> buildingzlist;

    public Toggle toggle;



    // Use this for initialization
    void Start()
    {
        storage = GameObject.Find("Storage");
      /*  Vector3 slots = GetComponent<Renderer>().bounds.size / grid;
        usedSpace = new int[Mathf.CeilToInt(slots.x), Mathf.CeilToInt(slots.z)];
        for (var x = 0; x < Mathf.CeilToInt(slots.x); x++)
        {
            for (var z = 0; z < Mathf.CeilToInt(slots.z); z++)
            {
                usedSpace[x, z] = 0;
            }
        }*/
        timer = 0;

    }



    void Update()
    {
        canBuild = toggle.isOn;
        place();
    }

    // Update is called once per frame
    void place()
    {
        timer -= Time.unscaledDeltaTime;

        

            Vector3 point;

            // point = Input.mousePosition;
            if (onHover)
            {
                if (canBuild)
                {


                    // Check for mouse ray collision with this object
                    if (getTargetLocation(out point))
                    {
                      /*  //I'm lazy and use the object size from the renderer..
                        Vector3 halfSlots = GetComponent<Renderer>().bounds.size / 2.0f;

                        // Transform position is the center point of this object, x and z are grid slots from 0..slots-1
                        int x = (int)Math.Round(Math.Round(point.x - transform.position.x + halfSlots.x - grid / 2.0f) / grid);
                        int z = (int)Math.Round(Math.Round(point.z - transform.position.z + halfSlots.z - grid / 2.0f) / grid);

                        // Calculate the quantized world coordinates on where to actually place the object
                        point.x = (float)(x) * grid - halfSlots.x + transform.position.x + grid / 2.0f;
                        point.z = (float)(z) * grid - halfSlots.z + transform.position.z + grid / 2.0f;
*/
                        // Create an object to show if this area is available for building
                        // Re-instantiate only when the slot has changed or the object not instantiated at all


                        // Create or move the object
                        if (!placementObject)
                        {
                            placementObject = (GameObject)Instantiate(prefabPlacementObject, point, Quaternion.identity);
                            placementObject.name = "Wall(placementObject)";
                        }
                        else
                        {
                            placementObject.transform.position = point;
                        }




                        GameObject.Find("Player").GetComponent<UnitSelectionComponent>().enabled = false;


                        // On left click, insert the object to the area and mark it as "used"
                        if (Input.GetMouseButton(0) && mouseClick == false && !Input.GetKeyDown(KeyCode.LeftShift))
                        {
                            // Place the object




                            for (int i = 0; i < prefabPlacementObject.GetComponent<Building>().materials.Count; i++)
                            {
                               


                                    if (storage.GetComponent<StorageInventory>().dictionary.ContainsKey(prefabPlacementObject.GetComponent<Building>().materials[i]))
                                    {
                                        if (timer <= 0)
                                        {
                                            Destroy(placementObject);



                                            storage.GetComponent<StorageInventory>().dictionary[prefabPlacementObject.GetComponent<Building>().materials[i]] -= prefabPlacementObject.GetComponent<Building>().materialAmount[i];
                                  /*  if (prefabPlacementObject.GetComponent<Building>().materials[i] == "Wood")
                                    {
                                        storage.GetComponent<StorageData>().Wood -= (int)prefabPlacementObject.GetComponent<Building>().materialAmount[i];
                                    }
                                    if (prefabPlacementObject.GetComponent<Building>().materials[i] == "Stone")
                                    {
                                        storage.GetComponent<StorageData>().Stone -= (int)prefabPlacementObject.GetComponent<Building>().materialAmount[i];
                                    }
                                    if (prefabPlacementObject.GetComponent<Building>().materials[i] == "Textiles")
                                    {
                                        storage.GetComponent<StorageData>().Textiles -= (int)prefabPlacementObject.GetComponent<Building>().materialAmount[i];
                                    }
                                    if (prefabPlacementObject.GetComponent<Building>().materials[i] == "Food")
                                    {
                                        storage.GetComponent<StorageData>().Food -= (int)prefabPlacementObject.GetComponent<Building>().materialAmount[i];
                                    }
                                    if (prefabPlacementObject.GetComponent<Building>().materials[i] == "Credits")
                                    {
                                        storage.GetComponent<StorageData>().Credits -= (int)prefabPlacementObject.GetComponent<Building>().materialAmount[i];
                                    }*/
                                    GameObject building = Instantiate(prefabPlacementObject, point, Quaternion.identity) as GameObject;
                                            timer += 0.3f;


                                           
                                            if (Input.GetMouseButton(0))
                                            {
                                                if (one)
                                                {
                                                    Destroy(building);
                                                    one = false;
                                                }
                                            }

                                            Debug.Log("Placed");
                                            building.GetComponent<BuildingStartUp>().hasPlaced = true;
                                            building.name = BuildingName;
                                            lastWallPlaced = building;
                                            buildingzlist.Add(building);
                                        }

                                    
                                }

                            }
                            if (!Input.GetMouseButton(0) && !Input.GetKeyDown(KeyCode.LeftShift) || !Input.GetMouseButtonDown(1))
                            {
                                mouseClick = false;

                            }

                            if (Input.GetMouseButtonUp(0))
                            {
                                timer = 0;
                                lastwall = false;
                                one = true;

                            }

                            if (Input.GetMouseButtonDown(1))
                            {
                                Destroy(placementObject);
                                toggle.isOn = false;
                                mouseClick = true;
                                canBuild = false;
                                one = true;
                                timer = 0;
                                lastwall = false;

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
    } 
        else
        {
            Destroy(placementObject);
            mouseClick = true;
            toggle.isOn = false;
            canBuild = false;
            one = true;
            timer = 0;
            lastwall = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            toggle.isOn = false;
            mouseClick = true;
            canBuild = false;
            one = true;
            timer = 0;
            lastwall = false;

        }
    }

    

    bool getTargetLocation(out Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(ray, out hitInfo, 10000))
        {
            if (hitInfo.collider.tag == "Ground")
            {
                point = hitInfo.point;

                return true;
            }
  
        }
        point = Vector3.zero;
        return false;
    }
}
