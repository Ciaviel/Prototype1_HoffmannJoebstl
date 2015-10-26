using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = new Color(Random.value + 0.2f, Random.value + 0.2f, Random.value + 0.3f, 1.0f);
    }
}
