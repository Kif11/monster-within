using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordMover : MonoBehaviour
{
    public float mouseSpeed = 2.0f;
    public float moveSpeed = 2.0f;

    void Start()
    {
    }

    void move(Vector3 direction) {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void Update()
    {
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
