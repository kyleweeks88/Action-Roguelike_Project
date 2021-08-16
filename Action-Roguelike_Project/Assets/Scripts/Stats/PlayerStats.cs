using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //PlayerName playerName;
    public float playerGravity;
    [HideInInspector] public float currentPlayerGravity;

    [SerializeField] PlayerEventChannel playerEventChannel;

    public override void Start()
    {
        base.Start();
        //playerName = GetComponent<PlayerName>();
        //base.charName = playerName.synchronizedName;
    }

    private void Update()
    {
        Debug.Log(attackDamage.value);
    }

    public override void TakeDamage(GameObject damager, float dmgVal)
    {
        playerEventChannel.DamageRecieved(dmgVal);

        base.TakeDamage(damager, dmgVal);
    }

    public override void Death()
    {
        base.Death();
        this.gameObject.transform.position = Vector3.zero;
        InitializeVitals();
    }
}

