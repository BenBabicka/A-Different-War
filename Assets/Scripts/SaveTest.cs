using Bayat.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{

    public SaveManager saveManager;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            saveManager.Save();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(loadData(.5f));
        }
    }

   public IEnumerator loadData(float waitTimer)
    {
        yield return new WaitForSeconds(waitTimer);
        saveManager.Load("Test");


    }

}
