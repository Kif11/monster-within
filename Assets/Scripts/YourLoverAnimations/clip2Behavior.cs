﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clip2Behavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.Blink();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 pos = animator.gameObject.transform.position;
        pos.y += 0.005f * Mathf.Sin(Time.fixedTime);
        animator.gameObject.transform.position = pos;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.SetHumanMode();
    }
}