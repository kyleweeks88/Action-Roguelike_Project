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
    WeaponManager weaponMgmt;
    AnimationManager animMgmt;

    protected float combatTimer = 10f;
    protected float currentCombatTimer;
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
    public bool lightAttack;
    public bool heavyAttack;

    protected int attackId;
    #endregion

    public virtual void Start()
    {
        charStats = GetComponent<CharacterStats>();
        weaponMgmt = GetComponent<WeaponManager>();
        animMgmt = GetComponent<AnimationManager>();

        canRecieveAttackInput = true;
    }

    public virtual void Update()
    {
        CheckMeleeAttack();

        if(attackInputHeld && heavyAttack)
        {
            ChargeMeleeAttack();
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
    }

    #region Ranged

    public virtual void Shoot()
    {
        RangedWeapon rw = GetComponent<WeaponManager>().currentlyEquippedWeapon as RangedWeapon;

        rw.SpawnProjectile(projectileSpawn, GetComponent<CameraController>().myCamera.transform.forward);
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
        if(!heavyAttack) { return; }
        // If you have a weapon equipped...
        if (weaponMgmt.currentlyEquippedWeapon != null)
        {
            // If the current weapon is chargeable...
            if (weaponMgmt.currentlyEquippedWeapon.weaponData.isChargeable)
            {
                // If the current weapon's charge is high enough, set the bool to true
                // to perform maxCharge special attack.
                if (charStats.currentAttackCharge >=
                    charStats.maxAttackCharge)
                {
                    animMgmt.animator.SetBool("maxAttackCharge", true);
                }

                if (charStats.currentAttackCharge <=
                    charStats.maxAttackCharge)
                {
                    charStats.currentAttackCharge +=
                        Time.deltaTime * charStats.attackChargeRate_Stat.value;
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
                animMgmt.animator.SetBool("maxAttackCharge", true);
            }

            if (charStats.currentAttackCharge <
                charStats.maxAttackCharge)
            {
                charStats.currentAttackCharge +=
                    Time.deltaTime * charStats.attackChargeRate_Stat.value;
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
        // If the attack is a Heavy Attack
        if (attackId > 1)
        {
            // If the attacker has a weapon equipped
            if (weaponMgmt.currentlyEquippedWeapon != null)
            {
                MeleeWeaponData meleeData = weaponMgmt.currentlyEquippedWeapon.weaponData as MeleeWeaponData;
                charStats.DamageStamina(meleeData.staminaCost);
            }
            // If the attacker is unarmed
            else
            {
                //Debug.Log("FIX THIS: DETERMINE HEAVY OR LIGHT ATTACK STAMINA COST!");
                charStats.DamageStamina(20f);
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
            }
        }
        // If the attack is a Light Attack
        else
        {
            if(weaponMgmt.currentlyEquippedWeapon == null)
            {
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
            }
        }

        attackId = 0;
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
            if(hit.transform.tag == "Invulnerable") { return; }

            // Create equippedWeapon's hit visuals
            GameObject hitVis = Instantiate(hitFX, hit.ClosestPoint(impactTrans.position), Quaternion.identity);
            CharacterStats hitStats = hit.gameObject.GetComponent<CharacterStats>();
            // If the collider hit has an NpcHealthManager component on it.
            if (hitStats != null)
            {
                ProcessAttack(hitStats);
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
            MeleeWeapon equippedWeapon = weaponMgmt.currentlyEquippedWeapon as MeleeWeapon;
            if (equippedWeapon != null)
            {
                // Creates the collider on the weapon, the weapon then calls the Cmd
                equippedWeapon.CreateImpactCollider(this);
            }
        }
    }

    public void ProcessAttack(CharacterStats target)
    {
        float dmgVal = charStats.attackDamage_Stat.value;

        Vector3 angleToTarget = target.transform.position - this.transform.position;
        if (Vector3.Dot(target.transform.forward, angleToTarget) >= 0f)
        {
            Debug.Log("BACKSTAB!");
            dmgVal += GetComponent<CharacterStats>().stealth_Stat.value;
        }

        target.TakeDamage(this.gameObject, dmgVal);
    }
    #endregion
}
