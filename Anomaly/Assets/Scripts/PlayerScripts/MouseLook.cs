using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    float rotationY;
    public float mouseSensitivity;
    public float minimumY = -90;
    public float maximumY = 90;
    void Start () {
	
	}
	
	void Update () {

        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
