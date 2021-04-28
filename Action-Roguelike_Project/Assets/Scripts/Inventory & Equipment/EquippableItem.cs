using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    MeleeWeapon,
    RangedWeapon,
    Relic,
    Gear
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public EquipmentType equipmentType;
}
