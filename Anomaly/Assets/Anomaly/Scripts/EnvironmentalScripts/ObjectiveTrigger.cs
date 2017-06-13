using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour {

    public GameObject objectiveManager;
    public int objectiveID;

    void OnTriggerEnter(Collider c)
    {
        if(c.transform.tag == "Player")
        {
            objectiveManager.GetComponent<Objectives>().CompletedObjective(objectiveID);
        }
    }
}
