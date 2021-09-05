using UnityEngine;

public enum StatType
{
    Speed,
    Strength,
    JumpForce,
    JumpMultiplier,
    Health
}

[CreateAssetMenu(fileName = "RelicData", menuName = "ItemData/RelicData")]
public class RelicData : EquippableItem
{
    public StatType statType;
    //public StatType statType;
    public StatModifier stadModifier;
    //public float modValue;

    public void Init(StatType _statTypes, StatModifier _statModifier)
    {
        statType = _statTypes;
        stadModifier = _statModifier;
    }
}
