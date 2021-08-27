using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [Header("Component Ref")]
    public PlayerEventChannel playerEventChannel;
    public InputManager inputMgmt;
    public Rigidbody myRb;
    public WeaponManager weaponMgmt;
    public PlayerStats playerStats;
    public AnimationManager animMgmt;
    public CombatManager combatMgmt;
    public PlayerMovement playerMovement;
    public DodgeControl dodgeCtrl;
    public SlideManager slideMgmt;
    public CameraController cameraCtrl;

    public bool isInteracting = false;

    void Awake()
    {
        myRb = gameObject.GetComponent<Rigidbody>();

        cameraCtrl = GetComponent<CameraController>();

        slideMgmt = GetComponent<SlideManager>();
        //slideMgmt.enabled = true;

        inputMgmt = gameObject.GetComponent<InputManager>();
        //inputMgmt.enabled = true;

        playerMovement = gameObject.GetComponent<PlayerMovement>();
        //playerMovement.enabled = true;

        weaponMgmt = gameObject.GetComponent<WeaponManager>();
        //weaponMgmt.enabled = true;

        playerStats = gameObject.GetComponent<PlayerStats>();
        //playerStats.enabled = true;

        animMgmt = gameObject.GetComponent<AnimationManager>();
        //animMgmt.enabled = true;

        combatMgmt = gameObject.GetComponent<CombatManager>();
        //combatMgmt.enabled = true;

        dodgeCtrl = gameObject.GetComponent<DodgeControl>();
        //dodgeCtrl.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
