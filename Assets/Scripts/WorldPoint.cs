using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WorldPoint : MonoBehaviour {

    public float radius;
    [Space]
    public GameObject map;
    public GameObject spawner;
    [Space]

    GameObject cloestPoint;
    GameObject secondPoint;
    GameObject thirdPoint;
    GameObject fourthPoint;

    bool gotPointsBool;
    bool gotPointsBool2;
    bool gotPointsBool3;
    bool gotPointsBool4;

    float gotPoints = 3.5f;
    float gotPoints2 = 1f;
    float gotPoints3 = .5f;
    float gotPoints4 = .5f;
    [Space]
    [Header("Closest Bases")]
    public List<GameObject> otherWorldPoints;
    public List<GameObject> secondClostestPoints;
    public List<GameObject> thirdclostestPoint;
    public List<GameObject> fourthclostestPoint;



    [Space]

    [Header("Paths")]
    public GameObject path;
    float waitTimer = 3f;
    bool path1bool;
    bool path2bool;
    bool path3bool;
    bool path4bool;

    public GameObject pathParent;

    [Space]
    [Header("Base Data")]
    public float baseLevel;
    [Range(0,100)]
    public float agression;
    public float attitude;

    [Space]
    [Header("Information")]
    public GameObject infomationPanelParent;
    public GameObject infoPanel;
    public GameObject infopanelinstance;

    bool allPathsSet;
    void Start()
    {
        baseLevel = Random.Range(0, 5);
        agression = Random.Range(0, 100);
        if(agression >= 0 && agression < 10)
        {
            attitude = 10;
        }
        if (agression >= 10 && agression < 20)
        {
            attitude = 5;
        }
        if (agression >= 20 && agression < 30)
        {
            attitude = 0;
        }
        if (agression >= 30 && agression < 40)
        {
            attitude = -5;
        }
        if (agression >= 40 && agression < 50)
        {
            attitude = -10;
        }
        if (agression >= 50 && agression < 60)
        {
            attitude = -15;
        }
        if (agression >= 60 && agression < 70)
        {
            attitude = -20;
        }
        if (agression >= 70 && agression < 80)
        {
            attitude = -30;
        }
        if (agression >= 80 && agression < 90)
        {
            attitude = -40;
        }
        if (agression >= 90 && agression <= 100)
        {
            attitude = -50;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        gotPoints -= Time.deltaTime;
        if (gotPoints < 0 && !gotPointsBool)
        {
            foreach (var point in map.GetComponent<WorldMap>().worldPoints)
            {
                if (!otherWorldPoints.Contains(point) && point != gameObject)
                {
                    otherWorldPoints.Add(point);
                }
            }

            gotPointsBool = true;

        }

        if(infopanelinstance)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Destroy(infopanelinstance);
            }
        }

        if(!allPathsSet)
        {
            if (gotPointsBool)
            {
                GameObject tMin = null;
                float minDist = Mathf.Infinity;
                Vector3 currentPos = transform.position;

                GameObject tMin2 = null;
                float minDist2 = Mathf.Infinity;
                Vector3 currentPos2 = transform.position;

                GameObject tMin3 = null;
                float minDist3 = Mathf.Infinity;
                Vector3 currentPos3 = transform.position;

                GameObject tMin4 = null;
                float minDist4 = Mathf.Infinity;
                Vector3 currentPos4 = transform.position;

                foreach (var point in otherWorldPoints)
                {
                    float dist = Vector3.Distance(point.transform.position, currentPos);
                    if (dist < minDist)
                    {
                        tMin = point;
                        minDist = dist;
                        cloestPoint = tMin;

                    }
                }
                if (cloestPoint)
                {
                    gotPoints2 -= Time.deltaTime;
                    if (gotPoints2 < 0 && !gotPointsBool2)
                    {
                        foreach (var point in map.GetComponent<WorldMap>().worldPoints)
                        {
                            if (!secondClostestPoints.Contains(point) && point != gameObject && point != cloestPoint)
                            {
                                secondClostestPoints.Add(point);
                            }
                        }
                        gotPointsBool2 = true;
                    }


                    foreach (var point in secondClostestPoints)
                    {
                        float dist = Vector3.Distance(point.transform.position, currentPos2);
                        if (dist < minDist2)
                        {

                            tMin2 = point;
                            minDist2 = dist;
                            secondPoint = tMin2;


                        }
                    }
                }
                if (secondPoint)
                {
                    gotPoints3 -= Time.deltaTime;
                    if (gotPoints3 < 0 && !gotPointsBool3)
                    {
                        foreach (var point in map.GetComponent<WorldMap>().worldPoints)
                        {
                            if (!thirdclostestPoint.Contains(point) && point != gameObject && point != cloestPoint && point != secondPoint)
                            {
                                thirdclostestPoint.Add(point);
                            }
                        }
                        gotPointsBool3 = true;
                    }


                    foreach (var point in thirdclostestPoint)
                    {
                        float dist = Vector3.Distance(point.transform.position, currentPos3);
                        if (dist < minDist3)
                        {

                            tMin3 = point;
                            minDist3 = dist;
                            thirdPoint = tMin3;


                        }
                    }
                }
                if (thirdPoint)
                {
                    gotPoints4 -= Time.deltaTime;
                    if (gotPoints4 < 0 && !gotPointsBool4)
                    {
                        foreach (var point in map.GetComponent<WorldMap>().worldPoints)
                        {
                            if (!fourthclostestPoint.Contains(point) && point != gameObject && point != cloestPoint && point != secondPoint && point != thirdPoint)
                            {
                                fourthclostestPoint.Add(point);
                            }
                        }
                        gotPointsBool4 = true;
                    }


                    foreach (var point in fourthclostestPoint)
                    {
                        float dist = Vector3.Distance(point.transform.position, currentPos4);
                        if (dist < minDist4)
                        {

                            tMin4 = point;
                            minDist4 = dist;
                            fourthPoint = tMin4;


                        }
                    }
                }
                if (Vector3.Distance(transform.position, cloestPoint.transform.position) < radius)
                {
                    spawner.GetComponent<WorldGenerator>().randomPosition(gameObject);
                }
                if (cloestPoint)
                {

                    waitTimer -= Time.deltaTime;
                    if (waitTimer < 0)
                    {
                        if (!path1bool)
                        {
                            minDist = Mathf.Infinity;

                            Vector3 pos = new Vector3(((transform.position.x + cloestPoint.transform.position.x) / 2), ((transform.position.y + cloestPoint.transform.position.y) / 2), 0);


                            GameObject path1 = Instantiate(path, pos, Quaternion.identity);

                            path1.transform.SetParent(pathParent.transform);

                            var dir = cloestPoint.transform.position - transform.position;
                            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                            path1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                            //rotation
                            float distance = Vector3.Distance(transform.position, cloestPoint.transform.position);
                            path1.GetComponent<RectTransform>().sizeDelta = new Vector2(path1.GetComponent<RectTransform>().sizeDelta.x, distance);

                            path1bool = true;
                        }
                        if (secondPoint)
                        {
                            if (!path2bool)
                            {
                                minDist2 = Mathf.Infinity;

                                Vector3 pos2 = new Vector3(((transform.position.x + secondPoint.transform.position.x) / 2), ((transform.position.y + secondPoint.transform.position.y) / 2), 0);


                                GameObject path2 = Instantiate(path, pos2, Quaternion.identity);

                                path2.transform.SetParent(pathParent.transform);

                                var dir2 = secondPoint.transform.position - transform.position;
                                var angle2 = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg + 90;
                                path2.transform.rotation = Quaternion.AngleAxis(angle2, Vector3.forward);

                                //rotation
                                float distance2 = Vector3.Distance(transform.position, secondPoint.transform.position);
                                path2.GetComponent<RectTransform>().sizeDelta = new Vector2(path2.GetComponent<RectTransform>().sizeDelta.x, distance2);

                                path2bool = true;


                            }
                            if (thirdPoint)
                            {
                                if (!path3bool)
                                {
                                    minDist3 = Mathf.Infinity;

                                    Vector3 pos3 = new Vector3(((transform.position.x + thirdPoint.transform.position.x) / 2), ((transform.position.y + thirdPoint.transform.position.y) / 2), 0);


                                    GameObject path3 = Instantiate(path, pos3, Quaternion.identity);

                                    path3.transform.SetParent(pathParent.transform);

                                    var dir3 = thirdPoint.transform.position - transform.position;
                                    var angle3 = Mathf.Atan2(dir3.y, dir3.x) * Mathf.Rad2Deg + 90;
                                    path3.transform.rotation = Quaternion.AngleAxis(angle3, Vector3.forward);

                                    //rotation
                                    float distance3 = Vector3.Distance(transform.position, thirdPoint.transform.position);
                                    path3.GetComponent<RectTransform>().sizeDelta = new Vector2(path3.GetComponent<RectTransform>().sizeDelta.x, distance3);

                                    path3bool = true;


                                }
                                if (fourthPoint)
                                {
                                    if (!path4bool)
                                    {
                                        minDist4 = Mathf.Infinity;

                                        Vector3 pos4 = new Vector3(((transform.position.x + fourthPoint.transform.position.x) / 2), ((transform.position.y + fourthPoint.transform.position.y) / 2), 0);


                                        GameObject path4 = Instantiate(path, pos4, Quaternion.identity);

                                        path4.transform.SetParent(pathParent.transform);

                                        var dir4 = fourthPoint.transform.position - transform.position;
                                        var angle4 = Mathf.Atan2(dir4.y, dir4.x) * Mathf.Rad2Deg + 90;
                                        path4.transform.rotation = Quaternion.AngleAxis(angle4, Vector3.forward);

                                        //rotation
                                        float distance4 = Vector3.Distance(transform.position, fourthPoint.transform.position);
                                        path4.GetComponent<RectTransform>().sizeDelta = new Vector2(path4.GetComponent<RectTransform>().sizeDelta.x, distance4);
                                        allPathsSet = true;
                                        path4bool = true;
                                        

                                    }

                                }
                            }
                        }
                    }

                }
            }
        }

    }

    public void openInfoPanal()
    {
        infopanelinstance = Instantiate(infoPanel,transform.position,Quaternion.identity);
        infopanelinstance.transform.SetParent(infomationPanelParent.transform);
        infopanelinstance.transform.position = new Vector3(transform.position.x, transform.position.y - 60, transform.position.z);
        infopanelinstance.transform.localScale = new Vector3(1, 1, 1);
        infopanelinstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Name";
        infopanelinstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Base level : " + baseLevel + "\n" + "Agression  : " + agression + "\n" + "Attiude       : " + attitude;


    }
}
