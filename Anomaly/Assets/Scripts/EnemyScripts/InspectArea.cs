using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectArea : MonoBehaviour {

    [HideInInspector]
    public Vector3 inspectPos;
    public float inspectRadius = 1f;

    public void RandomPosition ()
    {
        inspectPos = Random.insideUnitSphere * inspectRadius;
    }
}
