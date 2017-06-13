using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    bool isOpen;
    public Animator anim;

    public void Use()
    {
        if (isOpen)
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
