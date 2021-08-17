using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PathBuilder : MonoBehaviour {

    bool creating;

    bool canbuild = false;

    [Header ("Start and End")]
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject snapToAxisPrefab;
    [Space]
    public GameObject wallPrefab;

    GameObject wall;
    GameObject start;
    GameObject end;
    GameObject snapToSide;


    public bool zSnapping;
    public bool xSnapping;

    public float zAxis;
    public float xAxis;


    float distance;

    public Vector3 p;

    public bool allowToBuild;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.FindWithTag("Storage"))
        {
            allowToBuild = true;
        }

        if (allowToBuild)
        {
            GetInput();

           if (snapToSide)
            {
                p = snapToSide.transform.eulerAngles;
                if (snapToSide.transform.eulerAngles.y <= 45 || snapToSide.transform.eulerAngles.y >= 315)
                {
                    xSnapping = true;
                    zSnapping = false;
                }
                if (snapToSide.transform.eulerAngles.y <= 135 && snapToSide.transform.eulerAngles.y >= 45)
                {
                    xSnapping = false;
                    zSnapping = true;
                }
                if (snapToSide.transform.eulerAngles.y <= 225 && snapToSide.transform.eulerAngles.y >= 135)
                {
                    xSnapping = true;
                    zSnapping = false;
                }
                if (snapToSide.transform.eulerAngles.y <= 315 && snapToSide.transform.eulerAngles.y >= 225)
                {
                    xSnapping = false;
                    zSnapping = true;
                }
            }
        }
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            canbuild = !canbuild ;
        }


        if (canbuild)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(end);
                Destroy(start);
                Destroy(snapToSide);
                snapToSide = null;
                start = null;
                end = null;
                setStart();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                setEnd();
            }
            else
            {
                if (creating)
                {
                    Adjust();
                }
            }

        }
        




    }

    Vector3 gridSnap(Vector3 originalPosition)
    {
        int granularity = 1;
        Vector3 snappedPosition = new Vector3(Mathf.Floor(originalPosition.x / granularity) * granularity, originalPosition.y, Mathf.Floor(originalPosition.z / granularity) * granularity);
        return snappedPosition;
    }


    void setStart()
    {
        creating = true;
        start = Instantiate(startPrefab ,transform.position, transform.rotation);
        snapToSide = Instantiate(snapToAxisPrefab, transform.position, transform.rotation);
        start.transform.position = new Vector3(Mathf.Round(getWorldPoint().x), Mathf.Round(getWorldPoint().y), Mathf.Round(getWorldPoint().z));
        snapToSide.transform.position = start.transform.position;
        snapToSide.name = "snapToSide";
        wall = Instantiate(wallPrefab, new Vector3( start.transform.position.x, -0.4f, start.transform.position.z), transform.rotation);

    }

    void setEnd()
    {
        creating = false;
        wall.GetComponent<PathSpawner>().enabled = true;
        wall.GetComponent<PathSpawner>().zSnapping = zSnapping;

        end.transform.position = gridSnap(getWorldPoint());
        wall.transform.localScale =  new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance + 1);
        Destroy(end);
        Destroy(start);
        Destroy(snapToSide);
        snapToSide = null;
        start = null;
        end = null;

    }

   

    void Adjust()
    {
        if (!end)
        {          

            end = Instantiate(endPrefab, transform.position, transform.rotation);
        }

        end.transform.position = gridSnap(getWorldPoint());

        if (xSnapping)
        {
            end.transform.position = new Vector3(start.transform.position.x, end.transform.position.y, end.transform.position.z);
        }
        if (zSnapping)
        {
            end.transform.position = new Vector3(end.transform.position.x, end.transform.position.y, start.transform.position.z);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {

            snapToSide.transform.LookAt(hit.point);
        }

       
        adjustWall();
    }

    void adjustWall()
    {

        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);

        distance = Vector3.Distance(start.transform.position, end.transform.position);

        wall.transform.position = start.transform.position + distance / 2 * start.transform.forward;
        wall.transform.rotation = start.transform.rotation;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.x, distance);
        wall.transform.position = new Vector3(wall.transform.position.x, -0.4f, wall.transform.position.z);
    }

    Vector3 getWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast (ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

}
