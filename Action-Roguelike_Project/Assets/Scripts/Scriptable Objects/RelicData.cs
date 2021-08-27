using UnityEngine;

public enum StatType
{
    Speed,
    Strength,
    JumpForce,
    JumpMultiplier,
}

[CreateAssetMenu(fileName = "RelicData", menuName = "ItemData/RelicData")]
public class RelicData : EquippableItem
{
    public StatType[] statType;
    //public StatType statType;
    public StatModifier stadModifier;
    //public float modValue;
}
