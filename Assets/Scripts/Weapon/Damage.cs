using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    public float damage;

    public bool player;
    public bool enemy;


    void OnCollisionEnter(Collision hit)
    {
      /*  if (player == true)
        {
            
            if (hit.gameObject.tag == "Player")
            {
                hit.gameObject.GetComponent<Health>().d = damage;
                hit.gameObject.GetComponent<Health>().TakeDamage();
                Destroy(gameObject);
            }
            enemy = false;
        }
        if (enemy == true)
        {
            if (hit.gameObject.tag == "AI")
            {
                hit.gameObject.GetComponent<Health>().d = damage;
                hit.gameObject.GetComponent<Health>().TakeDamage();
                Destroy(gameObject);
            }
            player = false;
        }*/
    }
}
