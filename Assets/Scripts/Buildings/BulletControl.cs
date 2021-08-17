using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour {

    public float speed = 20f;
    public float range;
    public float sprayControl;
    void Start()
    {
        //transform.Rotate(transform.rotation.x, Random.Range(-sprayControl, sprayControl), transform.rotation.z);
    }

    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, range);
    }


}
