using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CountryInfo : MonoBehaviour {


    //Renfences
    public Image country_flag;
    public Text country_Relation;
    public Text textBox;
    public Text isImprovingText;
    [Space]

    //Panels
    public GameObject diplomacyPanel;
    public GameObject tradePanel;
    [Space]

    //Floats
    public float relation;
    [Space]

    //Dialog
    public TextAsset textFile;
    public string[] textLines;
    [Space]

    //Buttons
    public Button[] countrys;
    public Button improveRelationButton;
    [Space]

    bool greating;
    [Space]

    float days;
    [HideInInspector]
    public bool isImproving;


   

    // Use this for initialization
    void Start () {
		if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
       
	}
	// Update is called once per frame
	void Update () {

        if(relation <= -100)
        {
            relation = -100;
        }
        if (relation >= 100)
        {
            relation = 100;
        }
        foreach (var country in countrys)
        {
            country.onClick.AddListener(closeTab);
           
        }

        if (diplomacyPanel.activeSelf == false)
        {
            greating = true;
        }

        if (greating == true)
        {
            if (relation >= -10 & relation <= 10)
            {
                textBox.text = textLines[0];
                greating = false;
            }
            if (relation >= -50 & relation <= -11)
            {
                textBox.text = textLines[3];
                greating = false;
            }
            if (relation >= -1000 & relation <= -51)
            {
                textBox.text = textLines[4];
                greating = false;
            }
           
            if (relation >= 11 & relation <= 50)
            {
                textBox.text = textLines[1];
                greating = false;
            }
            if (relation >= 51 & relation <= 1000)
            {
                textBox.text = textLines[5];
                greating = false;
            }
        
        }

        if(isImproving == true)
        {
            isImprovingText.text = "Cancel Improving Relationship";
      
        }
        else
        {
            isImprovingText.text = "Improve Relationship";
        }

        country_Relation.text = relation.ToString();
    }
    void closeTab()
    {
        diplomacyPanel.SetActive(false);
    }

    public void Toggelpanel()
    {
        diplomacyPanel.SetActive(!diplomacyPanel.activeSelf);
    }

    public void ImpoveRelation()
    {
        isImproving =! isImproving;
    }

    public void Trade()
    {
        tradePanel.SetActive(!tradePanel.activeSelf);
    }

    public void OfferGift()
    {
        //Offer A Gift
    }

    public void DemandSupplies()
    {
        //Deamand Supplies
    }

    public void Insult()
    {
        relation -= 5;
        textBox.text = textLines[Random.Range(20, 23)];

    }

    public void DelcareWar()
    {
        improveRelationButton.interactable = false;
        isImproving = false;
        if (relation >= -25 & relation <= 1000)
        {
            textBox.text = textLines[23];
            relation = -100;

        }
        else if(relation >= -1000 && relation <= -25)
        {
            textBox.text = textLines[Random.Range(24, 27)];
            relation = -100;

        }
    }

}
