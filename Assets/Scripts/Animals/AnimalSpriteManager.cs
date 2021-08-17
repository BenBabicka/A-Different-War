using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpriteManager : MonoBehaviour
{

    private SpriteRenderer spriteHolder;

    public float vec;

    public Sprite frontSprite;
    public Sprite SideSprite;
    public Sprite backSprite;


    // Start is called before the first frame update
    void Start()
    {
        spriteHolder = GetComponentInChildren<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        vec = Mathf.Abs(transform.eulerAngles.y);
        var rot = transform.rotation;

        //Left
        if (vec <= 135 && vec >= 45)
        {
            spriteHolder.sprite = SideSprite;
            spriteHolder.flipX = false;
        }
        //front
        if (vec <= 225 && vec >= 135)
        {
            spriteHolder.sprite = frontSprite;
        }

        //right
        if (vec <= 315 && vec >= 225)
        {
            spriteHolder.sprite = SideSprite;
            spriteHolder.flipX = true;

        }
        //Back
        if (vec >= 315 || vec <= 45)
        {
            spriteHolder.sprite = backSprite;
        }
    }
}
