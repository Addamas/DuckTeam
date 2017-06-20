using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public Vector3 targetPos;

    public GameObject opening;
    void OnTriggerEnter(Collider c)
    {
        if(c.transform.tag == "Player")
        {
            opening.GetComponent<Opening>().StartFade(1);
            StartCoroutine(WaitForTp(c.gameObject));
        }
    }

    public void Teleport(GameObject p)
    {
        p.transform.position = targetPos;
    }

    public IEnumerator WaitForTp(GameObject p)
    {
        yield return new WaitForSeconds(1.3f);
        Teleport(p);
    }

    public void PrepareTp(GameObject p)
    {
        StartCoroutine(WaitForTp(p));
    }
}
