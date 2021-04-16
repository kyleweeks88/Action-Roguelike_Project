 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    #region Setup
    public LayerMask whatIsDamageable;

    [Header("Component Ref")]
    [SerializeField] protected GameObject hitFX;
    [SerializeField] protected Transform leftHand;
    [SerializeField] protected Transform rightHand;
    protected CharacterStats charStats;
    EquipmentManager equipmentMgmt;
    AnimationManager animMgmt;

    float combatTimer = 10f;
    float currentCombatTimer;
    [HideInInspector] public bool inCombat;
    [HideInInspector] public bool impactActivated;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public string attackAnim;

    [Header("RANGED TESTING")] 
    [SerializeField] protected Transform projectileSpawn;
    // THIS PROJECTILE NEEDS TO BE DETERMINED BY THE CURRENT WEAPON \\
    // IT SHOULD NOT BE ON THIS SCRIPT!!!
    [SerializeField] protected Projectile projectile = null; //<<<========== FIX THIS!!!
    float nextShotTime = 0f;
    [SerializeField] float msBetweenShots = 0f;

    public bool canRecieveAttackInput;
    public bool attackInputHeld;
    public bool rangedAttackHeld;
    #endregion

    public virtual void Start()
    {
        charStats = GetComponent<CharacterStats>();
        equipmentMgmt = GetComponent<EquipmentManager>();
        animMgmt = GetComponent<AnimationManager>();

        canRecieveAttackInput = true;
    }

    public virtual void Update()
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

    protected void Blocking()
    {
        isBlocking = true;
        GetComponent<CharacterStats>().blockReduction.AddModifer(GetComponent<CharacterStats>().blockModifier);
    }

    #region Ranged
    public virtual void RangedAttackPerformed()
    {
        if (!canRecieveAttackInput) { return; }

        rangedAttackHeld = true;
        attackAnim = "rangedAttackHold";
        animMgmt.HandleRangedAttackAnimation(rangedAttackHeld);
    }

    public virtual void ChargeRangedAttack()
    {
        // CHECK IF CHARACTER HAS A RANGED WEAPON \\

        // ACCESS THAT RANGED WEAPON SOMEHOW \\

        // INCREASE THAT RANGED WEAPON'S DAMAGE AND/OR PROJECTILE VELOCITY \\

        Debug.Log("CHARGING RANGED ATTACK!");
    }

    // CALLED BY AN ANIMATION EVENT 
    public virtual void CheckRangedAttack()
    {
        // Ask the server to check your pos, and spawn a projectile for the server
        SpawnProjectile(projectileSpawn.position, projectileSpawn.rotation, this.transform.forward);
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
    public virtual void AttackPerformed()
    {
        // If the player is unable to recieve attack input, exit.
        if (!canRecieveAttackInput) { return; }

        inCombat = true;
        currentCombatTimer = combatTimer;
    }

    public virtual void ChargeMeleeAttack()
    {
        // If you have a weapon equipped...
        if (equipmentMgmt.currentlyEquippedWeapon != null)
        {
            // If the current weapon is chargeable...
            if (equipmentMgmt.currentlyEquippedWeapon.weaponData.isChargeable)
            {
                // If the current weapon's charge is high enough, set the bool to true
                // to perform maxCharge special attack.
                if (charStats.currentAttackCharge >=
                    charStats.maxAttackCharge)
                {
                    animMgmt.myAnim.SetBool("maxAttackCharge", true);
                }

                if (charStats.currentAttackCharge <=
                    charStats.maxAttackCharge)
                {
                    charStats.currentAttackCharge +=
                        Time.deltaTime * charStats.attackChargeRate.value;
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
            if (charStats.currentAttackCharge >=
                    charStats.maxAttackCharge)
            {
                animMgmt.myAnim.SetBool("maxAttackCharge", true);
            }

            if (charStats.currentAttackCharge <=
                charStats.maxAttackCharge)
            {
                charStats.currentAttackCharge +=
                    Time.deltaTime * charStats.attackChargeRate.value;
            }
            else
            {
                return;
            }
        }

        // Makes the player move slower when charging an attack
        charStats.moveSpeed.AddModifer(charStats.combatMovementModifier);
    }

    /// <summary>
    /// Called by an Animation Event from the player checks an int
    /// to determine the means of the attack
    /// </summary>
    /// <param name="handInt"></param>
    public virtual void ActivateImpact(int impactID)
    {
        if (equipmentMgmt.currentlyEquippedWeapon != null)
        {
            MeleeWeapon myWeapon = equipmentMgmt.currentlyEquippedWeapon as MeleeWeapon;
            charStats.DamageStamina(myWeapon.meleeData.staminaCost);
        }
        else
        {
            charStats.DamageStamina(10f);
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

    public virtual void CreateImpactCollider(Transform impactTrans)
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
    public virtual void CheckMeleeAttack()
    {
        if (impactActivated)
        {
            MeleeWeapon equippedWeapon = equipmentMgmt.currentlyEquippedWeapon as MeleeWeapon;
            if (equippedWeapon != null)
            {
                // Creates the collider on the weapon, the weapon then calls the Cmd
                equippedWeapon.CreateImpactCollider(this);
            }
        }
    }

    public void ProcessAttack(CharacterStats target)
    {
        float dmgVal = charStats.attackDamage.value;

        target.TakeDamage(this.gameObject, dmgVal);
    }
    #endregion
}
