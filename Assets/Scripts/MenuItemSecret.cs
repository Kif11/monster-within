using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemSecret : MenuItemBasic
{
    public float secondsUntilActive = 10f;
    private bool clicked = false;

    public override void OnClick()
    {
        if (secondsFromClick < 0.5)
        {
            return;
        }

        outlineMat.SetFloat("_OutlineIntensity", 0f);
        //secret menu item is disabled for x seconds
        if(Time.fixedTime > secondsUntilActive && !clicked)
        {
            Debug.Log("activated");
            animator.SetInteger("menuItemID", animatorParam);
            audioSources[0].Play();
            clicked = true;
        }
        else
        {
            audioSources[1].Play();
        }
        secondsFromClick = 0.0f;
    }

    public override void Update()
    {
        secondsFromClick += Time.deltaTime;

        if (Time.fixedTime > secondsUntilActive && !clicked &&!hovering)
        {
            outlineMat.SetFloat("_OutlineIntensity", 0.5f + 0.5f * Mathf.Sin(2f * Time.fixedTime));
        }
    }
}
