using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    protected GameObject interactingEntity;
    // The weapon prefab for this pickup
    public GameObject item_Pf;

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
                playerMgmt.inputMgmt.interactEvent += PickupItem;

            if (!boolVal)
                playerMgmt.inputMgmt.interactEvent -= PickupItem;
        }
    }

    public virtual void PickupItem()
    {
        // CHECK WHAT KIND OF ITEM IS BEING PICKED UP

        // IF THE ITEM IS A WEAPON...
        if (item_Pf.GetComponent<Weapon>() != null)
        {
            // ADDS THE PICKED UP WEAPON TO THE EquipmentPanel UI SLOT
            EquippableItem prevItem;
            interactingEntity.GetComponentInChildren<EquipmentPanel>()
                .AddItem(item_Pf.GetComponent<Weapon>().weaponData, out prevItem);
            // TELLS INTERACTING ENTITY'S WeaponManger TO EQUIP THE WEAPON
            interactingEntity.GetComponent<WeaponManager>()
                .AddWeapon(item_Pf.GetComponent<Weapon>());
        }
        // ELSE IF ITEM IS CONSUMABLE
        // ELSE IF ITEM IS RELIC

        interactingEntity.GetComponent<PlayerManager>().inputMgmt.interactEvent -= PickupItem;
        Object.Destroy(this.gameObject);
    }
}
