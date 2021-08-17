using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bayat.SaveSystem;
using UnityEngine.SceneManagement;
public class LoadedItemContent : MonoBehaviour
{
    public string fileName;
    public string timeSaved;
     void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = fileName;
        transform.GetChild(3).GetComponent<Text>().text = timeSaved;
    }

    public void LoadFile()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        if(scene.buildIndex == 0)
        {
            transform.parent.parent.parent.parent.gameObject.SetActive(false);
        }
         GameObject.FindObjectOfType<SaveManager>().LoadGame(fileName);
    }
    public void delete()
    {
        deleteItem();
    }

    async void deleteItem()
    {
        await SaveSystemAPI.DeleteAsync(fileName);
        transform.parent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 60);
        Destroy(transform);

    }

}
