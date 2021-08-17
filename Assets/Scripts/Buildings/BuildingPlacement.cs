using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

    private Transform currentBuilding;
    public static bool hasPlaced;
    public bool isBuilding;
    public LayerMask layermask;

    // Use this for initialization
    void Start()
    {

        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (currentBuilding != null && !hasPlaced)
        {
            currentBuilding.transform.position = gridSnap(getWorldPoint());
            Vector3 m = Input.mousePosition;
            m = new Vector3(m.x, m.y, transform.position.y);
            Vector3 p = GetComponent<Camera>().ScreenToWorldPoint(m);
            currentBuilding.position = new Vector3(p.x, p.y, p.z);
            isBuilding = true;
            if (Input.GetMouseButtonDown(0))
            {
                hasPlaced = true;
                isBuilding = false;
               currentBuilding.gameObject.layer = LayerMask.NameToLayer("Building");
            }
            SelectableUnitComponent[] hinges = FindObjectsOfType(typeof(SelectableUnitComponent)) as SelectableUnitComponent[];
            foreach (SelectableUnitComponent hinge in hinges)
            {
                hinge.enabled = false;
            }

        }
        if(hasPlaced == true)
        {
            SelectableUnitComponent[] hinges = FindObjectsOfType(typeof(SelectableUnitComponent)) as SelectableUnitComponent[];
            foreach (SelectableUnitComponent hinge in hinges)
            {
                hinge.enabled = true;
            }
        }

    }

    Vector3 gridSnap(Vector3 originalPosition)
    {
        int granularity = 1;
        Vector3 snappedPosition = new Vector3(Mathf.Floor(originalPosition.x / granularity) * granularity, originalPosition.y, Mathf.Floor(originalPosition.z / granularity) * granularity);
        return snappedPosition;
    }

    public void SetItem(GameObject b)
    {
        hasPlaced = false;
        currentBuilding = ((GameObject)Instantiate(b)).transform;
    }
    Vector3 getWorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
