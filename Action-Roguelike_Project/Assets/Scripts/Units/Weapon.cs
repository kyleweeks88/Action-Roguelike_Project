using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public StatModifier damageMod;
    public int durability = 0;
    public float maxCharge = 2f;
    public float chargeRate = 5f;
    [HideInInspector] public float currentCharge = 1f;

    protected GameObject interactingEntity;

    private void Awake()
    {
        damageMod = new StatModifier(weaponData.damageMod, StatModType.PercentAdd);
    }

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

    void OnTriggerEnter(Collider col)
    {
        // Check if the colliding object has an EquipmentManager component
        PlayerManager playerMgmt = col.gameObject.GetComponent<PlayerManager>();
        if (playerMgmt != null)
        {
            interactingEntity = col.gameObject;
            HandleEntityInput(playerMgmt, true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        // Check if the colliding object has an EquipmentManager component
        PlayerManager playerMgmt = col.gameObject.GetComponent<PlayerManager>();
        if (playerMgmt != null)
        {
            interactingEntity = null; //SHOULD THIS BE HERE???
            HandleEntityInput(playerMgmt, false);
        }
    }

    /// <summary>
    /// Subscribes or Unsubscribes this Weapon Pickup object to an event
    /// on the interacting entity's InputManager.
    /// </summary>
    /// <param name="colObj"></param>
    /// <param name="boolVal"></param>
    void HandleEntityInput(PlayerManager playerMgmt, bool boolVal)
    {
        if (playerMgmt != null)
        {
            if (boolVal)
                playerMgmt.inputMgmt.interactEvent += PickupWeapon;

            if (!boolVal)
                playerMgmt.inputMgmt.interactEvent -= PickupWeapon;
        }
    }

    public virtual void PickupWeapon()
    {
        if(durability < 1) { return; }

        // ADDS THE PICKED UP WEAPON TO THE EquipmentPanel UI SLOT
        EquippableItem prevItem;
        interactingEntity.GetComponentInChildren<EquipmentPanel>()
            .AddItem(weaponData, out prevItem);
        // TELLS INTERACTING ENTITY'S WeaponManger TO EQUIP THE WEAPON
        interactingEntity.GetComponent<WeaponManager>()
            .AddWeapon(this);

        interactingEntity.GetComponent<PlayerManager>().inputMgmt.interactEvent -= PickupWeapon;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}

