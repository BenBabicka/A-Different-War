using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PritoryButton : MonoBehaviour
{
   public  enum pritoryButtonList
    {
        tree,rock,building
    }

    public pritoryButtonList buttonType;

    GameObject manager;
    public GameObject objectForPritory;
    void Start()
    {
        manager = GameObject.Find("Manager");
        gameObject.GetComponent<Button>().onClick.AddListener (delegate { pritoritseGameobject(); });

    }

    public void pritoritseGameobject()
    {
        if (buttonType == pritoryButtonList.tree)
        {
            if (!manager.GetComponent<PriorityList>().treePriority.Contains(objectForPritory))
            {
                manager.GetComponent<PriorityList>().treePriority.Add(objectForPritory);
            }
        }
        if (buttonType == pritoryButtonList.rock)
        {
            if (!manager.GetComponent<PriorityList>().minerPriority.Contains(objectForPritory))
            {
                manager.GetComponent<PriorityList>().minerPriority.Add(objectForPritory);
            }
        }
        if (buttonType == pritoryButtonList.building)
        {
            if (!manager.GetComponent<PriorityList>().buildingPriority.Contains(objectForPritory))
            {
                manager.GetComponent<PriorityList>().buildingPriority.Add(objectForPritory);
            }
        }
    }
}
