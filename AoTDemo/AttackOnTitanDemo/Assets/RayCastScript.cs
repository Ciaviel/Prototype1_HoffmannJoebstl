using UnityEngine;
using System.Collections;

public class RayCastScript : MonoBehaviour {

    public GameObject Sphere;
    private GameObject LocalSphereLeft;
    private GameObject LocalSphereRight;
    RaycastHit hitLeft;
    RaycastHit hitRight;
    Ray rayLeft;
    Ray rayRight;

    Vector3 targetLeft;
    Vector3 targetRight;

	// Use this for initialization

    void Update()
    {

    }
	
	void FixedUpdate () {
        rayLeft = new Ray(transform.position, Quaternion.Euler(0.0f, -15.0f, 0.0f) * transform.forward);
        rayRight = new Ray(transform.position, Quaternion.Euler(0.0f, 15.0f, 0.0f) * transform.forward);

        if (Physics.Raycast(rayLeft, out hitLeft))
        {
            if (LocalSphereLeft == null)
            {
                LocalSphereLeft = Instantiate(Sphere, hitLeft.point, Quaternion.identity) as GameObject;
            }
            else
            {
                LocalSphereLeft.transform.position = hitLeft.point;
            }
        }
        else
        {
            if (LocalSphereLeft != null)
            {
                Destroy(LocalSphereLeft);
                //targetLeft = Vector3.zero;
            }
        }

        if (Physics.Raycast(rayRight, out hitRight))
        {
            if (LocalSphereRight == null)
            {
                LocalSphereRight = Instantiate(Sphere, hitRight.point, Quaternion.identity) as GameObject;
            }
            else
            {
                LocalSphereRight.transform.position = hitRight.point;
            }
        }
        else
        {
            if (LocalSphereRight != null)
            {
                Destroy(LocalSphereRight);
                //targetRight = Vector3.zero;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (targetLeft == Vector3.zero)
            {
                targetLeft = hitLeft.point;
            }
            //else
            //{
            //    targetLeft = Vector3.zero;
            //}
            Debug.LogError(targetLeft);
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (targetRight == Vector3.zero)
            {
                targetRight = hitRight.point;
            }
            //else
            //{
            //    targetRight = Vector3.zero;
            //}
            Debug.LogError(targetRight);
        }

        if (targetLeft != Vector3.zero)
        {
            Vector3 Direction = Vector3.Cross(transform.position - hitLeft.point, Vector3.up).normalized;
            if (Mathf.Sign(Vector3.Dot(transform.forward, Vector3.Cross(transform.position - hitLeft.point, Vector3.up).normalized)) == -1)
            {
                Direction = Quaternion.Euler(0.0f, 180.0f, 0.0f) * Direction;
            }
            //transform.parent.position += Direction * 0.2f;
            transform.parent.GetComponent<Rigidbody>().AddForce(Direction * 10.0f, ForceMode.VelocityChange);

            Debug.DrawRay(transform.position, hitLeft.point - transform.position, Color.red);
            Debug.DrawLine(hitLeft.point, hitLeft.point + Vector3.up, Color.blue);
            Debug.DrawLine(hitLeft.point, hitLeft.point + Direction, Color.green);
        }

        if (targetRight != Vector3.zero)
        {
            Vector3 Direction = Vector3.Cross(transform.position - hitRight.point, Vector3.up).normalized;
            if (Mathf.Sign(Vector3.Dot(transform.forward, Vector3.Cross(transform.position - hitRight.point, Vector3.up).normalized)) == -1)
            {
                Direction = Quaternion.Euler(0.0f, 180.0f, 0.0f) * Direction;
            }
            //transform.parent.position += Direction * 0.2f;
            transform.parent.GetComponent<Rigidbody>().AddForce(Direction * 10.0f, ForceMode.VelocityChange);

            Debug.DrawRay(transform.position, hitRight.point - transform.position, Color.red);
            Debug.DrawLine(hitRight.point, hitRight.point + Vector3.up, Color.blue);
            Debug.DrawLine(hitRight.point, hitRight.point + Direction, Color.green);
        }

        //Debug.LogError("Left: " + Vector3.Dot(transform.forward, Vector3.Cross(transform.position - hitLeft.point, Vector3.up).normalized) + " Right: " + Vector3.Dot(transform.forward, Vector3.Cross(transform.position - hitRight.point, Vector3.up).normalized));
	}
}
