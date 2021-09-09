using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    [HideInInspector] public float currentCharge = 0f;
    [HideInInspector] public float currentDurability = 0f;

    [HideInInspector] public GameObject wieldingEntity;

    void Awake()
    {
        currentCharge = weaponData.maxCharge;
        currentDurability = weaponData.maxDurability;
    }

    public void ResetCharge()
    {
        currentCharge = 1f;
    }

    protected virtual void AffectDurability() 
    {
        // MAKE IT MORE FLESHED OUT i.e...
        // DURABILITY DEGRADES FASTER OR SLOWER BASED ON CHARACTER STATS etc.
        currentDurability -= 1;
        if (currentDurability <= 0)
        {
            wieldingEntity.GetComponent<WeaponManager>().DropWeapon();
            currentDurability = 0f;
        }
    }

    public virtual void InstantiateHitVisuals(Vector3 hitPoint)
    {
        GameObject hitVis = Instantiate(weaponData.hitVisuals, hitPoint, Quaternion.identity);
    }
}

