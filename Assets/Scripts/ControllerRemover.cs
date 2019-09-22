using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRemover : MonoBehaviour
{
    [SerializeField] string hand;
    [SerializeField] GameObject exitMenu;
    void Start()
    {
        if (hand == "Right" && OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        {
            gameObject.SetActive(false);
        }
        if (hand == "Left" && OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Start) || OVRInput.Get(OVRInput.Button.Back))
        {
            Debug.Log("HELLO");
            Vector3 pos = exitMenu.transform.position;
            pos.y = Camera.main.transform.position.y;   
            exitMenu.transform.position = pos;
            exitMenu.SetActive(true);
        }
    }
}
