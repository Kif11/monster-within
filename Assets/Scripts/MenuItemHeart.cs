using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemHeart : MenuItem
{
    public override void OnClick()
    {
        if (secondsFromClick < 0.5)
        {
            return;
        }
        outlineMat.SetFloat("_OutlineIntensity", 1f);
        animator.SetInteger("menuItemID", animatorParam);
        secondsFromClick = 0.0f;

        //play click sound
        audioSources[0].Play();

        animator.SetInteger("menuItemID", animatorParam);
    }
}
