using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentDebugging : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;

    public bool velocity;
    public bool desiredVelocity;
    public bool path;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    private void OnDrawGizmosSelected()
    {
        if(velocity)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + navAgent.velocity);
        }

        if(desiredVelocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + navAgent.desiredVelocity);
        }

        if(path)
        {
            Gizmos.color = Color.black;
            var agentPath = navAgent.path;
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners)
            {
                Gizmos.DrawLine(prevCorner, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                prevCorner = corner;
            }
        }
    }
}
