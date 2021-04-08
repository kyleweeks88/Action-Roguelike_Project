using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// I think this script will handle character leveling, dying, all the attributes/stats
// and the way they modify or mitigate incoming/outgoing damage.
public class CharacterStats : MonoBehaviour, IKillable, IDamageable<float>
{
    [Header("General settings")]
    public string charName;

    float gainInterval = 0f;
    float drainInterval = 0f;

    [Header("Locomotion stats")]
    public Stat moveSpeed;
    [Tooltip("The force behind the character's jump!")]
    public float jumpVelocity = 5f;

    public StatModifier sprintMovementModifier = new StatModifier(1f, StatModType.PercentAdd);
    public StatModifier aerialMovementModifier = new StatModifier(-0.5f, StatModType.PercentMulti);
    public StatModifier combatMovementModifier = new StatModifier(-0.5f, StatModType.PercentMulti);

    [Header("Combat settings")]
    public Stat attackDamage;

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
    public float maxHealthPoints;
    float currentHealthPoints;
    [Tooltip("How much health is gained for each healthGainTickrate when regenerating.")]
    public float healthGainAmount;
    [Tooltip("Delay before health recovery begins.")]
    public float healthGainDelay;
    [Tooltip("Miliseconds between health gain. The lower the number the faster the gain rate.")]
    public float healthGainTickrate = 100f;
    public float healthDrainAmount;
    public float healthDrainDelay;
    bool drainingHealth;

    public virtual void Start()
    {
        InitializeVitals();
    }

    //void Update()
    //{
    //    Debug.Log("Stamina: " + currentStaminaPoints + " / " + maxStaminaPoints);
    //}

    public void InitializeVitals()
    {
        currentHealthPoints = maxHealthPoints;
        currentStaminaPoints = maxStaminaPoints;
    }

    #region Death!!!
    public virtual void Death()
    {
        Debug.Log(charName + " has died!");
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

    public virtual void SetHealth(float setVal)
    {
        currentHealthPoints = Mathf.Clamp(setVal, 0f, maxHealthPoints);
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
        currentHealthPoints = Mathf.Clamp((currentHealthPoints += gainAmount), 0f, maxHealthPoints);
    }

    IEnumerator HealthGainDelay(float gainAmount, float oldValue)
    {
        yield return new WaitForSeconds(healthGainDelay);

        if (oldValue == currentHealthPoints)
        {
            drainingHealth = false;

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (currentHealthPoints < maxHealthPoints && !drainingHealth)
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
    public virtual void TakeDamage(GameObject damager, float dmgVal)
    {
        currentHealthPoints = Mathf.Clamp((currentHealthPoints -= dmgVal), 0f, maxHealthPoints);
        drainingHealth = true;

        if(currentHealthPoints <= 0f)
        {
            Death();
        }

        StartCoroutine(HealthGainDelay(healthGainAmount, currentHealthPoints));
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
    }

    #region Stamina Gain
    public void StaminaGainOverTime(float gainAmount)
    {
        if (ShouldAffectVital(gainInterval))
        {
            GainStamina(gainAmount);
            gainInterval = Time.time + staminaGainTickrate / 1000f;
        }
    }

    public void GainStamina(float gainAmount)
    {
        currentStaminaPoints = Mathf.Clamp((currentStaminaPoints += gainAmount), 0f, maxStaminaPoints);
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
}
