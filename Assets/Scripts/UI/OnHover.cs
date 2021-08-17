using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class OnHover : MonoBehaviour
{
    public List<GameObject> units;
    public bool selected;


    public GameObject player;
    public GameObject manager;

    public GameObject[] hinges;
    public GameObject[] builders;

    public bool hover;

    bool once;

    void Start()
    {
        manager = GameObject.Find("Manager");
        player = GameObject.Find("Player");
  

        if (!units.Contains(GameObject.FindGameObjectWithTag("Player")))
        {
            units.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        }

        if (units != GameObject.Find("Manager").GetComponent<UnitManager>().allUnits)
        {
            units = GameObject.Find("Manager").GetComponent<UnitManager>().allUnits;
        }
        hinges = GameObject.FindGameObjectsWithTag("Player");



        if (units != GameObject.Find("Manager").GetComponent<UnitManager>().allUnits)
        {
            units = GameObject.Find("Manager").GetComponent<UnitManager>().allUnits;
        }
        builders = GameObject.FindGameObjectsWithTag("BuildManager");
    }

   
   
    void Awake()
    {

        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player");
        }
        if (GameObject.Find("Player"))
        {
            selected = false;
            if (manager)
            {
                manager.GetComponent<GameManager>().canMove = true;
            }
        }
        for (int i = 0; i < builders.Length; i++)
        {
            if (builders[i].GetComponent<floorObjectPlacement>())
            {
                if (builders[i].GetComponent<floorObjectPlacement>().canBuild == true)
                {
                    builders[i].GetComponent<floorObjectPlacement>().onHover = true;
                }
            }
            if (builders[i].GetComponent<WallBuilding>())
            {
                if (builders[i].GetComponent<WallBuilding>().canBuild == true)
                {
                    builders[i].GetComponent<WallBuilding>().onHover = true;
                }
            }
        }
        for (int i = 0; i < units.Count; i++)
        {
            units[i].GetComponent<ItemPickUp>().hover = false;
        }
        hover = false;
        if (GameObject.Find("Storage"))
        {
            GameObject.Find("Storage").GetComponent<StorageInventory>().hover = false;
        }
    }

  
   

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
       
    }

   /* public bool IsMouseOverUIWithIngores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.GetComponent<MouseUIIngore>() != null)
            {
                raycastResultsList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultsList.Count > 0;
    }*/
    public void Update()
    {
        if (IsMouseOverUI() && !once)
        {
            player.GetComponent<UnitSelectionComponent>().canSelect = false;
            player.GetComponent<UnitSelectionComponent>().isSelecting = false;
            for (int i = 0; i < builders.Length; i++)
            {
                if (builders[i].GetComponent<floorObjectPlacement>())
                {
                    builders[i].GetComponent<floorObjectPlacement>().onHover = false;
                }
                if (builders[i].GetComponent<WallBuilding>())
                {
                    builders[i].GetComponent<WallBuilding>().onHover = false;
                }
            }
            for (int i = 0; i < units.Count; i++)
            {
                units[i].GetComponent<ItemPickUp>().hover = true;
            }
            GameObject.Find("Manager").GetComponent<UnitManager>().hover = true;


            selected = true;
            if (manager.GetComponent<GameManager>())
            {
                manager.GetComponent<GameManager>().canMove = false;
                manager.GetComponent<GameManager>().hovingOverUi = true;
            }
            hover = true;
            if (GameObject.Find("Storage"))
            {
                GameObject.Find("Storage").GetComponent<StorageInventory>().hover = true;
            }
            player.GetComponent<UnitSelectionComponent>().hover = true;
            once = true;
        }
        if (!IsMouseOverUI() && once)
        {
            selected = false;

            player.GetComponent<UnitSelectionComponent>().canSelect = true;
            player.GetComponent<UnitSelectionComponent>().isSelecting = false;

            if (manager.GetComponent<GameManager>())
            {
                manager.GetComponent<GameManager>().canMove = true;
                manager.GetComponent<GameManager>().hovingOverUi = false;
            }

            for (int i = 0; i < builders.Length; i++)
            {
                if (builders[i].GetComponent<floorObjectPlacement>())
                {

                    builders[i].GetComponent<floorObjectPlacement>().onHover = true;

                }
                if (builders[i].GetComponent<WallBuilding>())
                {

                    builders[i].GetComponent<WallBuilding>().onHover = true;

                }
            }
            Debug.Log("Exit");

            for (int i = 0; i < units.Count; i++)
            {
                units[i].GetComponent<ItemPickUp>().hover = false;
            }
            hover = false;
            if (GameObject.Find("Storage"))
            {
                GameObject.Find("Storage").GetComponent<StorageInventory>().hover = false;
            }
            GameObject.Find("Manager").GetComponent<UnitManager>().hover = false;
            player.GetComponent<UnitSelectionComponent>().hover = false;
            once = false;
        }
    }


    public void closeInventory()
    {
        
            selected = false;
            player.GetComponent<UnitSelectionComponent>().canSelect = true;
        player.GetComponent<UnitSelectionComponent>().isSelecting = false;

        if (manager.GetComponent<GameManager>())
            {
                manager.GetComponent<GameManager>().canMove = true;
            }
        
        for (int i = 0; i < builders.Length; i++)
        {
            if (builders[i].GetComponent<floorObjectPlacement>())
            {
                if (builders[i].GetComponent<floorObjectPlacement>().canBuild == true)
                {
                    builders[i].GetComponent<floorObjectPlacement>().onHover = true;
                }
            }
            if (builders[i].GetComponent<WallBuilding>())
            {
                if (builders[i].GetComponent<WallBuilding>().canBuild == true)
                {
                    builders[i].GetComponent<WallBuilding>().onHover = true;
                }
            }
        }
        for (int i = 0; i < units.Count; i++)
        {
            units[i].GetComponent<ItemPickUp>().hover = false;
        }
        hover = false;
        if (GameObject.Find("Storage"))
        {
            GameObject.Find("Storage").GetComponent<StorageInventory>().hover = false;
        }
        player.GetComponent<UnitSelectionComponent>().hover = false;

    }



}
    