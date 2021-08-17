using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteHolder : MonoBehaviour {
    public string spriteLocation;

    public Sprite characterSpriteFront;
    public Sprite characterSpriteSide;
    public Sprite characterSpriteBack;

    public bool front;
    public bool back;
    public int spriteindex;

    public bool helmet;
		void Update () {
        foreach(var item in transform.parent.GetComponentsInChildren<SpritePicker>())
        {
            if(item.spriteLocation == "Hair")
            {
                if(helmet)
                {
                    item.gameObject.SetActive(false);
                }
                if (!helmet)
                {
                    item.gameObject.SetActive(true);
                }
            }
        }
        

        if (front)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = characterSpriteFront;

        }
        if (back)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = characterSpriteBack;

        }
        if (!front && !back)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = characterSpriteSide;
        }

    }
}
