using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKJoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Axis;
    public Vector3 StartOffset;
    public float MinAngle;
    public float MaxAngle;
    public float Weight;

    void Awake()
    {
        StartOffset = transform.lossyScale.x * transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

