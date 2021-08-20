using UnityEngine;

public enum EquipmentType
{
    MainWeapon,
    SecondaryWeapon,
    Relic
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public EquipmentType equipmentType;
}
