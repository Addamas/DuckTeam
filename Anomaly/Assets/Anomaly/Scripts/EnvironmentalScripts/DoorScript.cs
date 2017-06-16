using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    bool isOpen;
    public Animator anim;
    public bool oneWay;
    public bool isInsane;
    public GameObject phone;
    public int insanityRaise;

    public void Use()
    {
        if (isInsane)
        {
            phone.GetComponent<Phone>().InsanityBoost(insanityRaise);
        }
        if (isOpen && !oneWay)
        {
            isOpen = false;
            anim.SetTrigger("Close");
            return;
        }
        else
        {
            isOpen = true;
            anim.SetTrigger("Open");
        }
    }
}
