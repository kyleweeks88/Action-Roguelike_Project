using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    PlayerEventChannel playerEventChannel;

    public int soulsGathered;

    private void Awake()
    {
        playerEventChannel = GetComponent<PlayerEventChannel>();
        playerEventChannel.soulGathered_Event += GatherSoul;
    }

    public void GatherSoul(CurrencyItem _soulToGather)
    {
        soulsGathered += _soulToGather.currencyValue;
    }
}
