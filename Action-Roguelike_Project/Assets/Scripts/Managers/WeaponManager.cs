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
    Weapon weaponToDrop;

    [SerializeField] Transform weaponEquipPos;

    void Start()
    {
        playerMgmt.inputMgmt.swapMainWeaponEvent += SwapMainWeapon;
        playerMgmt.inputMgmt.swapSecondaryWeaponEvent += SwapSecondaryWeapon;
        playerMgmt.inputMgmt.dropWeaponEvent += DropWeapon;

        // IF THE PLAYER HAS A WEAPON EQUIPPED PLAY THE WEAPONS ANIMATION SET
        if (currentlyEquippedWeapon != null)
        {
            playerMgmt.animMgmt.SetAnimation(currentlyEquippedWeapon.weaponData.animationSet);
        }
    }

    public void AddWeapon(Weapon _weaponToAdd)
    {
        //Weapon newWeapon = Instantiate(_weaponToAdd, weaponEquipPos);
        // PLACE NEWLY PICKED UP WEAPON IN SHEATHED LOCATION
        _weaponToAdd.transform.SetParent(weaponEquipPos);
        _weaponToAdd.transform.localPosition = Vector3.zero;
        _weaponToAdd.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _weaponToAdd.gameObject.SetActive(false);

        // If the new weapon is a Main Weapon
        if (_weaponToAdd.weaponData.equipmentType == EquipmentType.MainWeapon)
        {
            // If player has no main weapon
            if (mainWeapon == null)
            {
                mainWeapon = _weaponToAdd;
            }
            // If player already has a main weapon
            else
            {
                weaponToDrop = mainWeapon;

                // Drop old main weapon
                DropWeapon();

                mainWeapon = _weaponToAdd;
            }
        }
        if (_weaponToAdd.weaponData.equipmentType == EquipmentType.SecondaryWeapon)
        {
            secondaryWeapon = _weaponToAdd;
        }

        if (currentlyEquippedWeapon == null)
        {
            EquipWeapon(_weaponToAdd);
        }
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

    void UnequipWeapon(Weapon _weaponToUnequip)
    {
        // Deactivate currently equipped weapon
        _weaponToUnequip.gameObject.SetActive(false);
        // Reset animation set
        playerMgmt.animMgmt.ResetAnimation();
        // Clear any added bonuses from weapon
        currentlyEquippedWeapon = null;
    }

    void DropWeapon()
    {
        if(currentlyEquippedWeapon != null) { weaponToDrop = currentlyEquippedWeapon; }

        if(weaponToDrop.weaponData.equipmentType == EquipmentType.MainWeapon) { mainWeapon = null; }
        if(weaponToDrop.weaponData.equipmentType == EquipmentType.SecondaryWeapon) { secondaryWeapon = null; }
        playerMgmt.animMgmt.ResetAnimation();
        currentlyEquippedWeapon = null;
        weaponToDrop.GetComponent<CapsuleCollider>().enabled = true;
        weaponToDrop.transform.SetParent(null);
        weaponToDrop.transform.position = this.transform.position;
        weaponToDrop.transform.rotation = Quaternion.identity;
    }

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
        if (currentlyEquippedWeapon != null)
        {
            if (currentlyEquippedWeapon == secondaryWeapon)
            {
                UnequipWeapon(currentlyEquippedWeapon);
                currentlyEquippedWeapon = null;
                return;
            }
            else if (currentlyEquippedWeapon == mainWeapon)
            {
                UnequipWeapon(currentlyEquippedWeapon);
                EquipWeapon(secondaryWeapon);
            }
        }
        else
        {
            if (secondaryWeapon != null)
            {
                EquipWeapon(secondaryWeapon);
            }
            else
            {
                Debug.Log("NO MAIN WEAPON");
            }
        }
    }
}
