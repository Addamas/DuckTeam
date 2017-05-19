using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FieldOfView))]
public class EnemyBase : MonoBehaviour
{
    public float normalSpeed;
    public float chaseSpeed;
    public bool inspectingArea;
    public Animator anim;
    [HideInInspector]
    public GameObject player;
    public NavMeshAgent agent;
    [HideInInspector]
    public enum EnemyState { Idle, Chasing, Walking, Inspecting, ScriptedEvent }
    [HideInInspector]
    public EnemyState enemyState = EnemyState.Idle;
    [HideInInspector]
    public Vector3 randomPos;
    private NavMeshHit navHit;

    private bool doingScriptedEvent;
    private bool warpBackAfter;
    private Vector3 loc;

    public void Movement()
    {
        switch (enemyState)
        {
            // Inspecting //
            case EnemyState.Inspecting:
                GameObject area = GameObject.FindWithTag("InspectArea");
                area.GetComponent<InspectArea>().RandomPosition();
                NavMesh.SamplePosition(area.transform.position + area.GetComponent<InspectArea>().inspectPos, out navHit, area.GetComponent<InspectArea>().inspectRadius, NavMesh.AllAreas);
                agent.SetDestination(navHit.position);
                enemyState = EnemyState.Walking;
                break;

            // Idle //
            case EnemyState.Idle:
                randomPos = Random.insideUnitSphere * GetComponent<FieldOfView>().viewRadius;
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, ((GetComponent<FieldOfView>().viewRadius) * 2), NavMesh.AllAreas);
                agent.SetDestination(navHit.position);
                enemyState = EnemyState.Walking;
                break;

            // Chasing //
            case EnemyState.Chasing:
                float dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist > GetComponent<FieldOfView>().viewRadius)
                {
                    enemyState = EnemyState.Idle;
                    break;
                }
                else
                {
                    agent.SetDestination(player.transform.position);
                }
                break;

            // Walking //
            case EnemyState.Walking:
                if (inspectingArea)
                {
                    enemyState = EnemyState.Inspecting;
                }
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    enemyState = EnemyState.Idle;
                }
                break;

            // Scripted Events //
            case EnemyState.ScriptedEvent:
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    if (warpBackAfter)
                    {
                        agent.Warp(loc);
                    }
                    gameObject.GetComponent<FieldOfView>().targetMask = 9;
                    enemyState = EnemyState.Idle;
                }
                break;
        }
    }

    public void ScriptedEvent(Vector3 start, Vector3 end, bool seePlayer, bool warpBack)
    {
        warpBackAfter = warpBack;
        if (!seePlayer)
        {
            gameObject.GetComponent<FieldOfView>().targetMask = 0;
        }
        if (!doingScriptedEvent)
        {
            loc = transform.position;
            agent.Warp(start);
            doingScriptedEvent = true;
        }
        agent.SetDestination(end);
        enemyState = EnemyState.ScriptedEvent;
    }
}
