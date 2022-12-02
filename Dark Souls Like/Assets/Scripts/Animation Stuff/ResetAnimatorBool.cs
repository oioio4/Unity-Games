using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    //public string isInteractingBool = "isInteracting";
    //public bool isInteractingStatus = false;

    //public string isFiringSpellBool = "isFiringSpell";
    //public bool isFiringSpellStatus = false;

    public string targetBool;
    public bool status;
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool(targetBool, status);
    }
}
