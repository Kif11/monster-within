using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public IKJoint[] Joints;
    public float[] angles;
    public float SamplingDistance = .5f;
    public float LearningRate = .8f;
    public float DistanceThreshold = 0.05f;
    public GameObject IKTarget;
    public GameObject actualEndPoint;
    public float InitialOffset = 150.0f;

    void Start()
    {
        SkinnedMeshRenderer renderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        Joints = new IKJoint[renderer.bones.Length + 1];
        angles = new float[renderer.bones.Length + 1];

        Transform curBone = renderer.rootBone;
        int i = 0;
        while (curBone)
        {
            IKJoint joint = curBone.gameObject.GetComponent<IKJoint>();
            Joints[i] = joint;
            Joints[i].StartOffset = joint.transform.lossyScale.x * joint.transform.localPosition;
            angles[i] = 0.0f;
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
        angles[1] = InitialOffset;
        Joints[1].MaxAngle = InitialOffset;
    }


    public (Vector3, Quaternion) ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = Joints[0].transform.position;
        Quaternion rotation = Joints[0].transform.rotation;
        for (int i = 1; i < Joints.Length; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], Joints[i - 1].Axis);
            Vector3 nextPoint = prevPoint + rotation * Joints[i].StartOffset;

            prevPoint = nextPoint;
        }
        return (prevPoint, rotation);
    }

    public float DistanceFromTarget(Vector3 target, float[] angles)
    {
        (Vector3 endPoint, Quaternion endEffectorRotation) = ForwardKinematics(angles);

        float distancePenalty = Vector3.Distance(endPoint, target);
        float rotationPenalty =
        Mathf.Abs( Quaternion.Angle(endEffectorRotation, actualEndPoint.transform.rotation) / 360f );
            
        float torsionPenalty = 0;
        for (int i = 28; i < angles.Length; i+=1)
            torsionPenalty += 1.0f/Mathf.Abs(angles[i]);
        torsionPenalty = Mathf.Clamp(torsionPenalty, 0.0f, 1.0f);

        //float finalError = rotationPenalty + distancePenalty + torsionPenalty;
        float finalError = distancePenalty + 0.06f*torsionPenalty;
        return finalError;
    }
    public float PartialGradient(Vector3 target, float[] angles, int i)
    {
        // Saves the angle,
        // it will be restored later
        float angle = angles[i];

        // Gradient : [F(x+SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget(target, angles);

        angles[i] += SamplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);

        float gradient = (f_x_plus_d - f_x) / SamplingDistance;
        // Restores
        angles[i] = angle;

        return gradient;
    }

    public void InverseKinematics(Vector3 target, float[] angles)
    {
        for (int it = 0; it < 2; it++)
        {
            float dist = DistanceFromTarget(target, angles);
            if (dist < DistanceThreshold)
                return;

            for (int i = Joints.Length - 1; i >= 0; i--)
            {
                // Gradient descent
                // Update : Solution -= LearningRate * Gradient
                float gradient = PartialGradient(target, angles, i);
                angles[i] -= (Mathf.Max(Mathf.Pow(i / 4, 3), Joints[i].Weight) * LearningRate * gradient * (1.0f + dist));
                angles[i] = Mathf.Clamp(angles[i], Joints[i].MinAngle, Joints[i].MaxAngle);
                if (DistanceFromTarget(target, angles) < DistanceThreshold)
                    return;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Joints[0].transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        //Vector3 rot = Joints[0].transform.localRotation.eulerAngles;
        //rot = new Vector3(-rot.x, rot.y, rot.z);
        //Joints[0].transform.localRotation = Quaternion.Euler(rot);

        //Joints[0].transform.localRotation = Camera.main.transform.rotation;
        //angles[0] = 10.0f * Mathf.Sin(Time.fixedTime); //TODO come back to this
        angles[2] = 5.0f * Mathf.Sin(Time.fixedTime);
 

        InverseKinematics(IKTarget.transform.position, angles);
        for (int i = 0; i < Joints.Length; i++)
        {
            Joints[i].transform.localEulerAngles = angles[i] * Joints[i].Axis;
        }
        var point = ForwardKinematics(angles);
        actualEndPoint.transform.position = point.Item1;
    }
}
