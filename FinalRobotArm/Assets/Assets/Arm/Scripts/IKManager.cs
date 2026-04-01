using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    public JointManager m_root;
    public JointManager m_end;
    public GameObject Target;
    public float m_rate = 5f;
    public float m_threshold = 0.025f;
    public int m_steps = 10;
    float CalculateSlope(JointManager _joint)
    {
        float deltaTheta = 0.01f;
        float currentDist = GetDistance(m_end.transform.position, Target.transform.position);
        _joint.RotateJoint(deltaTheta);
        float newDist = GetDistance(m_end.transform.position, Target.transform.position);
        _joint.RotateJoint(-deltaTheta);
        return (newDist - currentDist) / deltaTheta;
    }
    void Update()
    {
        for (int i = 0; i < m_steps; i++)
        {
            if (GetDistance(m_end.transform.position, Target.transform.position) > m_threshold)
            {
                JointManager currentJoint = m_root;
                while (currentJoint != null)
                {
                    float slope = CalculateSlope(currentJoint);
                    currentJoint.RotateJoint(-slope * m_rate);
                    currentJoint = currentJoint.GetJointChild();
                }

            }
        }


    }
    float GetDistance(Vector3 _point1, Vector3 _point2)
    {
        return Vector3.Distance(_point1, _point2);
    }
}