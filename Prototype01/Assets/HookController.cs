using UnityEngine;
using System.Collections;

public class HookController : MonoBehaviour
{

    private Rigidbody myRigidbody;

    public Vector3 myUp;

    public float ropeForce;
    public float jumpforce;

    public Transform target1;
    public Transform target2;

    private float distToTarget1;
    private float distToTarget2;

    public float turnspeed;
    private Quaternion targetLookDir;

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

        if (Input.GetButtonDown("Jump"))
        {
            myRigidbody.AddForce(myUp * jumpforce, ForceMode.VelocityChange);
        }

        Debug.Log(Input.GetAxis("Trigger1") + "   " + Input.GetAxis("Trigger2"));


        if (Input.GetButton("Fire1") && target1 != null)
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
            else
            {
                myRigidbody.AddForce(dir * Input.GetAxis("Trigger1") * ropeForce, ForceMode.Acceleration);
            }
        }
        else
        {
            distToTarget1 = -1.0f;
        }

        if (Input.GetButton("Fire2") && target2 != null)
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
            else
            {
                myRigidbody.AddForce(dir * Input.GetAxis("Trigger2") * ropeForce, ForceMode.Acceleration);
            }
        }
        else
        {
            distToTarget2 = -1.0f;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLookDir, turnspeed);
    }

}
