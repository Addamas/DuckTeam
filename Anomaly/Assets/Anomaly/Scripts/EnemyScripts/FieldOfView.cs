using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public float viewRadius = 11f;
    public float noticeRadius = 3f;
    [Range(0,360)]
    public float viewAngle = 140f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public GameObject enemy;
    public bool isSeeingPlayer;
    
    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    } 

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTargets();

        }
    }

    void FindVisableTargets ()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);

        if(targetsInViewRadius.Length < 1)
        {
            if (isSeeingPlayer)
            {
                isSeeingPlayer = false;
                enemy.GetComponent<GerjohnEnemy>().seePlayer = false;
            }
            return;
        }

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            //print(viewAngle / 2);
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle/*/ viewAngle / 2 /*/)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    enemy.GetComponent<GerjohnEnemy>().FoundPlayer();
                    enemy.GetComponent<GerjohnEnemy>().seePlayer = true;
                    isSeeingPlayer = true;
                }   
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
