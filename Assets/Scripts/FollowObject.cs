using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public Transform obj;

    public float posX, posY, posZ;

    void FixedUpdate () {
        gameObject.transform.position = obj.transform.position;
        gameObject.transform.rotation = obj.transform.rotation;
        gameObject.transform.position += new Vector3(posX, obj.transform.position.y, posZ);
        gameObject.transform.position = new Vector3(obj.transform.position.x + posX, posY, obj.transform.position.z + posZ);

    }
}
