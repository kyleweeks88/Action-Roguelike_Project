using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //PlayerName playerName;
    public float playerGravity;
    [HideInInspector] public float currentPlayerGravity;

    void Start()
    {
        //playerName = GetComponent<PlayerName>();
        //base.charName = playerName.synchronizedName;
    }

    public override void Death()
    {
        base.Death();
        this.transform.position = Vector3.zero;
    }
}

