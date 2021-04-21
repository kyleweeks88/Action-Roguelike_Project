using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackHandler_asb : StateMachineBehaviour
{
    public string specialAttackName;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger(specialAttackName);
        animator.SetBool("specialAttackActive", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("specialAttackActive", false);
        animator.SetBool("maxAttackCharge", false);
    }
}
