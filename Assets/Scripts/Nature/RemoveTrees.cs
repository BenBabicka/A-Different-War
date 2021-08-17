using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveTrees : MonoBehaviour {

    public LayerMask TreeLayer;

    public List<GameObject> allTR;
    private float distance = 5f;

    void Update()
    {

        foreach (GameObject obj in allTR)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < distance)
                obj.SetActive(false);
            StartCoroutine(remove());
        }

        

    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(8);
        for (var i = 0; i < allTR.Count; i++)
        {
            Destroy(allTR[i].GetComponent<AddToList>());
            allTR.RemoveAt(i);
        }
    }

}
