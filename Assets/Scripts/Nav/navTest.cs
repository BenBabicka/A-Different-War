using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navTest : MonoBehaviour {

    NavMeshAgent nav;
    public Transform target;

    float navSpeed;

	// Use this for initialization
	void Start () {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        nav.SetDestination(target.transform.position);

        int waterMask = 1 << NavMesh.GetAreaFromName("Road");
        int crossingMask = 1 << NavMesh.GetAreaFromName("Crossing");
        int Walkable = 1 << NavMesh.GetAreaFromName("Walkable");

        NavMeshHit hit;
        nav.SamplePathPosition(-1, 0.0f, out hit);
        if (hit.mask == waterMask)//changed line
            nav.speed = .5f;
        if (hit.mask == Walkable)//changed line
            nav.speed = 2;
        if (hit.mask == crossingMask)//changed line
            nav.speed = 5f;
    }

    public void SetAreaCost(int areaIndex, float areaCost)
    {

        navSpeed = areaCost;
    }
}
