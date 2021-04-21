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

    public void DamageRecieved(float dmgVal)
    {
        damageRecieved_Event?.Invoke(dmgVal);
    }

    public void HealthChanged(float currentHealth)
    {
        healthChange_Event?.Invoke(currentHealth);
    }
}
