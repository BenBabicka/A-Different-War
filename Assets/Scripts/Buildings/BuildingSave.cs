using Bayat.SaveSystem;
using Bayat.SaveSystem.Demos;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingSave : MonoBehaviour {



    public BuildingSaveClass buildingInfomation;
   

    public GameObject buildingPrefab;


    public string ItemingSaving;

    public List<GameObject> Buildings;


    public SaveManager saveManager;

    public int amount;
    public List<Vector3> positions;
    public List<Vector3> rotation;

    public List<float> buildingProgress;
    public List<bool> hasPlaced;
    public List<buildingMaterials> usedBuildingMaterials;
    [System.Serializable]
    public class buildingMaterials
    {
        public List<string> currentMaterialsUsed;
        public List<float> currentMaterialsUsedAmount;
        public buildingMaterials(List<string> CMU, List<float> CMUA)
        {
            currentMaterialsUsed = CMU;
            currentMaterialsUsedAmount = CMUA;
        }

    }

    public bool load;

    int loadedBuildings;

    void Start()
    {

        saveManager = FindObjectOfType<SaveManager>();
        saveManager.buildingSave.Add(this);
        ItemingSaving = gameObject.GetComponent<floorObjectPlacement>().BuildingName;
        buildingInfomation = new BuildingSaveClass();
        buildingInfomation.buildingName = ItemingSaving;

    }


    public IEnumerator UpdateInfomation(float time)
    {
        yield return new WaitForSeconds(time);
        updateinfo();


    }

    void updateinfo()
    {

        
            amount = Buildings.Count;
        buildingInfomation.amount = amount;
        if (Buildings.Count > 0)
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                Debug.Log(Buildings[i].name + "pos:" + Buildings[i].transform.position);
                Vector3 pos = Buildings[i].transform.position;
                if (!positions.Contains(Buildings[i].transform.position))
                {
                    buildingProgress.Add(Buildings[i].GetComponent<Building>().buildProgress);
                    hasPlaced.Add(Buildings[i].GetComponent<BuildingStartUp>().hasPlaced);
                    rotation.Add(Buildings[i].transform.eulerAngles);
                    usedBuildingMaterials.Add(new buildingMaterials(Buildings[i].GetComponent<Building>().currentMaterialsUsed, Buildings[i].GetComponent<Building>().currentMaterialsUsedAmount));
                    positions.Add(Buildings[i].transform.position);
                }

               
            }
        }
        


        if(positions.Count > 0)
        {
            buildingInfomation.buildProgress = buildingProgress;
            buildingInfomation.hasPlaced = hasPlaced;

            buildingInfomation.buildingRotations = rotation;

            buildingInfomation.buildingPositions = positions;
            buildingInfomation.hasPlaced = hasPlaced;
        }

        saveManager.UpdateBuildingInformation();
        

    }

    public void  UpdateBuildProgress()
    {
        
            for (int i = 0; i < Buildings.Count; i++)
            {
                for (int x = 0; x < buildingProgress.Count; x++)
                {
                    if (buildingProgress[x] != Buildings[i].GetComponent<Building>().buildProgress)
                    {
                        buildingProgress[x] = Buildings[i].GetComponent<Building>().buildProgress;
                    }
                }
            }
        
    }

    public void UpdateBuildMaterials()
    {

        for (int i = 0; i < Buildings.Count; i++)
        {
            for (int x = 0; x < usedBuildingMaterials.Count; x++)
            {
                if (usedBuildingMaterials[x] != new buildingMaterials(Buildings[i].GetComponent<Building>().currentMaterialsUsed, Buildings[i].GetComponent<Building>().currentMaterialsUsedAmount))
                {
                    usedBuildingMaterials[x] = new buildingMaterials(Buildings[i].GetComponent<Building>().currentMaterialsUsed, Buildings[i].GetComponent<Building>().currentMaterialsUsedAmount);
                }
            }
        }

    }
    void Update()
    {
        if (load)
        {
            
                    amount = buildingInfomation.amount;
                    positions = buildingInfomation.buildingPositions;
                    rotation = buildingInfomation.buildingRotations;
                    buildingProgress = buildingInfomation.buildProgress;
                    hasPlaced = buildingInfomation.hasPlaced;

                    if (Buildings.Count >= amount && Buildings.Count > 0)
                    {
                        if (loadedBuildings < Buildings.Count)
                        {
                            for (int i = 0; i < Buildings.Count; i++)
                            {
                                Buildings[i].name = buildingPrefab.name;
                                Buildings[i].transform.position = buildingInfomation.buildingPositions[i];
                                Buildings[i].transform.eulerAngles = buildingInfomation.buildingRotations[i];
                                Buildings[i].GetComponent<Building>().buildProgress = buildingInfomation.buildProgress[i];
                                Buildings[i].GetComponent<BuildingStartUp>().hasPlaced = buildingInfomation.hasPlaced[i];
                                Buildings[i].GetComponent<BuildingID>().ID = i + 1;
                                loadedBuildings += 1;
                            }
                        }
                        else
                        {
                            for (int i = loadedBuildings; i < buildingInfomation.amount; i++)
                            {
                                GameObject building = Instantiate(buildingPrefab, transform.position, Quaternion.identity) as GameObject;
                                Buildings.Add(building);
                                building.name = buildingPrefab.name;
                                building.transform.position = buildingInfomation.buildingPositions[i];
                                building.transform.eulerAngles = buildingInfomation.buildingRotations[i];
                                building.GetComponent<Building>().buildProgress = buildingInfomation.buildProgress[i];
                                building.GetComponent<BuildingStartUp>().hasPlaced = buildingInfomation.hasPlaced[i];
                        Buildings[i].GetComponent<BuildingID>().ID = i + 1;
                        i++;
                            }
                        }
                    }
                    else
                    {

                        for (int i = 0; i < buildingInfomation.amount; i++)
                        {
                            GameObject building = Instantiate(buildingPrefab, transform.position, Quaternion.identity) as GameObject;
                            Buildings.Add(building);
                            building.name = buildingPrefab.name;
                            building.transform.position = buildingInfomation.buildingPositions[i];
                            building.transform.eulerAngles = buildingInfomation.buildingRotations[i];
                            building.GetComponent<Building>().buildProgress = buildingInfomation.buildProgress[i];
                            building.GetComponent<BuildingStartUp>().hasPlaced = buildingInfomation.hasPlaced[i];
                    Buildings[i].GetComponent<BuildingID>().ID = i + 1;
                    i++;
                        }
                    }
                }
            
        
    }
    public IEnumerator LoadBuildings(float waitTime)
    {
        load = true;
        
        yield return new WaitForSeconds(waitTime);
        load = false;
    }



}
