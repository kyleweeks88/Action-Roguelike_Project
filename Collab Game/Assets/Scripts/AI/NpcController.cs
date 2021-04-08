using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent navAgent;

    public float sightRange;

    public bool targetInSight;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        navAgent.destination = player.transform.position;
    }
}
