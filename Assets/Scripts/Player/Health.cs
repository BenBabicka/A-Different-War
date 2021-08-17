using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;

public class Health : MonoBehaviour
{

    public float health;
    public float MaxHealth;

    public GameObject deadSprite;
    public GameObject sprtieManager;

    public Component[] toDisable;
    public GameObject[] gameObjectsToDisable;

    private Toggle toggle;


    float posX;
    float posY;
    float posZ;


    public LayerMask deadlayer;

    public Vector3 center;
    public Vector3 size = new Vector3(5, 5, 5);

    public GameObject blood;

    public bool unconscious;



    void Start()
    {
        health = MaxHealth;
    }

   


    void Update()
    {
        center = transform.position;


     
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

       

        if (health <= 0)
        {
            gameObject.transform.SetParent(GameObject.Find("Dead").transform);
            if (gameObject.GetComponent<ItemPickUp>() != null)
            {
                if (gameObject.GetComponent<ItemPickUp>().weapon != null)
                {
                    gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponTransform>().playersTransform = null;
                    gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponTransform>().playersTransform = null;

                    toggle = gameObject.GetComponent<ItemPickUp>().weapon.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Toggle>();
                    toggle.isOn = false;
                    gameObject.tag = "Dead";

                }
            }
            foreach (var component in toDisable)
            {
                Destroy(component);
            }

            foreach (var obj in gameObjectsToDisable)
            {
                Destroy(obj);
            }


            transform.position = new Vector3(posX, posY, posZ);
            sprtieManager.SetActive(false);
            deadSprite.SetActive(true);
            gameObject.tag = "Dead";
            gameObject.layer = 14;

        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health -= col.GetComponent<Damage>().damage;
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.5f, Random.Range(-size.z / 2, size.z / 2));
            GameObject g = Instantiate(blood, pos, Quaternion.Euler(90, Random.Range(0, 360), 0));
            Destroy(col.gameObject);
        }



    }
}
