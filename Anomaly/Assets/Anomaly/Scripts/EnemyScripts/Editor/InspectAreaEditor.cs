using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InspectArea))]
public class InspectAreaEditor : Editor {

    private void OnSceneGUI()
    {
        InspectArea iA = (InspectArea)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(iA.transform.position, Vector3.up, Vector3.forward, 360, iA.inspectRadius);
    }
}
