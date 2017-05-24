using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    private bool triggerd;
    private float originalIntensity;
    private float intensity;

    private void Start()
    {
        originalIntensity = GetComponent<Light>().intensity;
        StartCoroutine(Flickering());
    }

    private void Update()
    {
        if (triggerd)
        {
            GetComponent<Light>().intensity = intensity;
        }
        else
        {
            GetComponent<Light>().intensity = originalIntensity;
        }
    }

    IEnumerator Flickering ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            triggerd = !triggerd;
        }
    }
}
