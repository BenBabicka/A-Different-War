using Bayat.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bayat.SaveSystem.Demos;

public class MapGenerationSave : MonoBehaviour
{

    public string ItemingSaving;

    public List<GameObject> itemList = new List<GameObject>();

    public int itemAmount;

    public List<Vector3> itemPosition;
    public List<Vector3> itemSize;


    public List<int> spriteValue;
    public List<bool> spriteFlip;
    public List<bool> disable;
    float timer = 2;

    public SaveManager saveManager;

   public TerrainGeneratorClass terrainGeneratorClass = new TerrainGeneratorClass();

    void Start()
    {
        StartCoroutine(LateStart(1f));
    }
  IEnumerator LateStart(float timer)
    {
        yield return new WaitForSeconds(timer);
        saveManager = FindObjectOfType<SaveManager>().GetComponent<SaveManager>();
        if (!saveManager.mapGenerationSaveObjects.Contains(this))
        {
            saveManager.mapGenerationSaveObjects.Add(this);
        }
        //  saveManager = GameObject.FindObjectOfType<SaveManager>();
        //  saveManager = GameObject.FindObjectOfType<SaveManager>();

        itemAmount = itemList.Count;
        for (int i = 0; i < itemList.Count;)
        {
            spriteValue.Add(itemList[i].GetComponent<Nature>().spriteImage);
            itemPosition.Add(itemList[i].transform.position);
            itemSize.Add(itemList[i].transform.localScale);
            spriteFlip.Add(itemList[i].GetComponent<Nature>().flip);
            //disable.Add(itemList[i].activeSelf);
            i++;
        }



        terrainGeneratorClass.name = ItemingSaving;

        terrainGeneratorClass.itemTransforms = itemPosition;
        terrainGeneratorClass.spriteValue = spriteValue;
        terrainGeneratorClass.amount = itemAmount;
        terrainGeneratorClass.itemSize = itemSize;
    }


    public void checkIfDisabled()
    {
        disable.Clear();
        for (int i = 0; i < itemList.Count;)
        {
            disable.Add(itemList[i].activeSelf);
            i++;
        }
    }
    public void Load()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            
            itemList[i].transform.position = itemPosition[i];
            itemList[i].GetComponent<Nature>().spriteImage = spriteValue[i];
            itemList[i].GetComponent<Nature>().flip = spriteFlip[i];
            itemList[i].GetComponent<Nature>().LoadSprite();

            if (disable[i])
            {
                itemList[i].SetActive(disable[i]);
            }
        }
    }
 

 
}
