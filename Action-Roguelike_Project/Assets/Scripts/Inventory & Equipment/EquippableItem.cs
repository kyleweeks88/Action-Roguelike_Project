using UnityEngine;

public enum ItemType
{
    MainWeapon,
    SecondaryWeapon,
    Relic
}

[CreateAssetMenu]
public class EquippableItem : ItemData
{
    public ItemType itemType;
}
