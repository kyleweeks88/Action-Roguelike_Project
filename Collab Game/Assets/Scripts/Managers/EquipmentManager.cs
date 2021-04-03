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
            //playerMgmt.animMgmt.SetAnimation(currentlyEquippedWeapon.weaponData.animationSet);
        }
    }

    public void CheckEquipWeapon(Weapon _weaponToEquip)
    {
        weaponToEquip = _weaponToEquip;
        EquipWeapon(weaponToEquip, weaponEquipPos);
    }

    //[Command]
    //void CmdEquipWeapon(NetworkIdentity netId, Transform equipPos)
    //{        
    //    EquipWeapon(netId, weaponToEquip, equipPos);
    //    RpcEquipWeapon(netId, equipPos);
    //}

    //[ClientRpc]
    //void RpcEquipWeapon(NetworkIdentity netId, Transform equipPos)
    //{
    //    if (base.hasAuthority) { return; }

    //    EquipWeapon(netId, weaponToEquip, equipPos);
    //}

    public void EquipWeapon(Weapon _weaponToEquip, Transform _weaponEquipPos)
    {
        if (currentlyEquippedWeapon != null)
        {
            // UNEQUIP WEAPON LOGIC
        }
        else
        {
            Weapon newWeapon = Instantiate(_weaponToEquip, _weaponEquipPos);
            newWeapon.transform.SetParent(_weaponEquipPos);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);

            //AnimationManager am = netId.gameObject.GetComponent<AnimationManager>();
            //am.SetAnimation(newWeapon.weaponData.animationSet);
            weaponToEquip = newWeapon;
            currentlyEquippedWeapon = newWeapon;
        }
    }
}
