using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCamera : MonoBehaviour {

    public List<GameObject> units;
   public int index;
    int players;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        players = -1 + units.Count ;
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            switchToPlayer();
            index += 1;

        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            switchToPlayer();
            index -= 1;

        }

        if (index >= units.Count)
        {
            index = 0;
        }
        if(index < 0)
        {
            index = players;
        }



        if (Input.GetKey(KeyCode.J))
        {
            gameObject.GetComponent<RtsCamera>().LookAt = new Vector3(0, 0, 0);
            print(transform.position.x);
        }
	}

    void switchToPlayer()
    {
        gameObject.GetComponent<RtsCamera>().LookAt = units[index].transform.position;
    }


}
