using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    MeleeWeapon,
    RangedWeapon,
    Relic,
    Consumable
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public EquipmentType equipmentType;
}
