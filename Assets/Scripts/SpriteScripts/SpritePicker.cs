using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePicker : MonoBehaviour {

    public string spriteLocation;

    public Sprite[] sideSprites;
    public Sprite[] frontsprites;
    public Sprite[] backsprites;

    public Sprite characterSpriteFront;
    public Sprite characterSpriteSide;
    public Sprite characterSpriteBack;

    public bool front;
    public bool back;
    public int spriteindex;

    float starttime = 1;
    bool start;
    public bool load;
    public bool loaded;
    void Update()
    {
        if (!load)
        {
            starttime -= Time.fixedDeltaTime;
            if (starttime < 0)
            {
                if (!start)
                {
                    if (spriteLocation != "Head")
                    {

                        spriteindex = Random.Range(0, sideSprites.Length);
                        characterSpriteSide = sideSprites[spriteindex];

                        if (frontsprites.Length > 0)
                        {
                            characterSpriteFront = frontsprites[spriteindex];
                        }
                        if (backsprites.Length > 0)
                        {
                            characterSpriteBack = backsprites[spriteindex];
                        }
                    }
                    StartCoroutine(LateStart(0.5f));
                    start = true;
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


            if ((front || back) && spriteLocation == "Head")
            {
                gameObject.transform.position = gameObject.transform.parent.TransformPoint(0, 0.27f, 0);
            }

            if ((!front && !back) && spriteLocation == "Head")
            {
                gameObject.transform.position = gameObject.transform.parent.TransformPoint(0.03f, 0.27f, 0);

            }
            if ((front || back) && spriteLocation == "Hair")
            {
                gameObject.transform.position = gameObject.transform.parent.TransformPoint(0, 0.35f, 0);
            }

            if ((!front && !back) && spriteLocation == "Hair")
            {
                gameObject.transform.position = gameObject.transform.parent.TransformPoint(0.05f, 0.35f, 0);

            }
        
    }


    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (spriteLocation == "Head")
        {
            if (transform.parent.GetChild(0).GetComponent<SpritePicker>().characterSpriteSide.name == "Character_body_004")
            {

                characterSpriteSide = sideSprites[0];
                characterSpriteFront = frontsprites[0];
                characterSpriteBack = backsprites[0];


            }
            if (transform.parent.GetChild(0).GetComponent<SpritePicker>().characterSpriteSide.name == "Character_body_005")
            {

                characterSpriteSide = sideSprites[1];
                characterSpriteFront = frontsprites[1];
                characterSpriteBack = backsprites[1];


            }
            if (transform.parent.GetChild(0).GetComponent<SpritePicker>().characterSpriteSide.name == "Character_body_006")
            {


                characterSpriteSide = sideSprites[2];
                characterSpriteFront = frontsprites[2];
                characterSpriteBack = backsprites[2];


            }
            if (transform.parent.GetChild(0).GetComponent<SpritePicker>().characterSpriteSide.name != "Character_body_004" && transform.parent.GetChild(0).GetComponent<SpritePicker>().characterSpriteSide.name != "Character_body_005" && transform.parent.GetChild(0).GetComponent<SpritePicker>().characterSpriteSide.name != "Character_body_006")
            {

                spriteindex = Random.Range(0, sideSprites.Length);
                characterSpriteFront = frontsprites[spriteindex];
                characterSpriteSide = sideSprites[spriteindex];
                characterSpriteBack = backsprites[spriteindex];

            }
            gameObject.GetComponent<SpriteRenderer>().sprite = characterSpriteSide;

        }
    }


    public IEnumerator loadSprite(float wait)
    {
        if (load)
        {
            if (!loaded)
            {
                yield return new WaitForSeconds(wait);

                characterSpriteSide = sideSprites[spriteindex];
                characterSpriteFront = frontsprites[spriteindex];
                characterSpriteBack = backsprites[spriteindex];

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
                loaded = false;
            }
        }
    }

}
