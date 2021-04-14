using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcStats : CharacterStats
{
    public override void Death()
    {
        base.Death();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<NpcController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<RagdollManager>().ActivateRagdoll();
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
