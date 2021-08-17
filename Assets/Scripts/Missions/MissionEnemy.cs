using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MissionEnemy : MonoBehaviour {

    public bool idle;
    public bool findingEnemy;
    public bool attacking;

    float nextPathTime = 5.0f;

    private NavMeshAgent nav;

    Vector3 pos;
    Vector3 enemyPos;
    Vector3 size = new Vector3(10,0, 10);
    Vector3 center;

    MissionFieldOfView FOV;

    GameObject enemy;

    public Transform post;
	// Use this for initialization
	void Start () {
        nav = gameObject.GetComponent<NavMeshAgent>();
        FOV = gameObject.GetComponent<MissionFieldOfView>();
    }
	
	// Update is called once per frame
	void Update () {
        findingEnemy = FOV.caution;
        if (FOV.units.Count >= 1)
        {

            enemy = FOV.units[Random.Range(0, FOV.units.Count)].gameObject;
            
            if(enemy)
            {
                center = enemy.transform.position;
            enemyPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.1f, Random.Range(-size.z / 2, size.z / 2));

            }
        }
        if (!post)
        {
            if (idle)
            {
                center = transform.position;
                pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.1f, Random.Range(-size.z / 2, size.z / 2));
                nextPathTime -= Time.deltaTime;
                if (nextPathTime <= 0)
                {
                    nav.SetDestination(pos);
                    nextPathTime = Random.Range(4, 10);
                }
            }
            if (findingEnemy)
            {
                nextPathTime -= Time.deltaTime;
                if (nextPathTime <= 0)
                {

                    nav.SetDestination(enemyPos);
                    nextPathTime = Random.Range(4, 10);
                }
            }
            if (attacking)
            {
                nav.SetDestination(enemy.transform.position);
            }
        }
        else
        {
            transform.LookAt(post.GetChild(0).transform);
            nav.SetDestination(post.position);
            if(attacking)
            {
                transform.LookAt(enemy.transform);
            }
        }
    }

    void clostestEnemy()
    {
        GameObject[] Trees;
        Trees = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Trees)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPlayer = go;
                enemy = closestPlayer;
                distance = curDistance;

            }
        }
    }

}
