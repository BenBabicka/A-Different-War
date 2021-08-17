using UnityEngine;
using System.Collections;

public class RaycastBuildingPlacement : MonoBehaviour {

    public GameObject building;
    public Vector3 specificVector;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))

        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                specificVector.Set(hit.point.x, hit.collider.transform.position.y, hit.point.z);
                building.transform.position = specificVector;
                Instantiate(building, specificVector, Quaternion.identity);
                Debug.DrawRay(ray.origin, ray.direction * 5000, Color.red);
            }

        }


    }
}
