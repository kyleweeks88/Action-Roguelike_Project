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

    public int soulWorth;

    public override void Start()
    {
        base.Start();

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public override void Death()
    {
        base.Death();

        SpawnSouls();
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

    public void SpawnSouls()
    {
        for (int i = 0; i < soulWorth; i++)
        {
            GameObject newSoul = Instantiate(Resources.Load("Soul Point"), transform.position, Quaternion.identity) as GameObject;

            newSoul.GetComponent<Rigidbody>().AddForce(newSoul.transform.up * 4.5f, ForceMode.Impulse);
            newSoul.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5,5),0,Random.Range(-5,5)).normalized * 1f, ForceMode.Impulse);
        }
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
