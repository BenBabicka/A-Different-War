using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestoryBuildings : MonoBehaviour {

    public LayerMask buildingLayer;
    [Space]
    public Toggle toggle;
    [Space]
    public Component[] componentsToDisable;
    [Space]
    public StorageData storage;

    bool toggleDestory;

    public GameObject architectPanel;

    StorageInventory storageInv;

    void Start()
    {
        toggleDestory = toggle.isOn;
        if (GameObject.FindWithTag("Storage"))
        {
            if (GameObject.Find("Storage").GetComponent<StorageInventory>())
            {
                storageInv = GameObject.Find("Storage").GetComponent<StorageInventory>();
            }
        }
    }


    void Update()
    {
        toggleDestory = toggle.isOn;

        if(architectPanel.activeSelf == false)
        {
            toggleDestory = false;
            toggle.isOn = false;
        }

        if (toggleDestory)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                if (componentsToDisable[i].GetComponent<floorObjectPlacement>())
                {
                    componentsToDisable[i].GetComponent<floorObjectPlacement>().canBuild = false;
                }
                if (componentsToDisable[i].GetComponent<WallBuilding>())
                {
                    componentsToDisable[i].GetComponent<WallBuilding>().canBuild = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10000, buildingLayer))
                {


                    if(hit.transform.GetComponent<Building>().built == false)
                    {

                        for (int i = 0; i < hit.transform.GetComponent<Building>().materials.Count; i++)
                        {
                            if(!storageInv.dictionary.ContainsKey( hit.transform.GetComponent<Building>().materials[i] ))
                            {
                                storageInv.dictionary.Add(hit.transform.GetComponent<Building>().materials[i], hit.transform.GetComponent<Building>().materialAmount[i]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < hit.transform.GetComponent<Building>().materials.Count; i++)
                        {
                            if (!storageInv.dictionary.ContainsKey(hit.transform.GetComponent<Building>().materials[i]))
                            {
                                storageInv.dictionary.Add(hit.transform.GetComponent<Building>().materials[i], hit.transform.GetComponent<Building>().materialAmount[i]/2);
                            }
                        }
                    }

                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }


}
