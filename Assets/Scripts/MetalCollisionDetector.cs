using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCollisionDetector : MonoBehaviour
{
    AudioSource audioSource;
    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with metal");
        audioSource.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
