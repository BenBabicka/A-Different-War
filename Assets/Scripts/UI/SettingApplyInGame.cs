using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class SettingApplyInGame : MonoBehaviour {

    GameSettings gameSettings;

   public List<AudioSource> musicSources;
   public List<AudioSource> sfxSources;


    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesetting.json"))
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesetting.json"));

            if (!musicSources.Contains(GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>()))
            {
                musicSources.Add(GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>());
            }
            foreach (AudioSource musicSource in musicSources)
            {
                musicSource.volume = gameSettings.muiscVolume;
            }
            if (GameObject.FindGameObjectWithTag("SFX") != null)
            {
                if (!sfxSources.Contains(GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>()))
                {
                    sfxSources.Add(GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>());
                }
            }
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesetting.json"));


            foreach (AudioSource sfxSource in sfxSources)
            {
                sfxSource.volume = gameSettings.sfxVolume;
            }
            AudioListener.volume = gameSettings.masterVolume;

        }
    }

}
