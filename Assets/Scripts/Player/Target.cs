using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

   // UnitSelectionComponent player;

    // Use this for initialization
   void Start () {
     //   player = GameObject.Find("Player").GetComponent<UnitSelectionComponent>();
	}

    void Update()
    {  
        if (Input.GetMouseButtonDown(0))
       {
          Vector3 temp = Input.mousePosition;
          temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
          this.transform.position = Camera.main.ScreenToWorldPoint(temp);
       }
    }
}
