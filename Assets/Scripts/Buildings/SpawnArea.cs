using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnArea : MonoBehaviour {

    public GameObject[] Object;
    float time = 1f;

    public Vector3 center;
    public Vector3 size;

   public float maxObj;

    public float Growthspeed;

    public float amount;

    public bool spawnE;

    public List<GameObject> allFood;

   public bool treespawner;
    public bool stonespawner;
    public bool spawn;

    void Spawn()
    {
        if (!spawn)
        {

            if (!spawnE)
            {
                if (maxObj <= amount)
                {
                    for (int i = 0; i < Object.Length; i++)
                    {


                        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.5f, Random.Range(-size.z / 2, size.z / 2));
                        GameObject g = Instantiate(Object[i], pos, Quaternion.Euler(90, 0, 0));
                        g.transform.SetParent(transform.Find("Trees"));
                        maxObj += 1;
                        allFood.Add(g);
                    }
                }
            }
            if (spawnE)
            {
                if (maxObj <= amount)
                {
                    for (int i = 0; i < Object.Length; i++)
                    {
                        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.5f, Random.Range(-size.z / 2, size.z / 2));
                        GameObject g = Instantiate(Object[i], pos, Quaternion.Euler(90, 0, 0)) as GameObject;
                        GameObject trees = GameObject.Find("Trees");
                        g.transform.SetParent(transform.Find("Trees"));

                        g.name = g.name + " " + maxObj;
                        g.transform.SetParent(GameObject.Find("Trees").transform);
                        maxObj += 1;
                        if (treespawner)
                        {
                            if (!gameObject.GetComponent<Removing>().Trees.Contains(g))
                            {
                                gameObject.GetComponent<Removing>().Trees.Add(g);
                            }
                        }
                        if (stonespawner)
                        {
                            if (!gameObject.GetComponent<Removing>().Trees.Contains(g))
                            {
                                gameObject.GetComponent<Removing>().Trees.Add(g);
                            }
                        }
                    }
                }
            }
        }
    }


    void Update()
    {
        center.z = transform.position.z;
        center.x = transform.position.x;
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Spawn();
            time = Growthspeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(center, size);
    }
}
