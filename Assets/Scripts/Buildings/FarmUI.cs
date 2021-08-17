using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmUI : MonoBehaviour {

    public bool ifMain;
    public bool isText;
    public float height = 2;
    public float width = 2;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {



        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Camera.main.orthographicSize / width, Camera.main.orthographicSize / height);
        if (ifMain)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -gameObject.GetComponent<RectTransform>().sizeDelta.y / 2);
        }
        if(isText)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector2(Camera.main.orthographicSize / width, Camera.main.orthographicSize / height);
        }
    }
}
