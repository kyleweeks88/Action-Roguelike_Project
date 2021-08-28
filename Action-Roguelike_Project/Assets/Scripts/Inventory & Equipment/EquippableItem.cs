using UnityEngine;

public enum EquipmentType
{
    MeleeWeapon,
    RangedWeapon,
    Relic
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public EquipmentType equipmentType;
}
