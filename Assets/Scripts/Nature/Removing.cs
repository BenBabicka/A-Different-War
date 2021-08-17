using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removing : MonoBehaviour {

   public List<GameObject> Trees;

  
    void Start()
    {
    
        if (!Trees.Contains(FindObjectOfType<StoneManager>().transform.gameObject))
        {
            Trees.Add(FindObjectOfType<StoneManager>().transform.gameObject);
        }
    }
}
