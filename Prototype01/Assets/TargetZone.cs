using UnityEngine;
using System.Collections.Generic;

public class TargetZone : MonoBehaviour
{

    public static List<TargetZone> allTargetZones;

    public bool isStarter;

    // Use this for initialization
    void Start()
    {
        if( allTargetZones == null)
        {
            allTargetZones = new List<TargetZone>();
        }
        allTargetZones.Add(this);

        gameObject.SetActive(isStarter);
    }

    private void OnTriggerEnter(Collider other)
    {


        int i;
        do
        {
            i = Random.Range(0, allTargetZones.Count - 1);
        } while (allTargetZones[i] == this);

        gameObject.SetActive(false);
        allTargetZones[i].gameObject.SetActive(true);
    }
}
