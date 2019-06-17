using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    Material tentacleMat;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] iks = GameObject.FindGameObjectsWithTag("IKObject");
        tentacleMat = iks[0].GetComponent<Renderer>().material;
    }
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        tentacleMat.SetVector("_ContactPoint", contact.point);
        tentacleMat.SetFloat("_TimeOfContact", Time.time);
    }

    private void OnCollisionExit(Collision collision)
    {
        tentacleMat.SetVector("_ContactPoint", new Vector4(100.0f, 100.0f, 100.0f, 100.0f));
    }

    // Update is called once per frame
    void Update()
    {
        tentacleMat.SetFloat("_GameTime", Time.time);

    }
}
