using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace dynamixelunity
{
    

    //further below you can find the "DynamixelObject" class.
    public class dynamixel : MonoBehaviour
    {
        const string dll_path = "dxl_x64_c";

        #region PortHandler
        [DllImport(dll_path)]
        public static extern int portHandler(string port_name);

        [DllImport(dll_path)]
        public static extern bool openPort(int port_num);
        [DllImport(dll_path)]
        public static extern void closePort(int port_num);
        [DllImport(dll_path)]
        public static extern void clearPort(int port_num);

        [DllImport(dll_path)]
        public static extern void setPortName(int port_num, string port_name);
        [DllImport(dll_path)]
        public static extern string getPortName(int port_num);

        [DllImport(dll_path)]
        public static extern bool setBaudRate(int port_num, int baudrate);
        [DllImport(dll_path)]
        public static extern int getBaudRate(int port_num);

        [DllImport(dll_path)]
        public static extern int readPort(int port_num, byte[] packet, int length);
        [DllImport(dll_path)]
        public static extern int writePort(int port_num, byte[] packet, int length);

        [DllImport(dll_path)]
        public static extern void setPacketTimeout(int port_num, UInt16 packet_length);
        [DllImport(dll_path)]
        public static extern void setPacketTimeoutMSec(int port_num, double msec);
        [DllImport(dll_path)]
        public static extern bool isPacketTimeout(int port_num);
        #endregion

        #region PacketHandler
        [DllImport(dll_path)]
        public static extern void packetHandler();

        [DllImport(dll_path)]
        public static extern IntPtr getTxRxResult(int protocol_version, int result);
        [DllImport(dll_path)]
        public static extern IntPtr getRxPacketError(int protocol_version, byte error);

        [DllImport(dll_path)]
        public static extern int getLastTxRxResult(int port_num, int protocol_version);
        [DllImport(dll_path)]
        public static extern byte getLastRxPacketError(int port_num, int protocol_version);

        [DllImport(dll_path)]
        public static extern void setDataWrite(int port_num, int protocol_version, UInt16 data_length, UInt16 data_pos, UInt32 data);
        [DllImport(dll_path)]
        public static extern UInt32 getDataRead(int port_num, int protocol_version, UInt16 data_length, UInt16 data_pos);

        [DllImport(dll_path)]
        public static extern void txPacket(int port_num, int protocol_version);

        [DllImport(dll_path)]
        public static extern void rxPacket(int port_num, int protocol_version);

        [DllImport(dll_path)]
        public static extern void txRxPacket(int port_num, int protocol_version);

        [DllImport(dll_path)]
        public static extern void ping(int port_num, int protocol_version, byte id);

        [DllImport(dll_path)]
        public static extern UInt16 pingGetModelNum(int port_num, int protocol_version, byte id);

        [DllImport(dll_path)]
        public static extern void broadcastPing(int port_num, int protocol_version);
        [DllImport(dll_path)]
        public static extern bool getBroadcastPingResult(int port_num, int protocol_version, int id);

        [DllImport(dll_path)]
        public static extern void reboot(int port_num, int protocol_version, byte id);

        [DllImport(dll_path)]
        public static extern void factoryReset(int port_num, int protocol_version, byte id, byte option);

        [DllImport(dll_path)]
        public static extern void readTx(int port_num, int protocol_version, byte id, UInt16 address, UInt16 length);
        [DllImport(dll_path)]
        public static extern void readRx(int port_num, int protocol_version, UInt16 length);
        [DllImport(dll_path)]
        public static extern void readTxRx(int port_num, int protocol_version, byte id, UInt16 address, UInt16 length);

        [DllImport(dll_path)]
        public static extern void read1ByteTx(int port_num, int protocol_version, byte id, UInt16 address);
        [DllImport(dll_path)]
        public static extern byte read1ByteRx(int port_num, int protocol_version);
        [DllImport(dll_path)]
        public static extern byte read1ByteTxRx(int port_num, int protocol_version, byte id, UInt16 address);

        [DllImport(dll_path)]
        public static extern void read2ByteTx(int port_num, int protocol_version, byte id, UInt16 address);
        [DllImport(dll_path)]
        public static extern UInt16 read2ByteRx(int port_num, int protocol_version);
        [DllImport(dll_path)]
        public static extern UInt16 read2ByteTxRx(int port_num, int protocol_version, byte id, UInt16 address);

        [DllImport(dll_path)]
        public static extern void read4ByteTx(int port_num, int protocol_version, byte id, UInt16 address);
        [DllImport(dll_path)]
        public static extern UInt32 read4ByteRx(int port_num, int protocol_version);
        [DllImport(dll_path)]
        public static extern UInt32 read4ByteTxRx(int port_num, int protocol_version, byte id, UInt16 address);

        [DllImport(dll_path)]
        public static extern void writeTxOnly(int port_num, int protocol_version, byte id, UInt16 address, UInt16 length);
        [DllImport(dll_path)]
        public static extern void writeTxRx(int port_num, int protocol_version, byte id, UInt16 address, UInt16 length);

        [DllImport(dll_path)]
        public static extern void write1ByteTxOnly(int port_num, int protocol_version, byte id, UInt16 address, byte data);
        [DllImport(dll_path)]
        public static extern void write1ByteTxRx(int port_num, int protocol_version, byte id, UInt16 address, byte data);

        [DllImport(dll_path)]
        public static extern void write2ByteTxOnly(int port_num, int protocol_version, byte id, UInt16 address, UInt16 data);
        [DllImport(dll_path)]
        public static extern void write2ByteTxRx(int port_num, int protocol_version, byte id, UInt16 address, UInt16 data);

        [DllImport(dll_path)]
        public static extern void write4ByteTxOnly(int port_num, int protocol_version, byte id, UInt16 address, UInt32 data);
        [DllImport(dll_path)]
        public static extern void write4ByteTxRx(int port_num, int protocol_version, byte id, UInt16 address, UInt32 data);

        [DllImport(dll_path)]
        public static extern void regWriteTxOnly(int port_num, int protocol_version, byte id, UInt16 address, UInt16 length);
        [DllImport(dll_path)]
        public static extern void regWriteTxRx(int port_num, int protocol_version, byte id, UInt16 address, UInt16 length);

        [DllImport(dll_path)]
        public static extern void syncReadTx(int port_num, int protocol_version, UInt16 start_address, UInt16 data_length, UInt16 param_length);
        // syncReadRx   -> GroupSyncRead
        // syncReadTxRx -> GroupSyncRead

        [DllImport(dll_path)]
        public static extern void syncWriteTxOnly(int port_num, int protocol_version, UInt16 start_address, UInt16 data_length, UInt16 param_length);

        [DllImport(dll_path)]
        public static extern void bulkReadTx(int port_num, int protocol_version, UInt16 param_length);
        // bulkReadRx   -> GroupBulkRead
        // bulkReadTxRx -> GroupBulkRead

        [DllImport(dll_path)]
        public static extern void bulkWriteTxOnly(int port_num, int protocol_version, UInt16 param_length);
        #endregion

        #region GroupBulkRead
        [DllImport(dll_path)]
        public static extern int groupBulkRead(int port_num, int protocol_version);

        [DllImport(dll_path)]
        public static extern bool groupBulkReadAddParam(int group_num, byte id, UInt16 start_address, UInt16 data_length);
        [DllImport(dll_path)]
        public static extern void groupBulkReadRemoveParam(int group_num, byte id);
        [DllImport(dll_path)]
        public static extern void groupBulkReadClearParam(int group_num);

        [DllImport(dll_path)]
        public static extern void groupBulkReadTxPacket(int group_num);
        [DllImport(dll_path)]
        public static extern void groupBulkReadRxPacket(int group_num);
        [DllImport(dll_path)]
        public static extern void groupBulkReadTxRxPacket(int group_num);

        [DllImport(dll_path)]
        public static extern bool groupBulkReadIsAvailable(int group_num, byte id, UInt16 address, UInt16 data_length);
        [DllImport(dll_path)]
        public static extern UInt32 groupBulkReadGetData(int group_num, byte id, UInt16 address, UInt16 data_length);
        #endregion

        #region GroupBulkWrite
        [DllImport(dll_path)]
        public static extern int groupBulkWrite(int port_num, int protocol_version);

        [DllImport(dll_path)]
        public static extern bool groupBulkWriteAddParam(int group_num, byte id, UInt16 start_address, UInt16 data_length, UInt32 data, UInt16 input_length);
        [DllImport(dll_path)]
        public static extern void groupBulkWriteRemoveParam(int group_num, byte id);
        [DllImport(dll_path)]
        public static extern bool groupBulkWriteChangeParam(int group_num, byte id, UInt16 start_address, UInt16 data_length, UInt32 data, UInt16 input_length, UInt16 data_pos);
        [DllImport(dll_path)]
        public static extern void groupBulkWriteClearParam(int group_num);

        [DllImport(dll_path)]
        public static extern void groupBulkWriteTxPacket(int group_num);
        #endregion

        #region GroupSyncRead
        [DllImport(dll_path)]
        public static extern int groupSyncRead(int port_num, int protocol_version, UInt16 start_address, UInt16 data_length);

        [DllImport(dll_path)]
        public static extern bool groupSyncReadAddParam(int group_num, byte id);
        [DllImport(dll_path)]
        public static extern void groupSyncReadRemoveParam(int group_num, byte id);
        [DllImport(dll_path)]
        public static extern void groupSyncReadClearParam(int group_num);

        [DllImport(dll_path)]
        public static extern void groupSyncReadTxPacket(int group_num);
        [DllImport(dll_path)]
        public static extern void groupSyncReadRxPacket(int group_num);
        [DllImport(dll_path)]
        public static extern void groupSyncReadTxRxPacket(int group_num);

        [DllImport(dll_path)]
        public static extern bool groupSyncReadIsAvailable(int group_num, byte id, UInt16 address, UInt16 data_length);
        [DllImport(dll_path)]
        public static extern UInt32 groupSyncReadGetData(int group_num, byte id, UInt16 address, UInt16 data_length);
        #endregion

        #region GroupSyncWrite
        [DllImport(dll_path)]
        public static extern int groupSyncWrite(int port_num, int protocol_version, UInt16 start_address, UInt16 data_length);

        [DllImport(dll_path)]
        public static extern bool groupSyncWriteAddParam(int group_num, byte id, UInt32 data, UInt16 data_length);
        [DllImport(dll_path)]
        public static extern void groupSyncWriteRemoveParam(int group_num, byte id);
        [DllImport(dll_path)]
        public static extern bool groupSyncWriteChangeParam(int group_num, byte id, UInt32 data, UInt16 data_length, UInt16 data_pos);
        [DllImport(dll_path)]
        public static extern void groupSyncWriteClearParam(int group_num);

        [DllImport(dll_path)]
        public static extern void groupSyncWriteTxPacket(int group_num);
        #endregion
    }
    [InitializeOnLoad]
    public static class PlayStateNotifier
    {

        static PlayStateNotifier()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
        }

        static void ModeChanged(PlayModeStateChange playModeState)
        {
            if (playModeState == PlayModeStateChange.EnteredEditMode)
            {
                Debug.Log("Entered Edit mode.");
                Debug.Log("Closing dynamixel port! :D");
                dynamixel.closePort(DynamixelObject.port_num);
            }
        }
    }

    
    public class DynamixelObject : MonoBehaviour
    {
        [SerializeField]
        public float mvStep = 3;
        public static float MotorPosition;
        // Control table address
        public const int ADDR_MX_TORQUE_ENABLE = 24;                  // Control table address is different in Dynamixel model
        public const int ADDR_MX_GOAL_POSITION = 30;
        public const int ADDR_MX_PRESENT_POSITION = 36;

        // Protocol version
        public const int PROTOCOL_VERSION = 1;                   // See which protocol version is used in the Dynamixel

        // Default setting
        [SerializeField]
        public const int DXL_ID = 1;                   // Dynamixel ID: 1 
        public const int BAUDRATE = 1000000;
        public const string DEVICENAME = "COM15";              // Check which port is being used on your controller
                                                               // ex) Windows: "COM1"   Linux: "/dev/ttyUSB0" Mac: "/dev/tty.usbserial-*"

        
        public const int TORQUE_ENABLE = 1;                   // Value for enabling the torque
        public const int TORQUE_DISABLE = 0;                   // Value for disabling the torque
        public const int DXL_MINIMUM_POSITION_VALUE = 100;                 // Dynamixel will rotate between this value
        public const int DXL_MAXIMUM_POSITION_VALUE = 4000;                // and this value (note that the Dynamixel would not move when the position value is out of movable range. s
        public const int DXL_MOVING_STATUS_THRESHOLD = 10;                  // Dynamixel moving status threshold

        public const byte ESC_ASCII_VALUE = 0x1b;

        public const int COMM_SUCCESS = 0;                   // Communication Success result value
        public const int COMM_TX_FAIL = -1001;               // Communication Tx Failed

        // Initialize PortHandler Structs
        // Set the port path
        // Get methods and members of PortHandlerLinux or PortHandlerWindows

        public static int port_num = dynamixel.portHandler(DEVICENAME);
        private ushort dxl_present_position, move_goal = 10;
        UInt16[] dxl_goal_position = new UInt16[2] { DXL_MINIMUM_POSITION_VALUE, DXL_MAXIMUM_POSITION_VALUE };         // Goal
        //private int index;
        private int dxl_comm_result;

        void Start()
        {


            // Initialize PacketHandler Structs
            dynamixel.packetHandler();

            //int index = 0;
            //int dxl_comm_result = COMM_TX_FAIL;                                   // Communication result
            UInt16[] dxl_goal_position = new UInt16[2] { DXL_MINIMUM_POSITION_VALUE, DXL_MAXIMUM_POSITION_VALUE };         // Goal position

            //byte dxl_error = 0;                                                   // Dynamixel error
            //UInt16 dxl_present_position = 0;                                      // Present position

            // Open port (COM9)
            if (dynamixel.openPort(port_num))
            {
                Debug.Log("Succeeded to open the port!");
            }
            else
            {
                Debug.Log("Failed to open the port!");

            }

            // Set port baudrate
            if (dynamixel.setBaudRate(port_num, BAUDRATE))
            {
                Debug.Log("Succeeded to change the baudrate!");
            }
            else
            {
                Debug.Log("Failed to change the baudrate!");
            }
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

            

            if (Input.GetKey(KeyCode.L))
            {
                move_goal = (ushort)(dxl_present_position - mvStep);
                WritePosition();
            }
            if (Input.GetKey(KeyCode.J))
            {
                move_goal = (ushort)(dxl_present_position + mvStep);
                WritePosition();
            }

            MotorPosition = dxl_present_position;
        }

        private void WritePosition()
        {


            // Write goal position
            dynamixel.write2ByteTxRx(port_num, PROTOCOL_VERSION, DXL_ID, ADDR_MX_GOAL_POSITION, move_goal);

            //print("Motor 1 Pos: " + MotorPosition + " Move Input: " + move_goal);
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