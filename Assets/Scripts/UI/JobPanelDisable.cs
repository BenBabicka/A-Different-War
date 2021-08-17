using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JobPanelDisable : MonoBehaviour {


    public bool disable;

  

    void Update()
    {

        if(disable)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<Image>().raycastTarget = false;
        }
        if (!disable)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<Image>().raycastTarget = true;

        }

    }
}
