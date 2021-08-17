using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;

public class Building : MonoBehaviour {

   public float buildSpeed;
    public float buildProgress;

    GameObject building;

    public List<string> materials;
    public List<float> materialAmount;
    [HideInInspector]
    public List<string> currentMaterialsUsed;
    [HideInInspector]

    public List<float>  currentMaterialsUsedAmount;


    public GameObject player;
    public bool built;
    [HideInInspector]
    public bool build;

    
    public Behaviour[] ComponentEnable;
    public Collider[] colliders;

   public bool isWall;
    
    public Material WallMat;
    public GameObject sprite;


    public string buildingTag;


    public Sprite finishedSprite;

    public GameObject FinshedSpriteLoaction;
    public float FinshedSpriteSize;

    public bool selected;
    public GameObject selectedInformation;
     Transform UIParent;
    GameObject selectedInformationInstance;
  public  List<Transform>allSprites;
    public List<Transform> insideGameobject;

    public bool hasCheckForObjectsInside;
    public GameObject informationHolder;
    // Use this for initialization
    void Awake()
    {
        building = gameObject.transform.GetChild(0).gameObject;
        currentMaterialsUsedAmount = new List<float>(new float[materialAmount.Count]);
        UIParent = GameObject.Find("Manager").GetComponent<GameManager>().UICanvas;
        if (gameObject.GetComponent<BuildingStartUp>().hasPlaced == true)
        {
            for (int i = 0; i < materials.Count; i++)
            {

                /*if (materials[i] == "Wood")
                {
                    GameObject.Find("Storage").GetComponent<StorageData>().Wood -= (int)materialAmount[i];
                }
                if (materials[i] == "Stone")
                {
                    GameObject.Find("Storage").GetComponent<StorageData>().Stone -= (int)materialAmount[i];
                }
                if (materials[i] == "Textiles")
                {
                    GameObject.Find("Storage").GetComponent<StorageData>().Textiles -= (int)materialAmount[i];
                }
                if (materials[i] == "Food")
                {
                    GameObject.Find("Storage").GetComponent<StorageData>().Food -= (int)materialAmount[i];
                }
                if (materials[i] == "Credits")
                {
                    GameObject.Find("Storage").GetComponent<StorageData>().Credits -= (int)materialAmount[i];
                }*/
                GameObject.Find("Storage").GetComponent<StorageInventory>().dictionary[materials[i]] -= materialAmount[i];
            }
        }
        
    }


  void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        informationHolder = GameObject.Find("InformationHolder");
        foreach (Transform nature in GameObject.Find("Manager").GetComponent<GameManager>().allNature)
        {
            if (nature.GetComponent<Nature>().Name == "Tree")
            {
                if (transform.GetComponent<BoxCollider>().bounds.Contains(nature.position))
                {


                    Debug.Log("get Nature in object");

                    if (!insideGameobject.Contains(nature))
                    {
                        insideGameobject.Add(nature);

                    }

                    if (gameObject.tag == "Storage")
                    {
                        nature.gameObject.SetActive(false);
                    }

                }
            }
            else
            {
                if (transform.GetComponent<BoxCollider>().bounds.Contains(nature.GetChild(0).position))
                {


                    Debug.Log("get Nature in object");

                    if (!insideGameobject.Contains(nature))
                    {
                        insideGameobject.Add(nature);

                    }

                    if (gameObject.tag == "Storage")
                    {
                        nature.gameObject.SetActive(false);
                    }

                }
            }
        }
                hasCheckForObjectsInside = true;
        
       
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        

        if (selectedInformationInstance)
        {
            selectedInformationInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<BuildingID>().Name;
            if (buildProgress < 100f)
            {
                selectedInformationInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Build Progress: " + (int)buildProgress + "\n" + gameObject.GetComponent<BuildingID>().Information;
            }
            else
            {
                selectedInformationInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<BuildingID>().Information;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {

            selected = false;

            if (Physics.Raycast(ray, out hit, 5000))
            {
                if(hit.transform == transform)
                {
                    if(selectedInformationInstance)
                    {
                        Destroy(selectedInformationInstance);

                    }
                    selected = true;
                    selectedInformationInstance = Instantiate(selectedInformation, transform.position, Quaternion.identity) as GameObject;
                    selectedInformationInstance.transform.SetParent(informationHolder.transform, false);
                    selectedInformationInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
                    selectedInformationInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    selectedInformationInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

                }
             
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            selected = false;

        }
        if (selected == false)
        {
            Destroy(selectedInformationInstance);
            selectedInformationInstance = null;
        }
       
        if (build == true)
        {
            buildProgress += buildSpeed / 100 * Time.deltaTime ;

        }
        if(buildProgress >= 0.1)
        {
            for (int i = 0; i < ComponentEnable.Length; i++)
            {
                ComponentEnable[i].enabled = true;
            }

            foreach (Collider col in colliders)
            {
                col.enabled = true;
            }
            sprite.GetComponent<Renderer>().material = WallMat;
        }
        if (buildProgress >=100)
        {
            build = false;
            gameObject.tag = buildingTag;

                buildProgress = 100;

            if (FinshedSpriteLoaction)
            {
                foreach (Transform child in FinshedSpriteLoaction.transform.parent.transform)
                {
                    child.transform.localScale = new Vector3(FinshedSpriteSize, FinshedSpriteSize, 1);

                }
               


                FinshedSpriteLoaction.GetComponent<SpriteRenderer>().sprite = finishedSprite;
                
            }
            else
            {
                transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = finishedSprite;
            }
            if(isWall)
            {
                transform.GetChild(0).GetChild(0).localScale = new Vector3(0.2f, 0.2f, 1);
                transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(34,34,34);
            }
            
            if (built == false)
            {
                GameObject.Find("Manager").GetComponent<GameManager>().Buildingslist.Remove(gameObject);

                if (player)
                {
                    player.GetComponent<JobManager>().closeBuilding = null;

                    player.GetComponent<JobManager>().FindBuildings();
                    player.GetComponent<JobManager>().one = false;

                  
                    player.GetComponent<JobManager>().index = 0;
                    player = null;
                    built = true;
                }
            }
        }
    }

  
  

 

}
