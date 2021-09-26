using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerEventChannel : MonoBehaviour
{
    public delegate void OnDamageRecieved(float dmgVal);
    public event OnDamageRecieved damageRecieved_Event;

    public delegate void OnHealthChanged(float currentHealth);
    public event OnHealthChanged healthChange_Event;

    public delegate void OnSoulGathered(CurrencyItem currency);
    public event OnSoulGathered soulGathered_Event;
    
    public delegate void OnRelicGathered(Relic relic);
    public event OnRelicGathered relicGathered_Event;

    public delegate void OnExperienceGained(float xpVal);
    public event OnExperienceGained experienceGained_Event;

    public void DamageRecieved(float dmgVal)
    {
        damageRecieved_Event?.Invoke(dmgVal);
    }

    public void HealthChanged(float currentHealth)
    {
        healthChange_Event?.Invoke(currentHealth);
    }

    public void SoulGathered(CurrencyItem soul)
    {
        soulGathered_Event?.Invoke(soul);
    }

    public void RelicGathered(Relic relic)
    {
        relicGathered_Event?.Invoke(relic);
    }

    public void ExperienceGained(float xpVal)
    {
        experienceGained_Event?.Invoke(xpVal);
    }
}
