using UnityEngine;

public class WeaponPickupResponse : MonoBehaviour, IPickupResponse
{
    public void OnPickup(Transform interactingEntity, Transform pickup)
    {
        Weapon weapon = pickup.GetComponent<Weapon>();

        // IF THE ITEM IS A WEAPON...
        if (weapon != null)
        {
            // ADDS THE PICKED UP WEAPON TO THE EquipmentPanel UI SLOT
            interactingEntity.GetComponentInChildren<EquipmentPanel>()
                .AddItem(weapon.weaponData);
            // TELLS INTERACTING ENTITY'S WeaponManger TO EQUIP THE WEAPON
            interactingEntity.GetComponent<WeaponManager>()
                .AddWeapon(weapon);

            pickup.GetComponent<CapsuleCollider>().enabled = false;
            pickup.GetComponent<BoxCollider>().enabled = false;
            pickup.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
