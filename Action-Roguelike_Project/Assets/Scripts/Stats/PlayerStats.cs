using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CurrentLevel
{
    One, Two, Three, Four, Five, 
    Six, Seven, Eight, Nine, Ten
}

public class PlayerStats : CharacterStats
{
    CurrentLevel currentLevel;
    public float playerGravity;
    public float currentXp = 0;
    public float requiredXp = 10;
    [HideInInspector] public float currentPlayerGravity;

    [SerializeField] PlayerEventChannel playerEventChannel;

    public override void Start()
    {
        base.Start();
        playerEventChannel.soulGathered_Event += GainExperienceFlatRate;
    }

    public override void TakeDamage(GameObject damager, float dmgVal)
    {
        playerEventChannel.DamageRecieved(dmgVal);

        base.TakeDamage(damager, dmgVal);
    }

    public override void Death()
    {
        base.Death();
        this.gameObject.transform.position = Vector3.zero;
        InitializeVitals();
    }

    #region Leveling & Experience
    public void LevelUp(float remainderXp)
    {
        currentXp = remainderXp;
        currentLevel++;
        requiredXp = UpdateRequiredExperiencePoints();
        UpdatePlayerStats();
    }

    public void GainExperienceFlatRate(CurrencyItem soul)
    {
        currentXp += soul.currencyValue;
        if (currentXp >= requiredXp)
            LevelUp(currentXp -= requiredXp);

        // Updates UI and any listeners
        playerEventChannel.ExperienceGained(soul.currencyValue);
    }

    public float UpdateRequiredExperiencePoints()
    {
        float xp = 0f;

        switch(currentLevel)
        {
            case CurrentLevel.One:
                xp = 10f;
                break;
            case CurrentLevel.Two:
                xp = 20f;
                break;
            case CurrentLevel.Three:
                xp = 50f;
                break;
            case CurrentLevel.Four:
                xp = 125f;
                break;
            case CurrentLevel.Five:
                xp = 275f;
                break;
            case CurrentLevel.Six:
                xp = 750f;
                break;
            case CurrentLevel.Seven:
                xp = 1500f;
                break;
            case CurrentLevel.Eight:
                xp = 4500f;
                break;
            case CurrentLevel.Nine:
                xp = 12000f;
                break;
            case CurrentLevel.Ten:
                xp = 30000f;
                break;
        }

        return xp;
    }

    void UpdatePlayerStats()
    {
        float hpToAdd = 0f;
        float dmgToAdd = 0f;
        float spToAdd = 0f;

        switch(currentLevel)
        {
            case CurrentLevel.Two:
                hpToAdd = 20f;
                spToAdd = 20f;
                dmgToAdd = 2f;
                break;
            case CurrentLevel.Five:
                hpToAdd = 30f;
                spToAdd = 30f;
                dmgToAdd = 4f;
                break;
            case CurrentLevel.Eight:
                hpToAdd = 40f;
                spToAdd = 40f;
                dmgToAdd = 8f;
                break;
        }

        health_Stat.AddModifer(new StatModifier(hpToAdd, StatModType.Flat));
        attackDamage_Stat.AddModifer(new StatModifier(dmgToAdd, StatModType.Flat));
        stamina_Stat.AddModifer(new StatModifier(spToAdd, StatModType.Flat));
    }
    #endregion

}

