using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Weapon[] equippedWeapons;
    [SerializeField] PlayerManager playerMgmt;
    public Weapon currentlyEquippedWeapon;
    Weapon weaponToEquip;

    [SerializeField] Transform weaponEquipPos;

    void Start()
    {
        if(currentlyEquippedWeapon != null)
        {
            playerMgmt.animMgmt.SetAnimation(currentlyEquippedWeapon.weaponData.animationSet);
        }
    }

    public void EquipWeapon(Weapon _weaponToEquip)
    {
        if (currentlyEquippedWeapon != null)
        {
            // UNEQUIP WEAPON LOGIC
            EquippableItem prevItem;
            GetComponentInChildren<EquipmentPanel>().AddItem(_weaponToEquip.weaponData, out prevItem);
            GetComponentInChildren<Inventory>().AddItem(prevItem);
        }
        else
        {
            EquippableItem prevItem;
            GetComponentInChildren<EquipmentPanel>().AddItem(_weaponToEquip.weaponData, out prevItem);
            // HERE IS WHERE I SHOULD DO SOMETHING WITH THE prevItem...
            // DROP ON THE GROUND MAYBE?
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
