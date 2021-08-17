using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour {


    public float maxDistance;
    public float minDistance;

    public float size;
    float Distance = 15f;
    float ZoomDampening = 5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        size = Distance;
        if (size < maxDistance)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
        {
                Distance += 1;
            }
        }
        if (size > minDistance)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
        {

                Distance -= 1;
            }
        }

        gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, Distance, Time.unscaledDeltaTime * ZoomDampening);


    }
}
