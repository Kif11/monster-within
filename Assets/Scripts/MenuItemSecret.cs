using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemSecret : MenuItemBasic
{
    public float secondsUntilActive = 10f;
    private bool clicked = false;

    public override void OnClick()
    {
        outlineMat.SetFloat("_OutlineIntensity", 0f);
        //secret menu item is disabled for 5 seconds
        if(Time.fixedTime > secondsUntilActive && !clicked)
        {
            animator.SetInteger("menuItemID", animatorParam);
            audioSources[0].Play();
            clicked = true;
        }
        else
        {
            audioSources[1].Play();
        }
    }

    public override void Update()
    {
        if (Time.fixedTime > secondsUntilActive && !clicked &&!hovering)
        {
            outlineMat.SetFloat("_OutlineIntensity", 0.5f + 0.5f * Mathf.Sin(2f * Time.fixedTime));
        }
    }
}
