using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TreeManager : MonoBehaviour {


    public float cutProgress;

    public float size;

    public Vector3 Randomize;
    // Use this for initialization
    public void randomise()
    {
        size = Random.Range(1f, 2);
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(size, size, size);
     
        if (size <= 1)
        {
            size += 0.1f / 14000;
        }

        if(size >= 2.25f)
        {
            gameObject.transform.tag = "Tree";
        }
      
	}



  


}
