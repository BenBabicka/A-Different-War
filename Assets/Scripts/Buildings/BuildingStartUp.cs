using UnityEngine;
using System.Collections;

public class BuildingStartUp : MonoBehaviour {

   public bool hasPlaced;

    public Behaviour[] components;
    public Collider[] colliders;

    public Collider[] collidersdisable;

    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (hasPlaced == true)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = true;
            }

            foreach (Collider col in colliders)
            {
                col.enabled = true;
            }

            foreach (Collider col in collidersdisable)
            {
                col.enabled = false;
            }
            if (gameObject.GetComponent<Building>().buildProgress < 100)
            {
                if (gameObject.tag != "JobBuilding")
                {
                    gameObject.tag = "JobBuilding";
                }
            }
        }
    }

}
