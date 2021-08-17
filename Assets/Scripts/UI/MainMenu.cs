using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Bayat.SaveSystem;
using System.Collections.Generic;
using System;

public class MainMenu : MonoBehaviour {

   
    public GameObject PlayPanel;
    public GameObject MainMenuPanel;
    public GameObject creditsPanel;
  //  public Image OptionsImage;
    public GameObject OptionsPanel;
    public Button ContinueButton;

   public bool closePanel;

    bool open;

public Animator playPanelAnimator;
    public List<string> loadList;
    public GameObject loadContentPrefab;
    public List<GameObject> loadedContent;
    public List<string> loadedList;
    public GameObject loadContainer;

    public GameObject loadPanel;

    void Update()
    {
        if(open)
        {
            playPanelAnimator.SetBool("Open", true);
        }
        else
        {
            playPanelAnimator.SetBool("Open", false);
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
                content.GetComponent<LoadedItemContent>().timeSaved = fileInfo.LastWriteTime.ToString() ;
                loadContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 60);
            }


        }
    }
    public void openSwent()
    {
        Application.OpenURL("www.swent.com.au");
    }

    public void openCloseCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void openLoadPanel()
    {
        loadPanel.SetActive(!loadPanel.activeSelf);
        if (loadPanel.activeSelf == true)
        {
            MainMenuPanel.SetActive(false);
        }
        else
        {
            MainMenuPanel.SetActive(true);

        }
    }

    public void OpenPlayPanel()
    {

        open = true;
        MainMenuPanel.SetActive(false);
    }

    public void ClosePlayPanel()
    {
        open = false;
        MainMenuPanel.SetActive(true);
    }

    public void OpenOptionsPanel()
    {
        closePanel = false;
        //OptionsImage.gameObject.SetActive(true);
        OptionsPanel.SetActive(true);
     //   OptionsImage.canvasRenderer.SetAlpha(0.01f);
       // OptionsImage.CrossFadeAlpha(1.0f, .5f, false);
        MainMenuPanel.SetActive(false);
    }

    public void CloseOptionsPanel()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesetting.json"))
        {
            gameObject.GetComponent<SettingsManager>().LoadSettings();
        }
        OptionsPanel.SetActive(false);
        //OptionsImage.CrossFadeAlpha(0, .5f, false);
        closePanel = true;
        MainMenuPanel.SetActive(true);
    }

    public void Continue()
    {
      //  GameObject.Find("SaveManager").GetComponent<SaveManager>().LoadGame();
    }


    public void NewGame()
    {
      
        gameObject.GetComponent<LevelLoader>().loadLevel(1);      //  GameObject.Find("SaveManager").GetComponent<SaveManager>().NewGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
