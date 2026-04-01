using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace dynamixelunity
{
    public class DynamixelMotor3 : MonoBehaviour
    {
        public float mvStep = 3;
        public static float MotorPosition;
        // Control table address
        public const int ADDR_MX_TORQUE_ENABLE = 24;                  // Control table address is different in Dynamixel model
        public const int ADDR_MX_GOAL_POSITION = 30;
        public const int ADDR_MX_PRESENT_POSITION = 36;

        // Protocol version
        public const int PROTOCOL_VERSION = 1;                   // See which protocol version is used in the Dynamixel

        public const int DXL_ID = 3;                   // Dynamixel ID: 1 
        public const string DEVICENAME = "COM15";              // Check which port is being used on your controller
                                                               // ex) Windows: "COM1"   Linux: "/dev/ttyUSB0" Mac: "/dev/tty.usbserial-*"


        public const int TORQUE_ENABLE = 1;                   // Value for enabling the torque
        public const int TORQUE_DISABLE = 0;                   // Value for disabling the torque



        public const int COMM_SUCCESS = 0;                   // Communication Success result value
        public const int COMM_TX_FAIL = -1001;               // Communication Tx Failed


        public static int port_num = dynamixel.portHandler(DEVICENAME);
        private ushort dxl_present_position, move_amt = 10;
        // Goal
        //private int index;
        private int dxl_comm_result;

        void Start()
        {


            InvokeRepeating("ReadPosition", 2, 0.1f);

        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Enable motor torque
                dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_TORQUE_ENABLE, TORQUE_ENABLE);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                //disable motor torque
                dynamixel.write1ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_TORQUE_ENABLE, TORQUE_DISABLE);
            }

            

            if (Input.GetKey(KeyCode.A))
            {
                move_amt = (ushort)(dxl_present_position - mvStep);
                WritePosition();
            }
            if (Input.GetKey(KeyCode.D))
            {
                move_amt = (ushort)(dxl_present_position + mvStep);
                WritePosition();
            }
            MotorPosition = dxl_present_position;
        }

        private void WritePosition()
        {


            // Write goal position
            dynamixel.write2ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_GOAL_POSITION, move_amt);


        }

        private void ReadPosition()
        {
            //print($"Motor reader: {id}");
            // Read present position
            dxl_present_position = dynamixel.read2ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_PRESENT_POSITION);
            if ((dxl_comm_result = dynamixel.getLastTxRxResult(port_num, PROTOCOL_VERSION)) != COMM_SUCCESS)
            {
                Debug.Log("Error: " + PROTOCOL_VERSION + " : " + dxl_comm_result);
            }
           

            //print("Pos: " +  dxl_present_position);
            //yield return new WaitForSeconds(0.1f);
        }
    }
    
}