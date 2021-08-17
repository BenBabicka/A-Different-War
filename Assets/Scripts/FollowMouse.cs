using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            gameObject.transform.position = new Vector3(hit.point.x, 4, hit.point.z);
        }
        
    }

 
}
