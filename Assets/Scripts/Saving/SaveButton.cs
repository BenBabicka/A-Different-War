using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveButton : MonoBehaviour {

    public string saveFileName;
    public InputField saveInputField;

     void Update()
    {
        saveFileName = saveInputField.text; 
    }

    public void save()
    {
        GameObject.Find("SaveManager").GetComponent<SaveManager>().saveFileName = saveFileName;
        GameObject.Find("SaveManager").GetComponent<SaveManager>().Save() ;
        transform.parent.gameObject.SetActive(false);
    }

}
