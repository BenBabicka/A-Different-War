using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MessageButton : MonoBehaviour, IPointerClickHandler
{

    GameObject parent;
    public string title;
    public string message;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject mes = Instantiate(parent.GetComponent<MessageBox>().messageBox, parent.GetComponent<MessageBox>().messageBox.transform.parent.transform.position, transform.rotation);
            mes.transform.SetParent(parent.GetComponent<MessageBox>().messageBox.transform.parent, false);
           mes.transform.position = parent.GetComponent<MessageBox>().messageBox.transform.parent.transform.position;
            mes.transform.GetChild(1).GetComponent<Text>().text = title;
            mes.transform.GetChild(2).GetComponent<Text>().text = message;

            mes.SetActive(true);
            transform.parent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, transform.GetComponent<RectTransform>().sizeDelta.y + 2);
            Destroy(parent);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            transform.parent.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, transform.GetComponent<RectTransform>().sizeDelta.y + 2);
            Destroy(parent);
        }
    }
}
