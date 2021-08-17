using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddToList : MonoBehaviour {

    List<GameObject> Trees;
  public  GameObject manager;
    private float tim;

    

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        manager = GameObject.Find("Manager");
        Trees = manager.GetComponent<Removing>().Trees;

        foreach (var tree in Trees)
        {
            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), tree.transform.position) <= 10)
            {
                if (tree.GetComponent<TreeManager>())
                {
                    tree.transform.position = new Vector3(Random.Range(-tree.GetComponent<TreeManager>().Randomize.x / 2, tree.GetComponent<TreeManager>().Randomize.x / 2), 0.5f, Random.Range(-tree.GetComponent<TreeManager>().Randomize.z / 2, tree.GetComponent<TreeManager>().Randomize.z / 2));
                    tree.GetComponent<TreeManager>().size = 0.5f;
                    tree.GetComponent<TreeManager>().cutProgress = 0;
                }
                if (tree.GetComponent<StoneManager>())
                {
                    transform.position = new Vector3(Random.Range(-tree.GetComponent<StoneManager>().Randomize.x / 2, tree.GetComponent<StoneManager>().Randomize.x / 2), 0.5f, Random.Range(-tree.GetComponent<StoneManager>().Randomize.z / 2, tree.GetComponent<StoneManager>().Randomize.z / 2));
                    tree.GetComponent<StoneManager>().cutProgress = 0;
                }
            }
        }
    }
}
