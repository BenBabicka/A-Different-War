using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalSpawner : MonoBehaviour {

    public List<GameObject> allAnimals;

    public List<GameObject> animals;


    public Vector3 center;
    public Vector3 size;

    public float number;
    public float amount;
    public float max2;
    float maxObj;

    bool canSpawn;
    bool one;
    public bool loadspawn;

    public bool forest;
    public bool desert;
    public bool jungle;


    void Spawn()
    {
        
        if (maxObj <= amount - 1)
        {
            if (canSpawn)
            {
                for (int i = 0; i < animals.Count; i++)
                {


                    Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -1.0f, Random.Range(-size.z / 2, size.z / 2));
                    GameObject E = Instantiate(animals[i], pos, Quaternion.Euler(90, 0, 0)) as GameObject;
                    E.name = E.name + "_" + number;
                    E.transform.SetParent(GameObject.Find("Animals").transform);
                    PlayerPrefs.SetString("Name" + E.GetComponent<AnimalManager>().AnimalName + number, E.name);
                    number += 1;
                    maxObj += 1;

                }
            }
        }
       
    }
    public void LoadSpawn()
    {
        
            for (int i = 0; i < animals.Count; i++)
            {
                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -1.0f, Random.Range(-size.z / 2, size.z / 2));
                GameObject E = Instantiate(animals[i], pos, Quaternion.Euler(90, 0, 0)) as GameObject;
                E.name = E.name + "_" + number;
                PlayerPrefs.SetString("Name" + E.GetComponent<AnimalManager>().AnimalName + number, E.name);
                number += 1;
                max2 += 1;

            }
        
    }

    void Update()
    {
        if (loadspawn)
        {
            LoadSpawn();
        }
        if (number <= 0)
        {
            number = 0;
        }


        Spawn();
        canSpawn = true;

        foreach (var animal in allAnimals)
        {

            if (animal.GetComponent<AnimalManager>().desert == desert && animal.GetComponent<AnimalManager>().desert != false)
            {
                if (!animals.Contains(animal))
                {
                    animals.Add(animal);
                }
            }
            if (animal.GetComponent<AnimalManager>().forest == forest && animal.GetComponent<AnimalManager>().forest != false)
            {
                if (!animals.Contains(animal))
                {
                    animals.Add(animal);
                }
            }
            if (animal.GetComponent<AnimalManager>().jungle == jungle && animal.GetComponent<AnimalManager>().jungle != false)
            {
                if (!animals.Contains(animal))
                {
                    animals.Add(animal);
                }
            }

        }
       
      

    }

}
