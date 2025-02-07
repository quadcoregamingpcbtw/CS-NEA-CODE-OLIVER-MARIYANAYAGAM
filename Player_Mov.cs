using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mov : MonoBehaviour
{
    private CharacterController controller;

    Vector3 velocity; // Velocity vector for movement
    bool isOnGround; // Check if player is grounded
    bool isMoving; // Check if player is moving
    public float speed = 12f; // Movement speed
    public float gravity = -9.81f * 2; // Gravity effect
    public float jumpHeight = 3f; // Jump height
    public Transform groundCheck; // Reference for ground check
    public float groundDist = 0.4f; // Distance for ground check
    public LayerMask groundMask; // Layer mask for ground detection
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f); // Last position for movement detection

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component
    }

    void Update()
    {
        // Check if player is on the ground
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        // Reset vertical velocity if grounded
        if (isOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z; // Movement direction

        controller.Move(move * speed * Time.deltaTime); // Move the player

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity
        controller.Move(velocity * Time.deltaTime); // Move the player with gravity

        // Check if the player is moving
        isMoving = lastPosition != gameObject.transform.position && isOnGround;

        lastPosition = gameObject.transform.position; // Update last position
    }
}
