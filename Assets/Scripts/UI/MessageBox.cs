using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MessageBox : MonoBehaviour
{

    public string message;
    public Text messageText;
    public Color textColour;

    public GameObject messageBox;

	// Use this for initialization
	void Start () {
        messageText.text = message;
	}
	
	// Update is called once per frame
	void Update () {
        messageText.color = textColour;
	}



    
}
