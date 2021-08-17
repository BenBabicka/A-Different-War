using UnityEngine;
using System.Collections;

public class CountUp : MonoBehaviour {

    public float value;

	
	void Update () {
        if (value <= 10)
        {
            value += Time.deltaTime; //Value + 1 every second
        }
        if(value >= 10)
        {
            value -= Time.deltaTime;            

         if(value >= 15)
            {
                value = 20;
            }   
        }
	}
}
