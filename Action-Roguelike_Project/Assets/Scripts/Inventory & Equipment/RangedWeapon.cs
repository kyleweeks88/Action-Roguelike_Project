using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] Transform projectileSpawnPos;

    RangedWeaponData rwData;

    private void Awake()
    {
        rwData = weaponData as RangedWeaponData;
    }

    public void SpawnProjectile(Transform pos, Vector3 dir)
    {
        Projectile newProjectile = Instantiate(rwData.projectile,
            pos.position,
            Quaternion.LookRotation(dir, Vector3.up));

        newProjectile.SetSpeed(20f, dir);
    }
}
