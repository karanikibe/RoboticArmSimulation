using UnityEngine;

public class GripperControl : MonoBehaviour
{
    public Transform leftPivot;
    public Transform rightPivot;
    public float rotationSpeed = 100f;
    public float maxOpenAngle = 20f; // Maximum opening angle

    private Quaternion leftOpenRotation;
    private Quaternion leftCloseRotation;
    private Quaternion rightOpenRotation;
    private Quaternion rightCloseRotation;

    void Start()
    {
        // Store rotations relative to the gripper base
        leftOpenRotation = Quaternion.Euler(0, 0, maxOpenAngle);  // Opens outward
        leftCloseRotation = Quaternion.Euler(0, 0, 0);

        rightOpenRotation = Quaternion.Euler(0, 0, -maxOpenAngle); // Opens outward
        rightCloseRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.O)) // Open gripper
        {
            leftPivot.localRotation = Quaternion.RotateTowards(leftPivot.localRotation, leftOpenRotation, rotationSpeed * Time.deltaTime);
            rightPivot.localRotation = Quaternion.RotateTowards(rightPivot.localRotation, rightOpenRotation, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.C)) // Close gripper
        {
            leftPivot.localRotation = Quaternion.RotateTowards(leftPivot.localRotation, leftCloseRotation, rotationSpeed * Time.deltaTime);
            rightPivot.localRotation = Quaternion.RotateTowards(rightPivot.localRotation, rightCloseRotation, rotationSpeed * Time.deltaTime);
        }
    }
}