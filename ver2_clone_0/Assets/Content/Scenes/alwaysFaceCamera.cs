using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysFaceCamera : MonoBehaviour
{

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

}