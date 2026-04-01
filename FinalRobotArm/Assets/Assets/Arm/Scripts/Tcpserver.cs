using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPServer : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;
    private bool isRunning = false;
    private float emgValue = 0f;
    private bool dataReceived = false; // Prevent rotation until data is received

    public JointManager jointManager; // Assign m1's JointManager in Inspector

    void Start()
    {
        serverThread = new Thread(StartServer);
        serverThread.IsBackground = true;
        serverThread.Start();
    }

    void StartServer()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, 1984);
            server.Start();
            isRunning = true;
            Debug.Log("TCP Server started, waiting for connections...");

            while (isRunning)
            {
                using (TcpClient client = server.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];

                    while (client.Connected)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            Debug.Log("Received EMG Data: " + receivedData);

                            if (float.TryParse(receivedData, out float value))
                            {
                                emgValue = Mathf.Clamp(value, 0f, 1f);
                                dataReceived = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Server Error: " + e.Message);
        }
    }

    void Update()
    {
        if (jointManager != null && dataReceived)
        {
            float angle = Mathf.Lerp(jointManager.AngleMin, jointManager.AngleMax, emgValue);
            jointManager.RotateJoint(angle);
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        server?.Stop();
        if (serverThread != null && serverThread.IsAlive)
        {
            serverThread.Abort();
        }
    }
}
