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
        float mag = collision.relativeVelocity.magnitude;
        audioSource.volume = Mathf.Clamp(Mathf.Pow(mag, 2.0f),0.0f, 1.0f);
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
