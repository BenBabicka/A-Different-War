using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI; 

public class EnemyItemPickup : MonoBehaviour
{

    [Header("Gameobject References")]
    public GameObject weapon;
    public GameObject weaponCanvas;
    public GameObject firePoint;

    private WeaponData weapondata;
    private WeaponTransform weapontransform;
    private GameObject weaponNotPickedUp;

    private NavMeshAgent nav;


    private bool selectedWeaponBool;
    private bool pickingUp;
    private bool setData;

    [Space]
    [Header("Bools")]
    public bool hover;
    public bool pickedUp;

    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
    
        if(weaponNotPickedUp == null)
        {
            FindWeapons();
        }

       if(weaponNotPickedUp)
        {
    if (pickedUp == false)
            {

                nav.SetDestination(weaponNotPickedUp.transform.position);
                pickingUp = true;
                if (Vector3.Distance(transform.position, weaponNotPickedUp.transform.position) <= 2)
                {
                    weapon = weaponNotPickedUp;
                    pickedUp = true;
                    pickingUp = false;
                }

            }
        }
        

           
        
        if (weapon != null)
        {
            weapondata = weapon.GetComponent<WeaponData>();
            weapontransform = weapon.GetComponent<WeaponTransform>();

            weapontransform.playersTransform = firePoint.transform;

            if (!setData)
            {

                gameObject.GetComponent<Shooting>().Spread = weapondata.spread;

                gameObject.GetComponent<Shooting>().aimTime = weapondata.aimTime;
                gameObject.GetComponent<Shooting>().reloadAmount = weapondata.reloadAmount;
                gameObject.GetComponent<Shooting>().ReloadSpeed = weapondata.ReloadSpeed;
                gameObject.GetComponent<Shooting>().bulletSpeed = weapondata.bulletSpeed;
                gameObject.GetComponent<Shooting>().timeBetweenShots = weapondata.fireRate;
                gameObject.GetComponent<Shooting>().ammo = weapondata.ammo;
                gameObject.GetComponent<Shooting>().Damager = weapondata.damage;
                gameObject.GetComponent<Shooting>().shotCounter = gameObject.GetComponent<Shooting>().aimTime;
                gameObject.GetComponent<Shooting>().ammo = gameObject.GetComponent<Shooting>().reloadAmount;
                gameObject.GetComponent<Shooting>().reloadTime = gameObject.GetComponent<Shooting>().ReloadSpeed;
                gameObject.GetComponent<Shooting>().gameObject.GetComponent<FieldOfView>().viewRadius = weapondata.range;
                setData = true;
            }
        }

    }

    public void FindWeapons()
    {


        GameObject[] Weapons;
        Weapons = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject closestWeapon = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;


        foreach (GameObject go in Weapons)
        {
            if (go.GetComponent<WeaponTransform>().isPickedup == false)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestWeapon = go;
                    distance = curDistance;

                    weaponNotPickedUp = closestWeapon.transform.gameObject;
                    
                    selectedWeaponBool = true;
                }
            }
        }
    }
}
