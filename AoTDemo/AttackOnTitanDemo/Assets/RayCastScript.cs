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

    Vector3 DirectionLeft;
    Vector3 DirectionRight;
	// Use this for initialization

    bool destroyLeft = false;
    bool destroyRight = false;

    void Update()
    {
        if (Input.GetButton("Fire1")) Debug.LogError("Fire1");
        if (Input.GetButton("Fire2")) Debug.LogError("Fire2");
        if (Input.GetButton("Fire3")) Debug.LogError("Fire3");
        if (Input.GetButton("Jump")) Debug.LogError("Jump");
        if (Input.GetButton("LB")) Debug.LogError("LB");
        if (Input.GetButton("RB")) Debug.LogError("RB");
    }
	
	void FixedUpdate ()
    {
        #region Raycasting
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
        #endregion

        #region Bumpermovement
        if (Input.GetMouseButtonUp(0) || Input.GetButtonUp("LB"))
        {
            if (targetLeft == Vector3.zero)
            {
                targetLeft = hitLeft.point;
                Vector3 Direction = Vector3.Cross(targetLeft - transform.position, Vector3.up).normalized;
                if (Mathf.Sign(Vector3.Dot(transform.forward, Direction)) == -1)
                {
                    Direction = Quaternion.Euler(0.0f, 180.0f, 0.0f) * Direction;
                }
                transform.parent.GetComponent<Rigidbody>().velocity = Direction * transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
            }
            
            
        }
        if (Input.GetMouseButtonUp(1) || Input.GetButtonUp("RB"))
        {
            if (targetRight == Vector3.zero)
            {
                targetRight = hitRight.point;
                Vector3 Direction = Vector3.Cross(targetRight - transform.position, Vector3.up).normalized;
                if (Mathf.Sign(Vector3.Dot(transform.forward, Direction)) == -1)
                {
                    Direction = Quaternion.Euler(0.0f, 180.0f, 0.0f) * Direction;
                }
                transform.parent.GetComponent<Rigidbody>().velocity = Direction * transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
            }
        }

        if (targetLeft != Vector3.zero)
        {
            DirectionLeft = Vector3.Cross(targetLeft - transform.position, Vector3.up).normalized;
            if (Mathf.Sign(Vector3.Dot(transform.parent.GetComponent<Rigidbody>().velocity - transform.position, DirectionLeft)) == -1)
            {
                Debug.LogError(Vector3.Dot(transform.parent.GetComponent<Rigidbody>().velocity - transform.position, DirectionRight));
                DirectionLeft = Quaternion.Euler(0.0f, 180.0f, 0.0f) * DirectionLeft;
            }

            Debug.DrawRay(transform.position, targetLeft - transform.position, Color.red);
            Debug.DrawLine(targetLeft, targetLeft + transform.up, Color.blue);
            Debug.DrawLine(targetLeft, targetLeft + DirectionLeft, Color.green);
        }

        if (targetRight != Vector3.zero)
        {
            DirectionRight = Vector3.Cross(targetRight - transform.position, Vector3.up).normalized;
            if (Mathf.Sign(Vector3.Dot(transform.parent.GetComponent<Rigidbody>().velocity - transform.position, DirectionRight)) == -1)
            {
                Debug.LogError(Vector3.Dot(transform.parent.GetComponent<Rigidbody>().velocity - transform.position, DirectionRight));
                DirectionRight = Quaternion.Euler(0.0f, 180.0f, 0.0f) * DirectionRight;
            }
            

            Debug.DrawRay(transform.position, targetRight - transform.position, Color.red);
            Debug.DrawLine(targetRight, targetRight + transform.up, Color.blue);
            Debug.DrawLine(targetRight, targetRight + DirectionRight, Color.green);
        }

        Vector3 Dir = DirectionLeft + DirectionRight;
        if(Dir != Vector3.zero) 
            transform.parent.GetComponent<Rigidbody>().velocity = Dir.normalized * transform.parent.GetComponent<Rigidbody>().velocity.magnitude;

        Debug.DrawLine(transform.position, transform.position + transform.parent.GetComponent<Rigidbody>().velocity, Color.white);
        #endregion

        #region Triggermovement

        if (targetLeft != Vector3.zero)
        {
            if (Input.GetAxis("LT") >= 0.5f)
            {
                destroyLeft = true;

                transform.parent.GetComponent<Rigidbody>().AddForce((targetLeft - transform.position).normalized * 2.0f, ForceMode.VelocityChange);
            }
            else if (Input.GetAxis("LT") <= 0.01f && destroyLeft)
            {
                targetLeft = Vector3.zero;
                DirectionLeft = Vector3.zero;

                destroyLeft = false;
            }
        }

        if (targetRight != Vector3.zero)
        {
            if (Input.GetAxis("RT") >= 0.5f)
            {
                destroyRight = true;

                transform.parent.GetComponent<Rigidbody>().AddForce((targetRight - transform.position).normalized * 2.0f, ForceMode.VelocityChange);
            }
            else if (Input.GetAxis("RT") <= 0.01f && destroyRight)
            {
                targetRight = Vector3.zero;
                DirectionRight = Vector3.zero;

                destroyRight = false;
            }
        }
        #endregion
    }
}
