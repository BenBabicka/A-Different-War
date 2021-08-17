using UnityEngine;
using System.Collections;

public class WeaponTransform : MonoBehaviour {

public    Transform playersTransform;
    public bool pick;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (playersTransform != null)
        {
            gameObject.transform.position = playersTransform.position;
            gameObject.transform.rotation = playersTransform.rotation;
        }
    }
}
