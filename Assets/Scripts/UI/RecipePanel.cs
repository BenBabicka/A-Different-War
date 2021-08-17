using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RecipePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject craftingRecipePanel;

   public bool onHover;


   public List<string> materialsList;
    string[] arrayOfItems;
    string stringOfItems;
    float time = 0.5f;

    void Start()
    {
        for (int i = 0; i < gameObject.transform.parent.GetChild(2).GetComponent<Crafting>().Materials.Count; i++)
        {

            materialsList.Add(gameObject.transform.parent.GetChild(2).GetComponent<Crafting>().MaterialsAmount[i] + " " + gameObject.transform.parent.GetChild(2).GetComponent<Crafting>().Materials[i] + " Needed");

        }
    }

    void Update()
    {

        if (onHover)
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0)
            {

                craftingRecipePanel.SetActive(true);
                craftingRecipePanel.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + -115, gameObject.transform.position.z);

                arrayOfItems = materialsList.ToArray();
                stringOfItems = string.Join("\n", arrayOfItems);
                craftingRecipePanel.transform.GetChild(0).GetComponent<Text>().text = stringOfItems;
            }
        }
      

    }



    public void OnPointerEnter(PointerEventData eventData)
    {

        onHover = true;
    }

    

    public void OnPointerExit(PointerEventData eventData)
    {
        craftingRecipePanel.SetActive(false);

        onHover = false;

        time = 0.5f;
    }

}
