using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsanityTrigger : MonoBehaviour {

    public float insanityStep;
    public GameObject phone;

    void Start()
    {
        phone = GameObject.FindGameObjectWithTag("Phone");
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.transform.tag == "Player")
        {
            phone.transform.GetComponent<Phone>().insaneBoost += insanityStep;
        }
    }

    public void OnTriggerExit(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            phone.transform.GetComponent<Phone>().insaneBoost -= insanityStep;
        }
    }

}
