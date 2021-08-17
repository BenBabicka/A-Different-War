using UnityEngine;
using System.Collections;

public class FarmManager : MonoBehaviour {

    public float cropProgress;
    SpawnArea spawn;
    public float speed;

    bool farm;

    GameObject player;

    

    void Start()
    {
        spawn = gameObject.GetComponent<SpawnArea>();

    }

    void Update()
    {
        if(farm)
        {
            cropProgress += Time.timeScale / speed;
            player.gameObject.GetComponent<JobManager>().closeFarm = gameObject;
        }
    

        if(cropProgress >= 2)
        {
            spawn.enabled = true;
        }
        
        if(cropProgress >= 100)
        {
            Harvest();
            cropProgress = 100;
        }
    }

    void Harvest()
    {
    
    
    }


    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player" && col.gameObject.GetComponent<JobManager>().Farmer == true)
        {
            farm = true;
            player = col.gameObject;
            player.gameObject.GetComponent<JobManager>().closeFarm = gameObject;


        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<JobManager>().Farmer == true)
        {
            
            farm = false;
        }
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<JobManager>().Farmer == false && player != null)
        {
            player = null;
            farm = false;
        }
    }

}
