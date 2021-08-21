using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    // Component Reference
    PlayerEventChannel playerEventChannel;

    public List<Relic> currentRelics = new List<Relic>();

    private void Awake()
    {
        playerEventChannel = GetComponent<PlayerEventChannel>();
    }

    private void OnEnable()
    {
        playerEventChannel.relicGathered_Event += AddRelic;
    }

    void AddRelic(Relic _relicToAdd_)
    {
        if(!currentRelics.Contains(_relicToAdd_))
        {
            currentRelics.Add(_relicToAdd_);
        }
    }
}
