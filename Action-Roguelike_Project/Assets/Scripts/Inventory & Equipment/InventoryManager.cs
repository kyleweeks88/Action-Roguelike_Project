using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    void EquipFromInventory(ItemData _item)
    {
        if(_item is EquippableItem)
        {
            Equip((EquippableItem)_item);
        }
    }

    void UnequipFromEquipPanel(ItemData _item)
    {
        if(_item is EquippableItem)
        {
            Unequip((EquippableItem)_item);
        }
    }

    public void Equip(EquippableItem _item)
    {
        // If we are able to remove the _item from the inventory...
        if(inventory.RemoveItem(_item))
        {
            // Create a new instance of an EquippableItem to be designated as the
            // out parameter for the AddItem function.
            EquippableItem prevItem;
            if(equipmentPanel.AddItem(_item, out prevItem))
            {
                // If there is something being passed in the out parameter...
                if(prevItem != null)
                {
                    // ...add that prevItem to the inventory.
                    inventory.AddItem(prevItem);
                }
            }
            else
            {
                // If unable to equip the _item, just add it back to the inventory.
                inventory.AddItem(_item);
            }
        }
    }

    public void Unequip(EquippableItem _item)
    {
        // If the inventory is not full, and we're able to remove the _item
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(_item))
        {
            // ... add the unequipped _item to the inventory.
            inventory.AddItem(_item);
        }
    }
}
