using UnityEngine;
using System.Collections;
[System.Serializable]
public class WeaponTransform : MonoBehaviour {

public    Transform playersTransform;
    public bool pick;
    public GameObject pickedup;
    public bool isPickedup;
    float[] pos = new float[2];

    public int playerID;

	// Use this for initialization
	void Start () {
        pos[0] = 20;
        pos[1] = -20;
        if (GameObject.Find("BrokenPlane"))
        {
            if ((transform.position - GameObject.Find("BrokenPlane").transform.position).sqrMagnitude < 30)
            {
                transform.position = new Vector3(transform.position.x + pos[Random.Range(1, 2)], transform.position.y, transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (playersTransform != null)
        {
            gameObject.transform.position = playersTransform.position;
            gameObject.transform.rotation = playersTransform.rotation;
            pickedup = playersTransform.gameObject;
            playerID = playersTransform.parent.GetComponent<PlayerSave>().playerID;
            isPickedup = true;
        }
        if(playersTransform == null)
        {
            gameObject.transform.position = gameObject.transform.position;
            gameObject.transform.rotation = gameObject.transform.rotation;
            playerID = 0;
            pick = false;
            pickedup = null;
            isPickedup = false;
        }
    }
}
