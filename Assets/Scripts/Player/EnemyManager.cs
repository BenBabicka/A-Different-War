using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

   public GameObject enemy;
    public Vector3 randomPos;
    public Vector3 center;
    public float amount;
    float max;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (max <= amount - 1)
        {
            max += 1;
            GameObject u = Instantiate(enemy, new Vector3(Random.Range(-randomPos.x / 2, randomPos.x / 2), transform.position.y, Random.Range(-randomPos.x / 2, randomPos.x / 2)), Quaternion.Euler(90, 0, 0));
            u.SetActive(true);
          
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(center, randomPos);
    }
}
