using UnityEngine;
using System.Collections;

public class StoneManager : MonoBehaviour {

    public float cutProgress;
    public Vector3 Randomize;

    public string WhatMat;


    void Start()
    {
     
    }

    void Update()
    {      
        if (cutProgress >= 100)
        {
            gameObject.active = false;
        }
    }

    
}
