using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingToolTipInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Building buildingInformation;
    public BuildingToolTip toolTip;

    public GameObject requiredMaterialsPlanel;

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
    bool hover;
     void Start()
    {
        for (int i = 0; i < buildingInformation.materials.Count; i++)
        {
            materialInformationList.Add(new materialInformation( buildingInformation.materials[i], buildingInformation.materialAmount[i]));
        }
    }

    void Update()
    {
        if(hover)
        {
            foreach (var item in materialInformationList)
            {
                if (!toolTip.instatiatedObjectNames.Contains(item.material))
                {
                    toolTip.materialInformationList.Add(new BuildingToolTip.materialInformation(item.material, item.amount));
                }
            }
        }
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.materialInformationList.Clear();
        toolTip.instatiatedObjectNames.Clear();
        toolTip.instantiatedObjects.Clear();
        toolTip.hover = true;
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.hover = false;
        hover = false;
    }

}
