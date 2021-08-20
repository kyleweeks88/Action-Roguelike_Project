using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //[SerializeField] List<ItemData> items;
    [SerializeField] Transform currencyParent;
    [SerializeField] CurrencySlot[] currencySlots;

    public event Action<ItemData> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < currencySlots.Length; i++)
        {
            currencySlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        if (currencyParent != null)
            currencySlots = currencyParent.GetComponentsInChildren<CurrencySlot>();

        //RefreshUI();
    }

    //private void RefreshUI()
    //{
    //    int i = 0;
    //    for(; i < items.Count && i < itemSlots.Length; i++)
    //    {
    //        itemSlots[i].item = items[i];
    //    }

    //    for(; i < itemSlots.Length; i++)
    //    {
    //        itemSlots[i].item = null;
    //    }
    //}

    public bool AddItem(CurrencyItem _itemToAdd)
    {
        for (int i = 0; i < currencySlots.Length; i++)
        {
            if(currencySlots[i].currencyType == _itemToAdd.currencyType)
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(CurrencyItem _itemToRemove)
    {
        return false;
    }
}
