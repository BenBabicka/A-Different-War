using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class CraftingManager : MonoBehaviour {

    
    public List<Crafting> craftingScripts;

    public GameObject recipe;

    public List<GameObject> craftingBenchs;
    public GameObject canvas;

    public List<Text> texts;
    public List<Image> images;
    public bool disable;
    float fixtimer = 0.5f;

    public List<craftingBench> craftingBenches;

    // Use this for initialization
    void Start () {

        
            texts.AddRange(gameObject.GetComponentsInChildren<Text>());
        
      
            images.AddRange(gameObject.GetComponentsInChildren<Image>());
        
 

        canvas = GameObject.FindWithTag("Crafting UI");
    }
 
    // Update is called once per frame
    void Update()
    {


        craftingBenches = GameObject.Find("Crafting Panel").GetComponent<CraftingSelectPanel>().craftingBenches;

        if (!texts.Contains(gameObject.GetComponentInChildren<Text>()))
        {
            texts.AddRange( gameObject.GetComponentsInChildren<Text>());
        }
        if (!images.Contains(gameObject.GetComponentInChildren<Image>()))
        {
            images.AddRange(gameObject.GetComponentsInChildren<Image>());
        }

        if(disable)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].raycastTarget = false;
            }
            for (int i = 0; i < images.Count; i++)
            {
                images[i].raycastTarget = false;
            }
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<Image>().raycastTarget = false;
        }
        if (!disable)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].raycastTarget = true;
            }
            for (int i = 0; i < images.Count; i++)
            {
                images[i].raycastTarget = true;
            }
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<Image>().raycastTarget = true;
        }

        
        

        

    }

   
}
