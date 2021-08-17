using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
    public bool isSelecting = false;
    
    Vector3 mousePosition1;


    public bool muiltiple;

    public GameObject selectionCirclePrefab;

    float isSelectingFloat;

    public List<GameObject> units;

    public bool canSelect;
    UnitManager unitmanager;

   public List<GameObject> seletable;
    public float delay = 0.1f;
    public bool DragingUI;
    public bool hover;
    bool dragoffUI;


    void Start()
    {
        unitmanager = GameObject.Find("Manager").GetComponent<UnitManager>();
    }

    void LateUpdate()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (hover && Input.GetMouseButtonDown(0))
        {
            dragoffUI = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            dragoffUI = false;

        }
       

          

        foreach (var item in seletable)
        {
            if(item == null)
            {
                seletable.Remove(item);
            }
        }
            
        if (Input.GetMouseButtonDown(0))
        {

            if (!hover)
            {
                mousePosition1 = Input.mousePosition;



                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 2000))
                {
                    if (hit.transform.tag == "Player")
                    {

                        Debug.Log(hit.transform.name);
                        hit.transform.GetComponent<SelectableUnitComponent>().Selected = true;
                        if (hit.transform.GetComponent<SelectableUnitComponent>().selectionCircle == null)
                        {
                            hit.transform.GetComponent<SelectableUnitComponent>().selectionCircle = Instantiate(selectionCirclePrefab, new Vector3(transform.position.x, 10, transform.position.z), Quaternion.identity);
                            hit.transform.GetComponent<SelectableUnitComponent>().selectionCircle.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<NameDisplay>().Player = hit.transform.gameObject;

                            hit.transform.GetComponent<SelectableUnitComponent>().selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                        }
                        if (!units.Contains(hit.transform.gameObject))
                        {
                            units.Add(hit.transform.gameObject);
                        }



                    }
                }
            }
            else
            {
                isSelecting = false;
            }
        }
            if (Input.GetMouseButtonUp(0))
            {
                delay = 0.1f;
                isSelecting = false;
            }
            if (canSelect)
            {
                if (Input.GetMouseButtonDown(0))
                {
                if (!hover)
                {
                     
                    mousePosition1 = Input.mousePosition;
                }
                else
                {
                    isSelecting = false;
                }

            }
            if (!hover)
            {
                if (Input.GetMouseButton(0))
                {

                    delay -= Time.fixedDeltaTime;
                }
                if (delay < 0)
                {

                    isSelecting = true;
                    muiltiple = true;
                    foreach (var item in units)
                    {
                        if (item)
                        {
                            if (!item.GetComponent<SelectableUnitComponent>().Selected)
                            {
                                if (item.GetComponent<SelectableUnitComponent>().selectionCircle != null)
                                {
                                    Destroy(item.GetComponent<SelectableUnitComponent>().selectionCircle.gameObject);
                                    item.GetComponent<SelectableUnitComponent>().selectionCircle = null;
                                }
                            }
                        }
                    }
                }

            }


                if (Input.GetMouseButtonDown(1))
                {
                    foreach (var item in units)
                    {
                        if (item)
                        {
                            if (item.GetComponent<SelectableUnitComponent>().selectionCircle != null)
                            {
                                Destroy(item.GetComponent<SelectableUnitComponent>().selectionCircle.gameObject);
                                item.GetComponent<SelectableUnitComponent>().selectionCircle = null;
                            }
                        }
                    }
                }

                // If we let go of the left mouse button, end selection
                if (Input.GetMouseButtonUp(0))
                {
                    var selectedObjects = new List<SelectableUnitComponent>();
                    foreach (var selectableObject in seletable)
                    {
                        if (IsWithinSelectionBounds(selectableObject.gameObject))
                        {
                            selectedObjects.Add(selectableObject.GetComponent<SelectableUnitComponent>());
                        }
                    }

                    var sb = new StringBuilder();
                    sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
                    foreach (var selectedObject in selectedObjects)
                        sb.AppendLine("-> " + selectedObject.gameObject.name);
                    Debug.Log(sb.ToString());
                    isSelectingFloat = 0;
                    isSelecting = false;
                }

                // Highlight all objects within the selection box
                if (isSelecting && muiltiple)
                {
                    foreach (var selectableObject in seletable)
                    {
                    if (IsWithinSelectionBounds(selectableObject.gameObject))
                    {
                        if (selectableObject.GetComponent<SelectableUnitComponent>().selectionCircle == null)
                        {
                            selectableObject.GetComponent<SelectableUnitComponent>().selectionCircle = Instantiate(selectionCirclePrefab, new Vector3(transform.position.x, 10, transform.position.z), Quaternion.identity);
                            if (!units.Contains(selectableObject.gameObject))
                            {
                                units.Add(selectableObject.gameObject);
                            }
                            selectableObject.GetComponent<SelectableUnitComponent>().selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                            foreach (var item in units)
                            {

                                item.GetComponent<SelectableUnitComponent>().Selected = true;

                            }
                        }
                    }
                    else
                    {
                        if (!IsWithinSelectionBounds(selectableObject.gameObject))
                        {
                            if (selectableObject.GetComponent<SelectableUnitComponent>().selectionCircle != null)
                            {
                                Destroy(selectableObject.GetComponent<SelectableUnitComponent>().selectionCircle.gameObject);
                                selectableObject.GetComponent<SelectableUnitComponent>().Selected = false;                                
                                units.Remove(selectableObject.gameObject);
                                selectableObject.GetComponent<SelectableUnitComponent>().selectionCircle = null;

                            }
                        }
                    }
                    }
                }
            }
            foreach (var item in units)
            {
                if (item)
                {
                    if (canSelect)
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            
                            StartCoroutine(unselect(units.Count - 1));

                        }

                    }
                }
            
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public IEnumerator unselect(int removeInt)
    {
        while (removeInt >=  0)
        {

            if (units.Count > 0)
            {
                unitmanager.unselect = true;

                yield return new WaitForSeconds(0.1f);
                if (units[removeInt])
                {
                    if (units[removeInt].GetComponent<SelectableUnitComponent>().Selected)
                    {
                        units[removeInt].GetComponent<SelectableUnitComponent>().Selected = false;
                    }

                    if (units[removeInt].GetComponent<SelectableUnitComponent>().selectionCircle != null)
                    {
                        Destroy(units[removeInt].GetComponent<SelectableUnitComponent>().selectionCircle.gameObject);
                        units[removeInt].GetComponent<SelectableUnitComponent>().selectionCircle = null;
                    }
                    unitmanager.unselect = false;
                }

                units.Remove(units[removeInt]);
                removeInt -= 1;
            }

        }
  
    }

}