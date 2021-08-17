using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSaveManager : MonoBehaviour
{

    public string itemSaveName;
    public List<StoneManager> stoneManagers = new List<StoneManager>();

    public List<float> cutProgesses = new List<float>();

    public SaveManager saveManager;
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        saveManager.rockSaveManagers.Add(this);
        itemSaveName = gameObject.GetComponent<MapGenerationSave>().ItemingSaving;
        StartCoroutine(LateStart(.2f));
    }
    IEnumerator LateStart(float timer)
    {
        yield return new WaitForSeconds(timer);
        foreach (var item in gameObject.GetComponent<MapGenerationSave>().itemList)
        {
            if (!stoneManagers.Contains(item.GetComponent<StoneManager>()))
            {
                stoneManagers.Add(item.GetComponent<StoneManager>());
            }
        }

        for (int i = 0; i < stoneManagers.Count;)
        {
            cutProgesses.Add(stoneManagers[i].cutProgress);
            i++;
        }
            
        
    }
    public void UpdateInformation()
    {
        foreach (var stone in stoneManagers)
        {
            for (int i = 0; i < cutProgesses.Count; i++)
            {
                if (cutProgesses[i] != stone.cutProgress)
                {
                    cutProgesses[i] = stone.cutProgress;
                }
            }
            
        }

        saveManager.UpdateRockInformation();

    }
    public void LoadStoneData()
    {
        foreach (var item in stoneManagers)
        {
            foreach (var progess in cutProgesses)
            {
                item.cutProgress = progess;
            }
        
        }
    }
}
