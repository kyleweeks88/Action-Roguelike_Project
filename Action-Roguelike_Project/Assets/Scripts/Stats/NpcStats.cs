using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcStats : CharacterStats
{
    SkinnedMeshRenderer skinnedMeshRenderer;

    float blinkIntensity = 10f;
    float blinkDuration = 0.1f;
    float blinkTimer;

    public override void Start()
    {
        base.Start();

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public override void Death()
    {
        base.Death();

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<NpcController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponentInChildren<HealthbarDisplay>().gameObject.SetActive(false);
        GetComponent<RagdollManager>().ActivateRagdoll();
        StartCoroutine(DelayedDestroy());
    }

    public override void TakeDamage(GameObject attacker, float dmgVal)
    {
        blinkTimer = blinkDuration;
        StartCoroutine(BlinkTimer());
        base.TakeDamage(attacker, dmgVal);
    }

    IEnumerator BlinkTimer()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while (blinkTimer > 0)
        {
            blinkTimer -= Time.deltaTime;
            float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
            float intensity = (lerp * blinkIntensity) + 1f;
            skinnedMeshRenderer.material.color = Color.white * intensity;
            yield return wait;
        }
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
