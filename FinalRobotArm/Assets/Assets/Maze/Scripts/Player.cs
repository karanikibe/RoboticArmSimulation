using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System;
using System.Net;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	TcpListener listener;
	IPAddress LocalAddr = IPAddress.Parse("127.0.0.1");
	
	public float ang, fc, dir = 1;
	public string fileName = "User_name";
	public bool overwriteExistingFile = false;
	private float  highThresh = 0.2f, lowThresh =0.01f;

	public float delay = 0.5f;
	private bool firstTrigger, secondTrigger;
	private float timeOfFirstTrigger;
	private bool reset;
    public GameObject imgCapture;


    void Start ()
	{
		listener = new TcpListener(LocalAddr, 55001);
		listener.Start();
		print("is listening");

		// logging
		string testFileName = fileName;
		if (!overwriteExistingFile)
		{
			int counter = 1;
			while (File.Exists("C:/Users/Muguro/Desktop/NeckEMG Data/" + testFileName))
			{
				testFileName = fileName + "_" + (counter++) + ".csv"; // e.g., "results_12.csv"
			}
            

        }
        
		fileName = testFileName;

        
        string imgFileName = fileName.Replace(".csv", ".jpg");

        TextWriter file = new StreamWriter("C:/Users/Muguro/Desktop/NeckEMG Data/" + fileName, true);

		file.Write("GameTime, xPosition, zPosition," + "Date" + "\n");
		file.Close();
        // call the takeshot 
        imgCapture.GetComponent<ImageCapture>().Snapshot("C:/Users/Muguro/Desktop/NeckEMG Data/IMG/" + imgFileName);
    }

	void FixedUpdate()
	{
		if (!listener.Pending())
		{ }
		else
		{

			//    //print("socket comes");
			TcpClient client = listener.AcceptTcpClient();
			NetworkStream ns = client.GetStream();
			StreamReader reader = new StreamReader(ns);
			string msg = reader.ReadToEnd();
			string[] data = msg.Split(',');
			ang = float.Parse(data[0]);
			fc = float.Parse(data[1]);

			//print($"Ang: Force: {fc}");

		}//end of slider

      
		transform.position += new Vector3(ang * Time.deltaTime, 0, dir * fc *2.5f * Time.deltaTime);
		WriteIt();
	}

	
	void WriteIt()
	{
		TextWriter file = new StreamWriter("C:/Users/Muguro/Desktop/NeckEMG Data/" + fileName, true);

		file.Write(Time.time.ToString() + "," + transform.position.x.ToString() + "," 
			+ transform.position.z.ToString() + "," + ang.ToString() + ","
			+ fc.ToString() +  "\n"); //"," + Occurence +
				
		file.Close();

	}

	void Update()
	{
		DoubleClick();
		
	}

	void DoubleClick()
	{
		if (fc > highThresh && firstTrigger && secondTrigger)
		{
			if (Time.time - timeOfFirstTrigger < delay)
			{
				Debug.Log("Double Clicked");
				dir = -1f * dir;
			}
		
			reset = true;
		}

		if (fc > highThresh && !firstTrigger)
		{
			firstTrigger = true;
			timeOfFirstTrigger = Time.time;
		}

		if (fc < lowThresh && firstTrigger)
		{
			secondTrigger = true;
			//timeOfFirstButton = Time.time;
		}

		if (reset)
		{
			firstTrigger = false;
			secondTrigger = false;
			reset = false;
		}
	}
}
