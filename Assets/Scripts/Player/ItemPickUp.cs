using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.AI;
[System.Serializable]
public class ItemPickUp : MonoBehaviour {

    [Header("Gameobject References")]
    public GameObject weapon;
    public GameObject firePoint;

  

    //script References
    private NavMeshAgent nav;
    private SelectableUnitComponent unit;
    private WeaponData weapondata;
    private WeaponTransform weapontransform;
    private Inventory inventory;

    public GameObject weaponNotPickedUp;
    public Transform itemPickUp;

    private bool selectedWeaponBool;
    private bool pickingUp;

    [Space]
    [Header ("Bools")]
    public bool hover;
    public bool pickedUp;
    public bool setData;

   // private bool startingWithAWeapon;

    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        unit = gameObject.GetComponent<SelectableUnitComponent>();
        inventory = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        #region Selecting Weapons

        if (unit.Selected)
        {
          
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, 2000f))
                {
                    if (hit.transform.tag == "Weapon")
                    {
                        if (gameObject.GetComponent<Inventory>().forhand_slot.GetComponent<MainSlot>().slotUsed == false)
                        {
                            weaponNotPickedUp = hit.transform.gameObject;
                            if (weaponNotPickedUp.GetComponent<WeaponTransform>().isPickedup == false)
                            {
                                Debug.Log("Picking up weapon");
                                NavMeshPath path = new NavMeshPath();

                                NavMesh.CalculatePath(transform.position, weaponNotPickedUp.transform.position, NavMesh.AllAreas, path);
                               
                                    nav.path = path;

                                

                            }
                        }
                    }
                }
                       
            }
        }

        if (weapon)
        {
            
            weapondata = weapon.GetComponent<WeaponData>();
            weapontransform = weapon.GetComponent<WeaponTransform>();

            weapontransform.playersTransform = firePoint.transform;
            pickedUp = true;
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
                gameObject.GetComponent<Shooting>().shotgun = weapondata.shotgun;
                gameObject.GetComponent<Shooting>().melee = weapondata.melee;

                setData = true;
            }
        }
        if(!weapon)
        {
            pickedUp = false;
        }

        if (weaponNotPickedUp)
        {
            if (Vector3.Distance(transform.position, weaponNotPickedUp.transform.position) < 2)
            {
                weapon = weaponNotPickedUp;

                weapondata = weapon.GetComponent<WeaponData>();
                weapontransform = weapon.GetComponent<WeaponTransform>();

                weapontransform.playersTransform = firePoint.transform;
                if (weapon)
                {
                    
                        if (weapon.GetComponent<ItemData>().Slot == State.forhand)
                        {
                            inventory.forhand = weapon.gameObject;
                        }
                    
                }
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
                    gameObject.GetComponent<Shooting>().shotgun = weapondata.shotgun;
                    gameObject.GetComponent<Shooting>().melee = weapondata.melee;

                    weapon.GetComponent<WeaponTransform>().isPickedup = true;
                    gameObject.GetComponent<Inventory>().forhand_slot.GetComponent<MainSlot>().slotUsed = true;
                    weaponNotPickedUp = null;
                    setData = true;
                }
            }
        }

        #endregion

        #region Pick Up Items
    
        if (unit.Selected)
        {

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == "Item")
                    {

                        itemPickUp = hit.transform;
                        if (itemPickUp)
                        {
                   
                            NavMeshPath path = new NavMeshPath();

                            NavMesh.CalculatePath(transform.position, itemPickUp.transform.position, NavMesh.AllAreas, path);
                            nav.path = path;
                        }
                        
                    }
                }

            }
        }




        if (itemPickUp) {
            if (Vector3.Distance(transform.position, itemPickUp.transform.position) < 2)
            {
                if (itemPickUp)
                {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.Head)
                    {
                        inventory.head = itemPickUp.gameObject;
                        itemPickUp = null;
                    }
                }
                if (itemPickUp)
                {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.Chest)
                    {
                        inventory.chest = itemPickUp.gameObject;
                        itemPickUp = null;
                    }
                }
                if (itemPickUp)
                {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.Legs)
                    {
                        inventory.leg = itemPickUp.gameObject;
                        itemPickUp = null;
                    }
                }
                if (itemPickUp)
                {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.Feet)
                    {
                        inventory.feet = itemPickUp.gameObject;
                        itemPickUp = null;
                    }
                }
                if (itemPickUp)
                {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.Vest)
                    {
                        inventory.vest = itemPickUp.gameObject;
                        itemPickUp = null;
                    }
                }
                if (itemPickUp)
                {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.Belt)
                    {
                        inventory.belt = itemPickUp.gameObject;
                        itemPickUp = null;
                    }
                }
                /*  if (itemPickUp)
              {
                    if (itemPickUp.GetComponent<ItemData>().Slot == State.inventorySlot)
                    {
                        if (itemPickUp.GetComponent<ItemData>().isPickedUp == false)
                        {
                            

                                if (gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().amount <= gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().capacity - 1)
                                {
                                    if (gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name == "" && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName || gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name == itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName)
                                    {
                                        gameObject.GetComponent<Inventory>().slot_Sprite_001.GetComponent<Image>().sprite = itemPickUp.GetComponent<ItemData>().sprite;
                                        gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().amount += 1;
                                        gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name = itemPickUp.GetComponent<ItemData>().itemName;
                                        itemPickUp.gameObject.SetActive(false);
                                        itemPickUp.GetComponent<ItemData>().isPickedUp = true;
                                    }
                                    else
                                     {

                                    if (gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name == "" && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName || gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name == itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName)
                                    {
                                        gameObject.GetComponent<Inventory>().slot_Sprite_002.GetComponent<Image>().sprite = itemPickUp.GetComponent<ItemData>().sprite;
                                        gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().amount += 1;
                                        gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name = itemPickUp.GetComponent<ItemData>().itemName;
                                        itemPickUp.gameObject.SetActive(false);
                                        itemPickUp.GetComponent<ItemData>().isPickedUp = true;
                                    }
                                    else
                                    {

                                        if (gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name == "" && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName || gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name == itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName)
                                        {
                                            gameObject.GetComponent<Inventory>().slot_Sprite_003.GetComponent<Image>().sprite = itemPickUp.GetComponent<ItemData>().sprite;
                                            gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().amount += 1;
                                            gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name = itemPickUp.GetComponent<ItemData>().itemName;

                                            itemPickUp.gameObject.SetActive(false);
                                            itemPickUp.GetComponent<ItemData>().isPickedUp = true;
                                        }
                                        else
                                        {

                                            if (gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name == "" && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName || gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name == itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName)
                                            {
                                                gameObject.GetComponent<Inventory>().slot_Sprite_004.GetComponent<Image>().sprite = itemPickUp.GetComponent<ItemData>().sprite;
                                                gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().amount += 1;
                                                gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name = itemPickUp.GetComponent<ItemData>().itemName;

                                                itemPickUp.gameObject.SetActive(false);
                                                itemPickUp.GetComponent<ItemData>().isPickedUp = true;
                                            }
                                            else
                                            {

                                                if (gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name == "" && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName || gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name == itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_002.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_003.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_004.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName && gameObject.GetComponent<Inventory>().slot_001.GetComponent<InventorySlot>().occupied_Name != itemPickUp.GetComponent<ItemData>().itemName)
                                                {
                                                    gameObject.GetComponent<Inventory>().slot_Sprite_005.GetComponent<Image>().sprite = itemPickUp.GetComponent<ItemData>().sprite;
                                                    gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().amount += 1;
                                                    gameObject.GetComponent<Inventory>().slot_005.GetComponent<InventorySlot>().occupied_Name = itemPickUp.GetComponent<ItemData>().itemName;
                                                    itemPickUp.gameObject.SetActive(false);
                                                    itemPickUp.GetComponent<ItemData>().isPickedUp = true;
                                                }
                                                else
                                                {

                                                    return;
                                                }
                                                 }
                                             }
                                          }
                                        }
                                                         
                                 }
                              }
                   
                                                 
                        }
                    }*/
            }

        }
        }

        #endregion


        /*  if (pickedUp == false)
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
      if (weapon != null)
      {
          weapondata = weapon.GetComponent<WeaponData>();
          weapontransform = weapon.GetComponent<WeaponTransform>();

          weapontransform.playersTransform = firePoint.transform;

          if (!setData)
          {
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

          }
      }*/
    

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
