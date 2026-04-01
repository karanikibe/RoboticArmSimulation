using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Speed of rotation
    public float minRotation = -45f;   // Minimum rotation angle
    public float maxRotation = 45f;    // Maximum rotation angle
    private bool canRotate = true;     // Flag to allow/disallow rotation

    void Update()
    {
        if (!canRotate) return; // Stop rotating if collided with ground

        // Get user input (Up/Down arrows)
        float input = Input.GetAxis("Vertical"); // Up = 1, Down = -1

        if (input != 0)
        {
            // Get current local rotation in degrees
            float currentAngle = transform.localEulerAngles.x;

            // Convert Unity's 0-360 range to -180 to 180 range for proper clamping
            if (currentAngle > 180) currentAngle -= 360;

            // Calculate new rotation angle
            float newRotation = currentAngle + (input * rotationSpeed * Time.deltaTime);

            // Clamp within limits
            newRotation = Mathf.Clamp(newRotation, minRotation, maxRotation);

            // Apply rotation
            transform.localRotation = Quaternion.Euler(newRotation, 0, 0);
        }
    }

    // Detect collision with the ground and stop movement
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canRotate = false; // Stop rotation
        }
    }

    // Allow movement again when not touching the ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canRotate = true; // Resume rotation
        }
    }
}
