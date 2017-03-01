using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseFollower : MonoBehaviour {

    public Transform mouseImage;
    public Vector3 imageOffset;

	// Use this for initialization
	void Start () {
        GetComponent<Image>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Input.mousePosition + imageOffset;
	}
}
