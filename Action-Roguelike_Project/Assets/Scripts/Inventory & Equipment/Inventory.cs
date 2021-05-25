using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemData> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemData> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        RefreshUI();
    }

    private void RefreshUI()
    {
        int i = 0;
        for(; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].item = items[i];
        }

        for(; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
        }
    }

    public bool AddItem(ItemData _itemToAdd)
    {
        if (IsFull()) return false;

        items.Add(_itemToAdd);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(ItemData _itemToRemove)
    {
        if(items.Remove(_itemToRemove))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
