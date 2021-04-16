using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStrafeTargetBehavior : StateMachineBehaviour
{
    NpcController controller;
    float currentTimer;
    float startTimer;

    public int randInt;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.transform.GetComponentInParent<NpcController>();
        controller.navAgent.stoppingDistance = 0.5f;
        controller.navAgent.velocity = Vector3.zero;
        controller.navAgent.speed = controller.walkSpeed;

        //randInt = Random.Range(0, 2);
        startTimer = Random.Range(1f, 3f);
        currentTimer = startTimer;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer > 0)
        {
            controller.StrafeTarget(randInt);
        }
        else
        {
            animator.SetBool("strafeTarget", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.navAgent.destination = animator.transform.position;
        controller.navAgent.velocity = Vector3.zero;
    }
}
