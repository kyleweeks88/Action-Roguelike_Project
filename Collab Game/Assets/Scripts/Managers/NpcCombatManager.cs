 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCombatManager : CombatManager
{
    [Header("NPC Settings")]
    public float timeBetweenAttacks = 0.5f;
    float currentTimeBetweenAttacks;
    public float meleeAttackDistance = 1f;

    public override void Start()
    {
        base.Start();
        currentTimeBetweenAttacks = timeBetweenAttacks;
    }

    public void HandleAttackTimer()
    {
        currentTimeBetweenAttacks -= Time.deltaTime;
        if (currentTimeBetweenAttacks <= 0)
        {
            canRecieveAttackInput = true;
            currentTimeBetweenAttacks = timeBetweenAttacks;
        }
    }

    #region Ranged
    //public virtual void RangedAttackPerformed()
    //{
    //    if (!canRecieveAttackInput) { return; }

    //    rangedAttackHeld = true;
    //    attackAnim = "rangedAttackHold";
    //    animMgmt.HandleRangedAttackAnimation(rangedAttackHeld);
    //}

    //public virtual void ChargeRangedAttack()
    //{
    //    // CHECK IF CHARACTER HAS A RANGED WEAPON \\

    //    // ACCESS THAT RANGED WEAPON SOMEHOW \\

    //    // INCREASE THAT RANGED WEAPON'S DAMAGE AND/OR PROJECTILE VELOCITY \\

    //    Debug.Log("CHARGING RANGED ATTACK!");
    //}

    //// CALLED BY AN ANIMATION EVENT 
    //public virtual void CheckRangedAttack()
    //{
    //    // Ask the server to check your pos, and spawn a projectile for the server
    //    SpawnProjectile(projectileSpawn.position, projectileSpawn.rotation, this.transform.forward);
    //}

    //void SpawnProjectile(Vector3 pos, Quaternion rot, Vector3 dir)
    //{
    //    Projectile newProjectile = Instantiate(projectile,
    //        pos,
    //        rot);
    //    newProjectile.SetSpeed(20f, dir);
    //}
    #endregion

    #region Melee
    ///// <summary>
    ///// Called by the player's attack input.
    ///// </summary>
    //public virtual void AttackPerformed()
    //{
    //    // If the player is unable to recieve attack input, exit.
    //    if (!canRecieveAttackInput) { return; }

    //    inCombat = true;
    //    currentCombatTimer = combatTimer;
    //}

    //public virtual void ChargeMeleeAttack()
    //{
    //    // If you have a weapon equipped...
    //    if (equipmentMgmt.currentlyEquippedWeapon != null)
    //    {
    //        // If the current weapon is chargeable...
    //        if (equipmentMgmt.currentlyEquippedWeapon.weaponData.isChargeable)
    //        {
    //            // If the current weapon's charge is high enough, set the bool to true
    //            // to perform maxCharge special attack.
    //            if (charStats.currentAttackCharge >=
    //                charStats.maxAttackCharge)
    //            {
    //                animMgmt.myAnim.SetBool("maxAttackCharge", true);
    //            }

    //            if (charStats.currentAttackCharge <=
    //                charStats.maxAttackCharge)
    //            {
    //                charStats.currentAttackCharge +=
    //                    Time.deltaTime * charStats.attackChargeRate.value;
    //            }
    //            else
    //            {
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        // UNARMED CHARGING LOGIC
    //        if (charStats.currentAttackCharge >=
    //                charStats.maxAttackCharge)
    //        {
    //            animMgmt.myAnim.SetBool("maxAttackCharge", true);
    //        }

    //        if (charStats.currentAttackCharge <=
    //            charStats.maxAttackCharge)
    //        {
    //            charStats.currentAttackCharge +=
    //                Time.deltaTime * charStats.attackChargeRate.value;
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }

    //    // Makes the player move slower when charging an attack
    //    charStats.moveSpeed.AddModifer(charStats.combatMovementModifier);
    //}

    public override void ActivateImpact(int impactID)
    {
        //charStats.DamageStamina(10f);

        switch (impactID)
        {
            case 1:
                CreateImpactCollider(leftHand);
                break;
            case 2:
                CreateImpactCollider(rightHand);
                break;
            case 3:
                Debug.Log("Kick");
                break;
        }
        impactActivated = true;
    }

    public override void CreateImpactCollider(Transform impactTrans)
    {
        // Generate a collider array that will act as the weapon's collision area
        Collider[] impactCollisions = null;

        impactCollisions = Physics.OverlapSphere(impactTrans.position, 1f, whatIsDamageable);

        // for each object the collider hits do this stuff...
        foreach (Collider hit in impactCollisions)
        {
            // Create equippedWeapon's hit visuals
            GameObject hitVis = Instantiate(hitFX, hit.ClosestPoint(impactTrans.position), Quaternion.identity);
            CharacterStats hitStats = hit.gameObject.GetComponent<CharacterStats>();
            // If the collider hit has an NpcHealthManager component on it.
            if (hitStats != null)
            {
                hitStats.TakeDamage(this.gameObject, charStats.attackDamage.value);

                impactActivated = false;
                charStats.ResetAttackCharge();
            }
        }
    }

    /// <summary>
    /// Waits for the impactActivated bool to be triggered by an Animation Event. Grabs the entity's
    /// currently equipped weapon and creates an impact collider based on the weapons specs.
    /// </summary>
    public override void CheckMeleeAttack()
    {
        // NULL
    }

    //public void ProcessAttack(CharacterStats target)
    //{
    //    float dmgVal = charStats.attackDamage.value;

    //    target.TakeDamage(this.gameObject, dmgVal);
    //}
    #endregion
}
