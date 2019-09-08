using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRaycaster : MonoBehaviour
{
    private MenuItemBasic lastMenuItem;

    void Start()
    {
    }

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
            MenuItemBasic menuItem = hit.collider.gameObject.GetComponent<MenuItemBasic>();
            lastMenuItem = menuItem;

            if (Input.GetMouseButton(0) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                menuItem.OnClick();
            }
            else
            {
                menuItem.OnHover();
            }
        }
    }
}
