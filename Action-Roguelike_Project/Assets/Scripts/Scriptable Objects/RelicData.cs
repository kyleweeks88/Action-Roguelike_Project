using UnityEngine;

public enum StatType
{
    Speed,
    Strength
}

[CreateAssetMenu(fileName = "RelicData", menuName = "ItemData/RelicData")]
public class RelicData : EquippableItem
{
    public StatType[] statType;
    //public StatType statType;
    public float modValue;
}
