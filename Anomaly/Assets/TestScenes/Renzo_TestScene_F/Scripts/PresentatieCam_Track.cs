using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PresentatieCam_Track : MonoBehaviour {
    
    public Transform[] waypoints;
    public int currentDest;
    public Transform nextDest;
    public NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	void Update () {
        NextDestination();
	}

    void NextDestination()
    {
        if (Input.GetButtonUp("Fire1")){
            nextDest = waypoints[currentDest + 1];
            agent.SetDestination(nextDest.position);
            currentDest++;
        }
    }
}
