using UnityEngine;
using System.Collections;

public class SpeedFeel : MonoBehaviour
{
    public Camera cam;

    public AnimationCurve speedToFov;
    public float baseFov;

    public AnimationCurve speedToDist;
    public float baseDist;

    public float maxSpeed;

    private Vector3 camPos;

    // Use this for initialization
    void Start()
    {
        camPos = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = Vector3.Project(GetComponent<Rigidbody>().velocity, cam.transform.forward).magnitude;//GetComponent<Rigidbody>().velocity.magnitude;
        Debug.Log(velocity);
        if(velocity <= 0.0f)
        {
            return;
        }


        cam.fieldOfView = baseFov * speedToFov.Evaluate(velocity / maxSpeed);
        camPos.z = baseDist * speedToDist.Evaluate(velocity / maxSpeed);
        cam.transform.localPosition = camPos;
    }
}
