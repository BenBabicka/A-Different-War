using UnityEngine;
using System.Collections;

public class GunSpiteManager : MonoBehaviour {

    public Sprite Right;
    public Sprite Left;

    public SpriteRenderer spriteRenderer;


    void Update () {
        var vec = transform.eulerAngles;
        vec.y = Mathf.Round(vec.y / 90) * 90;


        if (vec.y <= 180 && vec.y >= 0)
        {
            spriteRenderer.sprite = Right;

        }
        if (vec.y <= 360 && vec.y >= 180)
        {
            spriteRenderer.sprite = Left;
        }
     
    }
}
