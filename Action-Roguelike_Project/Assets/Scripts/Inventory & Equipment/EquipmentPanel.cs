using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;
    [SerializeField] PlayerEventChannel playerEventChannel;
    public event Action<ItemData> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }

        playerEventChannel.equippableItemGathered_Event += AddItem;
    }

    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public void AddItem(EquippableItem _itemToAdd)
    {
        // Go through each equipment slot
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            // IF THE itemType MATCHES THE SLOTS itemType
            if(equipmentSlots[i].itemType == _itemToAdd.equipmentType)
            {
                // IF THE SLOT IS EMPTY
                if (equipmentSlots[i].item == null)
                {
                    equipmentSlots[i].item = _itemToAdd;
                }
            }
        }
    }

    public bool RemoveItem(EquippableItem _itemToRemove)
    {
        // Go through each equipment slot
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            // If the item in the slot matches the item to remove.
            if (equipmentSlots[i].item == _itemToRemove)
            {
                equipmentSlots[i].item = null;
                return true;
            }
        }
        return false;
    }
}
