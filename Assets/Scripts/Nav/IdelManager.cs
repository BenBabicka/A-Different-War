using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IdelManager : MonoBehaviour {

    bool idle;
    NavMeshAgent nav;
    public Vector3 location;
   public Vector3 size = new Vector3(30,30,30);
     Vector3 center;
   public float idleTime;
    bool findPath;

    float time = 60;
    float velocity;

    public bool isSleeping;

    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        idleTime = Random.Range(10, 20);
        center = transform.position;
        location = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -1.0f, Random.Range(-size.z / 2, size.z / 2));

    }

    // Update is called once per frame
    void Update () {

        if(isSleeping)
        {
            return;
        }

        center = transform.position;
        velocity = nav.velocity.magnitude / nav.speed;

        if (!gameObject.GetComponent<JobManager>().injob && !gameObject.GetComponent<CombatEnable>().Drafted)
        {
            idle = true;
        }
   else
        {
            idle = false;
        }
      
        if(gameObject.GetComponent<SelectableUnitComponent>().Selected || (velocity > 0 && nav.destination != location))
        {
            idle = false;
        }
        if (idle)
        {
            idleTime -= Time.deltaTime;


        }
        else
        {
            idleTime = Random.Range(10, 20);
        }

        if (idleTime <= 0)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                location = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -1.0f, Random.Range(-size.z / 2, size.z / 2));
            }
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, location, NavMesh.AllAreas, path);
            nav.path = path;
            findPath = false;
            if (!findPath)
            {
                Debug.Log("idle");
                location = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -1.0f, Random.Range(-size.z / 2, size.z / 2));
                findPath = true;
            }
            if(Vector3.Distance (transform.position, location) <= 3)
            {
                idleTime = Random.Range(10, 20);
                time = 60;
            }
        }
        else
        {
            findPath = false;
        }


    }
}
