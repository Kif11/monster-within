using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMover : MonoBehaviour
{
    public GameObject IKTarget;
    public float mouseSpeed = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float scale = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    
        bool isConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);

        if (isConnected)
        {
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        }
        else
        {
            yaw += mouseSpeed * Input.GetAxis("Mouse X");
            pitch -= mouseSpeed * Input.GetAxis("Mouse Y");
            transform.localEulerAngles = new Vector3(pitch, yaw, 0.0f);

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                scale += 0.1f;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                scale -= 0.1f;
            }
        }

        IKTarget.transform.position = transform.position + scale * transform.forward;

    }
}
