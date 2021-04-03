using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStats : CharacterStats
{
    public override void Death()
    {
        base.Death();
        ServerDeath();
    }

    public virtual void ServerDeath()
    {
        //RpcDeath();
        Object.Destroy(this.gameObject);
    }

    //[ClientRpc]
    //void RpcDeath()
    //{
    //    if (base.hasAuthority) { return; }

    //    Object.Destroy(this.gameObject);
    //}
}
