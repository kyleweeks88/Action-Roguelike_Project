using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicMod_Stat : MonoBehaviour, IActivateRelic
{
    CharacterStats _characterStats;
    List<Stat> modStats = new List<Stat>();

    Stat DetermineStat(CharacterStats _stats_, StatType _statType_)
    {
        Stat newStat = null;
        switch(_statType_)
        {
            case StatType.Speed:
                newStat = _stats_.moveSpeed;
                return newStat;
            case StatType.Strength:
                newStat = _stats_.attackDamage;
                return newStat;
            case StatType.JumpMultiplier:
                newStat = _stats_.jumpMultiplier;
                return newStat;
            case StatType.JumpForce:
                newStat = _stats_.jumpForce;
                return newStat;
        }

        return null;
    }

    public void OnActivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        _characterStats = _interactingEntity_.GetComponent<CharacterStats>();

        foreach (StatType type in _relic_.relicData.statType)
        {
            DetermineStat(_characterStats, type).AddModifer(_relic_.statMod);
            modStats.Add(DetermineStat(_characterStats, type));
        }
    }

    public void OnDeactivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        // DOES THIS EVEN WORK?
        foreach (Stat stat in modStats)
        {
            if(stat.StatModifiers.Contains(_relic_.statMod))
            {
                stat.RemoveModifier(_relic_.statMod);
            }
        }
    }
}
