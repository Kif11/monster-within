using UnityEngine;
using System.Collections;

public class TentacleRotator : MonoBehaviour
{
    private Quaternion targetRotation;
    public GameObject staticTentacles;
    // Use this for initialization
    void Start()
    {
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Abs(transform.eulerAngles.y - Camera.main.transform.eulerAngles.y);
        if(angle > 90f)
        {
            targetRotation = Quaternion.Euler(0.0f, Camera.main.transform.eulerAngles.y, 0.0f);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 0.80f);
        staticTentacles.transform.rotation = transform.rotation;
    }
}
