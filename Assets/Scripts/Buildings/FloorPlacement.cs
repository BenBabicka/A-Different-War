using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlacement : MonoBehaviour
{
    public GameObject floor;
    float hieght;
    float width;
    float amount;
    Vector3 pos;
    public bool zSnapping;

    bool built;
    public List<GameObject> floorList;


    // Use this for initialization
    void Start()
    {
        pos = new Vector3( transform.position.x -transform.localScale.x / 2 +.5f, transform.position.y, transform.position.z + transform.localScale.z/2 - .5f) ;
        gameObject.GetComponent<Building>().materialAmount[0] = transform.localScale.x * transform.localScale.z;

    }

    // Update is called once per frame
    void Update()
    {



        

       


            if (pos.x < transform.position.x + transform.localScale.x /2)
            {
                
                    GameObject p = Instantiate(floor, pos, transform.rotation);
            p.transform.parent = gameObject.transform;
            floorList.Add(p);
                    GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist.Add(p);
            pos.x += 1;
              
            }
            else
        {
            if(hieght < transform.localScale.z-1)
            {
                hieght += 1;
                pos.z -= 1;
                pos.x = transform.position.x - transform.localScale.x / 2 + .5f;
            }
        }
        
    }


    void Build()
    {
        for (int i = 0; i < floorList.Count; i++)
        {

        }
    }

}
