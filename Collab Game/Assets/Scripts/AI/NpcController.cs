using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] Animator animator;
    [SerializeField] Transform raycastPos;

    public float walkSpeed;
    public float runSpeed;
    public float sprintSpeed;
    public float sightRange;

    public bool targetInSight;
    public bool debug;

    float distToPlayer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent.speed = walkSpeed;
    }

    private void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        CanSeeTarget();

        DetermineSpeed();

        animator.SetFloat("moveSpeed", navAgent.velocity.magnitude);
    }

    bool CanSeeTarget()
    {
        bool boolVal = false;
        targetInSight = boolVal;

        if (distToPlayer <= sightRange)
        {
            Ray sightRay = new Ray(raycastPos.position, -(raycastPos.position - player.GetComponent<CapsuleCollider>().bounds.center));
            RaycastHit hit;
            if(Physics.Raycast(sightRay, out hit, sightRange))
            {
                if(hit.transform.gameObject.tag == "Player")
                {
                    navAgent.destination = player.transform.position;
                    targetInSight = true;
                    return targetInSight;
                }
                else
                {
                    navAgent.destination = transform.position;
                    targetInSight = false;
                    return targetInSight;
                }
            }
        }
        else
        {
            navAgent.destination = transform.position;
            targetInSight = false;
            return targetInSight;
        }

        return targetInSight;
    }

    void DetermineSpeed()
    {
        if (targetInSight)
        {
            if (distToPlayer >= sprintSpeed)
            {
                navAgent.speed = sprintSpeed;
            }
            else if (distToPlayer >= runSpeed)
            {
                navAgent.speed = runSpeed;
            }
            else
            {
                navAgent.speed = walkSpeed;
            }
        }
        else
        {
            navAgent.speed = walkSpeed;
        }
    }

    IEnumerator SearchForTarget()
    {
        yield return new WaitForSeconds(.25f);

        
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        if(debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}
