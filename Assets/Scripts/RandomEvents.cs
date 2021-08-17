using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvents : MonoBehaviour {


    public DayNightCycle daySystem;
    public bool randomEvent;

    int randomEventNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (daySystem.time >= 43200)
        {
            if(!randomEvent)
            {
                randomEventNumber = Random.Range(0, 3);
            }
        }

        if(randomEventNumber == 0)
        {
            //Get Random Resources
        }
        if(randomEventNumber == 1)
        {
            //Attacked
        }
        if(randomEventNumber == 2)
        {
            //New Dude
        }
        if(randomEventNumber== 3)
        {

        }
	}
}
