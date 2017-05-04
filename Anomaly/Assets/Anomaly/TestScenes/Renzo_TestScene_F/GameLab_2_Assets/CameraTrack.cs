using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {

    public Transform[] anchors;
    public Transform[] waypoints;
    private Transform startMarker, endMarker;
    private int currentStartPoint;

    private float travelLength;
    private float startTime;
    public float speed = 1f;

    public bool switchz;

    public int count;
    public float turnSpeed = 0.1F;

    void Start () {
        currentStartPoint = 0;
        setWaypoints();
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / travelLength;

        //if (switchz == false)
        //{
            transform.rotation = Quaternion.Lerp(anchors[count - 1].rotation, anchors[count].rotation, fracJourney);
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
        //}
        //else
        //{
        //    //transform.rotation = Quaternion.Lerp(anchors[count].rotation, anchors[count - 1].rotation, fracJourney);
        //    transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fracJourney);
        //}

        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            if (fracJourney >= 1f && currentStartPoint + 1 < waypoints.Length)
            {
                //if (switchz == true)
                //    switchz = false;
                currentStartPoint++;
                setWaypoints();
                //count++;
            }
        }
        #region reverse
        //if (Input.GetButtonUp("Fire2") || Input.GetButtonUp("Use"))
        //{
        //    if (fracJourney >= 1f && currentStartPoint + 1 < waypoints.Length)
        //    {
        //        if (switchz == false)
        //            switchz = true;
        //        //currentStartPoint--;
        //        setWaypoints();
        //        //count++;
        //    }
        //}
        #endregion
    }

    void setWaypoints()
    {
        if (currentStartPoint == waypoints.Length -1)
        {
            currentStartPoint = 0;
            count = 0;
        }
        startMarker = waypoints[currentStartPoint];
        endMarker = waypoints[currentStartPoint + 1];
        startTime = Time.time;
        travelLength = Vector3.Distance(startMarker.position, endMarker.position);

        count++;
    }
}

