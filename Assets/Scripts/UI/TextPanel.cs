using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPanel : MonoBehaviour {

   public GameObject textPanel;
   public GameObject minPanel;

    public void maximizePanel()
    {
        textPanel.SetActive(true);
        minPanel.SetActive(false);
    }

    public void minimizePanel()
    {
        minPanel.SetActive(true);
        textPanel.SetActive(false);
    }

}
