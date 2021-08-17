using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksAndLeaves : MonoBehaviour {

    float size;

	// Use this for initialization
	void Start () {
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        size = Random.Range(1.0f, 6.0f);
        transform.localScale = new Vector3(size, size, 1);
	}
	
	
}
