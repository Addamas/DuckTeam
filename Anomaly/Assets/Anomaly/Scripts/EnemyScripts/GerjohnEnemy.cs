using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerjohnEnemy : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent agent;
    public Vector3[] waypoints;
    public int target;
    public GameObject player;
    public bool targetingPlayer;
    public bool seePlayer;
    public bool decaying;
    public int targetRange;
    public int chasingTime;
    public float chaseSpeed;
    public float chaseAcceleration;
    public AudioSource aClip;
    public bool freezePos;

    public GameObject bip;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetTarget();
        player = GameObject.FindGameObjectWithTag("Player");
        bip.transform.localEulerAngles = new Vector3(-90, 0, 100);
    }

    void Update()
    {
        if (!targetingPlayer && !freezePos)
        {
            float dist = Vector3.Distance(transform.position, waypoints[target]);
            if (transform.position.ToString() == waypoints[target].ToString() || dist < 0.5f)
            {
               // aClip.Play();
                if (target == waypoints.Length - 1)
                {
                    target = 0;
                    SetTarget();
                    return;
                }
                else
                {
                    target++;
                    SetTarget();
                }
            }
        }
        else
        {
            TargetPlayer();
        }
    }

    public void FoundPlayer()
    {
        print("DETECTED PLAYER");
        if (!targetingPlayer)
        {
            targetingPlayer = true;
            seePlayer = true;
        }
    }

    public void CantFindPlayer()
    {
        seePlayer = false;
        if (targetingPlayer)
        {
            if (!decaying)
            {
                print("Started decaying");
                decaying = true;
                StartCoroutine(Decay());
            }
        }
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(chasingTime);
        if (!seePlayer)
        {
            StopChasing();
        }
        decaying = false;
    }

    public void StopChasing()
    {
        print("Stoped Chasing player");
        targetingPlayer = false;
        SetTarget();
        agent.speed = 3.5f;
        agent.acceleration = 8;
    }

    public void TargetPlayer()
    {
        if (!freezePos)
        {
            agent.speed = chaseSpeed;
            agent.acceleration = chaseAcceleration;
            agent.SetDestination(player.transform.position);
            if (!seePlayer)
            {
                StartCoroutine(Decay());
            }
        }
    }

    public void SetTarget()
    {
        agent.SetDestination(waypoints[target]);
    }
    public void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            StopChasing();
            print("Player Died!");
        }
    }
}