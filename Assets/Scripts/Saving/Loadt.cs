using UnityEngine;
using System;

public class Loadt : MonoBehaviour {
   
    [SerializeField]
    private bool paused = false;
    [SerializeField]
    private string gameName = "Your Game";
    [SerializeField]
    private static bool logProgress = false;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    public void Load()
    {
       
          
                DateTime t = DateTime.Now;
                if (logProgress)
                {
                    Debug.Log(string.Format("Loaded in: {0:0.000} seconds", (DateTime.Now - t).TotalSeconds));
                }
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            
       
    }
}
