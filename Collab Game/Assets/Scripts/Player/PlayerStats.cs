using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    PlayerMovement playerMovement;

    public override void Start()
    {
        base.Start();

        playerMovement = GetComponent<PlayerMovement>();
    }

    
}
