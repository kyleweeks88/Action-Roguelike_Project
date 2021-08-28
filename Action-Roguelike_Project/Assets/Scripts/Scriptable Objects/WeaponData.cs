using UnityEngine;

public enum WieldStyle { OneHanded, TwoHanded, DualWield }

public abstract class WeaponData : EquippableItem
{
    public WieldStyle wieldStyle;

    public AnimatorOverrideController animationSet;

    public GameObject hitVisuals;
    public bool isChargeable;
}
