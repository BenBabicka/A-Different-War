using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeUI : MonoBehaviour {

    public RectTransform parentTransfrom;
    GridLayoutGroup grid;
    

	// Use this for initialization
	void Start () {
        grid = gameObject.GetComponent<GridLayoutGroup>();
	}
	
	// Update is called once per frame
	void Update () {
      //  grid.cellSize = new Vector2 (parentTransfrom.sizeDelta.x, 60);
	}
}
