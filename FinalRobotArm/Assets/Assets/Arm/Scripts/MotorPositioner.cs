using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System;
using System.Net;
using UnityEngine.UI;
public class MotorPositioner : MonoBehaviour
{
    public GameObject motor1, motor2, motor3;
   
    private void LateUpdate()
    {
        float m1 = Map(dynamixelunity.DynamixelObject.MotorPosition, 45, 980, -145, 145);
        motor1.transform.localRotation = Quaternion.Euler(0, m1, 0);

        float m2 = Map(dynamixelunity.DynamixelMotor2.MotorPosition, 45, 980, -145, 145);
        motor2.transform.localRotation = Quaternion.Euler( m2, 0, -90);

        float m3 = Map(dynamixelunity.DynamixelMotor3.MotorPosition, 45, 980, -145, 145);
        motor3.transform.localRotation = Quaternion.Euler(0, m3, 0);
    }
    
    private float Map(float value, float motorMin, float motorMax, float angMin, float angMax)
    {
        return (value - motorMin) / (motorMax - motorMin) * (angMax - angMin) + angMin;
    }

}
