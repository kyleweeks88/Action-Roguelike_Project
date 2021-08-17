using UnityEngine;

public enum CurrencyType
{
    Coins,
    Souls,
    Materials
}

[CreateAssetMenu]
public class CurrencyItem : ItemData
{
    public CurrencyType currencyType;
    public int currencyValue;
}
