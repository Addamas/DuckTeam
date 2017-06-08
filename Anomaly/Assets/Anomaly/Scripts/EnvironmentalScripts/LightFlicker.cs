using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public bool lightWillFlicker;
    public float waitForSeconds = 0.09f;
    private bool lightOff;
    private float originalIntensity;
    [Header("chance will be 1 in x amount to have a chance of flickering")]
    [Range(1,100)]
    public int chanceOfFlicker = 3;

    private void Start()
    {
        originalIntensity = GetComponent<Light>().intensity;
        StartCoroutine(Flickering());
    }

    private void Update()
    {

    }

    IEnumerator Flickering ()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitForSeconds);

            if (lightWillFlicker)
            {
                int i = Random.Range(1, chanceOfFlicker);
                float f = Random.Range(0,(originalIntensity/3));
                if (i == 1)
                {
                    lightOff = !lightOff;
                    if (lightOff)
                    {
                        GetComponent<Light>().intensity = f;
                    }
                    else
                    {
                        GetComponent<Light>().intensity = originalIntensity;
                    }
                }
            }
            else
            {
                lightOff = false;
                GetComponent<Light>().intensity = originalIntensity;
            }
        }
    }
}
