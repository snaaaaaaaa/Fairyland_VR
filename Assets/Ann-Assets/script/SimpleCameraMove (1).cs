using UnityEngine;

public class SimpleCameraMove : MonoBehaviour
{
    // How fast the camera moves with WASD
    public float moveSpeed = 5f;

    // How sensitive the mouse movement is
    public float mouseSensitivity = 200f;

    // This stores the up/down rotation so we can limit it
    float xRotation = 0f;

    void Start()
    {
        // Lock the mouse cursor to the centre of the screen
        // This makes it feel like an FPS camera
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ---------------------------------
        // 1. READ MOUSE MOVEMENT
        // ---------------------------------

        // Mouse X = left and right movement of mouse
        float mouseX = Input.GetAxis("Mouse X");

        // Mouse Y = up and down movement of mouse
        float mouseY = Input.GetAxis("Mouse Y");

        // Multiply by sensitivity so we can control speed
        mouseX *= mouseSensitivity * Time.deltaTime;
        mouseY *= mouseSensitivity * Time.deltaTime;


        // ---------------------------------
        // 2. ROTATE CAMERA UP AND DOWN
        // ---------------------------------

        // Moving mouse up should look up
        // So we subtract mouseY
        xRotation -= mouseY;

        // Prevent camera from flipping upside down
        // This limits vertical look to 90 degrees up and down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        // ---------------------------------
        // 3. ROTATE CAMERA LEFT AND RIGHT
        // ---------------------------------

        // We keep track of current Y rotation (left/right)
        float yRotation = transform.eulerAngles.y;

        // Add mouseX to turn left/right
        yRotation += mouseX;


        // ---------------------------------
        // 4. APPLY BOTH ROTATIONS
        // ---------------------------------

        // Combine vertical and horizontal rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);


        // ---------------------------------
        // 5. READ KEYBOARD INPUT (WASD)
        // ---------------------------------

        // A/D controls left and right movement
        float x = Input.GetAxis("Horizontal");

        // W/S controls forward and backward movement
        float z = Input.GetAxis("Vertical");


        // ---------------------------------
        // 6. CALCULATE MOVEMENT DIRECTION
        // ---------------------------------

        // transform.forward = direction camera is facing
        // transform.right = direction to the camera's right
        Vector3 moveDirection = transform.right * x + transform.forward * z;


        // ---------------------------------
        // 7. MOVE THE CAMERA
        // ---------------------------------

        // Move camera in that direction
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

    }
}
