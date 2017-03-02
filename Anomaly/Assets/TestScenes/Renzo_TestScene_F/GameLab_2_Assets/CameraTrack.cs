using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {

    public Transform[] anchors;
    public Transform[] waypoints;
    public int count = 1;

    public float turnSpeed = 0.1F;
    public float moveSpeed = 1f;
    public float travelTime;
    //private float travelLength;

    void Start () {
        //journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        //travelTime = Time.time;
        transform.LookAt(anchors[0].transform);
    }

    void Update () {
        float travelLength = Vector3.Distance(waypoints[count - 1].position, waypoints[count].position);
        float distCovered = (Time.time - travelTime) * moveSpeed;
        float travelDuration = distCovered / travelLength;

        transform.rotation = Quaternion.Lerp(anchors[count-1].rotation, anchors[count].rotation, Time.time * turnSpeed);
        transform.position = Vector3.Lerp(waypoints[0].position, waypoints[1].position, travelDuration);

        //= Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        if (Input.GetButtonUp("Fire1"))
        {
            
            count++;
        }
	}
}

