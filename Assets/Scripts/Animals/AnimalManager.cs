using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class AnimalManager : MonoBehaviour {

    NavMeshAgent nav;
    public Vector3 center;
    public Vector3 size;
    public Vector3 pos;
    private float time;
    private float time3 = 15;
    private float distance2 = 2;

    public bool forest;
    public bool desert;
    public bool jungle;


    public string AnimalName;

    // Use this for initialization
    void Start () {
        nav = gameObject.GetComponent<NavMeshAgent>();
        AnimalName = gameObject.name;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        
     
       
        if ((transform.position - pos).sqrMagnitude < distance2)
        {
            time3 -= Time.deltaTime;
            if (time3 <= 0)
            {
                randomPosition();
            }
        }
        if (time <= 0)
        {
            
            randomPosition();
        }
    }

    void randomPosition()
    {
   
        
        center = gameObject.transform.position;
        pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.1f, Random.Range(-size.z / 2, size.z / 2));
        nav.SetDestination(pos);
        time = 30;
        time3 = Random.Range(2, 20);
    }
}
