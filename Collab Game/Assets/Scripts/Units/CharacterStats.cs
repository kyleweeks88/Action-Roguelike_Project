using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{ 
    public string charName = string.Empty;

    float currentStamina;
    public float maxStamina;

    public delegate void OnStaminaChanged(float currentStamina, float maxStamina);
    public event OnStaminaChanged Event_StaminaChanged;

    public virtual void Start() 
    {
        currentStamina = maxStamina;
    }

    public virtual void ModifyStamina(float value)
    {
        currentStamina += value;

        this.Event_StaminaChanged?.Invoke(currentStamina, maxStamina);
    }
}
