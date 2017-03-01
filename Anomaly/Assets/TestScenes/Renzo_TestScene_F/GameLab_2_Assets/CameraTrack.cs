using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {

    public List<Transform> anchors = new List<Transform>();
    public int count = 1;

    public float speed = 0.1F;

    void Start () {
        transform.LookAt(anchors[0].transform);
    }

    void Update () {
        //transform.rotation = Quaternion.Lerp(this.transform.rotation, anchors[0].rotation, Time.time * speed);

        transform.rotation = Quaternion.Lerp(anchors[count-1].rotation, anchors[count].rotation, Time.time * speed);

        if (Input.GetButtonUp("Fire1"))
        {
            count++;
        }
	}
}

