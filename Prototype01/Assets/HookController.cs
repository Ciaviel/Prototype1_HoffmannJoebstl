﻿using UnityEngine;
using System.Collections;

public class HookController : MonoBehaviour
{

    private Rigidbody myRigidbody;

    public Vector3 myUp;

    public float ropeForce;
    public float jumpforce;
    public float strafeforce;
    public float dashforce;

    public Transform target1;
    public Transform target2;

    private float distToTarget1;
    private float distToTarget2;

    public float turnspeed;
    private Quaternion targetLookDir;

    public Transform Model;

    bool RotateWithCamera = true;
    float fuel = 100.0f;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myUp = Vector3.up;

        targetLookDir = Quaternion.LookRotation(transform.forward, myUp);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.up != myUp)
        {
            //look straight
            targetLookDir = Quaternion.LookRotation(transform.forward - Vector3.Project(transform.forward, myUp.normalized), myUp);
        }

        if (Input.GetAxis("L_XAxis_1") == 0.0f && Input.GetAxis("L_YAxis_1") == 0.0f && !Input.GetButton("A_1") && fuel <= 100.0f)
        {
            fuel += 1.0f;
            if(fuel > 100.0f){
                fuel = 100.0f;
            }
        }
        else if(fuel > 0.0f){
            if (Input.GetButton("A_1"))
            {
                myRigidbody.AddForce(Model.forward * dashforce, ForceMode.Acceleration);
            }

            myRigidbody.AddForce(Model.right * strafeforce * Input.GetAxis("L_XAxis_1"), ForceMode.Acceleration);
            myRigidbody.AddForce(Model.up * jumpforce * Input.GetAxis("L_YAxis_1"), ForceMode.Acceleration);
            fuel -= 1.0f;
        }

        //Debug.Log(Input.GetAxis("TriggersL_1") + "   " + Input.GetAxis("TriggersR_1"));


        if (Input.GetButton("LB_1") && target1 != null)
        {

            if (myRigidbody.velocity.sqrMagnitude > 0.1f)
            {
                targetLookDir = Quaternion.LookRotation(myRigidbody.velocity, myUp);
            }

            Vector3 dir = target1.position - transform.position;


            if (distToTarget1 <= 0.0f)
            {
                distToTarget1 = dir.magnitude;
            }

            if (dir.magnitude > distToTarget1)
            {
                transform.position = target1.position - dir.normalized * distToTarget1;
            }

            dir.Normalize();

            Vector3 radVel = Vector3.Project(myRigidbody.velocity, dir);
            if (radVel.magnitude > 0.0f && Vector3.Dot(radVel, dir) < 0.0f )
            {
                myRigidbody.AddForce(-radVel, ForceMode.VelocityChange);
            }

            myRigidbody.AddForce(dir * Input.GetAxis("TriggersL_1") * ropeForce, ForceMode.Acceleration);
        }
        else
        {
            distToTarget1 = -1.0f;
        }

        if (Input.GetButton("RB_1") && target2 != null)
        {
            if (myRigidbody.velocity.sqrMagnitude > 0.1f)
            {
                targetLookDir = Quaternion.LookRotation(myRigidbody.velocity, myUp);
            }

            Vector3 dir = target2.position - transform.position;

            if (distToTarget2 <= 0.0f)
            {
                distToTarget2 = dir.magnitude;
            }

            if (dir.magnitude > distToTarget2)
            {
                transform.position = target2.position - dir.normalized * distToTarget2;
            }

            dir.Normalize();

            Vector3 radVel = Vector3.Project(myRigidbody.velocity, dir);
            if (Vector3.Dot(radVel, dir) < 0.0f)
            {
                myRigidbody.AddForce(-radVel, ForceMode.VelocityChange);
            }
            myRigidbody.AddForce(dir * Input.GetAxis("TriggersR_1") * ropeForce, ForceMode.Acceleration);
        }
        else
        {
            distToTarget2 = -1.0f;
        }


        if(RotateWithCamera){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLookDir, turnspeed);
            Model.localRotation = Quaternion.identity;
        }
        else{
            Model.rotation = Quaternion.RotateTowards(Model.rotation, targetLookDir, turnspeed);
        }
    }

    void OnGUI()
    {
        string buttonString = "Rotate Camera with Character: " + RotateWithCamera.ToString();
        if (GUI.Button(new Rect(10, 10, 300, 50), buttonString))
        {
            RotateWithCamera = !RotateWithCamera;
        }
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        GUI.Label(new Rect(Screen.width - 300, 10, Screen.width - 10, 50), "Fuel: " + ((int)fuel).ToString(), style);
    }

}
