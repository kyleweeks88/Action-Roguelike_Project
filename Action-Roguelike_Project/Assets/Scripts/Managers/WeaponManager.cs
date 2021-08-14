using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerMgmt;
    public Weapon currentlyEquippedWeapon;
    [SerializeField] Weapon mainWeapon;
    [SerializeField] Weapon secondaryWeapon;
    Weapon weaponToEquip;

    [SerializeField] Transform weaponEquipPos;

    void Start()
    {
        playerMgmt.inputMgmt.swapMainWeaponEvent += SwapMainWeapon;

        // IF THE PLAYER HAS A WEAPON EQUIPPED PLAY THE WEAPONS ANIMATION SET
        if (currentlyEquippedWeapon != null)
        {
            playerMgmt.animMgmt.SetAnimation(currentlyEquippedWeapon.weaponData.animationSet);
        }
    }

    public void AddWeapon(Weapon _weaponToAdd)
    {
        Weapon newWeapon = Instantiate(_weaponToAdd, weaponEquipPos);
        // PLACE NEWLY PICKED UP WEAPON IN SHEATHED LOCATION
        newWeapon.gameObject.SetActive(false);

        // If the new weapon is a Main Weapon
        if (newWeapon.weaponData.equipmentType == EquipmentType.MainWeapon)
        {
            // If player has no main weapon
            if (mainWeapon == null)
            {
                mainWeapon = newWeapon;
            }
            // If player already has a main weapon
            else
            {
                // Drop old main weapon

                mainWeapon = newWeapon;
            }
        }
        if (newWeapon.weaponData.equipmentType == EquipmentType.SecondaryWeapon)
        {
            secondaryWeapon = newWeapon;
        }

        //if(currentlyEquippedWeapon == null)
        //{
        //    EquipWeapon(newWeapon);
        //}
    }

    public void EquipWeapon(Weapon _weaponToEquip)
    {
        if(currentlyEquippedWeapon == null)
        {
            currentlyEquippedWeapon = _weaponToEquip;
            _weaponToEquip.gameObject.SetActive(true);

            _weaponToEquip.transform.SetParent(weaponEquipPos);
            _weaponToEquip.transform.localPosition = Vector3.zero;
            _weaponToEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);

            playerMgmt.animMgmt.SetAnimation(_weaponToEquip.weaponData.animationSet);
        }
    }

    public void UnequipWeapon(Weapon _weaponToUnequip)
    {
        // Deactivate currently equipped weapon
        _weaponToUnequip.gameObject.SetActive(false);
        // Reset animation set
        playerMgmt.animMgmt.ResetAnimation();
        // Clear any added bonuses from weapon
    }

    // DROP WEAPON

    // EQUIP MAIN WEAPON
    void SwapMainWeapon()
    {
        if (currentlyEquippedWeapon != null)
        {
            if (currentlyEquippedWeapon == mainWeapon)
            {
                UnequipWeapon(currentlyEquippedWeapon);
                currentlyEquippedWeapon = null;
                return;
            }
            else if(currentlyEquippedWeapon == secondaryWeapon)
            {
                UnequipWeapon(currentlyEquippedWeapon);
                EquipWeapon(mainWeapon);
            }
        }
        else
        {
            if (mainWeapon != null)
            {
                EquipWeapon(mainWeapon);
            }
            else
            {
                Debug.Log("NO MAIN WEAPON");
            }
        }
    }

    void SwapSecondaryWeapon()
    {

    }

    // EQUIP SECONDARY WEAPON
}
