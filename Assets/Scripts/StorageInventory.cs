using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StorageInventory : MonoBehaviour {

    public Dictionary<string, float> dictionary = new Dictionary<string, float>();

    public GameObject storageDisplay;
    public GameObject panel;
    public RectTransform contectrect;

    [HideInInspector]
    public List<string> inDictionary;
    [HideInInspector]
    public List<GameObject> gameObjectsInDictionary;
    public List<GameObject> craftingObject;

    public bool hover;

    void Update()
    {
        if (!craftingObject.Contains(GameObject.FindWithTag("Crafting")))
        {
            craftingObject.Add(GameObject.FindWithTag("Crafting"));
        }

        if (craftingObject != null)
        {
            foreach (var craft in craftingObject)
            {
                if (craft == null)
                {
                    craftingObject.Remove(craft);
                    break;
                }

            }
           
        }

        if(storageDisplay.activeSelf)
        {
            GameObject.Find("Player").GetComponent<UnitSelectionComponent>().isSelecting = false;
            GameObject.Find("Player").GetComponent<UnitSelectionComponent>().canSelect = false;
        }
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {if (!hover)
                {
                    if (hit.transform.gameObject == gameObject)
                    {
                        
                        storageDisplay.SetActive(true);

                       
                    }
                    else
                    {
                        storageDisplay.SetActive(false);
                        GameObject.Find("Player").GetComponent<UnitSelectionComponent>().canSelect = true;

                    }
                }
            }
        }


        foreach (string key in dictionary.Keys)
        {
            float val = dictionary[key];
          
      /*      if (key == "Wood")
            {
                Debug.Log("update Wood");
                val = (float)gameObject.GetComponent<StorageData>().Wood;
            }
            if (key == "Textiles")
            {
                val = (float)gameObject.GetComponent<StorageData>().Textiles;

            }
            if (key == "Food")
            {
                val = (float)gameObject.GetComponent<StorageData>().Food;

            }
            if (key == "Stone")
            {
                val = (float)gameObject.GetComponent<StorageData>().Stone;

            }
            if (key == "Credits")
            {
                val = (float)gameObject.GetComponent<StorageData>().Credits;

            }

    */

            if (!inDictionary.Contains(key))
            {


                GameObject clone = Instantiate(panel, transform.position, transform.rotation);
                clone.SetActive(true);
                clone.transform.SetParent(contectrect.transform, false);
                contectrect.sizeDelta += new Vector2(0, clone.GetComponent<RectTransform>().sizeDelta.y + 1);
                gameObjectsInDictionary.Add(clone);
                clone.transform.GetChild(0).GetComponent<Text>().text = key;
                clone.transform.GetChild(1).GetComponent<Text>().text = val.ToString();
                clone.GetComponent<InventoryComponent>().itemName = key;
                inDictionary.Add(key);
            }

/*
            if (dictionary[key] <= 0)
            {

                foreach (var craft in craftingObject)
                {
                    if (craft.GetComponent<Crafting>().isCrafting == false)
                    {



                        foreach (var item in gameObjectsInDictionary)
                        {
                            if (item)
                            {
                                if (item.transform.GetChild(0).GetComponent<Text>().text == key)
                                {
                                    Debug.Log(item.transform.GetChild(0).GetComponent<Text>().text + " is the same");
                                    contectrect.sizeDelta -= new Vector2(0, 50);

                                    inDictionary.Remove(key);

                                    Destroy(item);
                                    dictionary.Remove(key);
                                    gameObjectsInDictionary.Remove(gameObjectsInDictionary[(Mathf.RoundToInt(val))].gameObject);

                                }
                            }
                        }
                    }

                }

            }
*/
            foreach (var item in gameObjectsInDictionary)
            {
                if (item)
                {
                    if (item.transform.GetChild(0).GetComponent<Text>().text == key)
                    {
                        item.transform.GetChild(0).GetComponent<Text>().text = key;
                        item.transform.GetChild(1).GetComponent<Text>().text = val.ToString();
                    }
                }
            }
        }

       


    }


}
