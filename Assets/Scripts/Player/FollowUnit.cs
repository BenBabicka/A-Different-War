using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUnit : MonoBehaviour
{


    bool isClicking;

    float clicking = 1f;

    private GameObject mainCamera;
    GameObject playermanager;
    Transform player;

    bool following;

    void Start()
    {
        mainCamera = Camera.main.gameObject;
        playermanager = GameObject.Find("Player");
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0))
        {
            clicking = 1;
        }
        if (Physics.Raycast(ray, out hit, 1000))
        {

            if (hit.transform.tag == "Player")
            {
                if (Input.GetMouseButton(0))
                {
                    clicking -= Time.fixedDeltaTime;
                    player = hit.transform;


                }
            }


         
        }
        if (clicking <= 0)
        {
            following = true;

        }
        if (following)
        {
            playermanager.GetComponent<UnitSelectionComponent>().canSelect = false;
            playermanager.GetComponent<UnitSelectionComponent>().isSelecting = false;
            /*   foreach (var item in GameObject.Find("Player").GetComponent<UnitSelectionComponent>().units)
               {
                   int childs = item.transform.childCount;

                   item.GetComponent<SelectableUnitComponent>().Selected = false;
                   Destroy(item.transform.GetChild(childs - 1).gameObject);
               }
               */
            int childs = player.transform.childCount;

            Destroy(GameObject.Find("SelectionCirclePrefab(Clone)"));

            player.GetComponent<SelectableUnitComponent>().Selected = false;

            playermanager.GetComponent<UnitSelectionComponent>().units.Clear();
            mainCamera.GetComponent<RtsCamera>().LookAt = player.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            following = false;
            playermanager.GetComponent<UnitSelectionComponent>().canSelect = true;
            isClicking = true;
            clicking = 1;

        }
        if (Input.GetMouseButtonUp(0))
        {

            isClicking = false;
        }

    }
}
