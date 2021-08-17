using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MissionManager : MonoBehaviour
{

    public GameObject missionsPanel;
    public GameObject MissionUIPanel;
    [Space]
    public List<GameObject> enemys;
    [Space]
    public List<GameObject> spawnPoints;
    [Space]
    public List<GameObject> markers;
    [Space]
    public List<GameObject> placedmarkers;
    [Space]
    float spawnTime = 0;
    float amount = 0;
    float max = 10;

    public bool paused;

    string map;

    bool inMission;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        }
    }

    void Update()
    {
        if (inMission)
        {
            if (enemys.Count >= 1)
            {
                foreach (var unit in enemys)
                {
                    if (unit.GetComponent<MissionFieldOfView>().caution == true || unit.GetComponent<MissionEnemy>().attacking == true)
                    {
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            enemys[i].GetComponent<MissionFieldOfView>().caution = true;
                        }
                    }
                }
            }
        }
        if (markers.Count >= 1)
        {
            if (amount < max)
            {
                if (paused == false)
                {
                    spawnTime -= Time.fixedDeltaTime;
                    if (spawnTime <= 0)
                    {

                        spawnTime = 2;
                        GameObject u = Instantiate(markers[Random.Range(0, markers.Count - 1)], new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));

                        GameObject i = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];

                        u.gameObject.GetComponent<MissionMarker>().marker = i;
                        u.transform.position = i.transform.position;

                        spawnPoints.Remove(i);




                        if (GameObject.Find("Map"))
                        {
                            u.transform.SetParent(GameObject.Find("Map").transform);
                        }
                        amount = placedmarkers.Count;
                        if (!placedmarkers.Contains(u))
                        {
                            placedmarkers.Add(u);
                        }

                        u.SetActive(true);


                    }
                }
            }
        }

    }

    public void playMap(string level)
    {
        Time.timeScale = 0;
        MissionUIPanel.SetActive(true);
        paused = true;
        map = level;
        inMission = true;
    }

    public void startMission()
    {
           SceneManager.LoadScene(map);
    }

    public void quitMissionPanel()
    {
        Time.timeScale = 1;
        MissionUIPanel.SetActive(false);
        paused = false;
    }


    public void ToggleMissionPanel()
    {
        missionsPanel.SetActive(!missionsPanel.activeSelf);
    }
}
