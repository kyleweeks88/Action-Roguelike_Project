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
    float setDestinationMaxTime = .1f;
    float setDestinationTimer;
    float distanceToTargetMaxTime = .1f;
    float distanceToTargetTimer;

    public bool targetInSight;
    public bool debug;

    float orbitTimer = 4f;

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
        //DetermineSpeed();

        if (CanSeeTarget() && DistanceToTarget() > 3f)
        {
            combatMgmt.inCombat = true;
            navAgent.speed = runSpeed;
            navAgent.stoppingDistance = combatMgmt.meleeAttackDistance;

            RotateTowardsTransform(target.transform);
            SetDestinationWithDelay();

            animator.SetBool("meleeAttackHold", false);
        }

        if(CanSeeTarget() && DistanceToTarget() <= 3f)
        {
            //OrbitTarget();
            StrafeTarget();
            navAgent.speed = walkSpeed;
            combatMgmt.inCombat = true;
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
            else
            {
                navAgent.velocity = Vector3.zero;
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
                else if (distanceToTarget > combatMgmt.meleeAttackDistance + 2f && distanceToTarget < sprintSpeed)
                {
                    navAgent.speed = runSpeed;
                }
                else if(distanceToTarget <= combatMgmt.meleeAttackDistance + 1)
                {
                    navAgent.speed = walkSpeed;
                }
            }
            else
            {
                navAgent.speed = walkSpeed;
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
        // THIS GETS THE POSITION OF THIS NPC IN RELATION TO IT'S TARGET
        Vector3 dir = (transform.position - target.transform.position).normalized;
        float angle = Vector3.Angle(dir, target.transform.forward);
            
        // IF THE ANGLE IS LESS THAN 60deg THAN THIS NPC IS IN FRONT OF THE TARGET
        if(angle < 160f)
        {
            OrbitTarget();
        }
        // ... OTHERWISE THIS NPC IS BEHIND IT'S TARGET
        else
        {
            navAgent.velocity = Vector3.zero;
        }
    }

    void OrbitTarget()
    {
        var targetOffset = target.transform.position - this.transform.position;
        var dir = Vector3.Cross(targetOffset, Vector3.up);
        RotateTowardsTransform(target.transform);
        navAgent.angularSpeed = 0f;

        int randInt = 1;
        orbitTimer -= Time.deltaTime;
        if(orbitTimer <= 0)
        {
            randInt = Random.Range(0, 2);

            if (randInt == 1)
            {
                navAgent.destination = (this.transform.position + dir);
            }
            else
            {
                navAgent.destination = (this.transform.position - dir);
            }

            orbitTimer = 4;
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
