using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Net;

public class MotorController : MonoBehaviour
{
    TcpListener listener;
    IPAddress LocalAddr = IPAddress.Parse("127.0.0.1");
    public JointManager[] joints;
    private float neckInput, biteInput, faceInput;
    public string fileName = "User_name";
    public bool overwriteExistingFile = false;
    private float highThresh = 0.2f, lowThresh = 0.01f;

    private bool firstTrigger, secondTrigger, resetClick, motorSet = false;
    private bool forwardMove;
    private float timeOfFirstTrigger, delay = 0.5f;
    private Transform currentMotor;
    public Transform endEffector;
    private Vector3 rotVector = Vector3.zero;
    public float MoveSpeed = 20;
    [SerializeField] private float biteGain = 5, faceGain = 7;

    void Start()
    {
        listener = new TcpListener(LocalAddr, 55001);
        listener.Start();
        print("is listening");

        string testFileName = fileName + ".csv";
        if (!overwriteExistingFile)
        {
            int counter = 1;
            while (File.Exists("C:/Users/Muguro/Desktop/NeckEMG Data/" + testFileName))
            {
                testFileName = fileName + "_" + (counter++) + ".csv"; // e.g., "results_12.csv"
            }
        }

        fileName = testFileName;
        //string imgFileName = fileName.Replace(".csv", ".jpg");

        TextWriter file = new StreamWriter("C:/Users/Muguro/Desktop/NeckEMG Data/" + fileName, true);

        file.Write("GameTime, TipPos X, TipPos Y, TipPos Z, Neck,  Bite, Face," + "\n");
        file.Close();
        //imgCapture.GetComponent<ImageCapture>().Snapshot("C:/Users/Muguro/Desktop/NeckEMG Data/IMG/" + imgFileName);
    }

    void FixedUpdate()
    {
        if (!listener.Pending())
        { }
        else
        {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            string msg = reader.ReadToEnd();
            string[] data = msg.Split(',');
            neckInput = float.Parse(data[0]);
            biteInput = float.Parse(data[1]);
            faceInput = float.Parse(data[2]);
        }//end of slider

                  

        DoubleClick();

        
            

        if (!motorSet)
        {
            // control motor 0 :: m2 X-axis
            currentMotor = joints[0].transform;
            rotVector = new Vector3(0, 1, 0);
        }
        else
        {
            //control motor 1:: m3 Y-axis
            currentMotor = joints[1].transform;
            rotVector = new Vector3(0, 1, 0);
        }
        if (forwardMove)
        {
            currentMotor.Rotate(rotVector * biteInput * Time.deltaTime * MoveSpeed * biteGain);
            //transform.position += new Vector3(ang * Time.deltaTime, 0, dir * fc * 2.5f * Time.deltaTime);
        }
        currentMotor.Rotate(rotVector * faceInput * Time.deltaTime * -MoveSpeed * faceGain);
        joints[2].transform.Rotate(new Vector3(0, 1, 0) * neckInput * Time.deltaTime * MoveSpeed);
        WriteFile();
    }

    void DoubleClick()
    {
        if (biteInput > highThresh && !firstTrigger)
        {
            firstTrigger = true;
            timeOfFirstTrigger = Time.time;
        }

        if (biteInput < lowThresh && firstTrigger && Time.time - timeOfFirstTrigger < delay)
        {
            secondTrigger = true;
            forwardMove = false;
        }
        

        if (biteInput > highThresh && firstTrigger && secondTrigger )
        {
            if (Time.time - timeOfFirstTrigger < delay)
            {
                Debug.Log("Double Click");
                motorSet = !motorSet;
                resetClick = true;
            }
            else
            {
                resetClick = true;
                
            }
        }
        
        if(Time.time - timeOfFirstTrigger > delay)
        {
            resetClick = true;
        }
         
        if (resetClick)
        {
            firstTrigger = false;
            secondTrigger = false;
            resetClick = false;
            forwardMove = true;
        }
    }
    void WriteFile()
    {
        TextWriter file = new StreamWriter("C:/Users/Muguro/Desktop/NeckEMG Data/" + fileName, true);

        file.Write(Time.time.ToString() + "," 
            + endEffector.position.x.ToString() + "," + endEffector.position.y.ToString() + "," + endEffector.position.z.ToString() + ","
            + neckInput.ToString() + "," + biteInput.ToString() + "," + faceInput.ToString() + "\n"); //"," + Occurence +

        file.Close();

    }
}
