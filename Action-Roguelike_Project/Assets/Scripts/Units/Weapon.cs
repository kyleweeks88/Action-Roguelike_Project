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
            //interactingEntity = null; SHOULD THIS BE HERE???
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
        // ADDS THE PICKED UP WEAPON TO THE EquipmentPanel UI SLOT
        EquippableItem prevItem;
        interactingEntity.GetComponentInChildren<EquipmentPanel>()
            .AddItem(weaponData, out prevItem);
        // TELLS INTERACTING ENTITY'S WeaponManger TO EQUIP THE WEAPON
        interactingEntity.GetComponent<WeaponManager>()
            .AddWeapon(this);

        interactingEntity.GetComponent<PlayerManager>().inputMgmt.interactEvent -= PickupWeapon;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }
}

