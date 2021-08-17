using UnityEngine;
using System.Collections;

public class StorageData : MonoBehaviour {

    public float Wood;
    public float Textiles;
    public float Food;
    public float Metal;

    // Use this for initialization
    void Start () {
        Wood = GameObject.Find("Manager").GetComponent<GameManager>().Wood;
        Textiles = GameObject.Find("Manager").GetComponent<GameManager>().Textiles;
        Food = GameObject.Find("Manager").GetComponent<GameManager>().Food;
        Metal = GameObject.Find("Manager").GetComponent<GameManager>().Metal;
    
    }

    // Update is called once per frame
    void Update () {
	
	}
}
