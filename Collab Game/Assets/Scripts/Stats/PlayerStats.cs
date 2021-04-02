using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //PlayerName playerName;
    public float playerGravity;
    [HideInInspector] public float currentPlayerGravity;

    public override void Start()
    {
        base.Start();
        //playerName = GetComponent<PlayerName>();
        //base.charName = playerName.synchronizedName;
    }

    public override void Death()
    {
        base.Death();
        this.transform.position = Vector3.zero;
        //CmdDeath();
    }

    //[Command]
    //public virtual void CmdDeath()
    //{
    //    Debug.Log(charName + " has died!");
    //    this.transform.position = Vector3.zero;
    //    RpcDeath();
    //}

    //[ClientRpc]
    //void RpcDeath()
    //{
    //    if (base.hasAuthority) { return; }

    //    Debug.Log(charName + " has died!");
    //    this.transform.position = Vector3.zero;
    //}
}

