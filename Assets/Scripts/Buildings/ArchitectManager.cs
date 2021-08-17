using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectManager : MonoBehaviour {

    public GameObject housingPanel;
    public GameObject wallPanel;
    public GameObject foodPanel;
    public GameObject miningPanel;
    public GameObject otherPanel;

    public GameObject[] buildManager;


   public void housingPanelOn()
    {
        housingPanel.SetActive(!housingPanel.activeSelf);
        wallPanel.SetActive(false);
        foodPanel.SetActive(false);
        miningPanel.SetActive(false);
        otherPanel.SetActive(false);

        /*  for (int i = 0; i < buildManager.Length; i++)
          {
              buildManager[i].GetComponent<floorObjectPlacement>().canBuild = false;
              buildManager[i].GetComponent<WallBuilding>().canBuild = false;
          }*/
    }

    public void wallPanelOn()
    {
        wallPanel.SetActive(!wallPanel.activeSelf);
        housingPanel.SetActive(false);
        foodPanel.SetActive(false);
        otherPanel.SetActive(false);

        /*  for (int i = 0; i < buildManager.Length; i++)
          {
              buildManager[i].GetComponent<floorObjectPlacement>().canBuild = false;
              buildManager[i].GetComponent<WallBuilding>().canBuild = false;
          }*/
    }

    public void FoodPanelOn()
    {
        foodPanel.SetActive(!foodPanel.activeSelf);
        housingPanel.SetActive(false);
        wallPanel.SetActive(false);
        miningPanel.SetActive(false);
        otherPanel.SetActive(false);

        /* for (int i = 0; i < buildManager.Length; i++)
         {
             buildManager[i].GetComponent<floorObjectPlacement>().canBuild = false;
             buildManager[i].GetComponent<WallBuilding>().canBuild = false;

         }*/
    }

    public void OtherPanelOn()
    {
        otherPanel.SetActive(!otherPanel.activeSelf);
        housingPanel.SetActive(false);
        wallPanel.SetActive(false);
        miningPanel.SetActive(false);
        foodPanel.SetActive(false);

        /* for (int i = 0; i < buildManager.Length; i++)
         {
             buildManager[i].GetComponent<floorObjectPlacement>().canBuild = false;
             buildManager[i].GetComponent<WallBuilding>().canBuild = false;

         }*/
    }
    public void miningPanelOn ()
    {
        wallPanel.SetActive(false);
        housingPanel.SetActive(false);
        foodPanel.SetActive(false);
        miningPanel.SetActive(!miningPanel.activeSelf);
        otherPanel.SetActive(false);

        /*  for (int i = 0; i < buildManager.Length; i++)
          {
              buildManager[i].GetComponent<floorObjectPlacement>().canBuild = false;
              buildManager[i].GetComponent<WallBuilding>().canBuild = false;
          }*/
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            housingPanel.SetActive(false);
            wallPanel.SetActive(false);
            foodPanel.SetActive(false);
            otherPanel.SetActive(false);
            miningPanel.SetActive(false);

            for (int i = 0; i < buildManager.Length; i++)
         {
                if (buildManager[i].GetComponent<floorObjectPlacement>())
                {
                    buildManager[i].GetComponent<floorObjectPlacement>().canBuild = false;
                }
                if (buildManager[i].GetComponent<WallBuilding>())
                {
                    buildManager[i].GetComponent<WallBuilding>().canBuild = false;
                }
         }
        }
    }

}
