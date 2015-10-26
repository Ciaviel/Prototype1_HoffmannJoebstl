using UnityEngine;
using System.Collections;

public class LookAtTargetZone : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        foreach(TargetZone target in TargetZone.allTargetZones)
        {
            if (target.gameObject.activeInHierarchy)
            {
                transform.LookAt(target.transform);
            }
        }
    }
}
