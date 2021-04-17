using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public LayerMask whatIsTarget;
    NpcCombatManager combatMgmt;
    [SerializeField] Transform raycastPos;
    [HideInInspector] public NavMeshAgent navAgent;
    Animator animator;
    public GameObject target;

    public float walkSpeed;
    public float runSpeed;
    public float sprintSpeed;
    public float sightRange;
    float distanceToTarget;
    float setDestinationMaxTime = .01f;
    float setDestinationTimer;
    float distanceToTargetMaxTime = .1f;
    float distanceToTargetTimer;

    public bool targetInSight;
    public bool debug;

    /*==TESTING==>*/
    public float combatRadius;
    float orbitTimer = 4f;
    /*==TESTING==>*/

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combatMgmt = GetComponent<NpcCombatManager>();

        navAgent.speed = walkSpeed;
    }

    private void Update()
    {
        GetDistanceToTargetWithDelay();
        //DetermineSpeed();

        if (CanSeeTarget() && distanceToTarget > combatRadius)
        {
            animator.SetBool("meleeAttackHold", false);
            animator.SetBool("chasingTarget", true);
            animator.SetBool("strafeTarget", false);
            animator.SetBool("inCombat", false);
        }

        if(CanSeeTarget() && distanceToTarget <= combatRadius)
        {
            RotateTowardsTransform(target.transform);
            animator.SetBool("inCombat", true);
            animator.SetBool("chasingTarget", false);
        }

        if (!CanSeeTarget())
        {
            //animator.SetBool("inCombat", false);
            animator.SetBool("chasingTarget", false);
            animator.SetBool("strafeTarget", false);
            animator.SetBool("meleeAttackHold", false);
            navAgent.stoppingDistance = 1f;
        }

        animator.SetFloat("moveSpeed", navAgent.velocity.magnitude);
    }

    public void RotateTowardsTransform(Transform _target)
    {
        var dirVector = _target.transform.position - transform.position;
        float singleStep = 120f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, dirVector, singleStep, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void GetDistanceToTargetWithDelay()
    {
        distanceToTargetTimer -= Time.deltaTime;
        if(distanceToTargetTimer <= 0f)
        {
            DistanceToTarget();
            distanceToTargetTimer = distanceToTargetMaxTime;
        }
    }

    public float DistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        return distanceToTarget;
    }

    public bool CanSeeTarget()
    {
        if (distanceToTarget <= sightRange)
        {
            Ray sightRay = new Ray(raycastPos.position, -(raycastPos.position - target.GetComponent<CapsuleCollider>().bounds.center));
            RaycastHit hit;

            var dirVector = target.transform.position - transform.position;
            var lookPercentage = Vector3.Dot(transform.forward.normalized, dirVector.normalized);
            
            if (lookPercentage >= 0.4f)
            {
                if (Physics.Raycast(sightRay, out hit, sightRange, whatIsTarget))
                {
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

    public void SetDestinationWithDelay()
    {
        setDestinationTimer -= Time.deltaTime;
        if(setDestinationTimer < 0f)
        {
            if(distanceToTarget > navAgent.stoppingDistance)
            {
                navAgent.destination = target.transform.position;
            }

            if(distanceToTarget <= navAgent.stoppingDistance)
            {
                navAgent.velocity = Vector3.zero;
            }

            setDestinationTimer = setDestinationMaxTime;
        }
    }

    public void StrafeTarget(int dirInt)
    {
        var targetOffset = target.transform.position - this.transform.position;
        var dir = Vector3.Cross(targetOffset, Vector3.up);

        RotateTowardsTransform(target.transform);
        navAgent.angularSpeed = 0f;

        if (dirInt == 1)
        {
            navAgent.destination = (this.transform.position + dir);
        }
        
        if(dirInt == 0)
        {
            navAgent.destination = (this.transform.position - dir);
        }
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
