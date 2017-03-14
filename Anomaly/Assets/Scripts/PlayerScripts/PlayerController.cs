using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed = 8.0F;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float gravity = 20.0F;
    public float mouseSensitivity = 1f;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 cameraPosition;
    public Vector3 crouchCameraPosition;
    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public GameObject playerCamera;
    private Vector3 ColLoc = new Vector3(0, 0, 0);
    [HideInInspector]
    public Animator cameraAnimator;
    private float height;

    void Start()
    {
        cameraAnimator = GameObject.Find("CameraHolder").GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.gameObject;
        height = controller.height;
    }

    void Update ()
    {
        Movement();
        Crouching();
        Bobbing();
    }

    public void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButton("Crouch"))
            {
                speed = crouchSpeed;
            }
            else if (Input.GetButton("Sprint"))
                speed = sprintSpeed;
            else speed = walkSpeed;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                cameraAnimator.SetTrigger("Jump");
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        transform.Rotate(0, Input.GetAxis("Mouse X")*mouseSensitivity, 0);
    }

    void Bobbing()
    {
        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            cameraAnimator.SetBool("Bobbing", true);
        }
        else
        {
            cameraAnimator.SetBool("Bobbing", false);
        }
    }

    public void Crouching()
    {
        if (Input.GetButton("Crouch"))
        {
            GetComponent<CharacterController>().height = height / 2;
            ColLoc.y = -(height / 4);
            GetComponent<CharacterController>().center = ColLoc;
            StartCoroutine("Crouch");
        }
        else if(!Input.GetButton("Crouch") && playerCamera.transform.localPosition != cameraPosition)
        {
            if (!Physics.Raycast(transform.position, transform.up, height))
            {
                GetComponent<CharacterController>().height = height;
                ColLoc.y = 0;
                GetComponent<CharacterController>().center = ColLoc;
                StartCoroutine("GetUp");
            }
        }
    }

    IEnumerator GetUp()
    {
        if (playerCamera.transform.localPosition == cameraPosition)
        {
            StopAllCoroutines();
        }
        playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, cameraPosition, 3f * Time.deltaTime);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator Crouch()
    {
        if (playerCamera.transform.localPosition == crouchCameraPosition)
        {
            StopAllCoroutines();
        }
        playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, crouchCameraPosition, 3f * Time.deltaTime);
        yield return new WaitForSeconds(0.2f);
    }
}
