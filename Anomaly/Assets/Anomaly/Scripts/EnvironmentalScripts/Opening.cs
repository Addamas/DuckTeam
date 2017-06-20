using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour {

    Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
    }

    public void StartFade(int i)
    {
        switch (i)
        {
            case 0:
                anim.SetTrigger("Off");
                break;
            case 1:
                anim.SetTrigger("On");
                StartCoroutine(WaitForNext(1));
                break;
        }
    }
    IEnumerator WaitForNext(int i)
    {
        yield return new WaitForSeconds(1.3f);
        switch (i)
        {
            case 1:
                StartFade(0);
                break;
        }
    }
}