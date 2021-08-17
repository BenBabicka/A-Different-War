using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public float cutProgress;
    public float harvastProgress;
    public string PlantName;

    public bool harvasted;

    public Vector3 size;


    public GameObject product;

    int max;
    void Start()
    {
        max = Random.Range(5, 20);

    }
    void Update()
    {
        if(harvastProgress >= 100)
        {
            
            harvasted = true;
        }

        if(harvasted)
        {
            harvastProgress -= Time.deltaTime / 100;
            if(harvastProgress == 0)
            {
                harvasted = false;
            }
        }

            if (cutProgress >= 100)
            {
            int i = 0;
            while (i < max)
            {
                Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
                Instantiate(product, pos, transform.rotation);
                i++;
            }
            gameObject.SetActive(false);
            }
        
    }
}
