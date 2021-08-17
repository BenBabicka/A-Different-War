using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour {

   public List<GameObject> array;
    public float distance;
    float stop = 1;

    public GameObject arrayMaster;

    void Update()
    {
        if(arrayMaster)
        {
            array = arrayMaster.GetComponent<MoveObjects>().array;
        }
        stop -= Time.deltaTime;
        if (stop > 0)
        {
            GetInactiveInRadius();
        }
    }


    void GetInactiveInRadius()
    {
        foreach (GameObject obj in array)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < distance)
            {
                obj.SetActive(false);
            }
          
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, distance);
    }

}
