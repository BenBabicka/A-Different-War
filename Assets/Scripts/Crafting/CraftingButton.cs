using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CraftingButton : MonoBehaviour {

    public GameObject craftingScript;

    int craftAmount = 1;

    public TMP_Text amountText;
    public void craft()
    {

        craftingScript.GetComponent<Crafting>().Craft(craftAmount);

    }

    public void add()
    {
        craftAmount += 1;
        amountText.text = craftAmount.ToString();
    }
    public void minus()
    {
        if(craftAmount > 1)
        {
            craftAmount -= 1;
            amountText.text = craftAmount.ToString();
        }
    }

}
