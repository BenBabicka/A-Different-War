using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class UnitSpawner : MonoBehaviour {

    public GameObject[] units;
    public Vector3 randomPos;

    public GameObject unit;
    public float amount;
    float max;
    int playerId = 1;

    [Space]
    public Text unitText;

    public GameObject unitButton;
    public GameObject unitButtonPanel;

    public bool load;
	// Use this for initialization
	void Start () {

        if (GameObject.Find("SaveManager"))
        {
            load = GameObject.Find("SaveManager").GetComponent<SaveManager>().loading;
        }

      /*  for (int i = 0; i < units.Length; i++)
        {
            units[i].transform.position = new Vector3(Random.Range(-randomPos.x / 2, randomPos.x / 2), units[i].transform.position.y, Random.Range(-randomPos.x / 2, randomPos.x / 2));
        }*/
	}

    void Update()
    {
        if (GameObject.Find("SaveManager"))
        { 
        unitText.text = amount.ToString();
        if (!load)
        {
                if (max <= amount - 1)
                {
                    max += 1;
                    GameObject u = Instantiate(unit, new Vector3(Random.Range(-randomPos.x / 2, randomPos.x / 2), transform.position.y, Random.Range(-randomPos.x / 2, randomPos.x / 2)), Quaternion.Euler(0, 0, 0));
                    u.SetActive(true);
                    u.GetComponent<SelectableUnitComponent>().canMove = true;
                    u.transform.SetParent(GameObject.Find("Player AI").transform, false);
                    GameObject unitbutton = Instantiate(unitButton, transform.position, transform.rotation);
                    unitbutton.SetActive(true);
                    unitbutton.transform.SetParent(unitButtonPanel.transform, false);
                    unitbutton.GetComponent<UnitFollowButton>().unit = u;
                    gameObject.GetComponent<UnitSaver>().players.Add(u);
                    gameObject.GetComponent<UnitSaver>().unitButtons.Add(unitbutton);
                    gameObject.GetComponent<UnitSaver>().amount += 1;
                    u.GetComponent<PlayerSave>().playerID = playerId;
                    playerId += 1;
                    if (!GameObject.Find("Manager").GetComponent<UnitManager>().allUnits.Contains(u))
                    {
                        GameObject.Find("Manager").GetComponent<UnitManager>().allUnits.Add(u);
                    }

                    if (!Camera.main.GetComponent<RestartCamera>().units.Contains(u))
                    {
                        Camera.main.GetComponent<RestartCamera>().units.Add(u);
                    }

                    if (!GameObject.Find("Player").GetComponent<UnitSelectionComponent>().seletable.Contains(u))
                    {
                        GameObject.Find("Player").GetComponent<UnitSelectionComponent>().seletable.Add(u);
                    }

                    if (GameObject.Find("Ground"))
                    {
                        if (!GameObject.Find("Ground").GetComponent<NavMeshPathBuilder>().players.Contains(u))
                        {
                            GameObject.Find("Ground").GetComponent<NavMeshPathBuilder>().players.Add(u);
                        }
                    }
                }
            }
        }
    }

    public void SpawnUnit()
    {
        GameObject u = Instantiate(unit, new Vector3(Random.Range(-randomPos.x / 2, randomPos.x / 2), transform.position.y, Random.Range(-randomPos.x / 2, randomPos.x / 2)), Quaternion.Euler(0, 0, 0));
        u.SetActive(true);
        u.GetComponent<SelectableUnitComponent>().canMove = true;
        u.transform.SetParent(GameObject.Find("Player AI").transform, false);
        GameObject unitbutton = Instantiate(unitButton, transform.position, transform.rotation);
        unitbutton.SetActive(true);
        unitbutton.transform.SetParent(unitButtonPanel.transform, false);
        unitbutton.GetComponent<UnitFollowButton>().unit = u;
        gameObject.GetComponent<UnitSaver>().players.Add(u);
        gameObject.GetComponent<UnitSaver>().amount += 1;
        u.GetComponent<PlayerSave>().playerID = playerId;
        playerId += 1;
        if (!GameObject.Find("Manager").GetComponent<UnitManager>().allUnits.Contains(u))
        {
            GameObject.Find("Manager").GetComponent<UnitManager>().allUnits.Add(u);
        }

        if (!Camera.main.GetComponent<RestartCamera>().units.Contains(u))
        {
            Camera.main.GetComponent<RestartCamera>().units.Add(u);
        }
        if (!GameObject.Find("Player").GetComponent<UnitSelectionComponent>().seletable.Contains(u))
        {
            GameObject.Find("Player").GetComponent<UnitSelectionComponent>().seletable.Add(u);
        }


        if (GameObject.Find("Ground"))
        {
            if (!GameObject.Find("Ground").GetComponent<NavMeshPathBuilder>().players.Contains(u))
            {
                GameObject.Find("Ground").GetComponent<NavMeshPathBuilder>().players.Add(u);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, randomPos);
    }
}
