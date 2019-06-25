using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItemAnimator : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private bool hasEaten;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasEaten = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
        {
            animator.SetInteger("menuItemID", -1);
        }
        if(!hasEaten && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3)
        {
            //remove a sushi game object
            GameObject sushiToRemove = (UnityEngine.GameObject)SushiGlobalData.sushiQueue.Dequeue();
            Destroy(sushiToRemove);
            hasEaten = true;
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.SetHumanMode();
    }

}
