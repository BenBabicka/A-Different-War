using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ItemPickUp : MonoBehaviour {

    public GameObject weaponCanvas;
    public GameObject oldWeapon;
    Toggle toggle;
   public Toggle drop;
    public bool pickup;
    public bool dropitem;
    NavMeshAgent nav;
    
    public GameObject pickUpPoint;
  public  bool pickedUp;

    public GameObject con;
    public GameObject weapon;
    public GameObject contect;
    public RectTransform contectrect;
    UnitInfomation info;

    GameObject gun;
    public bool one1;
  public  bool callpick;
    bool one;
    bool destroy;
    GameObject main;

    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        info = gameObject.GetComponent<UnitInfomation>();
        one = true;

    }

 
    // Update is called once per frame
    void Update()
    {

        if (gun != null)
        {
            one = false;
        }
        if (pickedUp)
        {
            if (one)
            {
                GameObject jobs = Instantiate(con, contect.transform.position, contect.transform.rotation) as GameObject;
                main = jobs;
                gun = jobs.transform.GetChild(0).transform.GetChild(0).GetChild(0).gameObject;
                jobs.gameObject.transform.SetParent(contect.transform);
                jobs.GetComponent<RectTransform>().localScale = con.GetComponent<RectTransform>().localScale;
                jobs.transform.position = con.transform.position;
                gun.transform.position = weapon.transform.position;
                jobs.SetActive(true);
                gun.transform.localScale = weapon.transform.localScale;
                gun.transform.position = weapon.transform.position;
        

                // position.y += postitionToggle;
            
                drop = gun.GetComponent<Toggle>();
            }

        }
  
        if (callpick)
        {
            pickedUp = true;
            weaponCanvas = oldWeapon;
        }

        if (pickedUp)
        {
            callpick = true;
            weaponCanvas = oldWeapon;

            oldWeapon.GetComponent<WeaponTransform>().pick = true;
            if (weaponCanvas.transform.position != pickUpPoint.transform.position && weaponCanvas.transform.rotation != pickUpPoint.transform.rotation)
            {

                weaponCanvas = null;
                pickedUp = false;
                pickup = false;

                
            }
            if (drop.isOn == true)
            {
                dropitem = true;
                gameObject.GetComponent<Shooting>().pickup = false;
                one = true;
                Destroy(gun);
                Destroy(main);

            }
            else
            {
                dropitem = false;
                gameObject.GetComponent<Shooting>().pickup = true;
                one = false;

            }
            if (dropitem)
            {
                if (weaponCanvas != null)
                {
                    weaponCanvas.transform.GetChild(0).transform.gameObject.SetActive(false);

                    weaponCanvas.GetComponent<WeaponTransform>().playersTransform = weaponCanvas.transform;
                    pickup = false;
                    pickedUp = false;
                    weaponCanvas.GetComponent<WeaponTransform>().pick = false;
                    weaponCanvas = null;
                    one1 = false;
                    oldWeapon = null;
                    weaponCanvas = null;
                    callpick = false;
                    drop.isOn = false;
                    dropitem = false;
                    destroy = true;
                }
                toggle.isOn = false;
            }
          
            
        }
            if (pickedUp == false)
            {

                RaycastHit hit;
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && gameObject.GetComponent<SelectableUnitComponent>().Selected == true)
                    {
                        if (hit.transform.gameObject.tag == "Weapon")
                        {
                        if (gameObject.GetComponent<Shooting>().pickup == false)
                        {
                            destroy = false;
                                weaponCanvas = hit.transform.gameObject;
                            
                            if(one1 == false)
                            {
                                oldWeapon = weaponCanvas;
                                one1 = true;
                            }

                            if (weaponCanvas.GetComponent<WeaponTransform>().pick != true)
                            {
                                weaponCanvas.transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }

                        }
                    }
                }

                if (weaponCanvas != null)
                {
                    if (Input.GetMouseButtonDown(1) && weaponCanvas.transform.GetChild(0).gameObject.activeSelf == true)
                    {
                        weaponCanvas.transform.GetChild(0).gameObject.SetActive(false);
                        weaponCanvas = null;
                    }
                    toggle = oldWeapon.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Toggle>();
                    if (toggle.isOn == true)
                    {
                        pickup = true;

                    }
                    else
                    {
                        pickup = false;
                    }


                }

                if (pickup)
                {
                    if (weaponCanvas.GetComponent<WeaponTransform>().pick != true)
                    {
                        float dist = Vector3.Distance(weaponCanvas.transform.position, transform.position);
                        if (dist <= 2)
                        {
                            weaponCanvas.transform.GetChild(0).transform.gameObject.SetActive(false);
                            weaponCanvas.GetComponent<WeaponTransform>().playersTransform = pickUpPoint.transform;
                            nav.SetDestination(gameObject.transform.position);
                            pickedUp = true;

                        }
                        drop.isOn = false;


                    }
                }
            

         
        }
    }



   
}
