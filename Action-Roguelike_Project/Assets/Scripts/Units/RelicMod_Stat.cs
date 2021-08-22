using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicMod_Stat : MonoBehaviour, IActivateRelic
{
    CharacterStats _characterStats;
    //Stat modStat;

    Stat DetermineStat(CharacterStats _stats_, StatType _statType_)
    {
        Stat newStat = null;
        switch(_statType_)
        {
            case StatType.Speed:
                newStat = _stats_.moveSpeed;
                return newStat;
                //break;
            case StatType.Strength:
                newStat = _stats_.attackDamage;
                return newStat;
                //break;
        }

        return null;
    }

    public void OnActivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        _characterStats = _interactingEntity_.GetComponent<CharacterStats>();

        foreach (StatType type in _relic_.relicData.statType)
        {
            DetermineStat(_characterStats, type).AddModifer(_relic_.statMod);
            //modStat = DetermineStat(_characterStats, type);
        }
        
        //modStat.AddModifer(_relic_.statMod);
    }

    public void OnDeactivateRelic(Transform _interactingEntity_, Relic _relic_)
    {
        //if(modStat.StatModifiers.Contains(_relic_.statMod))
        //{
        //    modStat.RemoveModifier(_relic_.statMod);
        //}
    }
}
