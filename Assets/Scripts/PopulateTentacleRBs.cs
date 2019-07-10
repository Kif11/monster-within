using UnityEditor;
using UnityEngine;

public class PopulateTentacleRBs
{

    //[MenuItem("Snay/Populate Tentacle RBs")]
    //public static void Execute()
    //{
        //GameObject[] tentacles;
        //tentacles = GameObject.FindGameObjectsWithTag("Tentacle");
        //foreach (GameObject tentacle in tentacles)
        //{
        //    Transform curBone = tentacle.transform.GetChild(0);
        //    int i = 0;
        //    while (curBone)
        //    {
        //        // add a box collider for every 4th joint 
        //        BoxCollider[] colliders = curBone.gameObject.GetComponents<BoxCollider>();
        //        foreach (BoxCollider collider1 in colliders)
        //            Object.DestroyImmediate(collider1, true);
        //        if (i % 8 == 0)
        //        {
        //            BoxCollider collider = curBone.gameObject.AddComponent<BoxCollider>();
        //            //collider.center = curBone.transform.position;
        //            collider.size = new Vector3(0.1f, 0.1f, 0.6f);
        //            collider.enabled = false;
        //        }
        //        // add a rigid body for every 4th joint 
        //        Rigidbody[] rigidbodies = curBone.gameObject.GetComponents<Rigidbody>();
        //        foreach (Rigidbody rigidbody1 in rigidbodies)
        //            Object.DestroyImmediate(rigidbody1, true);
        //        if (i % 8 == 0)
        //        {
        //            Rigidbody rigidbody = curBone.gameObject.AddComponent<Rigidbody>();
        //            rigidbody.isKinematic = true;
        //            rigidbody.useGravity = false;
        //        }
        //        if (curBone.childCount > 0)
        //        {
        //            curBone = curBone.GetChild(0);
        //            i++;
        //        }
        //        else
        //        {
        //            curBone = null;
        //        }
        //    }
        //}
        //EditorUtility.DisplayDialog(
            //"Populated Rigid Bodies and Colliders for ",
            //tentacles.Length + " tentacle game object in the scene",
            //"OK");
    //}
}