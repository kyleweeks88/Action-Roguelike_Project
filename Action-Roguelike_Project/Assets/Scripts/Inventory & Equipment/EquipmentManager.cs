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

    ///////////////////////////////////////////
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    void Start()
    {
        if(currentlyEquippedWeapon != null)
        {
            playerMgmt.animMgmt.SetAnimation(currentlyEquippedWeapon.weaponData.animationSet);
        }
    }

    public void CheckEquipWeapon(Weapon _weaponToEquip)
    {
        weaponToEquip = _weaponToEquip;
        EquipWeapon(weaponToEquip, weaponEquipPos);
    }

    public void EquipWeapon(Weapon _weaponToEquip, Transform _weaponEquipPos)
    {
        if (currentlyEquippedWeapon != null)
        {
            // UNEQUIP WEAPON LOGIC
        }
        else
        {
            EquippableItem prevItem;
            GetComponentInChildren<EquipmentPanel>().AddItem(_weaponToEquip.weaponData, out prevItem);
            Weapon newWeapon = Instantiate(_weaponToEquip, _weaponEquipPos);
            newWeapon.transform.SetParent(_weaponEquipPos);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);

            playerMgmt.animMgmt.SetAnimation(newWeapon.weaponData.animationSet);
            weaponToEquip = newWeapon;
            currentlyEquippedWeapon = newWeapon;
        }
    }

    /////////////////////////////
    public void Equip(EquippableItem _item)
    {
        if(inventory.RemoveItem(_item))
        {
            EquippableItem prevItem;
            if(equipmentPanel.AddItem(_item, out prevItem))
            {

            }
        }
    }
}
