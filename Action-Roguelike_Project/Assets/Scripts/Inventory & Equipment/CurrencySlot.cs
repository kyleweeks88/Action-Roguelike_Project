using TMPro;
using UnityEngine;

public class CurrencySlot : ItemSlot
{
    public CurrencyType currencyType;
    public int currencyAmount;
    public TextMeshProUGUI currencyAmountText;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = currencyType.ToString() + " Slot";
        currencyAmountText.SetText(currencyAmount.ToString());
    }
}
