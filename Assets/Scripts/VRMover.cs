using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMover : MonoBehaviour
{
  public float speed = 1.0f;
  private Transform cam;

  void Start()
  {
    // This camera is controlled by headset rotation
    cam = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>();
  }

  void Update()
  {
      transform.position = cam.position;
    // Quaternion q = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
    // bool isConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);

    //if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
    //{
    //  transform.Translate(cam.forward * speed * Time.deltaTime, Space.World);
    //}

    //if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
    //{
    //  transform.Translate(-cam.forward * speed * Time.deltaTime, Space.World);
    //}
  }
}