﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdHitAnimator : StateMachineBehaviour
{
    private bool hit = false;
    private PostEffect postEffect;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        postEffect = Camera.main.GetComponent<PostEffect>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float curTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float fract = curTime - Mathf.Floor(curTime);
        if (fract > 0.5f && !hit)
        {
            AudioSource a = animator.gameObject.GetComponent<AudioSource>();
            a.Play();
            if (a.volume > 0)
            {
                postEffect.PostMat.SetFloat("_HitAmount", 1.0f);
            }
            hit = true;
        }

        if (fract < 0.1f)
        {
            hit = false;
        }
    }
}