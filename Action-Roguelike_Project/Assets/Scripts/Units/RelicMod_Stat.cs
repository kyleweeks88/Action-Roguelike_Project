using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicMod_Stat : MonoBehaviour, IActivateRelic
{
    CharacterStats _characterStats;
    Stat modStat;

    Stat DetermineStat(CharacterStats _stats_, Relic _relic_)
    {
        Stat newStat = null;
        switch(_relic_.relicData.statType)
        {
            case StatType.Speed:
                newStat = _stats_.moveSpeed;
                break;
        }

        return newStat;
    }

    public void OnActivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        _characterStats = _interactingEntity_.GetComponent<CharacterStats>();
        modStat = DetermineStat(_characterStats, _relic_);
        modStat.AddModifer(_relic_.statMod);
    }

    public void OnDeactivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        if(modStat.StatModifiers.Contains(_relic_.statMod))
        {
            modStat.RemoveModifier(_relic_.statMod);
        }
    }
}
