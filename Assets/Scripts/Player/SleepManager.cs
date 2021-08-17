using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SleepManager : MonoBehaviour {

    public bool sleep;
    DayNightCycle time;
    GameObject bed;
    float speed;

    public float sleepTime = 0;
  public  List<GameObject> beds;



    bool needSleep;


   public float sleepingTime;

   public float sleepBar = 90;


    public float sleepBarSpeed;
    public float sleepTimeSpeed;

    bool searchForBed;

    void FindBed()
    {        searchForBed = true;

        if (FindObjectOfType(typeof(Bed)) as GameObject)
        {
            if (!beds.Contains(FindObjectOfType(typeof(Bed)) as GameObject))
            {
                beds.Add(FindObjectOfType(typeof(Bed)) as GameObject);
            }
        }
        
        if(beds.Count != 0)
        { 
        foreach (var b in beds)
        {
                if (b.GetComponent<Bed>().owner == null)
                {
                    bed = b;

                }
            }
        }

    }

    void Start()
    {
        time = Camera.main.GetComponent<DayNightCycle>();
        speed = gameObject.GetComponent<NavMeshAgent>().speed;
    }

    void Update() {

        sleepBar -= Time.deltaTime / sleepBarSpeed;

        if(sleepBar <= 0)
        {
            needSleep = true;
            if (gameObject.GetComponent<NavMeshAgent>().isStopped == false)
            {
                sleepTime += Time.deltaTime * 50;

            }
        }

        if (needSleep)
        {
            if (!searchForBed)
            {

                FindBed();
            }
        }
        if (needSleep)
        {
            Debug.Log("Sleep");
            if (bed)
            {
                gameObject.GetComponent<NavMeshAgent>().SetDestination(bed.transform.position);
                if(Vector3.Distance (bed.transform.position, transform.position) <= 0.5f)
                {
                    sleep = true;
                }
            }
            if (bed == null)
            {
                sleep = true;
                gameObject.GetComponent<NavMeshAgent>().SetDestination(transform.position);

            }
        }
        if (sleepTime >= 14400)
        {
            sleep = true;
        }

        if(sleepingTime >= 100)
        {
            sleep = false;
            sleepBar = 100;
            needSleep = false;
            sleepTime = 0;
            searchForBed = false;
            gameObject.GetComponent<PlayerFoodManager>().sleep = false;
            gameObject.GetComponent<JobManager>().sleeping = false;
            gameObject.GetComponent<IdelManager>().isSleeping = false;
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;

            sleepingTime = 0;

        }
        if (sleep)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<JobManager>().sleeping = true;
            gameObject.GetComponent<IdelManager>().isSleeping = true;
            gameObject.GetComponent<PlayerFoodManager>().sleep = true;
            sleepingTime += Time.deltaTime / sleepTimeSpeed;
        }



    }
}
