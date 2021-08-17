using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Inventory : MonoBehaviour {


    [Header("Slots")]
    public GameObject head_slot;
    public GameObject chest_slot;
    public GameObject leg_slot;
    public GameObject feet_slot;
    public GameObject forhand_slot;
    public GameObject vest_slot;
    public GameObject belt_slot;

    [Space]
    [Header("References")]
    public GameObject head;
    public GameObject chest;
    public GameObject leg;
    public GameObject feet;
    public GameObject forhand;
    public GameObject vest;
    public GameObject belt;
    public GameObject inventory_panel;

    [Space]
    [Header("Position")]
    public GameObject head_Position;
    public GameObject chest_Position;
    public GameObject leg_Position;
    public GameObject feet_Position;
    public GameObject forhand_Position;
    public GameObject vest_Position;
    public GameObject belt_Position;

    [Space]
    [Header("Buttons")]
    public Button head_button;
    public Button chest_button;
    public Button leg_button;
    public Button feet_button;
    public Button forhand_button;
    public Button vest_button;
    public Button belt_button;

    [Space]
    [Header("Sprites")]
    public Sprite head_Sprite;
    public Sprite chest_Sprite;
    public Sprite leg_Sprite;
    public Sprite feet_Sprite;
    public Sprite forhand_Sprite;
    public Sprite vest_Sprite;
    public Sprite belt_Sprite;


    [Space]
    [Header("Scripts")]
    public OnHover hover;
    private ItemPickUp pickUp;
    private WeaponTransform weapontransform;

    public class inventorySlot
    {
        public GameObject slot;
        public GameObject slot_sprite;
        public GameObject slot_button;
        public Sprite slot_orignal_sprite;
        public Text slot_text;
        public bool emptySlot;
        public bool slotFull;
        public int slot_id;
        public inventorySlot(GameObject slo, GameObject slo_spr, GameObject slo_but, Text slo_tex, Sprite org, int id)
        {
            slot = slo;
            slot_sprite = slo_spr;
            slot_button = slo_but;
            slot_text = slo_tex;
            slot_orignal_sprite = org;
            slot_id = id;
        }
    }

    public List<inventorySlot> slots = new List<inventorySlot>();

    public GameObject slotPrefab;
    public GameObject inventoryPanel;
    


    private StorageInventory storage;
    private NavMeshAgent nav;
    public int amountOfSlots;
    public int id;

    bool emptySlot;
    int slotNumber;

    void Start () {
        pickUp = gameObject.GetComponent<ItemPickUp>();
        if (GameObject.Find("Storage"))
        {
            storage = GameObject.Find("Storage").GetComponent<StorageInventory>();
        }
        nav = gameObject.GetComponent<NavMeshAgent>();
       
          
    }

    void Update () {
        if (slots.Count < amountOfSlots)
        {
            GameObject slot = Instantiate(slotPrefab, transform.position, Quaternion.identity);
            slot.transform.SetParent(inventoryPanel.transform);
            slot.transform.localScale = inventoryPanel.transform.localScale;
            slot.name = "Slot_" + id;
            slot.gameObject.GetComponent<InventorySlot>().slotId = id;
            slots.Add(new inventorySlot(slot, slot.transform.GetChild(0).gameObject, slot.transform.GetChild(0).GetChild(0).gameObject, slot.transform.GetChild(1).gameObject.GetComponent<Text>(), slot.GetComponent<InventorySlot>().org_sprite, id));
            id += 1;
        }
        for (int i = 0; i < slots.Count; i++)
        {

            slots[i].slot_text.text = slots[i].slot.GetComponent<InventorySlot>().amount.ToString();
          
        }

      
     


     

        if (head)
        {
            head.transform.SetParent(head_Position.transform);

            head.transform.position = head_Position.transform.position;
            head_slot.transform.GetChild(0).GetComponent<Image>().sprite = head.GetComponent<ItemData>().sprite;
            head_button.onClick.AddListener(Head_Clicked);

        }
        if (chest)
        {
            chest.transform.SetParent(chest_Position.transform);

            chest.transform.position = chest_Position.transform.position;
            chest_Position.transform.GetChild(0).GetComponent<Image>().sprite = chest.GetComponent<ItemData>().sprite;
            chest_button.onClick.AddListener(Chest_Clicked);

        }
        if (leg)
        {
            leg.transform.SetParent(leg_Position.transform);
                
            leg.transform.position = leg_Position.transform.position;
            leg_slot.transform.GetChild(0).GetComponent<Image>().sprite = leg.GetComponent<ItemData>().sprite;
            leg_button.onClick.AddListener(Leg_Clicked);

        }
        if (feet)
        {
            feet.transform.SetParent(feet_Position.transform);

            feet.transform.position = feet_Position.transform.position;
            feet_slot.transform.GetChild(0).GetComponent<Image>().sprite = feet.GetComponent<ItemData>().sprite;
            feet_button.onClick.AddListener(Feet_Clicked);

        }
        if (vest)
        {
            vest.transform.SetParent(vest_Position.transform);

            vest.transform.position = vest_Position.transform.position;
            vest_slot.transform.GetChild(0).GetComponent<Image>().sprite = vest.GetComponent<ItemData>().sprite;
            vest_button.onClick.AddListener(Vest_Clicked);

        }
        if (belt)
        {
            belt.transform.SetParent(belt_Position.transform);

            belt.transform.position = belt_Position.transform.position;
            belt_slot.transform.GetChild(0).GetComponent<Image>().sprite = belt.GetComponent<ItemData>().sprite;
            belt_button.onClick.AddListener(Belt_Clicked);

        }
        if (forhand)
        {
           
            forhand_slot.transform.GetChild(0).GetComponent<Image>().sprite = forhand.GetComponent<ItemData>().sprite;
            forhand_button.onClick.AddListener(Forhand_Clicked);

        }

      

        if(emptySlot)
        {
            if (slots[slotNumber].slot.GetComponent<InventorySlot>().amount > 0)
            {
                Debug.Log("Inventory Empty slot");

                storage = GameObject.Find("Storage").GetComponent<StorageInventory>();

                gameObject.GetComponent<JobManager>().injob = true;
                NavMeshPath path = new NavMeshPath();

                NavMesh.CalculatePath(transform.position, storage.transform.position, NavMesh.AllAreas, path);
                nav.path = path;

                if (Vector3.Distance(transform.position, storage.transform.position) <= 2)
                {
                    if (storage.dictionary.ContainsKey(slots[slotNumber].slot.GetComponent<InventorySlot>().occupied_Name))
                    {
                        //increases item in storage
                        storage.dictionary[slots[slotNumber].slot.GetComponent<InventorySlot>().occupied_Name] += 1;
                        slots[slotNumber].slot.GetComponent<InventorySlot>().amount -= 1;

                    }
                    else
                    {
                        //Adds New item
                        storage.dictionary.Add(slots[slotNumber].slot.GetComponent<InventorySlot>().occupied_Name, 1);
                        slots[slotNumber].slot.GetComponent<InventorySlot>().amount -= 1;

                    }
                }
            }
            else
            {
                gameObject.GetComponent<JobManager>().injob = false;
                emptySlot = false;
            }
        }
    }
   public void OpenAndClose()
    {
        if (inventory_panel.GetComponent<CanvasGroup>().alpha == 0)
        {
            inventory_panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            inventory_panel.GetComponent<CanvasGroup>().alpha = 1;
            Debug.Log("Open Inventory");
            
        }
        else
        {
            inventory_panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            inventory_panel.GetComponent<CanvasGroup>().alpha = 0;
            Debug.Log("close Inventory");

        }
    }

    void Head_Clicked()
    {
        if (head)
        {
            head.transform.parent = null;
            head.transform.position = head.transform.position;
            head_slot.transform.GetChild(0).GetComponent<Image>().sprite = head_Sprite;
            head = null;
        }
    }

    void Chest_Clicked()
    {

    }

    void Feet_Clicked()
    {

    }

    void Belt_Clicked()
    {

    }

    void Leg_Clicked()
    {

    }

    void Vest_Clicked()
    {

    }

    void Forhand_Clicked()
    {
        if (forhand)
        {
            weapontransform = forhand.GetComponent<WeaponTransform>();
            weapontransform.playersTransform = null;
            forhand.transform.position = forhand.transform.position;
            forhand_slot.transform.GetChild(0).GetComponent<Image>().sprite = forhand_Sprite;
            gameObject.GetComponent<ItemPickUp>().weapon = null;
            gameObject.GetComponent<ItemPickUp>().weaponNotPickedUp = null;
            gameObject.GetComponent<ItemPickUp>().setData = false;
            gameObject.GetComponent<Shooting>().aimTime = 0;
            gameObject.GetComponent<Shooting>().reloadAmount = 0;
            gameObject.GetComponent<Shooting>().Spread = 0;
            gameObject.GetComponent<Shooting>().ReloadSpeed = 0;
            gameObject.GetComponent<Shooting>().bulletSpeed = 0;
            gameObject.GetComponent<Shooting>().timeBetweenShots = 0;
            gameObject.GetComponent<Shooting>().ammo = 0;
            gameObject.GetComponent<Shooting>().Damager = 0;
            gameObject.GetComponent<Shooting>().shotCounter = 0;
            gameObject.GetComponent<Shooting>().ammo = 0;
            gameObject.GetComponent<Shooting>().reloadTime = 0;
            gameObject.GetComponent<Shooting>().gameObject.GetComponent<FieldOfView>().viewRadius = 0;
            gameObject.GetComponent<Inventory>().forhand_slot.GetComponent<MainSlot>().slotUsed = false;
            forhand = null;
        }
    }

    public void EmptySlot(int whatSlot)
    {

        emptySlot = true;
        slotNumber = whatSlot;

        
    }
}
