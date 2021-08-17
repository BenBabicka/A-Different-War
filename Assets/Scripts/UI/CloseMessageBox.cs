using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMessageBox : MonoBehaviour {

	public void close()
    {
        Destroy(transform.parent.gameObject);
    }
}
