using System.Collections;
using UnityEngine;

public interface IDamageable<T>
{
    void TakeDamage(GameObject damager, T damageTaken);
}
