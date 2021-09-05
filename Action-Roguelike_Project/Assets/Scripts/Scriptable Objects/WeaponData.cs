using UnityEngine;

public enum DamageType { Blunt, Slash, Pierce }

public abstract class WeaponData : EquippableItem
{
    // I think this SO should hold a 3D mesh for the weapon that
    // the MonoBehavior "Weapon" will instantiate on Awake.
    // maybe not... due to the generated collider?

    public DamageType damageType;

    [Tooltip("The animation set for this weapon that will override the current AnimatorController")]
    public AnimatorOverrideController animationSet;

    [Tooltip("The particle effects that will instantiate when the weapon collides with an object")]
    public GameObject hitVisuals;

    public int maxDurability = 0;

    public bool isChargeable;
    public float maxCharge = 2f;
    public float chargeRate = 5f;
}
