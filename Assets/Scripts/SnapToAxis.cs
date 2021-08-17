using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToAxis : MonoBehaviour {

    public bool x;
    public bool z;

    public bool xSnap;
    public bool zSnap;


    void Update()
    {
        Debug.Log(xSnap);
        Debug.Log(zSnap);
    }

    void OnMouseOver()
    {

        if (x)
        {
            xSnap = true;
            
        }
        if(z)
        {
            zSnap = true;
        }

    }

    void OnMouseExit()
    {

        if (x)
        {
            xSnap = false;

        }
        if (z)
        {
            zSnap = false;
        }
    }
}
