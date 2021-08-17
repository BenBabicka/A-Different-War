using ProceduralToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSave : MonoBehaviour
{
    public float amount;

    public List<GameObject> weapons;

    public List<int> weapon_ids;

    public List<int> weapon_spawn_id;

    public List<Vector3> positions;
    public List<Vector3> rotations;

    public List<bool> playersTransforms;
    public List<int> playerIDs;
    public void updateInformation()
    {
        amount = weapons.Count;
        weapon_ids.Clear();
        positions.Clear();
        rotations.Clear();
        weapon_spawn_id.Clear();
        playersTransforms.Clear();
        playerIDs.Clear();
        for (int i = 0; i < weapons.Count;)
        {
            weapon_ids.Add(weapons[i].GetComponent<ItemData>().item_ID);
            weapon_spawn_id.Add(weapons[i].GetComponent<WeaponData>().spawn_ID);
            positions.Add(weapons[i].transform.position);
            rotations.Add(weapons[i].transform.eulerAngles);
            playersTransforms.Add(weapons[i].GetComponent<WeaponTransform>().playersTransform);
            playerIDs.Add(weapons[i].GetComponent<WeaponTransform>().playerID);
            i++;
        }
        GameObject.Find("SaveManager").GetComponent<SaveManager>().UpdateWeaponSave();

    }

    public void loadInformation()
    {
        weapons.Clear();

            gameObject.GetComponent<WeaponSpawner>().amount = amount;
            gameObject.GetComponent<WeaponSpawner>().weapon_ids = weapon_ids;
            gameObject.GetComponent<WeaponSpawner>().weapon_spawn_id = weapon_spawn_id;
            gameObject.GetComponent<WeaponSpawner>().positions = positions;
            gameObject.GetComponent<WeaponSpawner>().rotations = rotations;
            gameObject.GetComponent<WeaponSpawner>().playerTransforms = playersTransforms;
            gameObject.GetComponent<WeaponSpawner>().playerIDs = playerIDs;


    }

}
