using UnityEngine;
using UnityEngine.SceneManagement;


public class EndingAnimator : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.Blink();
    }
}
