using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<ItemData> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquippableItem _itemToAdd, out EquippableItem _prevItem)
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
                    _prevItem = null;
                    return true;
                }
                // ELSE IF THE SLOT IS FULL
            }
        }
        _prevItem = null;
        return false;
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
