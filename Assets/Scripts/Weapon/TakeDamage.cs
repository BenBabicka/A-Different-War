using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {

    public Health health;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void OnTriggerEnter(Collider col) {
	if(col.tag == "Bullet")
        {
        health.health -= col.GetComponent<Damage>().damage;
        }
	}
}
