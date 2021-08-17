using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyShooting : MonoBehaviour {

    public float AimTime;
    public GameObject enemy;

    public bool isFiring;

    public BulletControl bullet;
    public float bulletSpeed;

    public float fireRate;

    public Transform firePoint;
    public float shotCounter;
    public float aimTime;

    public float ammo;
    public float reloadAmount;
    public float ReloadSpeed;
    public float reloadTime;
    public float Damager;


    bool isreloading;

    WeaponData weapondata;



    public GameObject gunShot;


    // Use this for initialization
    void Start () {
		
	}

    public IEnumerator Aim()
    {
        isFiring = true;
        transform.LookAt(enemy.transform);
        gameObject.GetComponent<NavMeshAgent>().speed /= 2;

        yield return new WaitForSeconds(AimTime);
        Shoot();

    }


   

    public void Shoot()
    {
        if (ammo >= 1)
        {
            isFiring = false;

                    BulletControl newbullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletControl;
                    newbullet.speed = bulletSpeed;
                    GameObject sound = GameObject.Instantiate(gunShot, gameObject.transform.position, transform.rotation) as GameObject;
                    newbullet.GetComponent<Damage>().damage = Damager;
            gameObject.GetComponent<NavMeshAgent>().speed *= 2 ;

            ammo -= 1;

        }


    }
    
    // Update is called once per frame
    void Update () {
        if (!enemy)
        {
            if (gameObject.GetComponent<MissionFieldOfView>().visibleTargets.Count >= 1)
            {
                closestEnemy();
            }
        }
        if(enemy && !isFiring && ammo >= 0)
        {
            StartCoroutine(Aim());
        }
        if(ammo == 0)
        {
            StartCoroutine( Reload());
        }
        if(isreloading)
        {
            isFiring = false;
        }
	}

    IEnumerator Reload()
    {
        isreloading = true;

        yield return new WaitForSeconds(reloadTime);
        ammo = reloadAmount;
        isreloading = false;
    }
    void closestEnemy()
    {
        foreach (var Enemy in gameObject.GetComponent<MissionFieldOfView>().visibleTargets)
        {
            GameObject closestPlayer = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            
                Vector3 diff = Enemy.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestPlayer = Enemy.gameObject;
                    enemy = closestPlayer;
                    distance = curDistance;

                }
            
        }
    }

}
