using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitFollowButton : MonoBehaviour {

    public GameObject unit;

    public void switchToUnit()
    {
      Camera.main.GetComponent<RtsCamera>().LookAt = unit.transform.position;
    }

    void Update()
    {
       GetComponentInChildren<Slider>().value = unit.GetComponent<Health>().health / 100;
    }
}
