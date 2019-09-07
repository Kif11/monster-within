using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleOffsetter : MonoBehaviour
{
    public GameObject tentacleLeftyPos;
    // Start is called before the first frame update
    void Start()
    {
        bool isGoLeftyConnected = OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);
        if (true)
        {
            transform.position = tentacleLeftyPos.transform.position;
            transform.rotation = tentacleLeftyPos.transform.rotation;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
