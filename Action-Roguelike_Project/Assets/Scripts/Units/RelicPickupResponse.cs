using TMPro;
using UnityEngine;

public class RelicPickupResponse : MonoBehaviour, IPickupResponse
{
    TextMeshProUGUI test_Text;
    [SerializeField] string pickupName;

    void Awake()
    {
        test_Text = GetComponentInChildren<TextMeshProUGUI>();
        test_Text.text = pickupName;
    }

    public void OnPickup(Transform interactingEntity, Transform pickup)
    {
        Relic relic = pickup.GetComponent<Relic>();
        if (relic != null)
        {
            PlayerEventChannel pec = interactingEntity.GetComponent<PlayerEventChannel>();

            // ADDS THE PICKED UP Relic TO THE EquipmentPanel UI SLOT
            pec.EquippableItemGathered(relic.relicData);
            // TELLS INTERACTING ENTITY'S PlayerEventChannel 
            pec.RelicGathered(relic);
        }
        else
        {
            Debug.LogError("ERROR: Item pickup mismatch type");
            Debug.Break();
        }
    }
}
