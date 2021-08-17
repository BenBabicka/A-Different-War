using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UnitInfomation : MonoBehaviour {

    public Text Stats;
    public Text Infomation;

    Shooting shooting;
    UnityEngine.AI.NavMeshAgent nav;
    Health health;

    UnitManager unitmanager;

    public GameObject Jobs;
    public GameObject contect;

    public GameObject saveJobs;

    public float postitionToggle;
   public RectTransform contectrect;


    public string UnitN;
    public float orgSpeed;
    public GameObject spawnedInJobPanel;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        nav = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        shooting = gameObject.GetComponent<Shooting>();
        unitmanager = GameObject.Find("Manager").GetComponent<UnitManager>();
        StartCoroutine(Wait());
        Jobs = GameObject.Find("Manager").GetComponent<GameManager>().Job;
        contect = GameObject.Find("Manager").GetComponent<GameManager>().Contect;
        contectrect = GameObject.Find("Manager").GetComponent<GameManager>().Contect.GetComponent<RectTransform>();
        nav.speed = Random.Range(5, 8);
        contect.GetComponent<VerticalLayoutGroup>().spacing = 2;
        orgSpeed = nav.speed;
     

    }



    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        spawnedInJobPanel = Instantiate(Jobs, new Vector3(0,0,0), contectrect.transform.rotation) as GameObject;
        saveJobs = spawnedInJobPanel;
        spawnedInJobPanel.gameObject.transform.SetParent(contect.transform);
        spawnedInJobPanel.SetActive(true);
        spawnedInJobPanel.transform.localScale = Jobs.transform.localScale;
        // position.y += postitionToggle;
        spawnedInJobPanel.GetComponentInChildren<Jobs>().player = gameObject;
        //contectrect.offsetMin += new Vector2(contectrect.offsetMin.x, 20);
        contectrect.sizeDelta += new Vector2(0, spawnedInJobPanel.GetComponent<RectTransform>().sizeDelta.y + 2);
        Transform Unitname = spawnedInJobPanel.transform.GetChild(0);
        Unitname.GetComponent<Text>().text = gameObject.name;
        spawnedInJobPanel.GetComponent<Jobs>().player = gameObject;
        PlayerPrefs.SetString("Name" + UnitN, gameObject.name);

    }







    void Update()
    {
     
                Stats.text = "Speed: " + nav.speed + "\n" + "Health: " + health.health + "\n" + "Max Health: " + health.MaxHealth + "\n" + "Hunger: " + Mathf.FloorToInt(gameObject.GetComponent<PlayerFoodManager>().hungerBar);
                Infomation.text = gameObject.name;
            
        
    }

}
    