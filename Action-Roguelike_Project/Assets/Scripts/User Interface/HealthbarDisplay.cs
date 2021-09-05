using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarDisplay : MonoBehaviour
{
    //[SerializeField] Image healthbar;
    [SerializeField] Image staminabar;
    [SerializeField] CharacterStats characterStats;

    void Awake()
    {
        //characterStats.healthChange_Event += HealthChanged;
        characterStats.staminaChange_Event += StaminaChanged;
    }

    //public void HealthChanged(float currentVal)
    //{
    //    healthbar.fillAmount = Mathf.Clamp(currentVal / 100, 0, 1);
    //}

    public void StaminaChanged(float currentVal)
    {
        if(staminabar != null)
            staminabar.fillAmount = Mathf.Clamp(currentVal / 100, 0, 1);
    }
}
