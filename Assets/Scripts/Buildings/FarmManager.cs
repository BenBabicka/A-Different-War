using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

    public enum crop
    {
         Carrot, Potato, Cabbage
    }

public class FarmManager : MonoBehaviour {

    public float cropProgress;
    SpawnArea spawn;
    public float speed;

    bool farm;

    public GameObject player;


     NavMeshAgent nav;

    public Vector3 center;
    public Vector3 size;
    public float maxObj;

   public float amount;

    public GameObject cropPrefab;
    public GameObject[] cropPrefabs;

    public List<GameObject> cropList;

    public GameObject ui;

    public crop crop;

    public int CropType;
    public string cropTypeString;
    GameManager manager;

    float plantTime = 3;

    bool planting;
    bool canPlant;

    float loadamount;
    float ID;
 public   bool load;
 public   GameObject plants;
    void Start()
    {
        spawn = gameObject.GetComponent<SpawnArea>();
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

   
    

    void Update()
    {
        if (gameObject.GetComponent<Building>().buildProgress >= 100)
        {
            if (load)
            {
                Load();
            }
            if (!planting)
            {
                crop = (crop)CropType;
            }
            cropTypeString = crop.ToString();
            if (CropType != -1)
            {
                cropPrefab = cropPrefabs[CropType];
            }
            center = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z + 0.09f);
            if (player)
            {
                nav = player.GetComponent<NavMeshAgent>();
            }
            if (canPlant && player)
            {
                NavMeshPath path = new NavMeshPath();

                NavMesh.CalculatePath(transform.position, gameObject.transform.position, NavMesh.AllAreas, path);
                nav.path = path;
            }
            if (amount == 0)
            {
                canPlant = true;
                ID = 0;
            }
            else
            {
                canPlant = false;
            }

            if (player)
            {
                if (CropType != -1)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) <= 4)
                    {
                        //PLANTING
                        Debug.Log("planting");
                        plantTime -= Time.deltaTime;
                        if (amount <= maxObj - 1)
                        {
                            if (plantTime <= 0)
                            {
                                planting = true;
                                ID += 1;
                                Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.5f, Random.Range(-size.z / 2, size.z / 2));
                                GameObject g = Instantiate(cropPrefab, pos, Quaternion.Euler(90, 0, 0));
                                if (!cropList.Contains(g))
                                {
                                    cropList.Add(g);
                                }
                                g.GetComponent<Crop>().Farm = gameObject;
                                g.GetComponent<Crop>().ID = ID;
                                g.transform.SetParent(plants.transform);
                                amount += 1;
                                plantTime = 3;

                            }
                        }
                        else
                        {
                            planting = false;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 2000))
                {
                    if (manager.canMove)
                    {
                        if (hit.transform.name == gameObject.transform.name)
                        {
                            ui.SetActive(true);
                        }
                        else
                        {
                            ui.SetActive(false);

                        }
                    }
                    Debug.Log(hit.transform.name);
                }

            }
        }
    }

    public void Save()
    {
        Debug.Log("Save");


        PlayerPrefs.SetFloat("amount" + gameObject.GetComponent<BuildingID>().ID, amount);
        PlayerPrefs.GetInt("farmCrop" + gameObject.GetComponent<BuildingID>().ID, CropType);
        foreach (var item in cropList)
        {
            PlayerPrefs.SetFloat("cropPosX" + item.GetComponent<Crop>().ID, item.transform.position.x);
            PlayerPrefs.SetFloat("cropPosY" + item.GetComponent<Crop>().ID, item.transform.position.y);
            PlayerPrefs.SetFloat("cropPosZ" + item.GetComponent<Crop>().ID, item.transform.position.z);

        }

    }

    public void Load()
    {
        Debug.Log("Load");

        amount = PlayerPrefs.GetFloat("amount" + gameObject.GetComponent<BuildingID>().ID);
        CropType = PlayerPrefs.GetInt("farmCrop" + gameObject.GetComponent<BuildingID>().ID);
        if (loadamount < amount)
        {

            ID += 1;

            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0.5f, Random.Range(-size.z / 2, size.z / 2));
                GameObject g = Instantiate(cropPrefab, pos, Quaternion.Euler(90, 0, 0));
                if (!cropList.Contains(g))
                {
                    cropList.Add(g);
                }
                g.GetComponent<Crop>().Farm = gameObject;
            g.GetComponent<Crop>().ID = ID;
            loadamount += 1;

            foreach (var item in cropList)
            {
                item.transform.position= new Vector3(PlayerPrefs.GetFloat("cropPosX" + item.GetComponent<Crop>().ID), PlayerPrefs.GetFloat("cropPosY" + item.GetComponent<Crop>().ID), PlayerPrefs.GetFloat("cropPosZ" + item.GetComponent<Crop>().ID));            

            }
            return;
        }

    }

    public void changeCropType(int type)
    {
        Debug.Log("Clicked");
        CropType = type;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(center, size);
    }
}
