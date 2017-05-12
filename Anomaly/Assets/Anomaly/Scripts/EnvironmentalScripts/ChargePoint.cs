using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePoint : MonoBehaviour
{
    public GameObject phone;
    public bool active;

    void Start()
    {
        phone = GameObject.FindGameObjectWithTag("Phone");
    }
    void Update()
    {
        if (active)
        {
            if (Input.GetButton("Use"))
            {
                Charge();
            }
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            active = true;
        }
    }
    public void OnTriggerExit(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            active = false;
        }
    }
    public void Charge()
    {
        phone.transform.GetComponent<Phone>().Charge();
    }
}