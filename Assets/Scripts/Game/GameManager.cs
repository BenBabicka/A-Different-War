using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour {

    public GameObject ArchitectPanel;
    public GameObject JobPanel;
    public GameObject ResearchPanel;
    public GameObject StoragePanel;
    public GameObject craftingUI;
    public GameObject MapPanel;

    public GameObject Job;
    public GameObject Contect;


    public bool canMove;

    public Toggle pause;
    public Toggle x1;
    public Toggle x2;
    public Toggle x3;
    public Toggle x4;

     public Image loadingplane;
    public Image loadingEffect;

    public float timer = 10;

    public List<GameObject> Buildingslist;

    public List<string> researchList;

    public Transform UICanvas;

    public bool selected;
    public GameObject selectedInformation;
    GameObject selectedInformationInstance;
    public GameObject informationHolder;
    GameObject hitObj;

    public GameObject priorityButtonPrefab;
    GameObject priorityButtonInstance;
    public GameObject priorityButtonHolder;

    public List<Transform> allNature;

    public bool hovingOverUi;

    public Camera mainCam;



    void Awake () {
        JobPanel.SetActive(false);

    }

    public void Pause()
    {
        x1.isOn = false;
        x2.isOn = false;
        x3.isOn = false;
        x4.isOn = false;
        Time.timeScale = 0;
    }

    public void X1()
    {
        pause.isOn = false;
        x2.isOn = false;
        x3.isOn = false;
        x4.isOn = false;
        Time.timeScale = 1;
    }
    public void X2()
    {
        pause.isOn = false;
        x1.isOn = false;
        x3.isOn = false;
        x4.isOn = false;
        Time.timeScale = 2;
    }
    public void X3()
    {
        pause.isOn = false;
        x2.isOn = false;
        x1.isOn = false;
        x4.isOn = false;
        Time.timeScale = 4;
    }
    public void X4()
    {
        pause.isOn = false;
        x2.isOn = false;
        x3.isOn = false;
        x1.isOn = false;
        Time.timeScale =  6;
    }

    void Update()
    {
       
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (selectedInformationInstance)
            {
                selectedInformationInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hitObj.transform.GetComponent<Nature>().Name;

                selectedInformationInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hitObj.transform.GetComponent<Nature>().Information;
            }
            if(hovingOverUi)
        {
            mainCam.GetComponent<RtsCameraKeys>().AllowZoom = false;
            mainCam.GetComponent<RtsCameraMouse>().AllowZoom = false;

        }
            else
        {
            mainCam.GetComponent<RtsCameraKeys>().AllowZoom = true;
            mainCam.GetComponent<RtsCameraMouse>().AllowZoom = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(!GameObject.Find("Player").GetComponent<UnitSelectionComponent>().hover)
            {
                if (ArchitectPanel.activeSelf == true)
                {
                    ArchitectPanel.SetActive(false);
                }
                if (ResearchPanel.activeSelf == true)
                {
                    ResearchPanel.SetActive(false);
                }
                if (StoragePanel.activeSelf == true)
                {
                    StoragePanel.SetActive(false);
                }
                if (craftingUI.GetComponent<CraftingSelectPanel>().disable == false)
                {
                    craftingUI.GetComponent<CraftingSelectPanel>().disable = true;
                }
                if (MapPanel.GetComponent<MissionPanel>().disable == false)
                {
                    MapPanel.GetComponent<MissionPanel>().disable = true;
                }
                if (JobPanel.activeSelf == true)
                {
                    JobPanel.SetActive(false);
                }
            }

        }

        if (Input.GetMouseButtonDown(0))
            {
            if (gameObject.GetComponent<UnitManager>().hover == false)
            {
                selected = false;
            }
            if (Physics.Raycast(ray, out hit, 2000))
            {
                if (gameObject.GetComponent<OnHover>().hover == false)
                {
                    if (gameObject.GetComponent<UnitManager>().units.Count == 0)
                        {
                        if (hit.transform.tag == "Tree" || hit.transform.tag == "Rock")
                        {
                            hitObj = hit.transform.gameObject;
                            if (selectedInformationInstance)
                            {
                                Destroy(selectedInformationInstance);

                            }

                            selected = true;
                            selectedInformationInstance = Instantiate(selectedInformation, new Vector3(300, 55), Quaternion.identity) as GameObject;
                            selectedInformationInstance.transform.localScale = informationHolder.transform.localScale;
                            selectedInformationInstance.transform.SetParent(informationHolder.transform, false);
                            selectedInformationInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
                            selectedInformationInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            selectedInformationInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);



                        }
                        if (hit.transform.tag == "Tree")
                        {

                            if (priorityButtonInstance)
                            {
                                Destroy(priorityButtonInstance);

                            }

                            priorityButtonInstance = Instantiate(priorityButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            priorityButtonInstance.transform.localScale = priorityButtonHolder.transform.localScale;
                            priorityButtonInstance.transform.SetParent(priorityButtonHolder.transform, false);
                            priorityButtonInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            priorityButtonInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
                            priorityButtonInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);


                            priorityButtonInstance.GetComponent<PritoryButton>().objectForPritory = hit.transform.gameObject;
                            priorityButtonInstance.GetComponent<PritoryButton>().buttonType = PritoryButton.pritoryButtonList.tree;
                        }
                        if (hit.transform.tag == "Rock")
                        {

                            if (priorityButtonInstance)
                            {
                                Destroy(priorityButtonInstance);

                            }

                            priorityButtonInstance = Instantiate(priorityButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            priorityButtonInstance.transform.localScale = UICanvas.transform.localScale;
                            priorityButtonInstance.transform.SetParent(priorityButtonHolder.transform, false);
                            priorityButtonInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            priorityButtonInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
                            priorityButtonInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);


                            priorityButtonInstance.GetComponent<PritoryButton>().objectForPritory = hit.transform.gameObject;
                            priorityButtonInstance.GetComponent<PritoryButton>().buttonType = PritoryButton.pritoryButtonList.rock;

                        }
                        if (hit.transform.tag == "JobBuilding")
                        {
                            selected = true;

                            if (priorityButtonInstance)
                            {
                                Destroy(priorityButtonInstance);

                            }

                            priorityButtonInstance = Instantiate(priorityButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            priorityButtonInstance.transform.localScale = UICanvas.transform.localScale;
                            priorityButtonInstance.transform.SetParent(priorityButtonHolder.transform, false);
                            priorityButtonInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                            priorityButtonInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
                            priorityButtonInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);


                            priorityButtonInstance.GetComponent<PritoryButton>().objectForPritory = hit.transform.gameObject;

                            priorityButtonInstance.GetComponent<PritoryButton>().buttonType = PritoryButton.pritoryButtonList.building;

                        }
                    }
                } }
            }
            if (Input.GetMouseButtonDown(1))
            {
           
                selected = false;
            
            }
            if (selected == false)
            {
                Destroy(selectedInformationInstance);
                selectedInformationInstance = null;
            Destroy(priorityButtonInstance);
            priorityButtonInstance = null;
            }
        if ( GameObject.Find("Player").GetComponent<UnitSelectionComponent>().units.Count > 0)
        {
            Destroy(selectedInformationInstance);
            selectedInformationInstance = null;
            Destroy(priorityButtonInstance);
            priorityButtonInstance = null;
        }
  
        for (int i = 0; i < researchList.Count; i++)
        {
            if(researchList[i] == null)
            {
                researchList.Remove(researchList[i]);
            }
        }

        if (loadingEffect != null && loadingplane != null)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                loadingplane.CrossFadeAlpha(0, .5f, true);  
                loadingEffect.CrossFadeAlpha(0, .5f, true);
            }
            if (timer <= -2)
            {
                loadingEffect.enabled = false;
                loadingplane.enabled = false;
            }
        }
        if (GameObject.FindGameObjectWithTag("JobBuilding"))
        {
            if (!Buildingslist.Contains(GameObject.FindGameObjectWithTag("JobBuilding")) && GameObject.FindGameObjectWithTag("JobBuilding").GetComponent<Building>().built == false)
            {
                Buildingslist.Add(GameObject.FindGameObjectWithTag("JobBuilding"));
            }
        }

        if (GameObject.FindGameObjectWithTag("WallJobBuilding"))
        {
            if (!Buildingslist.Contains(GameObject.FindGameObjectWithTag("WallJobBuilding")) && GameObject.FindGameObjectWithTag("WallJobBuilding").GetComponent<Building>().built == false)
            {
                Buildingslist.Add(GameObject.FindGameObjectWithTag("WallJobBuilding"));
            }
        }
    }

  

    public void TurnOnJobPanel()
    {
        if (ArchitectPanel.activeSelf == true)
        {
            ArchitectPanel.SetActive(false);
        }
        if (ResearchPanel.activeSelf == true)
        {
            ResearchPanel.SetActive(false);
        }
        if (StoragePanel.activeSelf == true)
        {
            StoragePanel.SetActive(false);
        }
        if (craftingUI.GetComponent<CraftingSelectPanel>().disable == false)
        {
            craftingUI.GetComponent<CraftingSelectPanel>().disable = true;
        }
        if (MapPanel.GetComponent<MissionPanel>().disable == false)
        {
            MapPanel.GetComponent<MissionPanel>().disable = true;
        }
        JobPanel.SetActive(!JobPanel.activeSelf);
        
        
    }
    public void TurnOnCraftingUI()
    {
        if (ArchitectPanel.activeSelf == true)
        {
            ArchitectPanel.SetActive(false);
        }
        if (ResearchPanel.activeSelf == true)
        {
            ResearchPanel.SetActive(false);
        }
        if (StoragePanel.activeSelf == true)
        {
            StoragePanel.SetActive(false);
        }
        if (JobPanel.activeSelf == true)
        {
            JobPanel.SetActive(false);
        }
        if (MapPanel.GetComponent<MissionPanel>().disable == false)
        {
            MapPanel.GetComponent<MissionPanel>().disable = true;
        }
        craftingUI.GetComponent<CraftingSelectPanel>().disable = !craftingUI.GetComponent<CraftingSelectPanel>().disable;
        
    }
    public  void TurnOnArchitectPanel()
    {
        if (JobPanel.activeSelf == true)
        {
            JobPanel.SetActive(false);
        }
        if (ResearchPanel.activeSelf == true)
        {
            ResearchPanel.SetActive(false);
        }
        if (StoragePanel.activeSelf == true)
        {
            StoragePanel.SetActive(false);
        }
        if (craftingUI.GetComponent<CraftingSelectPanel>().disable == false)
        {
            craftingUI.GetComponent<CraftingSelectPanel>().disable = true;
        }
        ArchitectPanel.SetActive(!ArchitectPanel.activeSelf);
        if (MapPanel.GetComponent<MissionPanel>().disable == false)
        {
            MapPanel.GetComponent<MissionPanel>().disable = true;
        }
    }
public  void TurnOnResearchPanel()
    {
        if (JobPanel.activeSelf == true)
        {
            JobPanel.SetActive(false);
        }
        if (ArchitectPanel.activeSelf == true)
        {
            ArchitectPanel.SetActive(false);
        }
        if (StoragePanel.activeSelf == true)
        {
            StoragePanel.SetActive(false);
        }
        if (craftingUI.GetComponent<CraftingSelectPanel>().disable == false)
        {
            craftingUI.GetComponent<CraftingSelectPanel>().disable = true;
        }
        ResearchPanel.SetActive(!ResearchPanel.activeSelf);
        if (MapPanel.GetComponent<MissionPanel>().disable == false)
        {
            MapPanel.GetComponent<MissionPanel>().disable = true;
        }
    }

    public void TurnOnStoragePanel()
    {
        if (ArchitectPanel.activeSelf == true)
        {
            ArchitectPanel.SetActive(false);
        }
        if (ResearchPanel.activeSelf == true)
        {
            ResearchPanel.SetActive(false);
        }
        if (JobPanel.activeSelf == true)
        {
            JobPanel.SetActive(false);
        }
        if (craftingUI.GetComponent<CraftingSelectPanel>().disable == false)
        {
            craftingUI.GetComponent<CraftingSelectPanel>().disable = true;
        }
        if (MapPanel.GetComponent<MissionPanel>().disable == false)
        {
            MapPanel.GetComponent<MissionPanel>().disable = true;
        }
        StoragePanel.SetActive(!StoragePanel.activeSelf);
    }

    public void ToggleMapPanel()
    {
        if (ArchitectPanel.activeSelf == true)
        {
            ArchitectPanel.SetActive(false);
        }
        if (ResearchPanel.activeSelf == true)
        {
            ResearchPanel.SetActive(false);
        }
        if (StoragePanel.activeSelf == true)
        {
            StoragePanel.SetActive(false);
        }
        if (JobPanel.activeSelf == true)
        {
            JobPanel.SetActive(false);
        }
        MapPanel.GetComponent<MissionPanel>().disable = !MapPanel.GetComponent<MissionPanel>().disable;



    }
}
