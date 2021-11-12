using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarDisplay : MonoBehaviour
{
    //[SerializeField] Image healthbar;
    [SerializeField] Image staminabar;
    [SerializeField] PlayerStats characterStats;
    [SerializeField] PlayerEventChannel playerEventChannel;

    [SerializeField] Image xpFillBar;
    [SerializeField] Image xpBackgroundBar;
    float delayTimer = 0f;
    float lerpTimer = 0f;

    void Awake()
    {
        //characterStats.healthChange_Event += HealthChanged;
        characterStats.staminaChange_Event += StaminaChanged;
        playerEventChannel.experienceGained_Event += GainExperience;
    }

    private void Start()
    {
        if (staminabar != null)
            staminabar.fillAmount = characterStats.GetCurrentStamina() / characterStats.stamina_Stat.value;

        if (xpFillBar != null)
            xpFillBar.fillAmount = characterStats.currentXp / characterStats.requiredXp;
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

    public void LevelChanged()
    {
        // Level up UI stuff
    }

    //public void UpdateLevelUI()
    //{
    //    // FANCY LERPING GRAPHICS, MAYBE USE LATER??
    //    //float xpNormalized = characterStats.GetNormalizedExperiencePoints();
    //    //float xpFill = xpFillBar.fillAmount;

    //    //if (xpFill < xpNormalized)
    //    //{
    //    //    delayTimer += Time.deltaTime;
    //    //    xpBackgroundBar.fillAmount = xpNormalized;
    //    //    if (delayTimer > 3)
    //    //    {
    //    //        lerpTimer += Time.deltaTime;
    //    //        float percentComplete = lerpTimer / 4;
    //    //        xpFillBar.fillAmount = Mathf.Lerp(xpFill, xpBackgroundBar.fillAmount, percentComplete);
    //    //    }
    //    //}
    //}

    public void GainExperience(float xpVal)
    {
        if (xpFillBar != null)
            xpFillBar.fillAmount = characterStats.currentXp / characterStats.requiredXp;

        print(xpFillBar.fillAmount);
    }
}
