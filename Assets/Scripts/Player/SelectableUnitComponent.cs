using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class SelectableUnitComponent : MonoBehaviour
{
    public GameObject selectionCircle;
    public bool Selected;
    public UnityEngine.AI.NavMeshAgent nav;
    Vector3 destination;
    private GameObject target;

    // public OnHover[] onhover;

    /* public OnHover onhover1;
     public OnHover onhover2;
     public OnHover onhover3;
     public OnHover onhover4;
     public OnHover onhover5;
     public OnHover onhover6;
     public OnHover onhover7;
     public OnHover onhover8;
     public OnHover onhover9;
     public OnHover onhover10;
     public OnHover onhover11;*/

    public UnitManager unitmanager;
    public UnitSelectionComponent unitSelectionComponent;

    public GameObject UnitsCanvas;
    public bool canMove;

    public bool muiltple;

    public bool Moving;

    public bool test;

    bool addUnitList;
    public bool Hover;

    public GameObject trans;
    GameObject oldTrans;

    public LayerMask mask;

    public GameObject canvas;


    private float distance2 = 2;

    public bool loaded;
    StorageInventory data;
    DayNightCycle gameTime;
    public bool inJob;

    float fixtime = .02f;

    GameObject manager;
    WeaponData[] weapon;

    public GameObject[] panels;
    float selectedTime = 0.05f;
    public GameObject inventory;
    void Start()
    {
        manager = GameObject.Find("Manager");
        weapon = FindObjectsOfType(typeof(WeaponData)) as WeaponData[];
        if (GameObject.Find("BrokenPlane"))
        {
            if ((transform.position - GameObject.Find("BrokenPlane").transform.position).sqrMagnitude < 20)
            {
                transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
            }
        }
        nav = gameObject.GetComponent<NavMeshAgent>();


    }

    public void load()
    {
        if (!loaded)
        {

        }
    }

    void Awake()
    {
        if (UnitsCanvas == null)
        {
            UnitsCanvas = gameObject.transform.GetChild(3).gameObject;
        }
        nav = gameObject.GetComponent<NavMeshAgent>();
        Selected = true;
        unitmanager = GameObject.Find("Manager").GetComponent<UnitManager>();
        if (!unitSelectionComponent)
        {
            unitSelectionComponent = GameObject.Find("Player").GetComponent<UnitSelectionComponent>();
        }
        //       unitSelectionComponent.muiltiple = true;
        //   StartCoroutine(fixBug());
        canvas.SetActive(true);
        Debug.Log("Starting" + gameObject.name);
        loaded = true;
        if (GameObject.Find("Camera"))
        {
            gameTime = GameObject.Find("Camera").GetComponent<DayNightCycle>();
        }
        if (GameObject.Find("Storage"))
        {
            data = GameObject.Find("Storage").GetComponent<StorageInventory>();
        }//     onhover1 = gameObject.transform.GetChild(3).transform.GetChild(0).GetComponent<OnHover>();
    }

    /* IEnumerator fixBug()
     {

     }*/

    void Update()
    {
        if (!nav)
        {
            nav = gameObject.GetComponent<NavMeshAgent>();
        }
        if (Vector3.Distance(transform.position, nav.pathEndPosition) < 0.5f)
        {
            nav.path = null;
            nav.isStopped = true;
        }

        float velocity = nav.velocity.magnitude;
        if (velocity > 1)
        {
            nav.updateRotation = false;

            transform.rotation = Quaternion.LookRotation(nav.velocity.normalized);
        }

        if (fixtime <= 1)
        {
            fixtime -= Time.deltaTime;
        }
        if (fixtime <= 0)
        {
            //    unitSelectionComponent.muiltiple = false;
            Selected = false;
            remove();
            UnitsCanvas.SetActive(false);
            canvas.SetActive(true);
            Debug.Log("fixed");
            fixtime = 5;
        }

        RaycastHit hit;

        /*   if (onhover1.selectied == true || onhover2.selectied == true || onhover3.selectied == true || onhover4.selectied == true || onhover5.selectied == true || onhover6.selectied == true || onhover7.selectied == true || onhover8.selectied == true || onhover9.selectied == true || onhover10.selectied == true || onhover11.selectied == true)
           {
               Hover = true;
           }
           else if (onhover1.selectied == false || onhover2.selectied == false || onhover3.selectied == false || onhover4.selectied == false || onhover5.selectied == false || onhover6.selectied == false || onhover7.selectied == false || onhover8.selectied == false || onhover9.selectied == false || onhover10.selectied == false || onhover11.selectied == false)
           {
               Hover = false;
           }*/

        if (Hover == false)
        {


            if (Input.GetMouseButtonDown(0))
            {
                if (Selected)
                {
                    //  nav.Resume();

                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 5000))
                    {


                        if (!inJob)
                        {
                            if (canMove)
                            {

                                if (hit.transform.tag != "Player" && hit.transform.gameObject.layer != 4)
                                {
                                    NavMeshPath path = new NavMeshPath();

                                    NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path);
                                    nav.path = path;
                                    unitSelectionComponent.muiltiple = false;
                                    UnitsCanvas.SetActive(false);
                                    Debug.Log(hit.transform.name);
                                    if (unitmanager.inMission)
                                    {
                                        gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                    else
                                    {
                                        gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                    Moving = true;
                                    StartCoroutine(unitSelectionComponent.unselect(unitSelectionComponent.units.Count - 1)) ;

                                }

                            }
                        }

                    }

                }



            }

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 5000))
                {
                    if (hit.transform.tag != "Weapon" && hit.transform.tag != "Item")
                    {
                        unitmanager.units.Remove(gameObject);

                        Selected = false;
                        UnitsCanvas.SetActive(false);
                    }
                }
            }
            if (Vector3.Distance(transform.position, nav.destination) < distance2)
            {
                Moving = false;
            }
        }

        if (manager)
        {
            if (manager.GetComponent<GameManager>())
            {
                canMove = manager.GetComponent<GameManager>().canMove;
            }
        }


        if (selectionCircle != null)
        {
            selectionCircle.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<NameDisplay>().Player = gameObject;
            selectionCircle.transform.SetParent(transform);
            selectionCircle.transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            selectionCircle.SetActive(true);
        }



        if (Selected)
        {
            if (!GameObject.Find("Player").GetComponent<UnitSelectionComponent>().units.Contains(gameObject))
            {
                GameObject.Find("Player").GetComponent<UnitSelectionComponent>().units.Add(gameObject);
            }
        }
        else
        {
            if (GameObject.Find("Player").GetComponent<UnitSelectionComponent>().units.Contains(gameObject))
            {
                GameObject.Find("Player").GetComponent<UnitSelectionComponent>().units.Remove(gameObject);
            }
            if (selectionCircle)
            {
                Destroy(selectionCircle);
            }
            inventory.GetComponent<CanvasGroup>().blocksRaycasts = false;
            inventory.GetComponent<CanvasGroup>().alpha = 0;
        }


        OnDrawGizmosSelected();
        if (Selected)
        {
            if (!unitmanager.units.Contains(gameObject))
            {
                unitmanager.selectedUnits.Add(gameObject);
            }


            selectedTime -= Time.unscaledDeltaTime;

            if (selectedTime >= 0)
            {
                if (nav.hasPath)
                {

                    nav.ResetPath();
                }
            }

        }
        if (!Selected)
        {
            selectedTime = 0.05f;
            if (manager.GetComponent<UnitManager>().units.Contains(gameObject))
            {
                manager.GetComponent<UnitManager>().units.Remove(gameObject);

            }
        }
        if (addUnitList == true)
        {
            addunit();
            addUnitList = false;
        }


        if (Hover == false)
        {

            if (muiltple == false)
            {
                if (Selected)
                {
                    canvas.GetComponent<Canvas>().enabled = true;
                }
                else if (!Selected)
                {
                    canvas.GetComponent<Canvas>().enabled = false;
                }
                unitmanager.units.Remove(gameObject);


            }
            if (muiltple == true)
            {

                if (!unitmanager.units.Contains(gameObject))
                {
                    if (Selected)
                    {

                        gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<Canvas>().enabled = false;
                        //UnitsCanvas.SetActive(true);
                    }
                }
                if (!Selected)
                {
                    unitmanager.units.Remove(gameObject);

                    UnitsCanvas.SetActive(false);
                    gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<Canvas>().enabled = false;
                    // addUnitList = true;
                }
            }

        }


    }

    IEnumerator removeUnit()
    {
        yield return new WaitForSeconds(0.1f);
        unitSelectionComponent.muiltiple = false;
    }

    void addunit()
    {
        unitmanager.units.Add(gameObject);
    }

    void remove()
    {
        unitmanager.units.Remove(gameObject);
    }

    void OnDrawGizmosSelected()
    {

        var nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.sortingLayerName = "Background";
            line.sortingLayerID = -1000;
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.white };
            line.SetWidth(0.2f, 0.2f);
            line.SetColors(Color.white, Color.white);
            //  line.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }

        var path = nav.path;
        line.numCornerVertices = 5;
        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }

}