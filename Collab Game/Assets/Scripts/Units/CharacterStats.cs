using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{ 
    public string charName = string.Empty;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainInterval = 0f;
    public float staminaDrainAmount = 0f;
    [HideInInspector] public float currentStamina;

    public delegate void OnStaminaChanged(float currentStamina, float maxStamina);
    public event OnStaminaChanged Event_StaminaChanged;

    #region Input Ref
    // Input System references
    PlayerControls playerControls;
    PlayerControls PlayerControls
    {
        get
        {
            if(playerControls != null) { return playerControls; }
            return playerControls = new PlayerControls();
        }
    }
    #endregion

    void OnEnable() => PlayerControls.Enable();
    void OnDisable() => PlayerControls.Disable();

    public virtual void Start() 
    {
        currentStamina = maxStamina;
        this.Event_StaminaChanged?.Invoke(currentStamina, maxStamina);
    }

    #region Stamina
    public virtual void ModifyStamina(float value)
    {
        currentStamina += value;

        this.Event_StaminaChanged?.Invoke(currentStamina, maxStamina);
    }

    #region Drain
    void UseStamina(float staminaDrain)
    {
        if(currentStamina - staminaDrain >= 0)
        {
            ModifyStamina(staminaDrain * -1f);
        }
    }

    public void StaminaDrain()
    {
        if(ShouldDrainStamina())
        {
            UseStamina(staminaDrainAmount);
            staminaDrainInterval = Time.time + 0.5f;
        }
    }

    bool ShouldDrainStamina()
    {
        bool result = (Time.time >= staminaDrainInterval);

        return result;
    }
    #endregion

    #region Gain
    void GainStamina(float staminaGain)
    {
        if(currentStamina + staminaGain <= maxStamina)
        {
            ModifyStamina(staminaGain);
        }
    }
    #endregion

    #endregion
}
