 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    #region Setup
    public LayerMask whatIsDamageable;

    [Header("Component Ref")]
    [SerializeField] PlayerManager playerMgmt;
    [SerializeField] GameObject hitFX;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    float combatTimer = 10f;
    float currentCombatTimer;
    [HideInInspector] public bool inCombat;
    [HideInInspector] public bool impactActivated;
    [HideInInspector] public string attackAnim;

    [Header("RANGED TESTING")] 
    [SerializeField] Transform projectileSpawn;
    // THIS PROJECTILE NEEDS TO BE DETERMINED BY THE CURRENT WEAPON \\
    // IT SHOULD NOT BE ON THIS SCRIPT!!!
    [SerializeField] Projectile projectile = null; //<<<========== FIX THIS!!!
    float nextShotTime = 0f;
    [SerializeField] float msBetweenShots = 0f;

    public bool canRecieveAttackInput;
    public bool attackInputRecieved;
    public bool attackInputHeld;
    public bool rangedAttackHeld;
    #endregion

    void Start()
    {
        canRecieveAttackInput = true;

        playerMgmt.inputMgmt.attackEventStarted += AttackPerformed;
        playerMgmt.inputMgmt.attackEventCancelled += AttackReleased;
        playerMgmt.inputMgmt.rangedAttackEventStarted += RangedAttackPerformed;
        playerMgmt.inputMgmt.rangedAttackEventCancelled += RangedAttackReleased;
    }

    private void Update()
    {
        CheckMeleeAttack();

        if(attackInputHeld)
        {
            ChargeMeleeAttack();
        }

        if(rangedAttackHeld)
        {
            ChargeRangedAttack();
        }
    }

    /// <summary>
    /// Handles the duration that the entity will be in combat and playing it's combat idle animtion.
    /// Turns the bool "inCombat" to false when timer reaches zero.
    /// </summary>
    public void HandleCombatTimer()
    {
        currentCombatTimer -= Time.deltaTime;

        if (currentCombatTimer <= 0)
        {
            currentCombatTimer = combatTimer;
            inCombat = false;
        }
    }

    #region Ranged
    public void RangedAttackPerformed()
    {
        // if player is locked into an "interacting" state then don't let this happen.
        if (playerMgmt.isInteracting) { return; }

        if (!canRecieveAttackInput) { return; }

        if (canRecieveAttackInput)
        {
            rangedAttackHeld = true;
            attackAnim = "rangedAttackHold";
            playerMgmt.animMgmt.HandleRangedAttackAnimation(rangedAttackHeld);
        }
    }

    public void RangedAttackReleased()
    {
        rangedAttackHeld = false;
        playerMgmt.animMgmt.HandleRangedAttackAnimation(rangedAttackHeld);
    }

    public virtual void ChargeRangedAttack()
    {
        // CHECK IF CHARACTER HAS A RANGED WEAPON \\

        // ACCESS THAT RANGED WEAPON SOMEHOW \\

        // INCREASE THAT RANGED WEAPON'S DAMAGE AND/OR PROJECTILE VELOCITY \\

        Debug.Log("CHARGING RANGED ATTACK!");
    }

    // CALLED BY AN ANIMATION EVENT 
    public void CheckRangedAttack()
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
    public void AttackPerformed()
    {
        // If the player is interacting with a contextual object, exit.
        if (playerMgmt.isInteracting) { return; }
        // If the player is unable to recieve attack input, exit.
        if (!canRecieveAttackInput) { return; }

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

            inCombat = true;
            currentCombatTimer = combatTimer;
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

    public virtual void ChargeMeleeAttack()
    {
        // If you have a weapon equipped...
        if (playerMgmt.equipmentMgmt.currentlyEquippedWeapon != null)
        {
            // If the current weapon is chargeable...
            if (playerMgmt.equipmentMgmt.currentlyEquippedWeapon.weaponData.isChargeable)
            {
                // If the current weapon's charge is high enough, set the bool to true
                // to perform maxCharge special attack.
                if (playerMgmt.playerStats.currentAttackCharge >=
                    playerMgmt.playerStats.maxAttackCharge)
                {
                    playerMgmt.animMgmt.myAnim.SetBool("maxAttackCharge", true);
                }

                if (playerMgmt.playerStats.currentAttackCharge <=
                    playerMgmt.playerStats.maxAttackCharge)
                {
                    playerMgmt.playerStats.currentAttackCharge +=
                        Time.deltaTime * playerMgmt.playerStats.attackChargeRate.value;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            // UNARMED CHARGING LOGIC
            if (playerMgmt.playerStats.currentAttackCharge >=
                    playerMgmt.playerStats.maxAttackCharge)
            {
                playerMgmt.animMgmt.myAnim.SetBool("maxAttackCharge", true);
            }

            if (playerMgmt.playerStats.currentAttackCharge <=
                playerMgmt.playerStats.maxAttackCharge)
            {
                playerMgmt.playerStats.currentAttackCharge +=
                    Time.deltaTime * playerMgmt.playerStats.attackChargeRate.value;
            }
            else
            {
                return;
            }
        }

        // Makes the player move slower when charging an attack
        playerMgmt.playerStats.moveSpeed.AddModifer(playerMgmt.playerStats.combatMovementModifier);
    }

    /// <summary>
    /// Called by an Animation Event from the player checks an int
    /// to determine the means of the attack
    /// </summary>
    /// <param name="handInt"></param>
    public void ActivateImpact(int impactID)
    {
        if (playerMgmt.equipmentMgmt.currentlyEquippedWeapon != null)
        {
            MeleeWeapon myWeapon = playerMgmt.equipmentMgmt.currentlyEquippedWeapon as MeleeWeapon;
            playerMgmt.playerStats.DamageStamina(myWeapon.meleeData.staminaCost);
        }
        else
        {
            playerMgmt.playerStats.DamageStamina(10f);
        }

        switch(impactID)
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

    public void CreateImpactCollider(Transform impactTrans)
    {
        // Generate a collider array that will act as the weapon's collision area
        Collider[] impactCollisions = null;

        impactCollisions = Physics.OverlapSphere(impactTrans.position, 1f, whatIsDamageable);

        // for each object the collider hits do this stuff...
        foreach (Collider hit in impactCollisions)
        {
            Debug.Log(hit.gameObject.name);

            // Create equippedWeapon's hit visuals
            GameObject hitVis = Instantiate(hitFX, hit.ClosestPoint(impactTrans.position), Quaternion.identity);

            // If the collider hit has an NpcHealthManager component on it.
            if (hit.gameObject.GetComponent<CharacterStats>() != null)
            {
                ProcessAttack(hit.gameObject.GetComponent<CharacterStats>());
                impactActivated = false;
                playerMgmt.playerStats.ResetAttackCharge();
            }
        }
    }

    public void SpecialAttack()
    {
        playerMgmt.myRb.AddForce((transform.forward + transform.up) * 50f, ForceMode.Impulse);
    }

    /// <summary>
    /// Waits for the impactActivated bool to be triggered by an Animation Event. Grabs the entity's
    /// currently equipped weapon and creates an impact collider based on the weapons specs.
    /// </summary>
    void CheckMeleeAttack()
    {
        if (impactActivated)
        {
            MeleeWeapon equippedWeapon = playerMgmt.equipmentMgmt.currentlyEquippedWeapon as MeleeWeapon;
            if (equippedWeapon != null)
            {
                // Creates the collider on the weapon, the weapon then calls the Cmd
                equippedWeapon.CreateImpactCollider(this);
            }
        }
    }

    public void ProcessAttack(CharacterStats target)
    {
        float dmgVal = playerMgmt.playerStats.attackDamage.value;

        target.TakeDamage(this.gameObject, dmgVal);
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
