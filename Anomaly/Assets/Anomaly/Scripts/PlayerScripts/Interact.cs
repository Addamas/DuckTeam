using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public GameObject ObjectiveManager;

	public void OnTriggerStay(Collider c)
    {
        if (Input.GetButtonDown("Use"))
        {
            if (c.transform.tag == "SuitCase")
            {
                ObjectiveManager.GetComponent<Objectives>().Suitcase();
            }
            else if (c.transform.tag == "Note")
            {
                ObjectiveManager.GetComponent<Objectives>().FoundNote();
                Destroy(c.gameObject);
            }
        }
    }
}
