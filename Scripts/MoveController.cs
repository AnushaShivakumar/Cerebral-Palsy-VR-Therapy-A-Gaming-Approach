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

    

    public Transform vrCamera;
	public Transform cart;
	public float toggleAngle = 30.0f;
	public float speed = 1.0f;
	
	public bool moveBackward = false;

	
	
	

	private CharacterController cc;


	float radius;
	float a;
	double b;
	Vector3 newPos;
	Vector3 newRot;
	UDPScript udp;


	// Use this for initialization
	void Start()
	{
		cc = GetComponent<CharacterController>();
		radius = cart.position.z;
		udp = GameObject.Find("dummy").transform.GetComponent<UDPScript>();
    }

   


    // Update is called once per frame
    void Update()
	{
		string currentData = udp.getText();
        //if (vrCamera.eulerAngles.x >= toggleAngle && vrCamera.eulerAngles.x < 90.0f)
        //{
        //	moveForward = true;
        //}
        //else
        //{
        //	moveForward = false;
        //}

        if (currentData.Equals("forward"))
        {
            moveForward = true;
        }
        if (currentData.Equals("stop"))
        {
            moveForward = false;
        }

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
