using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionMarker : MonoBehaviour {

    float lifeTime = 20;

    MissionManager manager;
  


    
    public GameObject marker;

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<MissionManager>();
    }


    void Update()
    {

        if (manager.paused == false)
        {

            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, lifeTime / 10);

            lifeTime -= Time.fixedDeltaTime;
            if (lifeTime <= 0)
            {
                if (!GameObject.Find("Manager").GetComponent<MissionManager>().spawnPoints.Contains(marker))
                    GameObject.Find("Manager").GetComponent<MissionManager>().spawnPoints.Add(marker);



                GameObject.Find("Manager").GetComponent<MissionManager>().placedmarkers.Remove(gameObject);

                Destroy(gameObject);
            }
        }
    }
}
