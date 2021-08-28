using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public int durability = 0;
    public float maxCharge = 2f;
    public float chargeRate = 5f;
    [HideInInspector] public float currentCharge = 1f;

    protected GameObject interactingEntity;

    public void ResetCharge()
    {
        currentCharge = 1f;
    }

    protected virtual void AffectDurability() 
    {
        // MAKE IT MORE FLESHED OUT i.e...
        // DURABILITY DEGRADES FASTER OR SLOWER BASED ON CHARACTER STATS etc.
        durability -= 1;
        if (durability < 1)
            interactingEntity.GetComponent<WeaponManager>().DropWeapon();
    }
}

