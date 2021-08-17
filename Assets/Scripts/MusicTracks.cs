using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTracks : MonoBehaviour {

    public List<AudioClip> music;

    AudioClip clip;
    int song;

	void Start () {
       
        for (int i = 0; i < music.Count; i++)
        {
            AudioClip temp = music[i];
            int randomIndex = Random.Range(i, music.Count);
            music[i] = music[randomIndex];
            music[randomIndex] = temp;
        }

        clip = music[song];
        gameObject.GetComponent<AudioSource>().clip = clip;
        StartCoroutine(playSong());

    }

    void NewSong () {


        clip = music[song];
        gameObject.GetComponent<AudioSource>().clip = clip;
        StartCoroutine(playSong());
    }

    void Update()
    {
        if (song == music.Count)
        {
            song = 0;
        }

    }



    IEnumerator playSong()
    {
        yield return new WaitForSecondsRealtime(Random.Range(5, 20));
        Debug.Log(clip);

        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(clip.length);
        song += 1;
        NewSong();
    }

}
