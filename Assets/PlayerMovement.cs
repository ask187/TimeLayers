using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the CharacterController component
    public CharacterController controller;

    // Movement and physics variables
    public float speed = 12f;
    //public float gravity = -9.81f;
    //public float jumpHeight = 3f;

    // Ground check variables
    //public Transform groundCheck;
    //public float groundDistance = 0f;
    //public LayerMask groundMask;

    // Velocity and grounding status
    //Vector3 velocity;
    //bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset the downward velocity if grounded
        // if (isGrounded && velocity.y < 0)
        // {
        //     velocity.y = -2f;
        // }

        // Get input for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player
        controller.Move(move * speed * Time.deltaTime);

        // Check for jump input and apply jump velocity if grounded
        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        // }

        // // Apply gravity to the velocity
        // velocity.y += gravity * Time.deltaTime;

        // // Move the player based on velocity
        // controller.Move(velocity * Time.deltaTime);
    }
}
