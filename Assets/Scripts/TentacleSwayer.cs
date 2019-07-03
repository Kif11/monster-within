using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleSwayer : MonoBehaviour
{
    public Transform[] Joints;
    public Quaternion[] Rotations;
    public Vector3 magnitude;

    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer renderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        Joints = new Transform[renderer.bones.Length + 1];
        Rotations = new Quaternion[renderer.bones.Length + 1];

        Transform curBone = renderer.rootBone;
        int i = 0;
        while (curBone)
        {
            Transform joint = curBone.gameObject.transform;
            Joints[i] = joint;
            Rotations[i] = joint.transform.rotation;
            if (curBone.childCount > 0)
            {
                curBone = curBone.GetChild(0);
                i++;
            }
            else
            {
                curBone = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < Joints.Length; i++)
        {
            Quaternion rot = Joints[i].rotation;
            rot.z = (Rotations[i].z + magnitude.z * Mathf.Pow(i / 30f, 3) * Mathf.Sin(Time.fixedTime));
            rot.x = (Rotations[i].x + magnitude.x * Mathf.Pow(i / 30f, 3) * Mathf.Cos(0.5f*Time.fixedTime));
            Joints[i].rotation = rot;
        }
    }
}
