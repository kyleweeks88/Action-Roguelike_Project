using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public string hitboxName;

    CharacterStats charStats;

    void Start()
    {
        charStats = GetComponentInParent<CharacterStats>();
    }

    public void OnHit(GameObject damager, float dmgVal, Vector3 hitPoint)
    {
        Debug.Log(charStats.charName + " was hit in the " + hitboxName + " by"+ damager.name +" for " + dmgVal + " damage!");
        GetComponent<Rigidbody>().AddForceAtPosition(damager.transform.forward * 2, hitPoint, ForceMode.Impulse);
        charStats.TakeDamage(damager, dmgVal);
    }
}
