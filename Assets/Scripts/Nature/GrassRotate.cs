using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRotate : MonoBehaviour {


  public  float flipxint;

    bool chooseSide;

    void Start()
    {
        if (!chooseSide)
        {
            flipxint = Random.Range(0.0f, 1.0f);
            chooseSide = true;
        }
        if (flipxint < 0.5f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
