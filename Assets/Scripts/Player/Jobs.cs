using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Jobs : MonoBehaviour {

   public GameObject player;


    public Toggle BuilderToggle;
    public Toggle MedicToggle;
    public Toggle FarmToggle;
    public Toggle LumberToggle;
    public Toggle MinerToggle;
    public Toggle HunterToggle;
    public Toggle cookerToggle;
    public Toggle weaverToggle;
    public Toggle researchToggle;
    public Toggle crafterToggle;
    void Start () {
	if(BuilderToggle == null)
        {
            BuilderToggle = null;
        }
        if (FarmToggle == null)
        {
            FarmToggle = null;
        }
        if (MedicToggle == null)
        {
            MedicToggle = null;
        }
        if(LumberToggle == null)
        {
            LumberToggle = null;
        }
        if(MinerToggle == null)
        {
            MinerToggle = null;
        }
        if(HunterToggle == null)
        {
            HunterToggle = null;
        }
        if(HunterToggle == null)
        {
            HunterToggle = null;
        }
        if(cookerToggle == null)
        {
            cookerToggle = null;
        }
        if(weaverToggle == null)
        {
            weaverToggle = null;
        }
        if(researchToggle == null)
        {
            researchToggle = null;
        }
        if(crafterToggle == null)
        {
            crafterToggle = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            transform.parent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, gameObject.GetComponent<RectTransform>().sizeDelta.y + 2);
            Destroy(gameObject);
        }
        else
        {
            if (BuilderToggle.isOn && BuilderToggle != null)
            {
                player.GetComponent<JobManager>().builder = true;

            }
            if (!BuilderToggle.isOn && BuilderToggle != null)
            {
                player.GetComponent<JobManager>().builder = false;
            }

            if (FarmToggle.isOn && FarmToggle != null)
            {
                player.GetComponent<JobManager>().Farmer = true;
            }
            if (!FarmToggle.isOn && FarmToggle != null)
            {
                player.GetComponent<JobManager>().Farmer = false;
            }


            if (MedicToggle.isOn && MedicToggle != null)
            {
                player.GetComponent<JobManager>().Medic = true;
            }
            if (!MedicToggle.isOn && MedicToggle != null)
            {
                player.GetComponent<JobManager>().Medic = false;
            }

            if (LumberToggle.isOn && LumberToggle != null)
            {
                player.GetComponent<JobManager>().Lumber = true;
            }
            if (!LumberToggle.isOn && LumberToggle != null)
            {
                player.GetComponent<JobManager>().Lumber = false;
            }

            if (MinerToggle.isOn && MinerToggle != null)
            {
                player.GetComponent<JobManager>().Miner = true;

            }
            if (!MinerToggle.isOn && MinerToggle != null)
            {
                player.GetComponent<JobManager>().Miner = false;
            }

            if (HunterToggle.isOn && HunterToggle != null)
            {
                player.GetComponent<JobManager>().hunter = true;

            }

            if (!HunterToggle.isOn && HunterToggle != null)
            {
                player.GetComponent<JobManager>().hunter = false;
            }

            if (cookerToggle.isOn && cookerToggle != null)
            {
                player.GetComponent<JobManager>().cooker = true;

            }

            if (!cookerToggle.isOn && cookerToggle != null)
            {
                player.GetComponent<JobManager>().cooker = false;
            }

            if (weaverToggle.isOn && weaverToggle != null)
            {
                player.GetComponent<JobManager>().weaver = true;

            }

            if (!weaverToggle.isOn && weaverToggle != null)
            {
                player.GetComponent<JobManager>().weaver = false;
            }

            if (researchToggle.isOn && researchToggle != null)
            {
                player.GetComponent<JobManager>().researcher = true;

            }

            if (!researchToggle.isOn && researchToggle != null)
            {
                player.GetComponent<JobManager>().researcher = false;
            }

            if (crafterToggle.isOn && crafterToggle != null)
            {
                player.GetComponent<JobManager>().crafter = true;

            }

            if (!crafterToggle.isOn && crafterToggle != null)
            {
                player.GetComponent<JobManager>().crafter = false;
            }

        }
    }

   
}
