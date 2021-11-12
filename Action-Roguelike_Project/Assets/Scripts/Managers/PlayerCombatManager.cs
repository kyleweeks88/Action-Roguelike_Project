using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : CombatManager
{
    [Header("Player Component Ref")]
    [SerializeField] PlayerManager playerMgmt;

    // SHOULD THIS SCRIPT CONTAIN THE PLAYER'S CURRENT
    // UNARMED COMBAT ANIMATION SET?

    public override void Start()
    {
        base.Start();

        playerMgmt.inputMgmt.heavyAttackStartedEvent += HeavyAttackPerformed;
        playerMgmt.inputMgmt.heavyAttackCancelledEvent += AttackReleased;
        playerMgmt.inputMgmt.lightAttackStartedEvent += LightAttackPerformed;
        playerMgmt.inputMgmt.lightAttackCancelledEvent += AttackReleased;
        playerMgmt.inputMgmt.combatContextEventStarted += BlockPerformed;
        playerMgmt.inputMgmt.combatContextEventCancelled += BlockReleased;
    }

    #region Ranged
    public void AimPerformed()
    {
        if (playerMgmt.playerMovement.isSprinting) { return; }
        playerMgmt.cameraCtrl.SetAim(true);
    }

    public void AimReleased() => playerMgmt.cameraCtrl.SetAim(false);

    // Called by an Animation Event
    public override void Shoot()
    {
        RangedWeapon rw = playerMgmt.weaponMgmt.currentlyEquippedWeapon as RangedWeapon;
        Vector3 aimDir = (playerMgmt.cameraCtrl.debugTransform.position - projectileSpawn.position).normalized;

        rw.SpawnProjectile(projectileSpawn, aimDir);
    }
    #endregion

    #region Melee
    public void LightAttackPerformed()
    {
        // If the player is interacting with a contextual object, exit.
        if (playerMgmt.isInteracting) { return; }
        if (playerMgmt.playerMovement.isSprinting) { return; }
        if (!canRecieveAttackInput) { return; }

        attackId = 1;
        inCombat = true;
        currentCombatTimer = combatTimer;

        if (canRecieveAttackInput)
        {
            attackInputHeld = true;
            lightAttack = true;

            // If you have a weapon equipped
            if (playerMgmt.weaponMgmt.currentlyEquippedWeapon != null)
            {
                // If current weapon is a melee type...
                if (playerMgmt.weaponMgmt.currentlyEquippedWeapon.weaponData.equipmentType == EquipmentType.MeleeWeapon)
                {
                    MeleeWeaponData meleeData = playerMgmt.weaponMgmt.currentlyEquippedWeapon.weaponData as MeleeWeaponData;
                    // Checks if the entity has enough stamina to do an attack...
                    if ((playerMgmt.playerStats.GetCurrentStamina() - meleeData.staminaCost) > 0)
                    {
                        attackAnim = "attackLight";
                        playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
                    }
                }
                // else if the weapon is a ranged type...
                else if(playerMgmt.weaponMgmt.currentlyEquippedWeapon.weaponData.equipmentType == EquipmentType.RangedWeapon)
                {
                    // IF THE PLAYER ISN'T AIMING DO A PUSH/BASH ATTACK
                    // IF THE PLAYER IS AIMING THEN CALL SHOOTING LOGIC
                    // DO I CALL SHOOTING LOGIC ON THE WEAPON ITSELF???

                    attackAnim = "attackLight";
                    playerMgmt.animMgmt.HandleRangedAttackAnimation(attackInputHeld);
                }
            }
            // If you have no equipped weapon, you're unarmed
            else
            {
                print("test");

                if (playerMgmt.playerStats.GetCurrentStamina() - 10f > 0)
                {
                    attackAnim = "attackLight";
                    playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
                }
            }
        }
    }

    public void HeavyAttackPerformed()
    {
        //if (!playerMgmt.weaponMgmt.currentlyEquippedWeapon.weaponData.isChargeable) { return; }
        // If the player is interacting with a contextual object, exit.
        if (playerMgmt.isInteracting) { return; }
        if (playerMgmt.playerMovement.isSprinting) { return; }
        if (!canRecieveAttackInput) { return; }

        attackId = 2;
        inCombat = true;
        currentCombatTimer = combatTimer;

        if (canRecieveAttackInput)
        {
            attackInputHeld = true;
            heavyAttack = true;

            //TEST============>// IF THE PLAYER IS IN THE AIR AND LOOKING DOWNWARDS!!!
            if (!playerMgmt.playerMovement.isGrounded)
            {
                if (Vector3.Dot(playerMgmt.cameraCtrl.myCamera.transform.forward, Vector3.up) <= -0.65f)
                {
                    playerMgmt.myRb.AddForce(playerMgmt.cameraCtrl.myCamera.transform.forward * 75f, ForceMode.Impulse);
                }
            }

            // If you have a weapon equipped
            if (playerMgmt.weaponMgmt.currentlyEquippedWeapon != null)
            {
                // If current weapon is a melee type...
                if (playerMgmt.weaponMgmt.currentlyEquippedWeapon.weaponData.equipmentType == EquipmentType.MeleeWeapon)
                {
                    MeleeWeaponData meleeData = playerMgmt.weaponMgmt.currentlyEquippedWeapon.weaponData as MeleeWeaponData;
                    // Checks if the entity has enough stamina to do an attack...
                    if ((playerMgmt.playerStats.GetCurrentStamina() - meleeData.staminaCost) > 0)
                    {
                        attackAnim = "attackHeavy";
                        playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
                    }
                }
                // else if the weapon is a ranged type...
            }
            // If you have no equipped weapon, you're unarmed
            else
            {
                if (playerMgmt.playerStats.GetCurrentStamina() - 10f > 0)
                {
                    attackAnim = "attackHeavy";
                    playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
                }
            }
        }
    }

    public void SpecialAttack()
    {
        print("ERROR: Disabled this function because it doesn't make sense right now.");
        //playerMgmt.myRb.AddForce((transform.forward + transform.up) * 50f, ForceMode.Impulse);
    }

    public void BlockPerformed()
    {
        if (playerMgmt.playerMovement.isSprinting) { return; }
        base.Blocking();
    }

    public void BlockReleased() => isBlocking = false;
    #endregion

    /// <summary>
    /// Called by the player releasing attack input.
    /// </summary>
    void AttackReleased()
    {
        attackInputHeld = false;
        lightAttack = false;
        heavyAttack = false;
        playerMgmt.animMgmt.HandleMeleeAttackAnimation(attackInputHeld);
        playerMgmt.animMgmt.HandleRangedAttackAnimation(attackInputHeld);

        playerMgmt.playerStats.ResetAttackCharge();

        playerMgmt.playerStats.moveSpeed.RemoveModifier(playerMgmt.playerStats.combatMovementModifier);
    }

    private void OnDisable()
    {
        playerMgmt.inputMgmt.heavyAttackStartedEvent -= HeavyAttackPerformed;
        playerMgmt.inputMgmt.heavyAttackCancelledEvent -= AttackReleased;
        playerMgmt.inputMgmt.lightAttackStartedEvent -= LightAttackPerformed;
        playerMgmt.inputMgmt.lightAttackCancelledEvent -= AttackReleased;
    }
}
