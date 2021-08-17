using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour {

    public GameObject player;

    [Space]
    public Transform researchPosition;

    [Space]
    public float ResearchSpeed;

   

    [Space]
    [Header("Progress Amount")]
    public float progressAmount;

    [Space]
    public string reseachName;

    [Space]
    public bool hasResearch;

    bool canResearch;
    float researchTime;
  public  float progress;
    public GameObject progressSlider;
    void Start()
    {
        if (!player)
        {
            player = null;
        }
        if(!progressSlider)
        {
            progressSlider = GameObject.Find("ResearchProgressSlider");
        }
        GameObject.Find("ToggleResearch").GetComponent<Button>().interactable = true;

    }



	void Update () {
        if (!player)
            return;

        if (!progressSlider)
        {
            progressSlider = GameObject.Find("ResearchProgressSlider");
        }
        float distance = Vector3.Distance(researchPosition.position, player.transform.position);

        if(distance <= 2)
        {
            canResearch = true;
            player.transform.rotation = transform.rotation;
        }

        if(reseachName != null)
        {
            hasResearch = true;
        }

        if(canResearch && hasResearch)
        {
            Research();
        }

        progressSlider.GetComponent< Slider>().value = progress;

	}


    void Research()
    {
        if (progress < progressAmount)
        {
            researchTime -= Time.deltaTime;

            if (researchTime <= 0)
            {
                progress += 10;

                researchTime += 5;

            }
        }
        else
        {
            GameObject.Find("Manager").GetComponent<GameManager>().researchList.Add(reseachName);
            progress = 0;

            hasResearch = false;
            reseachName = null;
        }
    }

}
