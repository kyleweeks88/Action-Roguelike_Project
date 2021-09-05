using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "ItemData/WeaponData/MeleeWeaponData")]
public class MeleeWeaponData : WeaponData
{
    [Header("Melee Settings")]
    public StatModifier weaponDamage;
    public float staminaCost;
}
