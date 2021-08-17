using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationManager : MonoBehaviour {

    public GameObject NationPanel;

    public void toggleNationPanel()
    {
        NationPanel.SetActive(!NationPanel.activeSelf);
    }
}
