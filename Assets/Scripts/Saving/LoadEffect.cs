using UnityEngine;
using System.Collections;

public class LoadEffect : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * -speed);
    }
}
