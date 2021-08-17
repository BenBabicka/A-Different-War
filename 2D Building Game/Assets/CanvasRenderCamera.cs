using UnityEngine;
using System.Collections;

public class CanvasRenderCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
    