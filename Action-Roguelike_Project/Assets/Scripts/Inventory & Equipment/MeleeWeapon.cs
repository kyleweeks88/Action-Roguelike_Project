using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Specs")]
    public Transform impactOrigin;
    public Transform impactEnd;
    public float impactRadius;


    /// SHOULD I CREATE THE IMPACT COLLIDER HERE ON THE WEAPON??? ///
    public void CreateImpactCollider(CombatManager _combatMgmt)
    {
        // Generate a collider array that will act as the weapon's collision area
        Collider[] impactCollisions = null;

        impactCollisions = Physics.OverlapCapsule(
            impactOrigin.position,
            impactEnd.position,
            impactRadius, _combatMgmt.whatIsDamageable);

        // for each object the collider hits do this stuff...
        foreach (Collider hit in impactCollisions)
        {
            // Create equippedWeapon's hit visuals
            InstantiateHitVisuals(hit.ClosestPoint(impactEnd.position));

            // If the collider hit has an NpcHealthManager component on it.
            if (hit.gameObject.GetComponent<CharacterStats>() != null)
            {
                _combatMgmt.ProcessAttack(hit.gameObject.GetComponent<CharacterStats>());
                _combatMgmt.impactActivated = false;
                base.ResetCharge();
            }

            AffectDurability();
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(impactOrigin.position, impactEnd.position);
    }
}
