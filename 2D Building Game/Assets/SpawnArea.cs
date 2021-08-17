using UnityEngine;
using System.Collections;

public class SpawnArea : MonoBehaviour {

    public GameObject Object;
    float time = 1f;

    public Vector3 center;
    public Vector3 size;

    float maxObj;

    public float Growthspeed;

    public float amount;

    void Spawn()
    {
        if (maxObj <= amount)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.5f, Random.Range(-size.z / 2, size.z / 2));
            Instantiate(Object, pos, Quaternion.Euler(90, 0, 0));
            maxObj += 1;
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
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
