using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseFollower : MonoBehaviour {

    public Transform mouseImage;
    public Vector3 imageOffset;
    
	void Start () {
        GetComponent<Image>().enabled = false;
    }
	
	void Update () {
        transform.position = Input.mousePosition + imageOffset;
	}
}
