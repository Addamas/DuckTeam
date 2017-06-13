using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public GameObject ObjectiveManager;
    bool delay;

	public void OnTriggerStay(Collider c)
    {
        if (Input.GetButtonDown("Use") && !delay)
        {
            switch (c.transform.tag)
            {
                case "Door":
                    c.GetComponent<DoorScript>().Use();
                    StartCoroutine(DoDelay());
                    break;
                case "Note":
                    ObjectiveManager.GetComponent<Objectives>().FoundNote();
                    Destroy(c.gameObject);
                    break;
                case "SuitCase":
                    ObjectiveManager.GetComponent<Objectives>().CompletedObjective(1);
                    break;
                case "Generator":
                    if(ObjectiveManager.GetComponent<Objectives>().objectiveID == 2)
                    {
                        ObjectiveManager.GetComponent<Objectives>().CompletedObjective(2);
                    }
                    break;
            }
        }
    }

    IEnumerator DoDelay()
    {
        delay = true;
        yield return new WaitForSeconds(0.5f);
        delay = false;
    }
}
