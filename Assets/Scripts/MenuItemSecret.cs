using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemSecret : MenuItem
{
    public override void OnClick()
    {
        if(secondsFromClick < 0.5)
        {
            return;
        }
        outlineMat.SetFloat("_OutlineIntensity", 2.5f);
        animator.SetInteger("menuItemID", animatorParam);
        secondsFromClick = 0.0f;

        //play click sound
        audioSource.Play();

        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.SetMonsterMode();

    }
}
