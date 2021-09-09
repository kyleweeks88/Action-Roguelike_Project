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
    [SerializeField] Transform dropPos;

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
        // PLACE NEWLY PICKED UP WEAPON IN SHEATHED LOCATION
        _weaponToAdd.transform.SetParent(weaponEquipPos);
        _weaponToAdd.transform.localPosition = Vector3.zero;
        _weaponToAdd.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _weaponToAdd.gameObject.SetActive(false);

        // If the new weapon is a Melee Weapon
        if (_weaponToAdd.weaponData.equipmentType == EquipmentType.MeleeWeapon)
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
        // If the new weapon is a Ranged Weapon
        else if (_weaponToAdd.weaponData.equipmentType == EquipmentType.RangedWeapon)
        {
            // If player has no main weapon
            if (secondaryWeapon == null)
            {
                secondaryWeapon = _weaponToAdd;
            }
            // If player already has a main weapon
            else
            {
                weaponToDrop = secondaryWeapon;

                // Drop old main weapon
                DropWeapon();

                secondaryWeapon = _weaponToAdd;
            }
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
            if(_weaponToEquip.weaponData.equipmentType == EquipmentType.MeleeWeapon)
            {
                MeleeWeaponData meleeData = _weaponToEquip.weaponData as MeleeWeaponData;
                playerMgmt.playerStats.attackDamage_Stat.AddModifer(meleeData.weaponDamage);
            }

            if(_weaponToEquip.weaponData.equipmentType == EquipmentType.RangedWeapon)
            {
                playerMgmt.inputMgmt.combatContextEventStarted -= playerMgmt.combatMgmt.BlockPerformed;
                playerMgmt.inputMgmt.combatContextEventCancelled -= playerMgmt.combatMgmt.BlockReleased;

                playerMgmt.inputMgmt.combatContextEventStarted += playerMgmt.combatMgmt.AimPerformed;
                playerMgmt.inputMgmt.combatContextEventCancelled += playerMgmt.combatMgmt.AimReleased;
            }

            currentlyEquippedWeapon = _weaponToEquip;
            _weaponToEquip.wieldingEntity = this.gameObject;
            _weaponToEquip.gameObject.SetActive(true);

            _weaponToEquip.transform.SetParent(weaponEquipPos);
            _weaponToEquip.transform.localPosition = Vector3.zero;
            _weaponToEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);

            playerMgmt.animMgmt.SetAnimation(_weaponToEquip.weaponData.animationSet);
        }
    }

    void UnequipWeapon(Weapon _weaponToUnequip)
    {
        if (_weaponToUnequip.weaponData.equipmentType == EquipmentType.RangedWeapon)
        {
            playerMgmt.inputMgmt.combatContextEventStarted -= playerMgmt.combatMgmt.AimPerformed;
            playerMgmt.inputMgmt.combatContextEventCancelled -= playerMgmt.combatMgmt.AimReleased;

            playerMgmt.inputMgmt.combatContextEventStarted += playerMgmt.combatMgmt.BlockPerformed;
            playerMgmt.inputMgmt.combatContextEventCancelled += playerMgmt.combatMgmt.BlockReleased;
        }

        // Deactivate currently equipped weapon
        _weaponToUnequip.gameObject.SetActive(false);
        // Clear modifiers
        if (_weaponToUnequip.weaponData.equipmentType == EquipmentType.MeleeWeapon)
        {
            MeleeWeaponData meleeData = _weaponToUnequip.weaponData as MeleeWeaponData;
            playerMgmt.playerStats.attackDamage_Stat.RemoveModifier(meleeData.weaponDamage);
        }
        // Reset animation set
        playerMgmt.animMgmt.ResetAnimation();
        // Clear any added bonuses from weapon
        currentlyEquippedWeapon = null;
    }

    public void DropWeapon()
    {
        if(currentlyEquippedWeapon != null) { weaponToDrop = currentlyEquippedWeapon; }

        if(weaponToDrop.weaponData.equipmentType == EquipmentType.MeleeWeapon) 
        {
            MeleeWeaponData meleeData = weaponToDrop.weaponData as MeleeWeaponData;
            playerMgmt.playerStats.attackDamage_Stat.RemoveModifier(meleeData.weaponDamage);
            mainWeapon = null; 
        }
        if(weaponToDrop.weaponData.equipmentType == EquipmentType.RangedWeapon) 
        {
            playerMgmt.inputMgmt.combatContextEventStarted -= playerMgmt.combatMgmt.AimPerformed;
            playerMgmt.inputMgmt.combatContextEventCancelled -= playerMgmt.combatMgmt.AimReleased;

            playerMgmt.inputMgmt.combatContextEventStarted += playerMgmt.combatMgmt.BlockPerformed;
            playerMgmt.inputMgmt.combatContextEventCancelled += playerMgmt.combatMgmt.BlockReleased;
            secondaryWeapon = null; 
        }

        gameObject.GetComponentInChildren<EquipmentPanel>().RemoveItem(weaponToDrop.GetComponent<Weapon>().weaponData);
        
        playerMgmt.animMgmt.ResetAnimation();
        currentlyEquippedWeapon = null;

        weaponToDrop.transform.SetParent(null);
        weaponToDrop.transform.position = dropPos.position;
        weaponToDrop.GetComponent<CapsuleCollider>().enabled = true;
        Rigidbody weaponRb = weaponToDrop.GetComponent<Rigidbody>();
        weaponToDrop.GetComponent<BoxCollider>().enabled = true;
        weaponRb.isKinematic = false;
        weaponRb.AddForce(transform.forward * 1f, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        weaponRb.AddTorque(new Vector3(random, random, random) * 10f);
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
                if (mainWeapon == null) { print("NO MAIN WEAPON!"); return; }

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
