using UnityEngine;
using System.Collections;
using Steamworks;
public class Steam_Achievment_Manager : MonoBehaviour {
    public bool Check_It;

    public float nunber;

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (nunber == 1) {
            SteamUserStats.SetAchievement("ACHIEVEMENT_1");
            SteamUserStats.StoreStats();
            Debug.Log("Achievement1: " + SteamUserStats.GetAchievement("ACHIEVEMENT_1", out Check_It));
        }
    }
}
