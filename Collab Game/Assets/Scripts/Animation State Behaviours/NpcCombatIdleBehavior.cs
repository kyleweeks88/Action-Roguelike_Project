using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCombatIdleBehavior : StateMachineBehaviour
{
    NpcCombatManager combatManager = null;
    NpcController npcCtrl = null;
    int randInt;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatManager = animator.transform.GetComponentInParent<NpcCombatManager>();
        npcCtrl = animator.transform.GetComponentInParent<NpcController>();
        npcCtrl.navAgent.velocity = Vector3.zero;
        npcCtrl.navAgent.angularSpeed = 240;
        npcCtrl.navAgent.stoppingDistance = combatManager.meleeAttackDistance;
        animator.SetBool("meleeAttackHold", false);

        randInt = Random.Range(0, 3);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (npcCtrl.CanSeeTarget())
        {
            npcCtrl.SetDestinationWithDelay();
            if (npcCtrl.DistanceToTarget() <= combatManager.meleeAttackDistance)
            {
                if (randInt > 0)
                {
                    // Handles a timer that upon reaching 0, flips canRecieveAttackInput to true!
                    combatManager.HandleAttackTimer();

                    if (combatManager.canRecieveAttackInput)
                    {
                        animator.SetBool("meleeAttackHold", true);
                    }
                }
                
                if(randInt == 0)
                {
                    int dirInt = Random.Range(0, 2);
                    animator.SetInteger("dirInt", dirInt);
                    animator.SetBool("strafeTarget", true);
                }
            }
        }
        else
        {
            combatManager.HandleCombatTimer();
        }
    }
}
