using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicMod_Stat : MonoBehaviour, IActivateRelic
{
    CharacterStats _characterStats;
    Stat modifiedStat = null;

    Stat DetermineStat(CharacterStats _stats_, StatType _statType_)
    {
        Stat newStat = null;
        switch(_statType_)
        {
            case StatType.Speed:
                newStat = _stats_.moveSpeed;
                return newStat;
            case StatType.Strength:
                newStat = _stats_.attackDamage_Stat;
                return newStat;
            case StatType.JumpMultiplier:
                newStat = _stats_.jumpMultiplier;
                return newStat;
            case StatType.JumpForce:
                newStat = _stats_.jumpForce;
                return newStat;
            case StatType.Health:
                newStat = _stats_.health_Stat;
                return newStat;
        }

        return null;
    }

    public void OnActivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        _characterStats = _interactingEntity_.GetComponent<CharacterStats>();

        // If the modified Stat is a health stat...
        if (_relic_.relicData.statType == StatType.Health)
        {
            // If the affected entity's current health is at max health...
            if (_characterStats.GetNormalizedHealth() == 1)
            {
                DetermineStat(_characterStats, _relic_.relicData.statType).AddModifer(_relic_.statMod);
                _characterStats.SetHealth(_characterStats.health_Stat.value);
            }
            else
            {
                var currentHealthPerc = _characterStats.GetNormalizedHealth();
                var currentHealth = _characterStats.GetCurrentHealth();

                var adjustedModValue = currentHealthPerc * _relic_.statMod.value;

                DetermineStat(_characterStats, _relic_.relicData.statType).AddModifer(_relic_.statMod);
                _characterStats.SetHealth(currentHealth += adjustedModValue);
            }

            modifiedStat = DetermineStat(_characterStats, _relic_.relicData.statType);
            return;
        }

        // Determines the Stat to be affected and adds the Relics stat modifier
        DetermineStat(_characterStats, _relic_.relicData.statType).AddModifer(_relic_.statMod);
        // Adds the modified Stat to a list of Stats for later retrieval
        modifiedStat = DetermineStat(_characterStats, _relic_.relicData.statType);
    }

    public void OnDeactivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        if(modifiedStat.StatModifiers.Contains(_relic_.relicData.stadModifier))
            modifiedStat.RemoveModifier(_relic_.statMod);
    }
}
