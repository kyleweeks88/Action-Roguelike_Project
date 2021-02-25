using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerStats playerStats = null;
    [SerializeField] Image staminaBarImage = null;

    void OnEnable() => playerStats.Event_StaminaChanged += HandleStaminaChanged;

    void OnDisable() => playerStats.Event_StaminaChanged -= HandleStaminaChanged;

    void HandleStaminaChanged(float currentStamina, float maxStamina)
    {
        staminaBarImage.fillAmount = currentStamina/maxStamina;
    }
}
