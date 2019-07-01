using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRaycaster : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private MenuItem lastMenuItem;

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
        if (lastMenuItem)
        {
            lastMenuItem.OnUp();
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            lineRenderer.SetPosition(0, ray.origin + 0.2f * ray.direction);
            lineRenderer.SetPosition(1, ray.origin + ray.direction * hit.distance);

            MenuItem menuItem = hit.collider.gameObject.GetComponent<MenuItem>();
            lastMenuItem = menuItem;

            if (Input.GetMouseButton(0) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                //trigger action of menuItem
                menuItem.OnClick();
            }
            else
            {
                menuItem.OnHover();
            }
        }
        else 
        {
            lineRenderer.SetPosition(0, ray.origin + 0.2f * ray.direction);
            lineRenderer.SetPosition(1, ray.origin + ray.direction);
        }
    }
}
