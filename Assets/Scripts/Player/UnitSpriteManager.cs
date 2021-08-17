using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class UnitSpriteManager : MonoBehaviour {



    public GameObject Character;
   public float vec;
    public GameObject sprites;
    void Start () {
        var rot = transform.rotation;

        sprites.transform.rotation = rot * Quaternion.Euler(180, 0, 180);
    }

    // Update is called once per frame
    void Update () {
        var rot = transform.rotation;




        vec = Mathf.Abs( Character.transform.eulerAngles.y);


        //back
       
        //left
        if (vec <= 135 && vec >= 45)
        {
            sprites.transform.rotation = rot * Quaternion.Euler(180, 180, 180);
            foreach (var item in transform.GetComponentsInChildren<SpritePicker>())
            {
                item.front = false;
                item.back = false;

            }
            foreach (var item in transform.GetComponentsInChildren<ItemSpriteHolder>())
            {
                item.front = false;
                item.back = false;

            }
        }
        //front
        if (vec <= 225 && vec >= 135)
        {
            foreach (var item in transform.GetComponentsInChildren<SpritePicker>())
            {
                item.front = true;
                item.back = false;

            }
            foreach (var item in transform.GetComponentsInChildren<ItemSpriteHolder>())
            {
                item.front = true;
                item.back = false;

            }
        }

        //right
        if (vec <= 315 && vec >= 225)
        {
            sprites.transform.rotation = rot * Quaternion.Euler(180, 0, 180);
            foreach (var item in transform.GetComponentsInChildren<SpritePicker>())
            {
                item.front = false;
                item.back = false;

            }
            foreach (var item in transform.GetComponentsInChildren<ItemSpriteHolder>())
            {
                item.front = false;
                item.back = false;

            }
        }
        if (vec >= 315 || vec <= 45)
        {
            foreach (var item in transform.GetComponentsInChildren<SpritePicker>())
            {
                item.back = true;
                item.front = false;

            }
            foreach (var item in transform.GetComponentsInChildren<ItemSpriteHolder>())
            {
                item.back = true;
                item.front = false;

            }
        }
    }
}
