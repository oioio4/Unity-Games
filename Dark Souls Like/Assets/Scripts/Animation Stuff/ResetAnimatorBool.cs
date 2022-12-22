using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    public string isInteractingBool = "isInteracting";
    public bool isInteractingStatus = false;

    public string isFiringSpellBool = "isFiringSpell";
    public bool isFiringSpellStatus = false;

    public string isRotatingWithRootMotion = "isRotatingWithRootMotion";
    public bool isRotatingWithRootMotionStatus = false;

    public string canRotate = "canRotate";
    public bool canRotateStatus = true;

    public string isInvulnerable = "isInvulnerable";
    public bool isInvulnerableStatus = false;

    public string isUsingRightHand = "isUsingRightHand";
    public bool isUsingRightHandStatus = false;

    public string isUsingLeftHand = "isUsingLeftHand";
    public bool isUsingLeftHandStatus = false;

    //public string targetBool;
    //public bool status;
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool(isInteractingBool, isInteractingStatus);
        animator.SetBool(isFiringSpellBool, isFiringSpellStatus);
        animator.SetBool(isRotatingWithRootMotion, isRotatingWithRootMotionStatus);
        animator.SetBool(canRotate, canRotateStatus);
        animator.SetBool(isInvulnerable, isInvulnerableStatus);
        animator.SetBool(isUsingRightHand, isUsingRightHandStatus);
        animator.SetBool(isUsingLeftHand, isUsingLeftHandStatus);
    }
}
