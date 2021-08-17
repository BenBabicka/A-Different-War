using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageManager : MonoBehaviour {

    public GameObject messageArea;
    public RectTransform messagerect;
    public GameObject messageBox;
  public  bool spawn;
	// Use this for initialization
	void Start () {
        SpawnMessage("Welcome!", Color.white, "Welcome", "Welcome to A Different War, your objective is to survive as long as possible and expand your influnce. Good Luck!");
        
	}
	
    void Update()
    {
        if (spawn)
        {
            SpawnMessage("test", Color.cyan, "text", "test test test test");
            spawn = false;
        }
    }
	// Update is called once per frame
	public void SpawnMessage (string messageText, Color textCol, string title, string messageT) {
      
            GameObject message = Instantiate(messageBox, transform.position, Quaternion.identity);
            message.transform.SetParent(messageArea.transform);
            messagerect.sizeDelta += new Vector2(0, messageBox.GetComponent<RectTransform>().sizeDelta.y + 2);
            message.GetComponent<MessageBox>().message = messageText;
        message.GetComponent<MessageBox>().textColour = textCol;
        message.transform.GetChild(1).GetComponent<MessageButton>().title = title;
        message.transform.GetChild(1).GetComponent<MessageButton>().message = messageT;


        message.SetActive(true);

	}
}
