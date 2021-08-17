    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpriteManager : MonoBehaviour {

    public GameObject spriteHolder;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (spriteHolder)
        {
            spriteHolder.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.z);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.z);
        }
    }
}
