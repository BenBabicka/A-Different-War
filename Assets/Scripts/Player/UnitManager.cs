using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitManager : MonoBehaviour {
   
    public List<GameObject> units;
    //UnitSelectionComponent unitSelectionComponent;

    public List<GameObject> allUnits;
    
  
   float objects;
    float Object;


   public GameObject Unitcanvas;

    public bool inMission;

   public bool unselect;

    float unitNameFloat;
    public bool hover;
    public GameObject[] panels;
    public GameObject craftingPanel;
    public GameObject jobManager;
    public GameObject mapPanel;
    public List<GameObject> treesToChop;
    public List<GameObject> selectedUnits;

    void Start () {

        // unitSelectionComponent = GameObject.Find("Player").GetComponent<UnitSelectionComponent>();
        if (!allUnits.Contains(GameObject.FindGameObjectWithTag("Player")))
        {
            
                allUnits.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            
        }
       
    }


    void Update () {

        for (int i = 0; i < panels.Length; i++)
        {
            if (!hover)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))

                {
                    craftingPanel.GetComponent<CraftingManager>().disable = true;
                    jobManager.GetComponent<JobPanelDisable>().disable = true;
                    mapPanel.GetComponent<MissionPanel>().disable = true;
                    if (panels[i].activeSelf == true)
                    {
                        panels[i].SetActive(false);
                       
                        
                    }
                }
            }
        }
        if (!allUnits.Contains(GameObject.FindGameObjectWithTag("Player")))
        {
          //  if (GameObject.FindGameObjectWithTag("unit").gameObject.layer == layer)
         //   {
                allUnits.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            //}
        }

        

        foreach (var item in allUnits)
        {
            if (item != null)
            {
                if (item.GetComponent<SelectableUnitComponent>() != null)
                {
                    if (item.GetComponent<SelectableUnitComponent>().Selected == true)
                    {
                        if (!units.Contains(item))
                            units.Add(item);
                    }
                    else
                    {
                        if (units.Contains(item))
                            {

                            units.Remove(item);
                        }

                    }


                    if (units.Count >= 2)
                    {
                        item.GetComponent<SelectableUnitComponent>().muiltple = true;
                        item.transform.GetChild(3).gameObject.SetActive(false);
                        Unitcanvas.SetActive(true);
                    }
                    if (!unselect)
                    {
                        if (units.Count <= 1)
                        {
                            item.GetComponent<SelectableUnitComponent>().muiltple = false;
                            if (inMission)
                            {
                                item.transform.GetChild(1).gameObject.SetActive(true);
                            }
                            else
                            {
                                item.transform.GetChild(3).gameObject.SetActive(true);
                            }
                            Unitcanvas.SetActive(false);
                        }
                    }
                }
            }

           
        }

        
            
               
    }



    public void OnToggle(bool Combat)
    {
        foreach (var item in units)
        { 

            if (Combat == true)
            {
                item.GetComponent<Shooting>().enabled = true;
                item.GetComponent<CombatEnable>().combatToggel.isOn = true;
                item.GetComponent<CombatEnable>().Drafted = true;
            }
            if (Combat == false)
            {
                item.GetComponent<Shooting>().enabled = false;
                item.GetComponent<Shooting>().isFiring = false;
                item.GetComponent<CombatEnable>().combatToggel.isOn = false;
                item.GetComponent<CombatEnable>().Drafted = false;
            }
        }
    }
}
