using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //PlayerName playerName;
    public float playerGravity;
    [HideInInspector] public float currentPlayerGravity;
    [HideInInspector] public float maxAttackCharge = 100f;
    [HideInInspector] public float currentAttackCharge = 0f;
    public Stat attackChargeRate;

    [SerializeField] PlayerEventChannel playerEventChannel;

    public override void Start()
    {
        base.Start();
        //playerName = GetComponent<PlayerName>();
        //base.charName = playerName.synchronizedName;
    }

    public override void TakeDamage(GameObject damager, float dmgVal)
    {
        playerEventChannel.DamageRecieved(dmgVal);

        base.TakeDamage(damager, dmgVal);
    }

    public void ResetAttackCharge()
    {
        if (currentAttackCharge != 0)
            currentAttackCharge = 0f;
    }

    public override void Death()
    {
        base.Death();
        this.gameObject.transform.position = Vector3.zero;
        InitializeVitals();
    }
}

