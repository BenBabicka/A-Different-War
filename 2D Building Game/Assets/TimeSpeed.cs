using UnityEngine;
using System.Collections;

public class TimeSpeed : MonoBehaviour {

    public bool x1;
    public bool x2;
    public bool x3;
    public bool x4;

    GameManager manager;
    // Use this for initialization
    void Start () {
       
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	public void OnToggle(bool toggled) {
        if (toggled)
        {
            if (x1)
            {
                manager.X1();
                Debug.Log("X1");
            }
            if (x2)
            {
                manager.X2();
                Debug.Log("X2");

            }
            if (x3)
            {
                manager.X3();
                Debug.Log("X3");

            }
            if (x4)
            {
                manager.X4();
                Debug.Log("X4");

            }
        }
    }
}
