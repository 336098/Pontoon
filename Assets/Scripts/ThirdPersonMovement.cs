using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //Public variables
    public CharacterController controller;
    public Transform cam;

    //Gravity variables
    Vector3 velocity;
    float gravity = -9.81f;

    //Jump variables
    float jumpHeight = 3;
    bool isOnGround;
    float groundDistance = 0.4f;
    Transform groundCheck;
    LayerMask groundMask;

    //Walk and rotation variables
    public float speed = 6f;
    float horizontal;
    float vertical;
    Vector3 direction;
    Vector3 moveDir;
    float targetAngle;
    float smoothTime = 0.1f;
    float angle;
    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gravity
        velocity.y += gravity * Time.deltaTime;

        //jump
        //isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //if (isOnGround && velocity.y < 0)
        //    velocity.y = -2f;

        //if (Input.GetButtonDown("Jump") && isOnGround)
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        controller.Move(velocity * Time.deltaTime);

        //walk
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
