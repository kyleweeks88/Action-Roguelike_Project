using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCombatIdleBehavior : StateMachineBehaviour
{
    NpcCombatManager combatManager = null;
    NpcController npcCtrl = null;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatManager = animator.transform.GetComponentInParent<NpcCombatManager>();
        npcCtrl = animator.transform.GetComponentInParent<NpcController>();
        animator.SetBool("meleeAttackHold", false);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (npcCtrl.DistanceToTarget() <= combatManager.meleeAttackDistance)
        {
            // Handles a timer that upon reaching 0, flips canRecieveAttackInput to true!
            combatManager.HandleAttackTimer();

            if (combatManager.canRecieveAttackInput)
            {
                animator.SetBool("meleeAttackHold", true);
            }
        }

        // TIMER FOR COMBAT IDLE ANIMATION
        if (combatManager.inCombat && !npcCtrl.CanSeeTarget())
        {
            combatManager.HandleCombatTimer();
        }
        else
        {
            return;
        }
    }
}
