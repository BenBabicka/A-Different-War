using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class Shooting : MonoBehaviour {


    public bool isFiring;

    public BulletControl bullet;
    public float bulletSpeed;

    public float timeBetweenShots;

    public Transform firePoint;
   public float shotCounter;
    public float aimTime;

    public float ammo;
    public float reloadAmount;
    public float ReloadSpeed;
    public float reloadTime;
    public float Damager;
    [Space]
    public float Spread;
    [Space]
    private bool isReload;
    private bool one;
    private bool oneShotAnimal;

    private float nextActionTime =1f;
    private float period = 1f;

    public bool pickup;

    WeaponData weapondata;

    public bool isPlayer;
    [HideInInspector]
    public bool ShootingBool;

   public GameObject gunShot;

    float time = 5;

    public bool enemy;

    bool foundTarget;
    [HideInInspector]
    public bool canShoot;

    [Space]

public bool shotgun;
    public bool melee;

    public float aimDamping;

    void Start()
    {
        shotCounter = aimTime;
        ammo = reloadAmount;
        reloadTime = ReloadSpeed;
        one = true;
        
    }

   public IEnumerator Aim()
    {
        if (gameObject.GetComponent<FieldOfView>().targetgameobject)
        {
            float velocity = gameObject.GetComponent<NavMeshAgent>().velocity.magnitude;
            if (velocity < 0.2f)
            {
                if (!gameObject.GetComponent<NavMeshAgent>().isStopped)
                {
                    var lookPos = gameObject.GetComponent<FieldOfView>().targetgameobject.transform.position - transform.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * aimDamping);

                    yield return new WaitForSeconds(aimTime);
                    isFiring = true;
                    Shoot();
                }
            }
        }    
    }

    public void AnimalAim()
    {
        if (gameObject.GetComponent<JobManager>().hunter)
        {
            gameObject.transform.LookAt(gameObject.GetComponent<JobManager>().closeAnimal.transform);
            time -= Time.deltaTime;
            if (time <= 0)
            {

                oneShotAnimal = false;
               StartCoroutine( AnimalShoot());
                time += weapondata.aimTime;
            }
        }
    }



    void Update()
    {
        if (isPlayer)
        {
            if (gameObject.GetComponent<ItemPickUp>().weapon)
            {
                weapondata = gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponData>();
            }
        }
        if(enemy)
        {
            if (gameObject.GetComponent<EnemyItemPickup>().weapon)
            {
                weapondata = gameObject.GetComponent<EnemyItemPickup>().weapon.GetComponent<WeaponData>();
            }

        }
        if(ammo <= 0)
        {
            if (!melee)
            {
                if (!isReload)
                {
                    reloadTime -= Time.deltaTime;
                    if (reloadTime <= 0)
                    {
                        StartCoroutine(Reload(ReloadSpeed));
                    }
                }
            }
        }
        if(ammo == reloadAmount)
        {
            isReload = true;
        }
        else
        {
            isReload = false;
                }
        if (Time.time > nextActionTime)
        {
            nextActionTime += timeBetweenShots;
            shotCounter -= 1;
        }

        if(shotCounter <= 0)
        {
            shotCounter = 0;
        }
        if (foundTarget)
        {
            if (canShoot)
            {
                StartCoroutine(Aim());
            }
        }

        if (gameObject.GetComponent<FieldOfView>().targetgameobject)
        {
            foundTarget = true;
        }
        else
        {
            foundTarget = false;
        }

       
    }

    IEnumerator  Reload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        reloadTime = ReloadSpeed;
        ammo = reloadAmount;
    }

    public void Shoot()
    {
        if (gameObject.GetComponent<FieldOfView>().targetgameobject)
        {
            if (melee)
            {
                if (Vector3.Distance(transform.position, gameObject.GetComponent<FieldOfView>().targetgameobject.transform.position) > (weapondata.meleeRange*2))
                {
                    NavMeshPath path = new NavMeshPath();

                    NavMesh.CalculatePath(transform.position, gameObject.GetComponent<FieldOfView>().targetgameobject.transform.position, NavMesh.AllAreas, path);
                    gameObject.GetComponent<NavMeshAgent>().path = path;
                }
                else
                {
                    
                        gameObject.GetComponent<NavMeshAgent>().ResetPath();
                     Debug.Log("Melee");
                }
                if (isFiring)
                {
                    if (shotCounter <= 0)
                    {
                        ShootingBool = true;



                        shotCounter = timeBetweenShots;
                        RaycastHit hit;

                        Vector3 fwd = transform.TransformDirection(Vector3.forward);
                        if (Physics.Raycast(transform.position, fwd, out hit, weapondata.meleeRange))
                        {
                            if (hit.transform.GetComponent<Health>())
                            {
                                hit.transform.GetComponent<Health>().health -= weapondata.damage;
                            }


                        }

                    }
                }
                if (isFiring == false)
                {
                    ShootingBool = false;
                }
            }
            if (ammo > 0)
            {
                if (shotgun)
                {
                    if (isFiring)
                    {
                        if (shotCounter <= 0)
                        {
                            ammo -= 1;
                            ShootingBool = true;
                            shotCounter = timeBetweenShots;

                            Vector3 shotGunFirePoint = firePoint.transform.position + firePoint.transform.forward;

                            BulletControl newbullet = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            BulletControl newbullet2 = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            BulletControl newbullet3 = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            BulletControl newbullet4 = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            BulletControl newbullet5 = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            BulletControl newbullet6 = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            BulletControl newbullet7 = Instantiate(bullet, shotGunFirePoint, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;

                            newbullet.speed = bulletSpeed;
                            newbullet2.speed = bulletSpeed;
                            newbullet3.speed = bulletSpeed;
                            newbullet4.speed = bulletSpeed;
                            newbullet5.speed = bulletSpeed;
                            newbullet6.speed = bulletSpeed;
                            newbullet7.speed = bulletSpeed;

                            GameObject sound = GameObject.Instantiate(gunShot, gameObject.transform.position, transform.rotation) as GameObject;
                            newbullet.GetComponent<Damage>().damage = Damager;
                            newbullet2.GetComponent<Damage>().damage = Damager;
                            newbullet3.GetComponent<Damage>().damage = Damager;
                            newbullet4.GetComponent<Damage>().damage = Damager;
                            newbullet5.GetComponent<Damage>().damage = Damager;
                            newbullet6.GetComponent<Damage>().damage = Damager;
                            newbullet7.GetComponent<Damage>().damage = Damager;

                        }
                    }
                    if (isFiring == false)
                    {
                        ShootingBool = false;
                    }
                }
                else
                {
                    if (isFiring)
                    {
                        if (shotCounter <= 0)
                        {
                            ammo -= 1;
                            ShootingBool = true;
                            shotCounter = timeBetweenShots;
                            BulletControl newbullet = Instantiate(bullet, firePoint.position, Quaternion.Euler(transform.rotation.x, gameObject.GetComponent<ItemPickUp>().weapon.transform.localRotation.eulerAngles.y + Random.Range(-Spread, Spread), transform.rotation.z)) as BulletControl;
                            newbullet.speed = bulletSpeed;
                            GameObject sound = GameObject.Instantiate(gunShot, gameObject.transform.position, transform.rotation) as GameObject;
                            newbullet.GetComponent<Damage>().damage = Damager;

                        }
                    }
                    if (isFiring == false)
                    {
                        ShootingBool = false;
                    }

                }
            }
        }
    }

     IEnumerator AnimalShoot()
    {
       
        if (ammo >= 1)
        {
            if (!oneShotAnimal)
            {
                yield return new WaitForSeconds(0.1f);
                    oneShotAnimal = true; 
                   ammo -= 1;
                    shotCounter = timeBetweenShots;
                    BulletControl newbullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletControl;
                    newbullet.speed = bulletSpeed;
                    GameObject sound = GameObject.Instantiate(gunShot, gameObject.transform.position, transform.rotation) as GameObject;
                    newbullet.GetComponent<Damage>().damage = Damager;
                AnimalAim();
            }
        }
    }
}
