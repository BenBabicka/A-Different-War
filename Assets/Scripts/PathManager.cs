using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PathManager : MonoBehaviour {

    public float speed;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<NavMeshAgent>().speed *= speed;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<NavMeshAgent>().speed = col.GetComponent<UnitInfomation>().orgSpeed;
        }
    }
}
