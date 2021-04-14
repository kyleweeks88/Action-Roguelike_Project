using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] Transform raycastPos;
    NavMeshAgent navAgent;
    Animator animator;
    GameObject player;

    public float walkSpeed;
    public float runSpeed;
    public float sprintSpeed;
    public float sightRange;
    public float maxTime = .5f;
    float timer;

    public bool targetInSight;
    public bool debug;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        navAgent.speed = walkSpeed;
    }

    private void Update()
    {
        CanSeeTarget();
        DetermineSpeed();
        //StrafePlayer();
        
        animator.SetFloat("moveSpeed", navAgent.velocity.magnitude);
    }

    public float DistanceToTarget()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        return distance;
    }

    bool CanSeeTarget()
    {
        bool boolVal = false;
        targetInSight = boolVal;

        if (DistanceToTarget() <= sightRange)
        {
            Ray sightRay = new Ray(raycastPos.position, -(raycastPos.position - player.GetComponent<CapsuleCollider>().bounds.center));
            RaycastHit hit;

            var dirVector = player.transform.position - transform.position;
            var lookPercentage = Vector3.Dot(transform.forward.normalized, dirVector.normalized);
            
            if (lookPercentage >= 0.5f)
            {
                if (Physics.Raycast(sightRay, out hit, sightRange))
                {
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        float singleStep = navAgent.angularSpeed * Time.deltaTime;
                        Vector3 newDir = Vector3.RotateTowards(transform.forward, dirVector, singleStep, 0f);
                        transform.rotation = Quaternion.LookRotation(newDir);
                        SetDestinationWithDelay();
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
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            if(DistanceToTarget() > navAgent.stoppingDistance)
            {
                navAgent.destination = player.transform.position;
            }
            timer = maxTime;
        }
    }

    void StrafePlayer()
    {
        if(DistanceToTarget() <= 4f)
        {
            Vector3 dir = (transform.position - player.transform.position).normalized;
            float angle = Vector3.Angle(dir, player.transform.forward);
            
            if(angle < 60f)
            {
                navAgent.isStopped = true;
            }
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

    void DetermineSpeed()
    {
        if (targetInSight)
        {
            if (DistanceToTarget() >= sprintSpeed)
            {
                navAgent.speed = sprintSpeed;
            }
            else if (DistanceToTarget() >= runSpeed)
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

    private void OnDrawGizmosSelected()
    {
        if(debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}
