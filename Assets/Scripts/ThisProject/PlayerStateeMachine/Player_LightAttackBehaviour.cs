using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LightAttackBehaviour : StateMachineBehaviour
{
    [Range(0,1)]
    public float timeToPlayNextAnim;
    public string animString;
    private bool canDoNextAttack;
    //public float delayToDectectInput = 0.1f;
    //private float currentDealyTimer;

    PlayerController pc;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponentInParent<PlayerController>();
        pc.cm.StopMoving();
        canDoNextAttack = false;
        //currentDealyTimer = delayToDectectInput;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.LightAttackInput();
       
        if (pc.cbv.isLightAttacking)
        {
            canDoNextAttack = true;
        }
        if (canDoNextAttack && stateInfo.normalizedTime > timeToPlayNextAnim)
        {
            animator.Play(animString);
        }

        //if (currentDealyTimer > 0)
        //{
        //    currentDealyTimer -= Time.deltaTime;
        //}
        //else
        //{
        //    if (pc.cbv.isLightAttacking)
        //    {
        //        canDoNextAttack = true;
        //    }
        //    if (canDoNextAttack && stateInfo.normalizedTime > timeToPlayNextAnim)
        //    {
        //        animator.Play(animString);
        //    }
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //if (!canDoNextAttack)
        //{
        //    PlayerController.i.playerState = PlayerState.Idle;
        //}
        //else
        //{
        //    PlayerController.i.playerState = PlayerState.LightAttack;
        //}
        
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
