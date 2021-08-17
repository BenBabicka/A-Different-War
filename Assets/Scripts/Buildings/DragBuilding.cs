using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DragBuilding : MonoBehaviour
{
    public GameObject points;
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    GameObject startPos;
    GameObject Point1;
    GameObject Point2;
    GameObject Point3;

    GameObject wall1;
    GameObject wall2;
    GameObject wall3;
    GameObject wall4;

    GameObject floor;

    float distance1;
    float distance2;
    float distance3;
    float distance4;


    Vector3 position;

    void Update()
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = hit.point;
        }
            if (Input.GetMouseButtonDown(0))
            {

                startPos = Instantiate(points, gridSnap(position), Quaternion.identity);
                Point1 = Instantiate(points, gridSnap(position), Quaternion.identity);
                Point2 = Instantiate(points, gridSnap(position), Quaternion.identity);
                Point3 = Instantiate(points, gridSnap(position), Quaternion.identity);
            wall1 = Instantiate(wallPrefab, new Vector3(startPos.transform.position.x, -0.4f, startPos.transform.position.z), transform.rotation);
            wall2 = Instantiate(wallPrefab, new Vector3(startPos.transform.position.x, -0.4f, startPos.transform.position.z), transform.rotation);
            wall3 = Instantiate(wallPrefab, new Vector3(startPos.transform.position.x, -0.4f, startPos.transform.position.z), transform.rotation);
            wall4 = Instantiate(wallPrefab, new Vector3(startPos.transform.position.x, -0.4f, startPos.transform.position.z), transform.rotation);

            floor = Instantiate(floorPrefab, new Vector3(startPos.transform.position.x, -0.4f, startPos.transform.position.z), transform.rotation);

        }
        if (Input.GetMouseButton(0))
            {
                Point1.transform.position = new Vector3(Point1.transform.position.x, Point1.transform.position.y, gridSnap(position).z);
                Point2.transform.position = new Vector3(gridSnap(position).x, Point2.transform.position.y, Point2.transform.position.z);
                Point3.transform.position = new Vector3(gridSnap(position).x, Point3.transform.position.y, gridSnap(position).z);


            distance1 = Vector3.Distance(startPos.transform.position, Point1.transform.position);
            wall1.transform.localScale = new Vector3(1, 1, distance1 + 1);
            wall1.transform.position = startPos.transform.position + (Point1.transform.position - startPos.transform.position) / 2;

            distance2 = Vector3.Distance(startPos.transform.position, Point2.transform.position);
            wall2.transform.localScale = new Vector3(distance2 + 1, 1, 1);
            wall2.transform.position = startPos.transform.position + (Point2.transform.position - startPos.transform.position) / 2;

            distance3 = Vector3.Distance(Point3.transform.position, Point2.transform.position);
            wall3.transform.localScale = new Vector3(1, 1, distance3 + 1);
            wall3.transform.position = Point3.transform.position + (Point2.transform.position - Point3.transform.position) / 2;

            distance4 = Vector3.Distance(Point3.transform.position, Point1.transform.position);
            wall4.transform.localScale = new Vector3(distance4 + 1, 1, 1);
            wall4.transform.position = Point3.transform.position + (Point1.transform.position - Point3.transform.position) / 2;


            floor.transform.position = Point3.transform.position + (startPos.transform.position - Point3.transform.position) / 2;
            floor.transform.localScale = new Vector3(Vector3.Distance(startPos.transform.position, Point2.transform.position) - 1, 1, Vector3.Distance(startPos.transform.position, Point1.transform.position)-1) ;

        }

        if (Input.GetMouseButtonUp(0))
        {
            floor.GetComponent<BuildingStartUp>().hasPlaced = true;
        }
    }
    Vector3 gridSnap(Vector3 originalPosition)
    {
        int granularity = 1;
        Vector3 snappedPosition = new Vector3(Mathf.Floor(originalPosition.x / granularity) * granularity, originalPosition.y, Mathf.Floor(originalPosition.z / granularity) * granularity);
        return snappedPosition;
    }

}


