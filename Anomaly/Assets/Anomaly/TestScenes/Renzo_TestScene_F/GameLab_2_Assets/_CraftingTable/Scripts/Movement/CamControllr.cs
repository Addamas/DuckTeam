using UnityEngine;
using System.Collections;

public class CamControllr : MonoBehaviour {

    public float ySpeed;
    public bool mayLook;

    // Use this for initialization
    void Start () {
        mayLook = true;
    }

    // Update is called once per frame
    void Update () {

        float mouseYTranslate = -Input.GetAxis ("Mouse Y") * ySpeed * Time.deltaTime;

        if (mayLook != false)
        {
            transform.Rotate(mouseYTranslate, 0, 0);
        }
    }
}
