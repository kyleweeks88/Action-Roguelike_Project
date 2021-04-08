using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CharacterStats entity = other.gameObject.GetComponent<CharacterStats>();
            if (entity != null)
            {
                entity.TakeDamage(this.gameObject, 1f);
            }
        }
    }
}
