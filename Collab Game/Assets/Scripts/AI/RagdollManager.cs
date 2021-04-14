using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;

    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        DeactivateRagdoll();
    }

    public void ActivateRagdoll()
    {
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.gameObject.GetComponent<Collider>().enabled = true;
        }
        animator.enabled = false;
    }

    public void DeactivateRagdoll()
    {
        foreach (var rigidbody in rigidBodies)
        {
            rigidbody.isKinematic = true;
            rigidbody.gameObject.GetComponent<Collider>().enabled = false;
        }
        animator.enabled = true;
    }
}
