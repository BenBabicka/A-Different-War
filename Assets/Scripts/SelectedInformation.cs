using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedInformation : MonoBehaviour {
    public Vector2 uiPosition;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<RectTransform>().localPosition = uiPosition;

    }
}
