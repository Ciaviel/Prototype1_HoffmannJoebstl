using UnityEngine;
using System.Collections;

public class HookController : MonoBehaviour
{

    private Rigidbody myRigidbody;

    public Transform target;

    public float camspeed;

    private Quaternion targetLookDir;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //pick target



        //if (Input.GetButtonDown("Fire1"))
        {
            if (myRigidbody.velocity.sqrMagnitude > 0.1f)
            {
                targetLookDir = Quaternion.LookRotation(myRigidbody.velocity, Vector3.up);
            }
            else
            {
                targetLookDir = Quaternion.LookRotation(transform.forward, Vector3.up);

            }
        }

        if (Input.GetButton("Fire1"))
        {
            Vector3 dir = target.position - transform.position;
            dir.Normalize();

            Vector3 radVel = Vector3.Project(myRigidbody.velocity, dir);

            //Vector3 remain = myRigidbody.velocity - radVel;


            myRigidbody.AddForce(-radVel, ForceMode.VelocityChange);
            //myRigidbody.AddForce(remain.normalized*radVel.magnitude, ForceMode.VelocityChange);



            //float distToTarget = (target.position - transform.position).magnitude;

            //Vector3 dir = target.position - transform.position;
            //dir.Normalize();

            //myRigidbody.AddForce(dir * force, ForceMode.VelocityChange);
        }


        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetLookDir, camspeed);
    }
}
