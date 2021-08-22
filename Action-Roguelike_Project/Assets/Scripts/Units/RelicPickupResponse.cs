using UnityEngine;

public class RelicPickupResponse : MonoBehaviour, IPickupResponse
{
    public void OnPickup(Transform interactingEntity, Transform pickup)
    {
        Relic relic = pickup.GetComponent<Relic>();

        // IF THE ITEM IS A WEAPON...
        if (relic != null)
        {
            // ADDS THE PICKED UP WEAPON TO THE EquipmentPanel UI SLOT
            interactingEntity.GetComponentInChildren<EquipmentPanel>()
                .AddItem(relic.relicData);
            // TELLS INTERACTING ENTITY'S WeaponManger TO EQUIP THE WEAPON
            interactingEntity.GetComponent<PlayerEventChannel>().
                RelicGathered(relic);

            //pickup.gameObject.SetActive(false);
            //pickup.GetComponent<CapsuleCollider>().enabled = false;
            //pickup.GetComponent<BoxCollider>().enabled = false;
            //pickup.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
