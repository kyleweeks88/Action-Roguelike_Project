using UnityEngine;

public enum ItemType
{
    MainWeapon,
    SecondaryWeapon,
    Relic,
    Consumable
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public ItemType itemType;
}
