using UnityEngine;
using System.Collections;

public class AudioShot : MonoBehaviour {


    float timer = 5.0f;

	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
	}
}
