using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManagerSave : MonoBehaviour
{

    public List<TreeManager> treeManagers = new List<TreeManager>();
    public List<float> cutProgesses = new List<float>();
    public List<float> size = new List<float>();
    
    public SaveManager saveManager;
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        saveManager.treeManager = this;
        StartCoroutine(LateStart(.2f));
    }
    IEnumerator LateStart(float timer)
    {
        yield return new WaitForSeconds(timer);
        foreach (var item in gameObject.GetComponent<MapGenerationSave>().itemList)
        {
            if (!treeManagers.Contains(item.GetComponent<TreeManager>()))
            {
                treeManagers.Add(item.GetComponent<TreeManager>());
            }
        }

        foreach (var item in treeManagers)
        {
            if (!size.Contains(item.size))
            {
                cutProgesses.Add(item.cutProgress);
                size.Add(item.size);

            }
        }
    }

    public IEnumerator UpdateInformation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       
        
            for (int i = 0; i < cutProgesses.Count; i++)
            {
                if (cutProgesses[i] != treeManagers[i].cutProgress)
                {
                    cutProgesses[i] = treeManagers[i].cutProgress;
                }
            }
            for (int i = 0; i < size.Count; i++)
            {
                if(size[i] != treeManagers[i].size)
                {
                    size[i] = treeManagers[i].size;
                }
            }
    


        saveManager.UpdateTreeInformation();

    }


    public void LoadTreeData()
    {
        for (int i = 0; i < treeManagers.Count; i++)
        {
            treeManagers[i].cutProgress = cutProgesses[i];
            treeManagers[i].size = size[i];
           
        }

       
    }
}
