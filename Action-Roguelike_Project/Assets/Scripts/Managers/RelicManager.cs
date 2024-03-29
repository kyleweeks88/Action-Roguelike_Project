﻿using System.Collections;
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

            if(_relicToAdd_.GetComponent<RelicMod_Stat>())
                _relicToAdd_.ActivateRelic(this.transform);
        }
    }

    void RemoveRelic(Relic _relicToRemove_)
    {
        if (currentRelics.Contains(_relicToRemove_))
        {
            currentRelics.Remove(_relicToRemove_);
            _relicToRemove_.DeactivateRelic(this.transform);
        }
    }
}
