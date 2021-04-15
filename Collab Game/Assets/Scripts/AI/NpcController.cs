using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    NpcCombatManager combatMgmt;
    [SerializeField] Transform raycastPos;
    NavMeshAgent navAgent;
    Animator animator;
    GameObject target;

    public float walkSpeed;
    public float runSpeed;
    public float sprintSpeed;
    public float sightRange;
    float distanceToTarget;
    float setDestinationMaxTime = .25f;
    float setDestinationTimer;
    float distanceToTargetMaxTime = .25f;
    float distanceToTargetTimer;

    public bool targetInSight;
    public bool debug;


    private void Awake()
    {
/*==TESTING==>*/target = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combatMgmt = GetComponent<NpcCombatManager>();

        navAgent.speed = walkSpeed;
    }

    private void Update()
    {
        GetDistanceToTargetWithDelay();
        DetermineSpeed();

        if (CanSeeTarget() && distanceToTarget > combatMgmt.meleeAttackDistance)
        {
            combatMgmt.inCombat = true;
            navAgent.stoppingDistance = 3f;

            RotateTowardsTransform(target.transform);
            SetDestinationWithDelay();

            animator.SetBool("meleeAttackHold", false);
        }

        if(CanSeeTarget() && distanceToTarget <= combatMgmt.meleeAttackDistance)
        {
            combatMgmt.inCombat = true;
            RotateTowardsTransform(target.transform);
        }

        if(!CanSeeTarget())
        {
            combatMgmt.inCombat = false;
            animator.SetBool("meleeAttackHold", false);
            navAgent.stoppingDistance = 1f;
        }

        animator.SetFloat("moveSpeed", navAgent.velocity.magnitude);
        animator.SetBool("inCombat", combatMgmt.inCombat);
    }

    void RotateTowardsTransform(Transform _target)
    {
        var dirVector = _target.transform.position - transform.position;
        float singleStep = navAgent.angularSpeed * Time.deltaTime;
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
        bool boolVal = false;
        targetInSight = boolVal;

        if (distanceToTarget <= sightRange)
        {
            Ray sightRay = new Ray(raycastPos.position, -(raycastPos.position - target.GetComponent<CapsuleCollider>().bounds.center));
            RaycastHit hit;

            var dirVector = target.transform.position - transform.position;
            var lookPercentage = Vector3.Dot(transform.forward.normalized, dirVector.normalized);
            
            if (lookPercentage >= 0.5f)
            {
                if (Physics.Raycast(sightRay, out hit, sightRange))
                {
                    if (hit.transform.gameObject.tag == "Player")
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
        }
        else
        {
            navAgent.destination = transform.position;
            targetInSight = false;
            return targetInSight;
        }

        return targetInSight;
    }

    void SetDestinationWithDelay()
    {
        setDestinationTimer -= Time.deltaTime;
        if(setDestinationTimer < 0f)
        {
            if(distanceToTarget > navAgent.stoppingDistance)
            {
                navAgent.destination = target.transform.position;
            }
            setDestinationTimer = setDestinationMaxTime;
        }
    }

    void DetermineSpeed()
    {
        if (combatMgmt.inCombat)
        {
            if (CanSeeTarget())
            {
                if (distanceToTarget >= sprintSpeed)
                {
                    navAgent.speed = sprintSpeed;
                }
                else if (distanceToTarget >= runSpeed)
                {
                    navAgent.speed = runSpeed;
                }
                else
                {
                    navAgent.speed = runSpeed;
                }
            }
            else
            {
                navAgent.speed = runSpeed;
            }
        }
        else
        {
            if (CanSeeTarget())
            {
                if (distanceToTarget >= sprintSpeed)
                {
                    navAgent.speed = sprintSpeed;
                }
                else if (distanceToTarget >= runSpeed)
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
    }

    // TESTING THIS
    void StrafeTarget()
    {
        if(distanceToTarget <= 4f)
        {
            // THIS GETS THE POSITION OF THIS NPC IN RELATION TO IT'S TARGET
            Vector3 dir = (transform.position - target.transform.position).normalized;
            float angle = Vector3.Angle(dir, target.transform.forward);
            
            // IF THE ANGLE IS LESS THAN 60deg THAN THIS NPC IS IN FRONT OF THE TARGET
            if(angle < 60f)
            {
                navAgent.isStopped = true;
            }
            // ... OTHERWISE THIS NPC IS BEHIND IT'S TARGET
            else
            {
                navAgent.isStopped = false;
            }
        }
        else
        {
            navAgent.isStopped = false;
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
