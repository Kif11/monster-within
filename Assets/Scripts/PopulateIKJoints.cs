using UnityEditor;
using UnityEngine;

public class PopulateIKJoints
{

    //[MenuItem("Snay/Populate IKJoints")]
    //public static void Execute()
    //{
    //    GameObject[] iks;
    //    Vector3[] axes = new Vector3[2];
    //    axes[0] = new Vector3(1.0f, 0.0f, 0.0f);
    //    axes[1] = new Vector3(0.0f, 1.0f, 0.0f);


    //    iks = GameObject.FindGameObjectsWithTag("IKObject");
    //    foreach (GameObject ik in iks)
    //    {
    //        SkinnedMeshRenderer renderer = ik.GetComponent<SkinnedMeshRenderer>();
    //        Transform curBone = renderer.rootBone;
    //        int i = 0;
    //        while (curBone)
    //        {
    //            IKJoint[] joints = curBone.gameObject.GetComponents<IKJoint>();
    //            foreach (IKJoint joint1 in joints)
    //                Object.DestroyImmediate(joint1, true);

    //            IKJoint joint = curBone.gameObject.AddComponent<IKJoint>();
    //            joint.StartOffset = joint.transform.localPosition;
    //            joint.Axis = axes[i%2];
    //            joint.MinAngle = -45;
    //            joint.MaxAngle = 45;

    //            // add a box collider for every 4th joint 
    //            BoxCollider[] colliders = curBone.gameObject.GetComponents<BoxCollider>();
    //            foreach (BoxCollider collider1 in colliders)
    //                Object.DestroyImmediate(collider1, true);
    //            if (i % 4 == 0)
    //            {
    //                BoxCollider collider = curBone.gameObject.AddComponent<BoxCollider>();
    //                //collider.center = curBone.transform.position;
    //                collider.size = new Vector3(0.1f, 0.1f, 0.3f);
    //            }
    //            // add a rigid body for every 4th joint 
    //            Rigidbody[] rigidbodies = curBone.gameObject.GetComponents<Rigidbody>();
    //            foreach (Rigidbody rigidbody1 in rigidbodies)
    //                Object.DestroyImmediate(rigidbody1, true);
    //            if (i % 4 == 0)
    //            {
    //                Rigidbody rigidbody = curBone.gameObject.AddComponent<Rigidbody>();
    //                //collider.center = curBone.transform.position;
    //                rigidbody.isKinematic = true;
    //                rigidbody.useGravity = false;
    //            }

    //            if (curBone.childCount > 0)
    //            {
    //                curBone = curBone.GetChild(0);
    //                i++;
    //            }
    //            else
    //            {
    //                curBone = null;
    //            }
    //        }
    //    }
    //    EditorUtility.DisplayDialog(
    //        "Populated IKJoints for ",
    //        iks.Length + " IK game object in the scene",
    //        "OK");
    //}
}