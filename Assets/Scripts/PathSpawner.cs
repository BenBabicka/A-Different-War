using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathSpawner : MonoBehaviour {

    public GameObject path;
    float lenght;
    float amount;
    float pos;
    public bool zSnapping;

    bool built;
	// Use this for initialization
	void Start () {
        pos = -transform.localScale.z / 2 + 0.5f;

    }

    // Update is called once per frame
    void Update () {
        lenght = transform.localScale.z;

      

        if (amount <= lenght - 1)
        {
            if (!zSnapping)
            {
              GameObject p =  Instantiate(path, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + pos), transform.rotation);
                p.GetComponent<BuildingStartUp>().hasPlaced = true;

                GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist.Add(p);
            }
        else
            {
                GameObject p = Instantiate(path, new Vector3(transform.localPosition.x + pos, transform.localPosition.y, transform.localPosition.z ), transform.rotation);
                p.GetComponent<BuildingStartUp>().hasPlaced = true;

                GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist.Add(p);
            }
            amount += 1;
            pos += 1;
        }

    }
}
