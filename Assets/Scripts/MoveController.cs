using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class MoveController : MonoBehaviour
{
    public bool moveForward;

    // UDP Code

    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; // local host
    [SerializeField] int rxPort = 8000; // port to receive data from Python on
    [SerializeField] int txPort = 8001; // port to send data to Python on

    //int i = 0; // DELETE THIS: Added to show sending data from Unity to Python via UDP

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread

    //IEnumerator SendDataCoroutine() // DELETE THIS: Added to show sending data from Unity to Python via UDP
    //{
    //    while (true)
    //    {
    //        SendData("Sent from Unity: " + i.ToString());
    //        i++;
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    public void SendData(string message) // Use to send data to Python
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    void Awake()
    {
        // Create remote endpoint (to Matlab) 
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

        // Create local client
        client = new UdpClient(rxPort);

        // local endpoint define (where messages are received)
        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialize (seen in comments window)
        print("UDP Comms Initialised");

        //StartCoroutine(SendDataCoroutine()); // DELETE THIS: Added to show sending data from Unity to Python via UDP
    }

    // Receive data, update packets received
    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                print(">> " + text);
                if (text.Equals("forward"))
                {
                    moveForward = true;
                }
                else
                {
                    moveForward = false;
                }
                ProcessInput(text);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ProcessInput(string input)
    {
        // PROCESS INPUT RECEIVED STRING HERE

        if (!isTxStarted) // First data arrived so tx started
        {
            isTxStarted = true;
        }
    }

    //Prevent crashes - close clients and threads properly!
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }




    public Transform vrCamera;
	public Transform cart;
	public float toggleAngle = 30.0f;
	public float speed = 3.0f;
	
	public bool moveBackward = false;
	
	

	private CharacterController cc;


	float radius;
	float a;
	double b;
	Vector3 newPos;
	Vector3 newRot;

	

	// Use this for initialization
	void Start()
	{
		cc = GetComponent<CharacterController>();
		radius = cart.position.z;
		
	}

	

	// Update is called once per frame
	void Update()
	{
		//if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f)
		//{
		//	moveForward = true;
		//}
		//else
		//{
		//	moveForward = false;
		//}

		// cart code
		a = vrCamera.eulerAngles.y;
		b = (a * (Math.PI)) / 180;
		newPos = new Vector3(transform.position.x + radius * (float)Math.Sin(b), cart.position.y, transform.position.z + radius * (float)Math.Cos(b));
		newRot = new Vector3(cart.eulerAngles.x, vrCamera.eulerAngles.y - 90, cart.eulerAngles.z);
		//parent.GetComponent<CharacterController>().center = new Vector3(+radius * (float)Math.Sin(b), 0f, radius * (float)Math.Cos(b));
		cart.eulerAngles = newRot;



		if ((newPos.x > -64f && newPos.x < 26f) && (newPos.z > -4f && newPos.z < 57f))
		{
			if (((newPos.x > 21f && newPos.x < 29f) || (newPos.x > -10f && newPos.x < 2f) || (newPos.x > -39f && newPos.x < -27f) || (newPos.x > -61f && newPos.x < -59f)) && (newPos.z > 8f && newPos.z < 50f))
			{
				moveForward = false;
			}

			else
			{
				cart.position = newPos;
				//latitude = Input.location.lastData.latitude;
				//longitude = Input.location.lastData.longitude;
				//Vector3 newPos1 = Quaternion.AngleAxis(longitude, -Vector3.up) * Quaternion.AngleAxis(latitude, -Vector3.right) * new Vector3(0, 0, 1);
				//transform.position = new Vector3(newPos1.x, 3.2f, newPos1.y);

			}

			if (moveForward)
			{
				Vector3 forward = vrCamera.TransformDirection(Vector3.forward);

				cc.SimpleMove(forward * speed);




				//Vector3 backward = vrCamera.TransformDirection(Vector3.back);

				//cc.SimpleMove(backward * speed);
			}


		}


       
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//Application.Quit();
			System.Diagnostics.Process.GetCurrentProcess().Kill();
		}

	}



}
