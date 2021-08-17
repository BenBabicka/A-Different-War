using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUI : MonoBehaviour {
    public GameObject storageDisplay;
    public GameObject panel;
    public RectTransform contectrect;
    public GameObject scrollView;
    public GameObject storageText;
    public List<GameObject> storageBuildings;
  
	void Update () {

        if (GameObject.FindWithTag("Storage"))
        {
            if (!storageBuildings.Contains(GameObject.FindWithTag("Storage")))
            {
                storageBuildings.Add(GameObject.FindWithTag("Storage"));
            }
        }
        for (int i = 0; i < storageBuildings.Count; i++)
        {
            if(storageBuildings[i] == null)
            {
                storageBuildings.Remove(storageBuildings[i]);
            }
        }
        if(storageBuildings.Count > 0)
        {
            for (int i = 0; i < storageBuildings.Count; i++)
            {
                storageBuildings[i].GetComponent<StorageInventory>().storageDisplay = storageDisplay;
                storageBuildings[i].GetComponent<StorageInventory>().panel = panel;
                storageBuildings[i].GetComponent<StorageInventory>().contectrect = contectrect;

            }
            scrollView.SetActive(true);
            storageText.SetActive(false);
        }
        else
        {
            scrollView.SetActive(false);
            storageText.SetActive(true);
        }

    }
}
