using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthographic : MonoBehaviour
{
  void Update()
    {
        gameObject.GetComponent<Camera>().orthographicSize = transform.parent.GetComponent<Camera>().orthographicSize;
    }
}
