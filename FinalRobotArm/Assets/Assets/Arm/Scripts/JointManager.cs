using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointManager : MonoBehaviour
{
    public JointManager m_child;

    public float AngleMin = -130f, AngleMax = 130f;

    public enum RotAxis
    {
        X_Axis, Y_Axis, Z_Axis
    }

    public RotAxis axis = RotAxis.X_Axis;

    private Vector3 rotVector;

    private void Start()
    {
        // Initialize the local rotation axis vector
        switch (axis)
        {
            case RotAxis.X_Axis:
                rotVector = Vector3.right;
                break;
            case RotAxis.Y_Axis:
                rotVector = Vector3.up;
                break;
            case RotAxis.Z_Axis:
                rotVector = Vector3.forward;
                break;
        }
    }

    public JointManager GetJointChild()
    {
        return m_child;
    }

    public void RotateJoint(float _angle)
    {
        try
        {
            // Rotate in local space using Space.Self to ensure child joints follow
            transform.Rotate(rotVector, _angle, Space.Self);
        }
        catch (System.Exception)
        {
            Debug.LogError($"Invalid rotation: {transform.localRotation} : {_angle}");
            throw;
        }
    }
}
