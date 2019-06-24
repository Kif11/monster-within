using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItemAnimator : StateMachineBehaviour
{
    public GameObject prefab;
    public string MenuItemName;

    private Material menuMat;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(prefab, new Vector3(-0.507f, 1.34f, -2.73f), Quaternion.identity);
        Renderer r = GameObject.Find(MenuItemName).GetComponent<Renderer>();
        menuMat = r.material;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 pos = animator.gameObject.transform.position;
        pos.x = -2.0f + Mathf.Sin(Time.fixedTime);
        animator.gameObject.transform.position = pos;
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("menuItemID", -1);
        menuMat.SetFloat("_OutlineIntensity", 0.0f);

        ClipUtils clipUtils = animator.gameObject.GetComponent<ClipUtils>();
        clipUtils.SetHumanMode();
    }
}
