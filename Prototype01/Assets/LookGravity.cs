using UnityEngine;
using System.Collections;

public class LookGravity : MonoBehaviour
{
    public HookController hookController;


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-hookController.myUp);
    }
}
