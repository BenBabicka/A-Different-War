using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingToolTip : MonoBehaviour
{
    public List<materialInformation> materialInformationList;
    [System.Serializable]
    public class materialInformation
    {
        public string material;
        public float amount;
        public materialInformation(string materialName, float materialAmount)
        {
            material = materialName;
            amount = materialAmount;
        }
    }

    public GameObject textEntry;
    public Transform parent;
    
    public List<GameObject> instantiatedObjects;
    public List<string> instatiatedObjectNames;

    public bool hover;

     void Update()
    {
        if(hover)
        {
            parent.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            parent.transform.parent.gameObject.SetActive(false);
           

        }
        foreach (var item in materialInformationList)
        {
            if (!instatiatedObjectNames.Contains(item.material))
            {
                GameObject text = GameObject.Instantiate(textEntry, parent);
                text.gameObject.GetComponent<Text>().text = item.amount.ToString() + " " + item.material;
                text.gameObject.GetComponent<buildingToolTipText>().materialName = item.material;
                text.gameObject.SetActive(true);
                instantiatedObjects.Add(text);
                instatiatedObjectNames.Add(item.material);
            }
        }
    }


}
