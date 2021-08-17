using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMapGeneration : MonoBehaviour {


    [System.Serializable]
    public class terrainSpawn
    {
        public GameObject[] tree;



        [Header("left")]
        public Vector3 leftCenter;
        public Vector3 leftSize;
        public float LeftmaxObj;
        [Space]

        [Header("Right")]
        public Vector3 rightCenter;
        public Vector3 rightSize;
        public float rightmaxObj;
        [Space]

        [Header("Up")]
        public Vector3 upCenter;
        public Vector3 upSize;
        public float upmaxObj;
        [Space]

        [Header("Down")]
        public Vector3 downCenter;
        public Vector3 downSize;
        public float downmaxObj;
        [Space]

        public float amount;

    }
    public terrainSpawn terrainSpawning;

    [System.Serializable]
    public class mapCustomization
    {
        public GameObject[] maps;
    }
    public mapCustomization mapCustomizationing;

    [HideInInspector]
    public float time = 1f;

    void Start()
    {
        
            Instantiate(mapCustomizationing.maps[Random.Range(0, mapCustomizationing.maps.Length)], transform.position, Quaternion.Euler(0,0,0));
        
    }

    void Spawn()
    {
        for (int i = 0; i < terrainSpawning.tree.Length; i++)
        {

            if (terrainSpawning.LeftmaxObj <= terrainSpawning.amount - 1)
            {
                Vector3 pos = terrainSpawning.leftCenter + new Vector3(Random.Range(-terrainSpawning.leftSize.x / 2, terrainSpawning.leftSize.x / 2), 0.5f, Random.Range(-terrainSpawning.leftSize.z / 2, terrainSpawning.leftSize.z / 2));
                GameObject g = Instantiate(terrainSpawning.tree[i], pos, Quaternion.Euler(90, 0, 0));
                terrainSpawning.LeftmaxObj += 1;
            }
            if (terrainSpawning.rightmaxObj <= terrainSpawning.amount - 1)
            {
                Vector3 pos = terrainSpawning.rightCenter + new Vector3(Random.Range(-terrainSpawning.rightSize.x / 2, terrainSpawning.rightSize.x / 2), 0.5f, Random.Range(-terrainSpawning.rightSize.z / 2, terrainSpawning.rightSize.z / 2));
                GameObject g = Instantiate(terrainSpawning.tree[i], pos, Quaternion.Euler(90, 0, 0));
                terrainSpawning.rightmaxObj += 1;
            }
            if (terrainSpawning.upmaxObj <= terrainSpawning.amount - 1)
            {
                Vector3 pos = terrainSpawning.upCenter + new Vector3(Random.Range(-terrainSpawning.upSize.x / 2, terrainSpawning.upSize.x / 2), 0.5f, Random.Range(-terrainSpawning.upSize.z / 2, terrainSpawning.upSize.z / 2));
                GameObject g = Instantiate(terrainSpawning.tree[i], pos, Quaternion.Euler(90, 0, 0));
                terrainSpawning.upmaxObj += 1;
            }
            if (terrainSpawning.downmaxObj <= terrainSpawning.amount - 1)
            {
                Vector3 pos = terrainSpawning.downCenter + new Vector3(Random.Range(-terrainSpawning.downSize.x / 2, terrainSpawning.downSize.x / 2), 0.5f, Random.Range(-terrainSpawning.downSize.z / 2, terrainSpawning.downSize.z / 2));
                GameObject g = Instantiate(terrainSpawning.tree[i], pos, Quaternion.Euler(90, 0, 0));
                terrainSpawning.downmaxObj += 1;
            }
        }

          

        
    }



    void Update()
    {

        time -= Time.deltaTime;
        if (time <= 0)
        {
            Spawn();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(terrainSpawning.leftCenter, terrainSpawning.leftSize);
        Gizmos.DrawCube(terrainSpawning.rightCenter, terrainSpawning.rightSize);
        Gizmos.DrawCube(terrainSpawning.upCenter, terrainSpawning.upSize);
        Gizmos.DrawCube(terrainSpawning.downCenter, terrainSpawning.downSize);

    }
}
