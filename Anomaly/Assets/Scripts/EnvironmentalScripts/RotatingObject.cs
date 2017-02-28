using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {

    [Header("Up(1,0,0), Down(-1,0,0), Right(0,-1,0), left(0,1,0) ")]
    public Vector3 rotDir = new Vector3(0,-1,0);
    [Header("Speed it uses to rotate, standard is 40")]
    public float rotSpeed = 40;

    private void Update()
    {
        transform.Rotate(rotDir, rotSpeed * Time.deltaTime);
    }
}
