using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrowdBehavior : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.SetMonsterMode();

        GameObject crowd = GameObject.Find("Crowd");
        CrowdSimulator cs = crowd.GetComponent<CrowdSimulator>();
        cs.enabled = true;

        GameObject tableSet = GameObject.Find("TableSetMain");
        Rigidbody rb = tableSet.AddComponent<Rigidbody>();
        rb.AddForce(new Vector3(-40.0f, 5.0f, 0.0f), ForceMode.Impulse);

        GameObject player = GameObject.Find("Player");
        Vector3 pos = player.transform.position;
        pos.y = 1.077f;
        clipUtils.StartMovePlayerTowards(player, pos);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("Dead");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
