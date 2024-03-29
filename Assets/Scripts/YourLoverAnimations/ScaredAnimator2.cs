﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredAnimator2 : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.Blink();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
