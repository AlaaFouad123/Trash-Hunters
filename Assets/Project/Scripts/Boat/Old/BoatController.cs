using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 5f; // Speed of the boat movement
    public float turnSpeed = 200f; // Speed of the boat rotation

    private void Update()
    {
        // Get keyboard _input for moving the boat
        float moveInput = Input.GetAxis("Vertical"); // W/S or Up/Down
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Move the boat forward/backward
        transform.Translate(Vector3.forward * moveInput * speed * Time.deltaTime);

        // Rotate the boat left/right
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    }
}