using UnityEngine;

public interface IPickupResponse
{
    void OnPickup(Transform interactingEntity, Transform pickup);
}
