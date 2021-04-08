using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStats : CharacterStats
{
    public override void Death()
    {
        base.Death();
        Object.Destroy(this.gameObject);
    }
}
