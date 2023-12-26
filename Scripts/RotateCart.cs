using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateCart : MonoBehaviour
{

    public Transform vrCamera;
    public GameObject parent;
    float radius;
    float speed = 5f;
    float count;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        //radius = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //float a = vrCamera.eulerAngles.y;
        //double b = (a * (Math.PI)) / 180;
        //Vector3 newPos = new Vector3(parent.transform.position.x + radius * (float)Math.Sin(b), transform.position.y, parent.transform.position.z + radius * (float)Math.Cos(b));
        //Vector3 newRot = new Vector3(transform.eulerAngles.x, vrCamera.eulerAngles.y - 90, transform.eulerAngles.z);
        ////parent.GetComponent<CharacterController>().center = new Vector3(+radius * (float)Math.Sin(b), 0f, radius * (float)Math.Cos(b));
        //transform.eulerAngles = newRot;
        //print(newPos.x);
        //print(newPos.z);
        //if ((newPos.x>23f && newPos.x<27f) && (newPos.z>10f && newPos.z<48f))
        //{

        //    print("true");
        //    transform.position = transform.position;
        //}
        //else
        //{
        //    print("false");
        //    transform.position = newPos;
        //}


        //Vector3 targetDirection = newPos - transform.position;
        //float singleStep = speed * Time.deltaTime;
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);

        //transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, newRot, Time.deltaTime, 0.0f);
        //transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime);




        count = transform.childCount - 40;
        if (isFull())
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
        
    }

    public bool isFull()
    {

        if (count >= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
