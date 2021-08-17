using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class JobUIManager : MonoBehaviour {

    Toggle mytoggle;



    public List<Toggle> toggles;

    bool one;

    void Start()
    {
        mytoggle = gameObject.GetComponent<Toggle>();
    }

	void Update () {
        if (one == false)
        {
            if (mytoggle.isOn == true)
            {
                foreach (var toggle in toggles)
                {
                    toggle.isOn = false;
                }
                if(toggles.Contains(mytoggle))
                {
                    toggles.Remove(mytoggle);
                }
                one = true;
            }
        }

        if(mytoggle.isOn == false)
        {
            one = false;
        }
    }
}
