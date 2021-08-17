using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    public List<Text> texts;
    public List<Image> images;
    public bool disable;

    void Start()
    {


        texts.AddRange(GetComponentsInChildren<Text>());


        images.AddRange(GetComponentsInChildren<Image>());
        
        disable = true;
    }
    void Update()
    {
        if (disable)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<Image>().raycastTarget = false;

            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].raycastTarget = false;
            }
            for (int i = 0; i < images.Count; i++)
            {
                images[i].raycastTarget = false;
            }
        
        }
        if (!disable)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            GetComponent<Image>().raycastTarget = true;
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
}
