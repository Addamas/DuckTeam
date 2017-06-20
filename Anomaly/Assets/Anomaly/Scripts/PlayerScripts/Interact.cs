using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public GameObject ObjectiveManager;
    GameObject player;
    bool delay;
    AudioSource allstar;
    public GameObject opening;

    void Start()
    {
        allstar = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

	public void OnTriggerStay(Collider c)
    {
        if (Input.GetButtonDown("Use") && !delay)
        {
            switch (c.transform.tag)
            {
                case "Door":
                    if(c.GetComponent<DoorScript>() != null)
                    {
                        c.GetComponent<DoorScript>().Use();
                    }
                    else
                    {
                        opening.GetComponent<Opening>().StartFade(1);
                        c.GetComponent<Teleporter>().PrepareTp(player);
                    }
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
                    else
                    {
                        if (ObjectiveManager.GetComponent<Objectives>().hasFuel)
                        {
                            ObjectiveManager.GetComponent<Objectives>().CompletedObjective(4);
                        }
                    }
                    break;
                case "Jerrycan":
                    if (ObjectiveManager.GetComponent<Objectives>().objectiveID == 3)
                    {
                        ObjectiveManager.GetComponent<Objectives>().hasJerrycan = true;
                        Destroy(c.gameObject);
                    }
                    break;
                case "Tractor":
                    if (ObjectiveManager.GetComponent<Objectives>().hasJerrycan && ObjectiveManager.GetComponent<Objectives>().hasFuel != true)
                    {
                        ObjectiveManager.GetComponent<Objectives>().CompletedObjective(3);
                    }
                    break;
                case "Shrek":
                    if (!allstar.isPlaying)
                    {
                        allstar.Play();
                    }
                    else
                    {
                        allstar.Stop();
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
