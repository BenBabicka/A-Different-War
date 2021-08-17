using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {

   public GameObject[] weapons;
    public GameObject prefab;
    public float amount;
     float max;
    int id;
    public Vector3 randomPos;

    public List<GameObject> all_weapons;

    public List<GameObject> spawnedInWeapons;


    public List<int> weapon_ids;

    public List<int> weapon_spawn_id;

    public List<Vector3> positions;
    public List<Vector3> rotations;

    public List<GameObject> deletedWeapons = new List<GameObject>();
    public List<GameObject> loadedWeapons;

   
    public List<bool> playerTransforms;
    public List<GameObject> players;
    public List<int> playerIDs;
    bool loading;
    void Start()
    {
        if (GameObject.Find("SaveManager"))
        {
            loading = GameObject.Find("SaveManager").GetComponent<SaveManager>().loading;
        }
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.Find("Manager").GetComponent<UnitManager>().allUnits;


        if (!loading)
        {
            

                if (max < amount)
                {

                    max += 1;
                    prefab = weapons[Random.Range(0, weapons.Length)].gameObject;
                    GameObject u = Instantiate(prefab, new Vector3(Random.Range(-randomPos.x / 2, randomPos.x / 2), transform.position.y, Random.Range(-randomPos.x / 2, randomPos.x / 2)), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    u.transform.SetParent(GameObject.Find("Guns").transform);
                    u.GetComponent<WeaponData>().spawn_ID = id;
                    gameObject.GetComponent<WeaponSave>().weapons.Add(u);
                    spawnedInWeapons.Add(u);
                    gameObject.GetComponent<WeaponSave>().updateInformation();


                    id += 1;
                }
            
        }
        if (loading)
        {
            

                foreach (var weapon_id in weapon_ids)
                {
                    foreach (var weapon in all_weapons)
                    {

                        if (max < amount)
                        {
                            if (weapon.GetComponent<ItemData>().item_ID == weapon_id)
                            {
                                GameObject u = Instantiate(weapon, positions[id], Quaternion.Euler(rotations[id]));
                                u.transform.SetParent(GameObject.Find("Guns").transform);
                                u.GetComponent<WeaponData>().spawn_ID = weapon_spawn_id[id];
                                if (playerTransforms[id] == true)
                                {
                                    for (int i = 0; i < players.Count; i++)
                                    {


                                        if (playerIDs[id] == players[i].GetComponent<PlayerSave>().playerID)
                                        {
                                            u.GetComponent<WeaponTransform>().playersTransform = players[i].transform.GetChild(1).transform;
                                        }
                                    }
                                }
                                gameObject.GetComponent<WeaponSave>().weapons.Add(u);
                                loadedWeapons.Add(u);
                                max += 1;
                                id += 1;
                            }
                        }
                    }
                }
                if (max == amount)
                {

                    spawnedInWeapons = loadedWeapons;

                    gameObject.GetComponent<WeaponSave>().updateInformation();
                }

            
        }
    }

   
}
