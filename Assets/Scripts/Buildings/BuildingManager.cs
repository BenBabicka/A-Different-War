using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

    public GameObject[] buildings;
    private BuildingPlacement buildingPlacement;

    // Use this for initialization
    void Start()
    {



        buildingPlacement = GetComponent<BuildingPlacement>();

    }

    // Update is called once per frame
    void Update()
    {

    }

   public void BuildHouse()
    { 
                buildingPlacement.SetItem(buildings[1]);

    }
}
