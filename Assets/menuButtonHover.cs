using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class menuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Vector2 normalSize;
    public Vector2 hoverSize;
    public float smooth;

    bool expand;

    public void OnPointerEnter(PointerEventData eventData)
    {
        expand = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        expand = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            expand = false;
        }
        if(expand)
        {
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(transform.GetChild(0).GetComponent<RectTransform>().sizeDelta, hoverSize, smooth * Time.deltaTime);

        }
        else
        {
            transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(transform.GetChild(0).GetComponent<RectTransform>().sizeDelta, normalSize, smooth * Time.deltaTime);

        }
    }


}
