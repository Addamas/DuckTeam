using UnityEngine;
using System.Collections;

public class PlayerControllr : MonoBehaviour {

    public float speed;
    public float xSpeed;
    public bool mayLook;

    // Use this for initialization
    void Start () {
        mayLook = true;
	}
	
	// Update is called once per frame
	void Update () {
        float forwd = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
        float sidewd = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
        float mouseXTranslate = Input.GetAxis ("Mouse X") * xSpeed * Time.deltaTime;

        transform.Translate (0, 0, forwd);
        transform.Translate (sidewd, 0 ,0);
        if (mayLook != false)
        {
            transform.Rotate(0, mouseXTranslate, 0);
        }
    }
}
