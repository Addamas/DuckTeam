using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float mouseSensitivity = 1f;
    private Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public CharacterController controller;
    private Vector3 ColLoc = new Vector3(0, 0, 0);
    private float height;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        height = controller.height;
    }

    void Update ()
    {
        Movement();
        Crouching();
    }

    public void Movement()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        transform.Rotate(0, Input.GetAxis("Mouse X")*mouseSensitivity, 0);
    }

    public void Crouching()
    {
        if (Input.GetButton("Crouch"))
        {
            GetComponent<CharacterController>().height = height / 2;
            ColLoc.y = -(height / 4);
            GetComponent<CharacterController>().center = ColLoc;
        }
        else if(!Input.GetButton("Crouch") && ColLoc.y < 0)
        {
            if (!Physics.Raycast(transform.position, transform.up, height) && !Input.GetButton("Crouch"))
            {
                GetComponent<CharacterController>().height = height;
                ColLoc.y = 0;
                GetComponent<CharacterController>().center = ColLoc;
            }
        }
    }


}
