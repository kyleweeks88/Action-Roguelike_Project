using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    GameObject interactingEntity;
    // The weapon prefab for this pickup
    public Weapon weaponToPickup_Pf;

    void OnTriggerEnter(Collider col)
    {
        // Check if the colliding object has an EquipmentManager component
        EquipmentManager colEquipMgmt = col.gameObject.GetComponent<EquipmentManager>();
        if (colEquipMgmt != null)
        {
            interactingEntity = col.gameObject;
            HandleEntityInput(interactingEntity, true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        // Check if the colliding object has an EquipmentManager component
        EquipmentManager colEquipMgmt = col.gameObject.GetComponent<EquipmentManager>();
        if (colEquipMgmt != null)
        {
            HandleEntityInput(interactingEntity, false);
        }
    }

    /// <summary>
    /// Subscribes or Unsubscribes this Weapon Pickup object to an event
    /// on the interacting entity's InputManager.
    /// </summary>
    /// <param name="colObj"></param>
    /// <param name="boolVal"></param>
    void HandleEntityInput(GameObject colObj, bool boolVal)
    {
        PlayerManager pm = colObj.GetComponent<PlayerManager>();

        if (pm != null)
        {
            if (boolVal)
                pm.inputMgmt.interactEvent += PickupWeapon;

            if (!boolVal)
                pm.inputMgmt.interactEvent -= PickupWeapon;
        }
    }

    void PickupWeapon()
    {
        EquipmentManager entityEquipment = interactingEntity.GetComponent<EquipmentManager>();
        if (entityEquipment != null)
        {
            entityEquipment.EquipWeapon(weaponToPickup_Pf);
        }

        interactingEntity.GetComponent<PlayerManager>().inputMgmt.interactEvent -= PickupWeapon;
        Object.Destroy(this.gameObject);
    }
}
