using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PlayerFoodManager : MonoBehaviour {

    public List<GameObject> foodItems;
    GameObject storageInv;
    public float foodEatingSpeed;

    bool eat;
    bool wantsToEat;

    float eatspeed = 20;

    public float hungerBar = 100;
    public bool sleep;

    NavMeshAgent nav;
    float speed;

    // Use this for initialization
    void Start () {
        storageInv = GameObject.Find("Storage");

        nav = gameObject.GetComponent<NavMeshAgent>();

        speed = nav.speed;
    }

    // Update is called once per frame
    void Update () {


        hungerBar -= Time.deltaTime / 15;
        
        if(hungerBar <= 60 )
        {
            wantsToEat = true;
        }
        else
        {
            wantsToEat = false;
        }
         if(hungerBar <= 50)
        {
            eat = true;
        }
        else
        {
            eat = false;
        }
        if (hungerBar <= 10)
        {
            gameObject.GetComponent<Health>().unconscious = true;
        }

        if (hungerBar <= 20)
        {
            gameObject.GetComponent<NavMeshAgent>().speed = speed / 2;
        }


        if(storageInv == null &&  GameObject.FindWithTag("Storage"))
        {
            storageInv = GameObject.FindWithTag("Storage");
        }

       
        foreach (var item in Resources.LoadAll<GameObject>("Items/Food"))
        {
            if (!foodItems.Contains(item))
            {
                foodItems.AddRange(Resources.LoadAll<GameObject>("Items/Food"));
            }
        }
        if (storageInv)
        {
            if (wantsToEat && gameObject.GetComponent<JobManager>().injob == false)
            {
                foreach (var item in foodItems)
                {
                    if (storageInv)
                    {
                        foreach (var key in storageInv.GetComponent<StorageInventory>().dictionary.Keys)
                        {
                            if (storageInv.GetComponent<StorageInventory>().dictionary[key] > 0)
                            {


                                Eat();


                            }
                        }
                    }
                }
            }

            if (eat)
            {
                foreach (var item in foodItems)
                {
                    if (storageInv)
                    {
                        foreach (var key in storageInv.GetComponent<StorageInventory>().dictionary.Keys)
                        {
                            if (storageInv.GetComponent<StorageInventory>().dictionary[key] > 0)
                            {


                                Eat();


                            }
                        }
                    }
                }
            }


        }

    }


    public void Eat()
    {
        if (eat)
        {
            if (gameObject.GetComponent<Health>().unconscious == false)
            {
                gameObject.GetComponent<SelectableUnitComponent>().canMove = false;
                if (Vector3.Distance(transform.position, storageInv.transform.position) > 4)
                {
                    NavMeshPath path = new NavMeshPath();

                    NavMesh.CalculatePath(transform.position, storageInv.transform.position, NavMesh.AllAreas, path);
                    nav.path = path;
                }
                if (Vector3.Distance(transform.position, storageInv.transform.position) < 3)
                {
                    nav.path = new NavMeshPath();
                }

                    if (Vector3.Distance(transform.position, storageInv.transform.position) <= 5)
                {
                    eatspeed -= Time.deltaTime;
                    if (eatspeed <= 0)
                    {
                        foreach (var item in foodItems)
                        {
                            foreach (var key in storageInv.GetComponent<StorageInventory>().dictionary.Keys)
                            {
                                if (storageInv.GetComponent<StorageInventory>().dictionary[key] > 0)
                                {
                                    if (key == item.GetComponent<ItemData>().itemName)
                                    {
                                        Debug.Log("findingfood");
                                        storageInv.GetComponent<StorageInventory>().dictionary[key] -= 1;
                                        Debug.Log("Eaten " + item);
                                        hungerBar = 100;//Add food amount
                                        gameObject.GetComponent<NavMeshAgent>().speed = speed;
                                        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                                        eat = false;
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

    }
}
