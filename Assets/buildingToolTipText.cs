using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingToolTipText : MonoBehaviour
{
    public string materialName;

    public BuildingToolTip toolTip;
     void Update()
    {
        if(!toolTip.instantiatedObjects.Contains(gameObject))
        {
            Destroy(gameObject);
        }
    }

}
