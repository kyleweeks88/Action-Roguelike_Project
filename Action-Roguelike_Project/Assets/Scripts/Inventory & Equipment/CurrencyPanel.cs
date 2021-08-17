using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] PlayerEventChannel playerEventChannel;
    [SerializeField] Transform currencyParent;
    [SerializeField] CurrencySlot[] currencySlots;

    public event Action<ItemData> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < currencySlots.Length; i++)
        {
            currencySlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }

        playerEventChannel.soulGathered_Event += AddItem;
    }

    private void OnValidate()
    {
        if (currencyParent != null)
            currencySlots = currencyParent.GetComponentsInChildren<CurrencySlot>();
    }

    public void AddItem(CurrencyItem _currencyToAdd)
    {
        for (int i = 0; i < currencySlots.Length; i++)
        {
            if(currencySlots[i].currencyType == _currencyToAdd.currencyType)
            {
                currencySlots[i].currencyAmount += _currencyToAdd.currencyValue;
                currencySlots[i].currencyAmountText.SetText(
                    currencySlots[i].currencyAmount.ToString());
            }
        }
    }

    public bool RemoveItem(CurrencyItem _itemToRemove)
    {
        return false;
    }
}
