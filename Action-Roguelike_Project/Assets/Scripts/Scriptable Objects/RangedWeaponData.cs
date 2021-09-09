using UnityEngine;

[CreateAssetMenu(fileName = "New_RangedWeaponData", menuName = "ItemData/WeaponData/RangedWeaponData")]
public class RangedWeaponData : WeaponData
{
    [Header("Ranged Settings")]
    public Projectile projectile;
    public float staminaCost;
}
