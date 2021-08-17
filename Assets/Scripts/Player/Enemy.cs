using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public NavMeshAgent nav;
    public GameObject target;

    public bool scout;

    public string UnitN;

    // Use this for initialization
    void Start () {
        nav = gameObject.GetComponent<NavMeshAgent>();
      //  FindObjectOfType<SaveManager>().GetComponent<SaveManager>().allEnemy.Add(gameObject);

    }

    // Update is called once per frame

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                target = closest;
         
            }
        }
        return closest;
    }
    void Update()
    {

        if (scout)
        {
            if (target == null)
            {
                Debug.Log("Find Player");
                FindClosestEnemy();
            }
         //   if (gameObject.GetComponent<EnemyItemPickup>().weapon == true)
          //  {
                if (target)
                {

                NavMeshPath path = new NavMeshPath();

                NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
                nav.path = path;
            }
             
           // }
        }
    }

}
