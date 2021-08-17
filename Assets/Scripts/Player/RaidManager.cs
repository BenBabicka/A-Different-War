using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaidManager : MonoBehaviour {

    public float timer;
    public float amount;

    public GameObject Enemy;

    public Vector3 center;
    public Vector3 size;
    
    public float number;

   public List<GameObject> enemies;

    public float max2;

    float maxObj;
    bool canSpawn;

    public bool loadspawn;

    public GameObject raidButton;
    bool spawnMessage;
    bool spawnedMessage;

    float loaction;

    public GameObject[] weapons;

    void Spawn()
    {
        if (maxObj <= amount - 1)
        { 


            if (canSpawn)
            {
                int weapon = Random.Range(0, weapons.Length);
                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),0, Random.Range(-size.z / 2, size.z / 2));
                GameObject W = Instantiate(weapons[weapon], pos, Quaternion.identity);

               GameObject E = Instantiate(Enemy, pos, Quaternion.Euler(90, 0, 0)) as GameObject;
                E.GetComponent<EnemyItemPickup>().weapon = W;
                if (!enemies.Contains(E))
                {
                    enemies.Add(E);
                }
                E.GetComponent<Enemy>().UnitN = "EAI" + number;
                PlayerPrefs.SetString("Name" + "EAI" + number, E.GetComponent<Enemy>().UnitN);
                number += 1;
                maxObj += 1;

            }
        }
        if(maxObj >= amount)
        {
            canSpawn = false;
            timer = Random.Range(1800, 7200);
            amount = Random.Range(1, 20);
            maxObj = 0;

        }
    }
           

    void Update () {
       
        if (number <= 0)
        {
            number = 0;
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (enemies.Count < 1)
            {
                loaction = Mathf.Abs(Random.Range(0, 3));
                if (loaction == 0)
                {
                    center = new Vector3(-185, 0, 185);
                }
                if (loaction == 1)
                {
                    center = new Vector3(185, 0, 185);
                }
                if (loaction == 2)
                {
                    center = new Vector3(-185, 0, -185);
                }
                if (loaction == 3)
                {
                    center = new Vector3(185, 0, -185);
                }
            }
            Spawn();
            spawnMessage = true;

            //    GameObject.Find("SaveManager").GetComponent<SaveManager>().saveInfo();
            canSpawn = true;
        }

        if (enemies.Count >= 1)
        {
            raidButton.SetActive(true);
            GameObject.Find("Manager").GetComponent<GameManager>().x3.interactable = false;
            GameObject.Find("Manager").GetComponent<GameManager>().x4.interactable = false;
           
        }
        else
        {
            GameObject.Find("Manager").GetComponent<GameManager>().x3.interactable = true;
            GameObject.Find("Manager").GetComponent<GameManager>().x4.interactable = true;
            raidButton.SetActive(false);
            spawnedMessage = false;
        }
        if (spawnMessage)
        {
            if (!spawnedMessage)
            {
                Time.timeScale = 1;

                gameObject.GetComponent<MessageManager>().SpawnMessage("RAID!", Color.red, "Raid", "Your being Raided");
                spawnMessage = false;
                spawnedMessage = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
