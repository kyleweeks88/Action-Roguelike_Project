using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
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
    }

    public bool AddItem(CurrencyItem _itemToAdd, int _amountToAdd)
    {
        for (int i = 0; i < currencySlots.Length; i++)
        {
            if(currencySlots[i].currencyType == _itemToAdd.currencyType)
            {
                currencySlots[i].currencyAmount += _amountToAdd;
                currencySlots[i].currencyAmountText.SetText(
                    currencySlots[i].currencyAmount.ToString());
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
