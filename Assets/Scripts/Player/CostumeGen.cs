using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeGen : MonoBehaviour {

    public List<GameObject> costume;
    public int costumeNumber;
    GameObject prefab;

    void Start () {

        costumeNumber = Random.Range(0, costume.Count);

           prefab = costume[costumeNumber];
        prefab.SetActive(true);
    }


    void Update () {
		
	}
}
