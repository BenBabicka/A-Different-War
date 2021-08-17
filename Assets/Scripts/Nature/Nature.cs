using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Nature : MonoBehaviour {


    public string Name;
    public float ID;
    public string Information;
    public List<Sprite> sprites;
    public int spriteImage;
    Sprite spirte;
    public bool flip;
    float size;
 
    // Use this for initialization
   public void randomiseObject () {
        size = Random.Range(4, 8);
        spriteImage = Random.Range(0, sprites.Count);
        spirte = sprites[spriteImage];
       
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spirte;

        var flixInt = Random.Range(0, 1);
        if (flixInt == 1)
        {
            flip = true;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public void LoadSprite()
    {
        spirte = sprites[spriteImage];
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spirte;
        if(flip)
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;

        }

    }



}
