using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMover : MonoBehaviour
{
    public GameObject IKTarget;
    public float mouseSpeed = 2.0f;
    public float minReachDistance = 0.05f;
    public float maxReachDistance = 3.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float scale = 1.5f;
    private float touchPosY = 0.0f;

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
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

            Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            if (OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad)){
                touchPosY = input.y;
            }
            if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
            {

                if (input.y - touchPosY > 0)
                {
                    scale += 1f * Time.deltaTime;
                }
                else if (input.y - touchPosY < 0)
                {
                    scale -= 1f * Time.deltaTime;
                }
                scale = Mathf.Clamp(scale, minReachDistance, maxReachDistance);

            }
        }
        else
        {
            yaw += mouseSpeed * Input.GetAxis("Mouse X");
            pitch -= mouseSpeed * Input.GetAxis("Mouse Y");
            transform.localEulerAngles = new Vector3(pitch, yaw, 0.0f);

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                scale += 10f * Time.deltaTime;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                scale -= 10f * Time.deltaTime;
            }
            scale = Mathf.Clamp(scale, minReachDistance, maxReachDistance);
        }

        IKTarget.transform.position = transform.position + scale * transform.forward;

    }
}
