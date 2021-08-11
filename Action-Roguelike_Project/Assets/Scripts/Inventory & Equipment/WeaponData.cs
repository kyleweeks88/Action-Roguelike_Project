using UnityEngine;

public enum WeaponType { Melee,Ranged}

public abstract class WeaponData : EquippableItem
{
    public WeaponType weaponType;
    public enum WieldStyle { OneHanded, TwoHanded, DualWield }
    public WieldStyle wieldStyle;

    public AnimatorOverrideController animationSet;

    public GameObject hitVisuals;
    public float damage;
    public bool isChargeable;
}
