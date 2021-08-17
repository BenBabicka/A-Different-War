using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughSprite : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Color transpartentColour;

    public float speed = 1;
 

    bool fadeInBool;
    bool fadeOutBool;


    void Start()
    {
        if(!sprite)
        {
            sprite = GetComponentInParent<SpriteRenderer>();
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            fadeOutBool = false;

            fadeInBool = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fadeInBool = false;

            fadeOutBool = true;

        }
    }

     void FixedUpdate()
    {
        sprite.color = transpartentColour;

        if(fadeInBool && fadeOutBool == false)
        {

            if(transpartentColour.a >= .54f)
            {
                transpartentColour.a -= (Time.fixedDeltaTime * speed) / 100;

            }
            else
            {
                transpartentColour.a = .54f;
                fadeInBool = false;
            }

        }

        if(fadeOutBool && fadeInBool == false)
        {
            if (transpartentColour.a <= 1)
            {
                transpartentColour.a += (Time.fixedDeltaTime * speed)/100;

            }
            else
            {
                transpartentColour.a = 1;
                fadeOutBool = false;
            }


        }
    }

}
