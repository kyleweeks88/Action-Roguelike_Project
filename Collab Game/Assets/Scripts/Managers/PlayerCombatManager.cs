using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CombatManager
{
    [Header("Player Component Ref")]
    [SerializeField] PlayerManager playerMgmt;

    public override void Start()
    {
        base.Start();

        playerMgmt.inputMgmt.attackEventStarted += AttackPerformed;
        playerMgmt.inputMgmt.attackEventCancelled += AttackReleased;
        playerMgmt.inputMgmt.rangedAttackEventStarted += RangedAttackPerformed;
        playerMgmt.inputMgmt.rangedAttackEventCancelled += RangedAttackReleased;
        playerMgmt.inputMgmt.blockEventStarted += BlockPerformed;
        playerMgmt.inputMgmt.blockEventCancelled += BlockReleased;
    }

    void BlockPerformed()
    {
        base.Blocking();
    }

    void BlockReleased() => isBlocking = false;

    #region Ranged
    public override void RangedAttackPerformed()
    {
        // if player is locked into an "interacting" state then don't let this happen.
        if (playerMgmt.isInteracting) { return; }

        base.RangedAttackPerformed();
    }

    public void RangedAttackReleased()
    {
        rangedAttackHeld = false;
        playerMgmt.animMgmt.HandleRangedAttackAnimation(rangedAttackHeld);
    }

    // CALLED BY AN ANIMATION EVENT 
    public override void CheckRangedAttack()
    {
        // Ask the server to check your pos, and spawn a projectile for the server
        SpawnProjectile(projectileSpawn.position, projectileSpawn.rotation, playerMgmt.myCamera.transform.forward);
    }

    void SpawnProjectile(Vector3 pos, Quaternion rot, Vector3 dir)
    {
        Projectile newProjectile = Instantiate(projectile,
            pos,
            rot);
        newProjectile.SetSpeed(20f, dir);
    }
    #endregion

    #region Melee
    /// <summary>
    /// Called by the player's attack input.
    /// </summary>
    public override void AttackPerformed()
    {
        // If the player is interacting with a contextual object, exit.
        if (playerMgmt.isInteracting) { return; }

        base.AttackPerformed();

        if (canRecieveAttackInput)
        {
            attackInputHeld = true;

//TEST============>// IF THE PLAYER IS IN THE AIR AND LOOKING DOWNWARDS!!!
            if (!playerMgmt.playerMovement.isGrounded)
            {
                if (Vector3.Dot(playerMgmt.myCamera.transform.forward, Vector3.up) <= -0.65f)
                {
                    playerMgmt.myRb.AddForce(playerMgmt.myCamera.transform.forward * 75f, ForceMode.Impulse);
                }
            }

            // If you have a weapon equipped
            if (playerMgmt.equipmentMgmt.currentlyEquippedWeapon != null)
            {
                // If current weapon is a melee type...
                if (playerMgmt.equipmentMgmt.currentlyEquippedWeapon.weaponData.weaponType == WeaponData.WeaponType.Melee)
                {
                    MeleeWeapon myWeapon = playerMgmt.equipmentMgmt.currentlyEquippedWeapon as MeleeWeapon;
                    // Checks if the entity has enough stamina to do an attack...
                    if ((playerMgmt.playerStats.GetCurrentStamina() - myWeapon.meleeData.staminaCost) > 0)
                    {
                        // If the weapon is a chargeable weapon...
                        if (myWeapon.meleeData.isChargeable)
                        {
                            attackAnim = "meleeAttackHold";
                            playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
                        }
                        // If the weapon is a rapid attack weapon...
                        else
                        {
                            attackAnim = "meleeAttackTrigger";
                        }
                    }

                }
            }
            // If you have no equipped weapon, you're unarmed
            else
            {
                if (playerMgmt.playerStats.GetCurrentStamina() - 10f > 0)
                {
                    attackAnim = "meleeAttackHold";
                    playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
                }
            }
        }
    }

    /// <summary>
    /// Called by the player releasing attack input.
    /// </summary>
    void AttackReleased()
    {
        attackInputHeld = false;
        playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);

        playerMgmt.playerStats.ResetAttackCharge();

        playerMgmt.playerStats.moveSpeed.RemoveModifier(playerMgmt.playerStats.combatMovementModifier);
    }

    public void SpecialAttack()
    {
        playerMgmt.myRb.AddForce((transform.forward + transform.up) * 50f, ForceMode.Impulse);
    }
    #endregion

    private void OnDisable()
    {
        playerMgmt.inputMgmt.attackEventStarted -= AttackPerformed;
        playerMgmt.inputMgmt.attackEventCancelled -= AttackReleased;
        playerMgmt.inputMgmt.rangedAttackEventStarted -= RangedAttackPerformed;
        playerMgmt.inputMgmt.rangedAttackEventCancelled -= RangedAttackReleased;
    }
}
