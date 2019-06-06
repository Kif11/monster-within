using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordMover : MonoBehaviour
{
    public float mouseSpeed = 2.0f;
    public float moveSpeed = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void move(Vector3 direction) {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void Update()
    {
        //yaw += mouseSpeed * Input.GetAxis("Mouse X");
        //pitch -= mouseSpeed * Input.GetAxis("Mouse Y");

        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKey(KeyCode.W))
            move(transform.forward);

        if (Input.GetKey(KeyCode.S))
            move(-transform.forward);

        if (Input.GetKey(KeyCode.D))
            move(transform.right);

        if (Input.GetKey(KeyCode.A))
            move(-transform.right);

    }
}
