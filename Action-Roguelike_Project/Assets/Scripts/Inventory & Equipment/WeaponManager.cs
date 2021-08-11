using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] equippedWeapons;
    [SerializeField] PlayerManager playerMgmt;
    public Weapon currentlyEquippedWeapon;
    Weapon weaponToEquip;

    [SerializeField] Transform weaponEquipPos;

    void Start()
    {
        // IF THE PLAYER HAS A WEAPON EQUIPPED PLAY THE WEAPONS ANIMATION SET
        if(currentlyEquippedWeapon != null)
        {
            playerMgmt.animMgmt.SetAnimation(currentlyEquippedWeapon.weaponData.animationSet);
        }
    }

    public void EquipWeapon(Weapon _weaponToEquip)
    {
        // IF THERE IS A WEAPON...
        if (currentlyEquippedWeapon != null)
        {
            Debug.Log("Weapon Already Equipped");
            // HERE IS WHERE I SHOULD DO SOMETHING WITH THE EQUIPPED WEAPON...
            // DROP ON THE GROUND MAYBE?
        }
        // IF THERE IS NO WEAPON...
        else
        {
            Weapon newWeapon = Instantiate(_weaponToEquip, weaponEquipPos);
            newWeapon.transform.SetParent(weaponEquipPos);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);

            playerMgmt.animMgmt.SetAnimation(newWeapon.weaponData.animationSet);
            weaponToEquip = newWeapon;
            currentlyEquippedWeapon = newWeapon;
        }
    }
}
