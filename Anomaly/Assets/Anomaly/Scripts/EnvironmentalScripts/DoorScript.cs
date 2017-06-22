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
            StopCoroutine(AutoCloser());
            return;
        }
        else
        {
            isOpen = true;
            anim.SetTrigger("Open");
            StartCoroutine(AutoCloser());
        }
    }

    IEnumerator AutoCloser()
    {
        yield return new WaitForSeconds(5);
        if (isOpen)
        {
            anim.SetTrigger("Close");
            isOpen = false;
            float dist = Vector3.Distance(transform.position, phone.transform.position);
            if(dist < 10)
            {
                //Play door close sound
                phone.GetComponent<Phone>().InsanityBoost(insanityRaise);
            }
        }
    }
}
