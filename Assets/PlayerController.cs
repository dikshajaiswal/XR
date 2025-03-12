//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public float speed = 5.0f; // Movement speed
//    public float mouseSensitivity = 2.0f; // Mouse look sensitivity

//    private float verticalRotation = 0.0f; // Vertical rotation angle

//    void Update()
//    {
//        // Move the player using WASD keys
//        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

//        Vector3 move = transform.right * moveX + transform.forward * moveZ;
//        transform.Translate(move, Space.World);

//        // Rotate the player using the mouse
//        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

//        verticalRotation -= mouseY;
//        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Clamp vertical look angle

//        transform.Rotate(Vector3.up * mouseX); // Horizontal rotation
//        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // Vertical rotation
//    }
//}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float gravity = 0f; // Simulated gravity
    public float jumpSpeed = 8.0f; // Jump height

    private float verticalRotation = 0.0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        // Apply movement
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply gravity
        if (characterController.isGrounded)
        {
            move.y = 0f; // Reset vertical velocity if grounded
            if (Input.GetButton("Jump"))
            {
                move.y = jumpSpeed;  // Apply jump force
            }
        }
        else
        {
            move.y -= gravity * Time.deltaTime;  // Apply gravity
        }

        // Apply movement to the character controller
        characterController.Move(move * Time.deltaTime);

        // Rotate the player using the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);  // Vertical camera rotation limit

        transform.Rotate(Vector3.up * mouseX);  // Horizontal rotation
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);  // Vertical rotation
    }
}
