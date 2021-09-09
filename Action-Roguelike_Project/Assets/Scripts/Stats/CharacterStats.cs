using System.Collections;
using UnityEngine;

// I think this script will handle character leveling, dying, all the attributes/stats
// and the way they modify or mitigate incoming/outgoing damage.
public class CharacterStats : MonoBehaviour, IKillable, IDamageable<float>
{
    public delegate void OnHealthChanged(float currentHealth);
    public event OnHealthChanged healthChange_Event;

    public delegate void OnStaminaChanged(float currentHealth);
    public event OnStaminaChanged staminaChange_Event;

    //public delegate void OnDeath();
    //public event OnDeath onDeath_Event;
    public event System.Action OnDeath;

    [Header("General settings")]
    [SerializeField] Rigidbody chestRb;
    public string charName;

    float gainInterval = 0f;
    float drainInterval = 0f;

    [Header("Locomotion stats")]
    public Stat moveSpeed;
    public Stat sprintSpeed;

    [Tooltip("The force behind the character's jump!")]
    public Stat jumpForce;
    public Stat jumpMultiplier;

    public StatModifier sprintMovementModifier = new StatModifier(1f, StatModType.PercentAdd);
    public StatModifier aerialMovementModifier = new StatModifier(-0.5f, StatModType.PercentMulti);
    public StatModifier combatMovementModifier = new StatModifier(-0.5f, StatModType.PercentMulti);

    [Header("Combat Stats")]
    public Stat attackDamage_Stat;
    public Stat attackChargeRate_Stat;
    public Stat blockReduction_Stat;
    //public Stat weaponDurability;
    [HideInInspector] public float maxAttackCharge = 100f;
    [HideInInspector] public float currentAttackCharge = 0f;

    [Header("General Stats")]
    public Stat stealth_Stat;

    #region Vitals Stamina & Health
    [Header("Stamina vital")]
    public float maxStaminaPoints;
    float currentStaminaPoints;
    public float staminaGainAmount;
    public float staminaGainDelay;
    [Tooltip("Miliseconds between stamina gain. The lower the number the faster the gain rate.")]
    public float staminaGainTickrate = 100f;
    public float staminaDrainAmount;
    public float staminaDrainDelay;
    bool drainingStamina;

    [Header("Health vital")]
    //public float maxHealthPoints;
    protected float currentHealthPoints;
    [Tooltip("How much health is gained for each healthGainTickrate when regenerating.")]
    public float healthGainAmount;
    [Tooltip("Delay before health recovery begins.")]
    public float healthGainDelay;
    [Tooltip("Miliseconds between health gain. The lower the number the faster the gain rate.")]
    public float healthGainTickrate = 100f;
    public float healthDrainAmount;
    public float healthDrainDelay;
    bool drainingHealth;

    public Stat health_Stat;
    public Stat stamina_Stat;
    [SerializeField] protected Renderer r;
    Color lerpedColor = Color.white;
    float normalizedHealth = 0f;
    #endregion

    public virtual void Start()
    {
        r = GetComponentInChildren<Renderer>();
        InitializeVitals();
        healthChange_Event += ChangeColor;
    }

    protected void ChangeColor(float currentHealth)
    {
        lerpedColor = Color.Lerp(Color.red, Color.white, GetNormalizedHealth());
        r.material.color = lerpedColor;
        //print("TEST: " + GetNormalizedHealth());
    }

    public void InitializeVitals()
    {
        SetHealth(health_Stat.baseValue);
        SetStamina(maxStaminaPoints);
    }

    #region Death!!!
    public virtual void Death()
    {
        if (OnDeath != null)
            OnDeath();
    }
    #endregion

    #region Invulnerability
    public void Invulnerability()
    {
        StartCoroutine(InvulnerableTimer(0.5f));
    }

    IEnumerator InvulnerableTimer(float timer)
    {
        string originalTag = gameObject.tag;

        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while(timer > 0)
        {
            this.gameObject.tag = "Invulnerable";
            timer -= Time.deltaTime;
            yield return wait;
        }

        gameObject.tag = originalTag;
    }
    #endregion

    #region Health
    public float GetCurrentHealth()
    {
        return currentHealthPoints;
    }

    public float GetNormalizedHealth()
    {
        normalizedHealth = currentHealthPoints / health_Stat.value;
        return normalizedHealth;
    }

    public virtual void SetHealth(float setVal)
    {
        currentHealthPoints = Mathf.Clamp(setVal, 0f, health_Stat.value);
        healthChange_Event?.Invoke(currentHealthPoints);
    }

    #region Health Gain
    public void HealthGainOverTime(float gainAmount)
    {
        if (ShouldAffectVital(gainInterval))
        {
            GainHealth(gainAmount);
            gainInterval = Time.time + healthGainTickrate / 1000f;
        }
    }

    public void GainHealth(float gainAmount)
    {
        currentHealthPoints = Mathf.Clamp((currentHealthPoints += gainAmount), 0f, health_Stat.value);
    }

    IEnumerator HealthGainDelay(float gainAmount, float oldValue)
    {
        yield return new WaitForSeconds(healthGainDelay);

        if (oldValue == currentHealthPoints)
        {
            drainingHealth = false;

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (currentHealthPoints < health_Stat.value && !drainingHealth)
            {
                HealthGainOverTime(gainAmount);
                yield return wait;
            }
        }
        else
        {
            yield return null;
        }
    }
    #endregion

    #region Health Drain
    public virtual void TakeDamage(GameObject attacker, float dmgVal)
    {
        // Determines if the damager is from in front or behind the reciever.
        Vector3 angleToAttacker = attacker.transform.position - this.transform.position;
        if(Vector3.Dot(this.transform.forward, angleToAttacker) > 0.5f)
        {
            if(GetComponent<CombatManager>().isBlocking)
            {
                if(blockReduction_Stat.value >= 1)
                {
                    dmgVal /= blockReduction_Stat.value;
                    DamageStamina(dmgVal / 2);
                }
                else
                {
                    dmgVal *= blockReduction_Stat.value;
                    DamageStamina(dmgVal / 2);
                }
            }
        }

        // Affect the currentHealthPoints of this Character!
        // Clamps between 0 and the maxHealthPoints
        currentHealthPoints = Mathf.Clamp((currentHealthPoints -= dmgVal), 0f, health_Stat.value);
        healthChange_Event?.Invoke(currentHealthPoints);
        drainingHealth = true;

        // WE ONLY WANNA START THIS HEALTH REGEN IF THE PLAYER HAS HEALTH REGEN AVAILABLE???
        //StartCoroutine(HealthGainDelay(healthGainAmount, currentHealthPoints));

        if (currentHealthPoints <= 0f)
        {
            Death();

            // This adds force to the character ragdoll in the...
            // forward direction of the damaging object.
            var rb = GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Chest).GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForceAtPosition((attacker.transform.forward + attacker.transform.up) * 15f,
                rb.gameObject.transform.position, ForceMode.VelocityChange);
            }
        }
    }

    public void HealthDrainOverTime(float drainAmount, float drainDelay)
    {
        if (ShouldAffectVital(drainInterval))
        {
            drainingHealth = true;
            TakeDamage(null, drainAmount);
            drainInterval = Time.time + drainDelay / 1000f;
            StartCoroutine(HealthGainDelay(healthGainAmount, currentHealthPoints));
        }
    }
    #endregion

    #endregion

    #region Stamina
    public float GetCurrentStamina()
    {
        return currentStaminaPoints;
    }

    public virtual void SetStamina(float setVal)
    {
        currentStaminaPoints = Mathf.Clamp(setVal, 0f, maxStaminaPoints);
        staminaChange_Event?.Invoke(currentStaminaPoints);
    }

    #region Stamina Gain
    public void StaminaGainOverTime(float gainAmount)
    {
        if(GetComponent<CombatManager>().isBlocking) { return; }
        if (ShouldAffectVital(gainInterval))
        {
            GainStamina(gainAmount);
            gainInterval = Time.time + staminaGainTickrate / 1000f;
        }
    }

    public void GainStamina(float gainAmount)
    {
        currentStaminaPoints = Mathf.Clamp((currentStaminaPoints += gainAmount), 0f, maxStaminaPoints);
        staminaChange_Event?.Invoke(currentStaminaPoints);
    }

    IEnumerator StaminaGainDelay(float gainAmount, float oldValue)
    {
        yield return new WaitForSeconds(staminaGainDelay);

        if (oldValue == currentStaminaPoints)
        {
            drainingStamina = false;

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (currentStaminaPoints < maxStaminaPoints && !drainingStamina)
            {
                StaminaGainOverTime(gainAmount);
                yield return wait;
            }
        }
        else
        {
            yield return null;
        }
    }
    #endregion

    #region Stamina Drain
    public void DamageStamina(float dmgVal)
    {
        currentStaminaPoints = Mathf.Clamp((currentStaminaPoints -= dmgVal), 0f, maxStaminaPoints);
        staminaChange_Event?.Invoke(currentStaminaPoints);
        drainingStamina = true;

        StartCoroutine(StaminaGainDelay(staminaGainAmount, currentStaminaPoints));
    }

    public void StaminaDrainOverTime(float drainAmount, float drainDelay)
    {
        if (ShouldAffectVital(drainInterval))
        {
            drainingStamina = true;
            DamageStamina(drainAmount);
            drainInterval = Time.time + drainDelay / 1000f;
            StartCoroutine(StaminaGainDelay(staminaGainAmount, currentStaminaPoints));
        }
    }
    #endregion

    #endregion

    protected bool ShouldAffectVital(float interval)
    {
        bool result = (Time.time >= interval);

        return result;
    }

    public void ResetAttackCharge()
    {
        if (currentAttackCharge != 0)
            currentAttackCharge = 0f;
    }
}
