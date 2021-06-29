using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HeavyAttackBehaviour : StateMachineBehaviour
{

    [Range(0, 1)]
    public float timeToPlayNextAnim;
    public string animString;
    [SerializeField]
    private bool canDoNextAttack;

    PlayerController pc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponentInParent<PlayerController>();
        pc.pm.cgm.StopMoving();
        canDoNextAttack = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!canDoNextAttack) {
            if (pc.IsPressingHeavyAttack()) {
                canDoNextAttack = true;
            }
        }
        if (canDoNextAttack && stateInfo.normalizedTime > timeToPlayNextAnim)
        {
            animator.Play(animString);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
