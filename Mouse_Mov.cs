using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Mov : MonoBehaviour
{
    // Rotation variables for x and y axes
    float xRotation = 0f;
    float yRotation = 0f;

    // Mouse sensitivity multiplier
    public float mouseSensitivity = 550f;

    // Clamping values for vertical rotation to prevent flipping
    public float clampTopView = -90f; // Maximum upward rotation
    public float clampBottomView = 90f; // Maximum downward rotation

    void Start()
    {
        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical movement

        // Adjust the x rotation based on mouse Y movement
        xRotation -= mouseY;

        // Clamp the x rotation to prevent flipping
        xRotation = Mathf.Clamp(xRotation, clampTopView, clampBottomView);

        // Adjust the y rotation based on mouse X movement
        yRotation += mouseX;

        // Apply the rotation to the transform of the object
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}

