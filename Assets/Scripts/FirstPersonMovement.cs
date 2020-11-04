using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;

    float speed = 12;
    float gravity = -9.81f;
    float groundDistance = 0.4f;
    float jumpHeight = 3f;
    float x;
    float z;
    bool isOnGround;
    Vector3 move;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance);
        if (isOnGround && velocity.y < 0)
            velocity.y = -2f;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isOnGround)
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
