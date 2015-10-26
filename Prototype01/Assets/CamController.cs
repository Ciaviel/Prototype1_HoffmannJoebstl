using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour
{
    public HookController hookController;

    public Vector3 ray1Offset;
    public Vector3 ray1Dir;

    public Vector3 ray2Offset;
    public Vector3 ray2Dir;

    public GameObject debugBall1;
    public GameObject debugBall2;

    // Update is called once per frame
    void Update()
    {
        float camX = Input.GetAxis("R_XAxis_1");
        float camY = Input.GetAxis("R_YAxis_1");

        transform.localRotation *= Quaternion.AngleAxis(camX * 2, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(camY * 2, Vector3.right);

        if (!Input.GetButton("LB_1"))
        {
            Ray ray1 = new Ray(transform.position + transform.parent.rotation * ray1Offset, transform.rotation * ray1Dir);
            RaycastHit result1;
            Physics.Raycast(ray1, out result1);

            if (result1.collider != null)
            {
                Debug.DrawLine(transform.position + transform.parent.rotation * ray1Offset, result1.point, Color.green);
                debugBall1.transform.position = result1.point;
                debugBall1.SetActive(true);
                hookController.target1 = debugBall1.transform;
            }
            else
            {
                debugBall1.SetActive(false);
                hookController.target1 = null;
            }
        }
        else
        {
            Debug.DrawLine(transform.position + transform.parent.rotation * ray1Offset, debugBall1.transform.position, Color.blue);
        }

        if (!Input.GetButton("RB_1"))
        {
            Ray ray2 = new Ray(transform.position + transform.parent.rotation * ray2Offset, transform.rotation * ray2Dir);
            RaycastHit result2;
            Physics.Raycast(ray2, out result2);

            if (result2.collider != null)
            {
                Debug.DrawLine(transform.position + transform.parent.rotation * ray2Offset, result2.point, Color.green);
                debugBall2.transform.position = result2.point;
                debugBall2.SetActive(true);
                hookController.target2 = debugBall2.transform;
            }
            else
            {
                debugBall2.SetActive(false);
                hookController.target2 = null;
            }
        }
        else
        {
            Debug.DrawLine(transform.position + transform.parent.rotation * ray2Offset, debugBall2.transform.position, Color.blue);
        }

    }
}
