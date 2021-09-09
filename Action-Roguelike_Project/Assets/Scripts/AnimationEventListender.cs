using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListender : MonoBehaviour
{
    CombatManager combatManager;
    PlayerCombatManager pcm;

    private void Start()
    {
        combatManager = GetComponentInParent<CombatManager>();
        pcm = GetComponentInParent<PlayerCombatManager>();
    }

    public void SpecialAttack()
    {
        if(pcm != null)
            pcm.SpecialAttack();
    }

    public void ActivateImpact(int handInt)
    {
        combatManager.ActivateImpact(handInt);
    }

    public void DeactivateImpact()
    {
        combatManager.impactActivated = false;
    }

    public void CheckRangedAttack()
    {
        combatManager.Shoot();
    }
}
