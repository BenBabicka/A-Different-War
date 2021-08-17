using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCooking : MonoBehaviour {

    public GameObject cookingStation;

    public float speed;
    public float progress;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(progress >= 99)
        {
            tag = "Food";
            progress = 100;
        }

		if(cookingStation)
        {
            if(Vector3.Distance(transform.position, cookingStation.transform.position) <= 5)
            {
                progress += 0.001f * speed * Time.deltaTime;
                transform.position = cookingStation.transform.position;
                Debug.Log("Cooking");
            }
        }
	}
}
