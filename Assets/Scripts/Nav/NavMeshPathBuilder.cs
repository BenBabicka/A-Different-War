using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathBuilder : MonoBehaviour {

    public List<GameObject> players;
    public List<Vector3> playerPositions;

    bool building;

	// Use this for initialization
	void Start () {
		
	}

 
	// Update is called once per frame
	public IEnumerator Build () {
        building = true;
        gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < players.Count; i++)
        {
             players[i].transform.position = playerPositions[i];
        }
        building = false;
    }
    void LateUpdate()
    {
        if (!players.Contains(GameObject.FindGameObjectWithTag("Player")))
        {
            players.Add(GameObject.FindGameObjectWithTag("Player"));
        }
        if (players.Count > 0)
        {
            foreach (var item in players)
            {
                if (item == null)
                {
                    players.Remove(item);
                }
            }
        }

        if (playerPositions.Count != players.Count)
        {
            playerPositions = new List<Vector3>(new Vector3[players.Count]);
        }
        if (!building)
        {
            for (int i = 0; i < players.Count; i++)
            {
                playerPositions[i] = players[i].transform.position;
            }
        }
    }
}
