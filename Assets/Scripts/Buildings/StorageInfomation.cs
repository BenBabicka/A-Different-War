using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageInfomation : MonoBehaviour {

    public Text stone, wood, food, textile, money;
    StorageData storageData;

    bool dispaly;
    public bool hover;


	// Use this for initialization
	void Start () {
        storageData = gameObject.GetComponent<StorageData>();
    }

    // Update is called once per frame
    void Update()
    {
      /* stone.text = storageData.Stone.ToString();
        wood.text = storageData.Wood.ToString();
        food.text = storageData.Food.ToString();
        textile.text = storageData.Textiles.ToString();
        money.text = storageData.Credits.ToString();
        */
    }
}
