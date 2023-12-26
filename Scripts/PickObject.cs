using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class PickObject : MonoBehaviour
{
    Vector3 initialPos;
    Vector3 initialRot;
    Vector3 initialScale;
    Transform rack;
    public GameObject canvas;
    GameObject canvasClone;
    public Transform vrCamera;
    float radius = 5f;
    float radiusObj = 2.7f;
    public Transform parent;
    public Transform dest;
    public GameObject cart;
    //Shader shader1;
    //Shader shader2;
    //Renderer rend;
    UDPScript udp;
    udp = GameObject.Find("dummy").transform.GetComponent<UDPScript>();
    string currentData

    void Start()
    {

        initialPos = transform.position;
        initialRot = transform.eulerAngles;
        initialScale = transform.localScale;
        rack = transform.parent;
        GetComponent<Rigidbody>().maxDepenetrationVelocity = 0;
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { OnDown(); });
        trigger.triggers.Add(entry);
       
        //shader1 = Shader.Find("Standard");
        //shader2 = Shader.Find("Outlined/Custom");
        //if (transform.childCount == 0)
        //{
        //    rend = GetComponent<Renderer>();
        //}
        //else
        //{
        //    if(transform.GetChild(0).name == "Head_and_shoulders")
        //    {
        //        rend = transform.GetChild(0).GetChild(1).GetComponent<Renderer>();
        //    }
        //    else if(transform.GetChild(0).name == "Box002")
        //    {
        //        rend = transform.GetChild(1).GetComponent<Renderer>();
        //    }
        //    else
        //    {
        //       rend = transform.GetChild(0).GetComponent<Renderer>();
        //    }

    //}

}

    // Update is called once per frame
    void Update()
    {
        currentData = udp.getText();
        if (Mathf.Abs(parent.transform.position.x - GetComponent<Rigidbody>().position.x) < 10 &&
          Mathf.Abs(parent.transform.position.y - GetComponent<Rigidbody>().position.y) < 10 &&
          Mathf.Abs(parent.transform.position.z - GetComponent<Rigidbody>().position.z) < 10)
        {

            //GetComponent<Outline>().enabled = true;
            //rend.material.shader = shader2;
        }
        else
        {

            GetComponent<Outline>().enabled = false;
            //rend.material.shader = shader1;
        }
        if (transform.parent == dest.transform)
        {

            GetComponent<Outline>().enabled = false;
            //rend.material.shader = shader1;
        }
        if (transform.parent == cart.transform)
        {

            GetComponent<Outline>().enabled = false;
            //rend.material.shader = shader1;
        }


    }


    public void OnDown()
    {
        print("picked");
        print(transform.name);
        //if ((Mathf.Abs(parent.transform.position.x - GetComponent<Rigidbody>().position.x) < 10 &&
        //    Mathf.Abs(parent.transform.position.y - GetComponent<Rigidbody>().position.y) < 10 &&
        //    Mathf.Abs(parent.transform.position.z - GetComponent<Rigidbody>().position.z) < 10 &&
        //    !cart.GetComponent<RotateCart>().isFull()) || transform.parent == cart.transform)
        //{
        //    transform.position = dest.transform.position;
        //    transform.parent = dest.transform;
        //    GetComponent<Rigidbody>().useGravity = false;
        //    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


        //    float a = vrCamera.eulerAngles.y;
        //    double b = (a * (Math.PI)) / 180;
        //    Vector3 newPos = new Vector3(parent.position.x + radius * (float)Math.Sin(b), 2.4f, parent.position.z + (radius * (float)Math.Cos(b) + 0.2f));
        //    Vector3 newRot = new Vector3(transform.eulerAngles.x, vrCamera.eulerAngles.y, 0f);


        //    canvasClone = Instantiate(canvas, parent);

        //    //canvas.SetActive(true);


        //    //canvas.GetComponent<RectTransform>().anchoredPosition = newPos;
        //    Transform btn1 = canvasClone.transform.GetChild(0).GetChild(2);
        //    btn1.GetComponent<Button>().onClick.AddListener(Place);
        //    Transform btn2 = canvasClone.transform.GetChild(0).GetChild(3);
        //    btn2.GetComponent<Button>().onClick.AddListener(Replace);

        //    canvasClone.transform.eulerAngles = newRot;
        //    canvasClone.transform.position = newPos;
        //    canvasClone.transform.localScale = Vector3.one;
        //}
    }

  

    public void Place()
    {
        print("place");
        GameObject child = dest.GetChild(0).gameObject;
        if (Mathf.Abs(cart.transform.position.x - child.transform.position.x) < 10 &&
            Mathf.Abs(cart.transform.position.y - child.transform.position.y) < 10 &&
            Mathf.Abs(cart.transform.position.z - child.transform.position.z) < 10)
        {
            if (child)
            {
                Destroy(canvasClone);
                child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                float a = vrCamera.eulerAngles.y;
                double b = (a * (Math.PI)) / 180;
                Vector3 newPos = new Vector3(parent.position.x + radiusObj * (float)Math.Sin(b), 1f, parent.position.z + radiusObj * (float)Math.Cos(b));
                child.transform.position = new Vector3(cart.transform.position.x, 1f, cart.transform.position.z);
                //child.transform.eulerAngles = new Vector3(transform.eulerAngles.x, vrCamera.eulerAngles.y - 90, transform.eulerAngles.z);
                child.transform.parent = cart.transform;

                child.GetComponent<Rigidbody>().useGravity = true;
                //child.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //child.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //canvas.SetActive(false);
                
            }


        }
    }


    public void Replace()
    {
        print("replace");
        Destroy(canvasClone);
        GameObject child = dest.GetChild(0).gameObject;
        child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        child.transform.parent = rack;
        child.transform.position = initialPos;
        child.transform.eulerAngles = initialRot;
        child.transform.localScale = initialScale;
        //print(child.GetComponent<PickObject>().getInitial());

        child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //canvas.SetActive(false);
       
    }

}
