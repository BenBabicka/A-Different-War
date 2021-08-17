using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResearchButton : MonoBehaviour {

    public string researchName;
    public float researchAmount;
    public List<string> requiedResearch;
    public bool canResearch;
    bool firstResearch;

    GameObject manager;
  public  List<GameObject> researchBenchs;

    void Update()
    {
        if (manager == null)
        {
            manager = GameObject.Find("Manager");
        }
        if (!researchBenchs.Contains(GameObject.FindGameObjectWithTag("Research")))
        {

            researchBenchs.Add(GameObject.FindGameObjectWithTag("Research"));

        }
        if (manager.GetComponent<GameManager>().researchList.Contains(researchName))
        {
            canResearch = false;
        }
        if (researchBenchs.Count != 0 )
        {
            foreach (var researchBench in researchBenchs)
            {
                if (researchBench == null)
                {
                    researchBenchs.Remove(researchBench);
                }
            }
        }  
            if (requiedResearch.Count != 0)
            {
                foreach (var research in requiedResearch)
                {
                    if (manager.GetComponent<GameManager>().researchList.Contains(research))
                    {
                        requiedResearch.Remove(research);

                    }
                }
            }
            if (requiedResearch.Count == 0)
            {
                canResearch = true;
                gameObject.GetComponent<Button>().interactable = true;
            }
            if (researchBenchs.Count != 0)
            {
                foreach (var researchBench in researchBenchs)
                {
                    if (researchBench.GetComponent<ResearchManager>().reseachName != null)
                    {
                        canResearch = false;
                    }
                }
            }


        
    }
    public void research()
    {
        if (canResearch)
        {
            if (!firstResearch)
            {
                if (researchBenchs.Count != 0)
                {
                    foreach (var researchBench in researchBenchs)
                    {
                        researchBench.GetComponent<ResearchManager>().reseachName = researchName;
                        researchBench.GetComponent<ResearchManager>().progressAmount = researchAmount;
                        researchBench.GetComponent<ResearchManager>().hasResearch = true;
                        firstResearch = true;
                    }
                }
            }
        }
    }


}
