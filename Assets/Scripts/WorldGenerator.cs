using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public GameObject worldMapBase;

    public Vector3 size;
    public GameObject map;
    float curretAmount;
    public float amount;
    public List<GameObject> allBases;
    [Space]
    [Header("Place Object")]
    public GameObject worldPoint;
    public GameObject paths;
    public GameObject informationPanels;
    void Start()
    {
        if (amount > 6)
        {
            amount += 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curretAmount < amount)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
            GameObject bases = Instantiate(worldMapBase, pos, Quaternion.identity);
            if (!allBases.Contains(bases))
            {
                allBases.Add(bases);
            }
            if (!map.GetComponent<WorldMap>().worldPoints.Contains(bases))
            {
                map.GetComponent<WorldMap>().worldPoints.Add(bases);
            }
            bases.GetComponent<WorldPoint>().map = map;
            bases.GetComponent<WorldPoint>().spawner = gameObject;
            bases.GetComponent<WorldPoint>().pathParent = paths;
            bases.GetComponent<WorldPoint>().infomationPanelParent = informationPanels;
            bases.transform.SetParent(worldPoint.transform);
            bases.transform.localScale = new Vector3(1, 1, 1);

            curretAmount += 1;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawCube(transform.position, new Vector3(size.x, size.y, 0));
    }

    public void randomPosition(GameObject point)
    {
        Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        point.transform.position = pos;
        
    }

}
