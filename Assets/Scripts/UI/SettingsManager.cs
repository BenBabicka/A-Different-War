using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class SettingsManager : MonoBehaviour
{

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown texturequalityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider masterVoulmeSlider;
    public Slider musicVoulmeSlider;
    public Slider sfxVoulmeSlider;
    public Button applyButton;

    public AudioSource musicSouce;
    public Resolution[] resolutions;
    public GameSettings gameSettings;
    public Resolution[] resolutionWithDuplicates;
    public List<Vector2> r;

    void Start()
    {

        resolutionWithDuplicates = Screen.resolutions;

        List<Resolution> resolutionList = new List<Resolution>();
        foreach (Resolution resolution in resolutionWithDuplicates)
        {
            if (!resolutionList.Contains(resolution))
            {
                resolutionList.Add(resolution);
            }
        }

        resolutions = resolutionList.ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResoltionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
           
                string option = resolutions[i].ToString();
                string optionWithOutAt = option.Replace("@", "");
                string optionWithoutHz = optionWithOutAt.Substring(0, optionWithOutAt.Length - 6);
            string optionTrimed = optionWithoutHz.Trim();
            if (!options.Contains(optionTrimed))
            {
                Resolution r1 = new Resolution();
                r1 = resolutions[i];
                r.Add(new Vector2(r1.width, r1.height));

                options.Add(optionTrimed);
            }
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResoltionIndex = i;
                }
            
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResoltionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void OnEnable()
    {

        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        texturequalityDropdown.onValueChanged.AddListener(delegate { OnTextureQuailtyChnage(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        masterVoulmeSlider.onValueChanged.AddListener(delegate { OnMasterVolume(); });
        musicVoulmeSlider.onValueChanged.AddListener(delegate { OnMusicVolume(); });
        sfxVoulmeSlider.onValueChanged.AddListener(delegate { OnSFXVolume(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        

       

        if(File.Exists(Application.persistentDataPath + "/gamesetting.json"))
        {
        LoadSettings();
        }
        else
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);

        }    
    }

    public void OnFullscreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution((int)r[resolutionDropdown.value].x,(int) r[resolutionDropdown.value].y, Screen.fullScreen);
        gameSettings.resolutuinIndex = resolutionDropdown.value;
    }

    public void OnTextureQuailtyChnage()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = texturequalityDropdown.value;

    }

    public void OnAntialiasingChange()
    {
        if(antialiasingDropdown.value == 0)
        {
            QualitySettings.antiAliasing = 0;
            gameSettings.antialisasing = 0;
        }
        if (antialiasingDropdown.value == 1)
        {
            QualitySettings.antiAliasing = gameSettings.antialisasing = 2;
            gameSettings.antialisasing = 1;
        }
        if (antialiasingDropdown.value == 2)
        {
            QualitySettings.antiAliasing = gameSettings.antialisasing = 4;
            gameSettings.antialisasing = 2;
        }
        if (antialiasingDropdown.value == 3)
        {
            QualitySettings.antiAliasing = gameSettings.antialisasing = 8;
            gameSettings.antialisasing = 3;
        }
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = gameSettings.vSync = vSyncDropdown.value;
    }

    public void OnMasterVolume()
    {
        AudioListener.volume = gameSettings.masterVolume = masterVoulmeSlider.value / 100;
    }

    public void OnMusicVolume()
    {
        musicSouce.volume = gameSettings.muiscVolume = musicVoulmeSlider.value / 100;
    }

    public void OnSFXVolume()
    {
      gameSettings.sfxVolume = sfxVoulmeSlider.value / 100;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()  
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesetting.json", jsonData);
    }

    public void LoadSettings()
    {
        File.ReadAllText(Application.persistentDataPath + "/gamesetting.json");
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText (Application.persistentDataPath + "/gamesetting.json"));
        masterVoulmeSlider.value = gameSettings.masterVolume *100;
        musicVoulmeSlider.value = gameSettings.muiscVolume * 100;
        sfxVoulmeSlider.value = gameSettings.sfxVolume * 100;
        antialiasingDropdown.value = gameSettings.antialisasing;
        vSyncDropdown.value = gameSettings.vSync;
        texturequalityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutuinIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        Screen.fullScreen = gameSettings.fullscreen;
        musicSouce.volume = gameSettings.muiscVolume = musicVoulmeSlider.value / 100;
        resolutionDropdown.RefreshShownValue();
    }

  
}
