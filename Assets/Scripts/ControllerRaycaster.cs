using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRaycaster : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = 1 << 9;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * hit.distance);
            lineRenderer.enabled = true;

            //Debug.DrawRay(ray.origin, ray.direction * 2.0f, Color.yellow);

            if (Input.GetMouseButton(0))
            {
                //trigger action of menuItem
                MenuItem menuItem = hit.collider.gameObject.GetComponent<MenuItem>();
                menuItem.OnClick();
            }
        }
        else 
        {
            lineRenderer.enabled = false;
        }
    }
}
