using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Bayat.SaveSystem;
using System.Collections.Generic;
using System.IO;

public class PauseManager : MonoBehaviour {

    public UnitSelectionComponent dragScripts;

    public GameObject PausePanel;
    public GameObject OptionsPanel;
    public GameObject savemanager;
    [Space]
    [Header("Loading and saving Data")]
    public GameObject savePanel;

    public GameObject loadPanel;

    float time;
    bool esc;
    public List<string> loadList;
    public GameObject loadContentPrefab;
    public List<GameObject> loadedContent;
    public List<string> loadedList;
    public GameObject loadContainer;
    void Start()
    {
        savemanager = GameObject.Find("SaveManager");
        time = Time.timeScale;


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc = !esc;

            if (esc)
            {
                OpenPanel();
            }
            else
            {
                ClosePanel();
            }
        }

        GetLoadedData();
    }
    async void GetLoadedData()
    {
        loadList = await SaveSystemAPI.LoadCatalogAsync();
        foreach (var item in loadList)
        {

          
                if (!loadedList.Contains(item))
                {
                    GameObject content = Instantiate(loadContentPrefab, loadContainer.transform);
                    content.transform.SetParent(loadContainer.transform);
                content.transform.SetAsFirstSibling();
                    content.SetActive(true);
                    loadedContent.Add(content);
                    loadedList.Add(item);
                    content.GetComponent<LoadedItemContent>().fileName = item;
                    FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/" + item);
                    content.GetComponent<LoadedItemContent>().timeSaved = fileInfo.LastWriteTime.ToString();
              
                loadContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 37);
                }
            

        }
    }
    public void openLoadPanel()
    {
        loadPanel.SetActive(!loadPanel.activeSelf);
    }
    
    public void openSavePanel()
    {
        savePanel.SetActive(!savePanel.activeSelf);
    }
    public void OpenPanel()
    {
        PausePanel.SetActive(true);
        time = Time.timeScale;
        Time.timeScale = 0;
        Camera.main.GetComponent<RtsCamera>().enabled = false;
        Camera.main.GetComponent<RtsCameraKeys>().enabled = false;
        Camera.main.GetComponent<RtsCameraMouse>().enabled = false;
        dragScripts.enabled = false;
    }

    public void ClosePanel()
    {
        PausePanel.SetActive(false);
        Time.timeScale = time;
        Camera.main.GetComponent<RtsCamera>().enabled = true;
        Camera.main.GetComponent<RtsCameraKeys>().enabled = true;
        Camera.main.GetComponent<RtsCameraMouse>().enabled = true;
        dragScripts.enabled = true;
    }

    public void OpenOptionsPanel()
    {
        OptionsPanel.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        OptionsPanel.SetActive(false);
        OptionsPanel.GetComponent<SettingsManager>().LoadSettings();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        Destroy( savemanager);
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitToDesktop()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void ExitToWorld()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TestLevel");
    }

}
