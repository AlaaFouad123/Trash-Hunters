using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    public Transform fishingRod; // Reference to the fishing rod transform
    public float moveSpeed = 0.1f; // Speed of the rod movement

    void Update()
    {
        // Get mouse _input for moving the fishing rod
        float mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse movement

        // Change the position of the fishing rod along the z-axis
        fishingRod.position += new Vector3( mouseY * moveSpeed, 0,0);
    }
}
