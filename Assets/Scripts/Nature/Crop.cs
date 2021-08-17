using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour {

    public float cropSpeed = 1;
    public float progress;
    public bool harvest;
    public bool harvested;
    public GameObject Farm;
    public float ID;

    [HideInInspector]
  public  bool ifPickedUp;

    [Space]
    public float foodAmount;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!ifPickedUp)
        {
            progress += Time.deltaTime * cropSpeed / 100;

            if (progress >= 100)
            {
                harvest = true;
            }
            if (harvested)
            {
                Farm.GetComponent<FarmManager>().cropList.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
