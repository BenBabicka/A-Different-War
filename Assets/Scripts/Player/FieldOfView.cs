using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public GameObject targetgameobject;

    public bool hunting;

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
               
            }
           
        }

        if(gameObject.GetComponent<JobManager>())
        {
            hunting = gameObject.GetComponent<JobManager>().hunter;                       
        }

        //Targeting Humans
        if (!hunting)
        {
            Transform closestTarget = null;
            Vector3 position = transform.position;
            foreach (var target in visibleTargets)
            {
                Vector3 diff = target.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                float distance = gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponData>().range * 10;

                if (curDistance < distance  )
                {
                    closestTarget = target;
                    targetgameobject = closestTarget.transform.gameObject;
                    Debug.Log(closestTarget.name);
                }
                else
                {
                    closestTarget = null;
                    targetgameobject = null;
                }
            }

        }

        //Targeting Animals
        if (hunting)
        {
                Transform closestTarget = null;
                Vector3 position = transform.position;
                foreach (var target in visibleTargets)
                {
                    Vector3 diff = target.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                float distance = gameObject.GetComponent<ItemPickUp>().weapon.GetComponent<WeaponData>().range * 10;
                if (curDistance < distance)
                {
                    
                    closestTarget = target;
                        targetgameobject = closestTarget.transform.gameObject;
                        Debug.Log(closestTarget.name);
                   }
                else
                {
                    closestTarget = null;
                    targetgameobject = null;
                }
            }
          }
     }     
    


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawSphere(transform.position, viewRadius);
    }

}