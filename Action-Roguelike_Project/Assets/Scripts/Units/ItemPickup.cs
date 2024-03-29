﻿using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    protected GameObject interactingEntity;
    private IPickupResponse _pickupResponse;

    private void Awake() => _pickupResponse = GetComponent<IPickupResponse>();

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
        _pickupResponse.OnPickup(interactingEntity.transform, this.transform);

        interactingEntity.GetComponent<PlayerManager>().inputMgmt.interactEvent -= PickupItem;
        //Object.Destroy(this.gameObject);
    }
}
