using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSelectPanel : MonoBehaviour
{
    public GameObject WoodenPanel;
    public GameObject StonePanel;
    public GameObject SteelPanel;
    public GameObject MetalPanel;
    public GameObject UtilityPanel;
    public GameObject WeaponsPanel;
    public GameObject MiscPanel;

    public List<Text> texts;
    public List<Image> images;
    public List<CraftingManager> craftingManagers;
    public bool disable;
    public GameObject craftingSelectPanel;
    public List<craftingBench> craftingBenches;
    void Start()
    {

        craftingManagers.AddRange(gameObject.GetComponentsInChildren<CraftingManager>());

        texts.AddRange(craftingSelectPanel.GetComponentsInChildren<Text>());


        images.AddRange(craftingSelectPanel.GetComponentsInChildren<Image>());
        for (int i = 0; i < craftingManagers.Count; i++)
        {
            craftingManagers[i].disable = true;

        }
        disable = true;
    }
    void Update()
    {
        if (disable)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            craftingSelectPanel.GetComponent<Image>().raycastTarget = false;

            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].raycastTarget = false;
            }
            for (int i = 0; i < images.Count; i++)
            {
                images[i].raycastTarget = false;
            }
            for (int i = 0; i < craftingManagers.Count; i++)
            {
                craftingManagers[i].disable = true;

            }
        }
        if (!disable)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            craftingSelectPanel.GetComponent<Image>().raycastTarget = true;
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].raycastTarget = true;
            }
            for (int i = 0; i < images.Count; i++)
            {
                images[i].raycastTarget = true;
            }
         
        }
    }

    public void OpenWoodenPanel()
    {
        WoodenPanel.GetComponent<CraftingManager>().disable = !WoodenPanel.GetComponent<CraftingManager>().disable;
        StonePanel.GetComponent<CraftingManager>().disable = true;
        SteelPanel.GetComponent<CraftingManager>().disable = true;
        MetalPanel.GetComponent<CraftingManager>().disable = true;
        UtilityPanel.GetComponent<CraftingManager>().disable = true;
        WeaponsPanel.GetComponent<CraftingManager>().disable = true;
        MiscPanel.GetComponent<CraftingManager>().disable = true;
    }
    public void OpenStonePanel()
    {
        StonePanel.GetComponent<CraftingManager>().disable = !StonePanel.GetComponent<CraftingManager>().disable;
        WoodenPanel.GetComponent<CraftingManager>().disable = true;
        SteelPanel.GetComponent<CraftingManager>().disable = true;
        MetalPanel.GetComponent<CraftingManager>().disable = true;
        UtilityPanel.GetComponent<CraftingManager>().disable = true;
        WeaponsPanel.GetComponent<CraftingManager>().disable = true;
        MiscPanel.GetComponent<CraftingManager>().disable = true;
    }
    public void OpenSteelPanel()
    {
        SteelPanel.GetComponent<CraftingManager>().disable = !SteelPanel.GetComponent<CraftingManager>().disable;
        StonePanel.GetComponent<CraftingManager>().disable = true;
        WoodenPanel.GetComponent<CraftingManager>().disable = true;
        MetalPanel.GetComponent<CraftingManager>().disable = true;
        UtilityPanel.GetComponent<CraftingManager>().disable = true;
        WeaponsPanel.GetComponent<CraftingManager>().disable = true;
        MiscPanel.GetComponent<CraftingManager>().disable = true;
    }
    public void OpenMetalPanel()
    {
        MetalPanel.GetComponent<CraftingManager>().disable = !MetalPanel.GetComponent<CraftingManager>().disable;
        StonePanel.GetComponent<CraftingManager>().disable = true;
        SteelPanel.GetComponent<CraftingManager>().disable = true;
        WoodenPanel.GetComponent<CraftingManager>().disable = true;
        UtilityPanel.GetComponent<CraftingManager>().disable = true;
        WeaponsPanel.GetComponent<CraftingManager>().disable = true;
        MiscPanel.GetComponent<CraftingManager>().disable = true;
    }
    public void OpenUtilityPanel()
    {
        UtilityPanel.GetComponent<CraftingManager>().disable = !UtilityPanel.GetComponent<CraftingManager>().disable;
        StonePanel.GetComponent<CraftingManager>().disable = true;
        SteelPanel.GetComponent<CraftingManager>().disable = true;
        MetalPanel.GetComponent<CraftingManager>().disable = true;
        WoodenPanel.GetComponent<CraftingManager>().disable = true;
        WeaponsPanel.GetComponent<CraftingManager>().disable = true;
        MiscPanel.GetComponent<CraftingManager>().disable = true;
    }
    public void OpenWeaponsPanel()
    {
        WeaponsPanel.GetComponent<CraftingManager>().disable = !WeaponsPanel.GetComponent<CraftingManager>().disable;
        StonePanel.GetComponent<CraftingManager>().disable = true;
        SteelPanel.GetComponent<CraftingManager>().disable = true;
        MetalPanel.GetComponent<CraftingManager>().disable = true;
        UtilityPanel.GetComponent<CraftingManager>().disable = true;
        WoodenPanel.GetComponent<CraftingManager>().disable = true;
        MiscPanel.GetComponent<CraftingManager>().disable = true;
    }
    public void OpenMiscPanel()
    {
        MiscPanel.GetComponent<CraftingManager>().disable = !MiscPanel.GetComponent<CraftingManager>().disable;
        StonePanel.GetComponent<CraftingManager>().disable = true;
        SteelPanel.GetComponent<CraftingManager>().disable = true;
        MetalPanel.GetComponent<CraftingManager>().disable = true;
        UtilityPanel.GetComponent<CraftingManager>().disable = true;
        WeaponsPanel.GetComponent<CraftingManager>().disable = true;
        WoodenPanel.GetComponent<CraftingManager>().disable = true;
    }
   
}
