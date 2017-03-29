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
    public List<AudioManger> sound = new List<AudioManger>();
    [HideInInspector]
    public enum EnemyState {Idle, Chasing, Walking, Inspecting}
    [HideInInspector]
    public EnemyState enemyState = EnemyState.Idle;
    [HideInInspector]
    public Vector3 randomPos;
    private NavMeshHit navHit;

    public void Movement ()
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
                NavMesh.SamplePosition(transform.position + randomPos, out navHit, ((GetComponent<FieldOfView>().viewRadius) *2), NavMesh.AllAreas);
                agent.SetDestination(navHit.position);
                enemyState = EnemyState.Walking;
                break;

            // Chasing //
            case EnemyState.Chasing:
                if(agent.speed != chaseSpeed)
                    agent.speed = chaseSpeed;
                float dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist > GetComponent<FieldOfView>().viewRadius)
                {
                    enemyState = EnemyState.Idle;
                    break;
                }
                else agent.SetDestination(player.transform.position);
                break;

            // Walking //
            case EnemyState.Walking:
                if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                    if(inspectingArea)
                        enemyState = EnemyState.Inspecting;
                    else
                        enemyState = EnemyState.Idle;
                break;
        }
    }

    public void MakeSound (int num)
    {
        // sound[num].soundClip; //
    }

}
