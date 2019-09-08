using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMover : MonoBehaviour
{
    public GameObject IKTarget;
    [SerializeField] GameObject Hand;
    public float mouseSpeed = 2.0f;
    public float minReachDistance = 0.05f;
    public float maxReachDistance = 3.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float scale = 1.5f;
    private float touchPosY = 0.0f;
    public string hand;
    public GameObject exitMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(hand == "Right" && OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
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
        bool isGoConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote);
        bool isGoLeftyConnected = OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);
        bool isQuestConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTouch);

        if (isGoConnected || isGoLeftyConnected)
        {
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

            if (OVRInput.Get(OVRInput.Button.Back))
            {
                Vector3 pos = exitMenu.transform.position;
                pos.y = Camera.main.transform.position.y;
                exitMenu.transform.position = pos;
                exitMenu.SetActive(true);
                Hand.SetActive(true);
            }

        } else if (isQuestConnected) {
            if(hand == "Left")
            {
                return;
            }
            Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            if(OVRInput.Get(OVRInput.Touch.SecondaryThumbstick)) {
                if(input.y > 0.1){
                    scale += (1f + input.y) * Time.deltaTime;
                } else if(input.y < -0.1){
                    scale -= (1f - input.y) * Time.deltaTime;
                }
                scale = Mathf.Clamp(scale, minReachDistance, maxReachDistance);
            }
            if (OVRInput.Get(OVRInput.Button.Start))
            {
                Vector3 pos = exitMenu.transform.position;
                pos.y = Camera.main.transform.position.y;
                exitMenu.transform.position = pos;
                exitMenu.SetActive(true);
                Hand.SetActive(true);
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Vector3 pos = exitMenu.transform.position;
                pos.y = Camera.main.transform.position.y;
                exitMenu.transform.position = pos;
                exitMenu.SetActive(true);
                Hand.SetActive(true);
            }
        }
        IKTarget.transform.position = transform.position + scale * transform.forward;
    }
}
