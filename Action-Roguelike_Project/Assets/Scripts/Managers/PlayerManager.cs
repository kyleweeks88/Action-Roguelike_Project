﻿using System.Collections;
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

    [Header("Camera Ref")]
    public Camera myCamera = null;
    public GameObject freeLook;
    public GameObject sprintCamera;
    public CinemachineVirtualCameraBase uiCamera;

    public bool isInteracting = false;

    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody>();

        inputMgmt = gameObject.GetComponent<InputManager>();
        inputMgmt.enabled = true;

        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerMovement.enabled = true;

        weaponMgmt = gameObject.GetComponent<WeaponManager>();
        weaponMgmt.enabled = true;

        playerStats = gameObject.GetComponent<PlayerStats>();
        playerStats.enabled = true;

        animMgmt = gameObject.GetComponent<AnimationManager>();
        animMgmt.enabled = true;

        combatMgmt = gameObject.GetComponent<CombatManager>();
        combatMgmt.enabled = true;

        dodgeCtrl = gameObject.GetComponent<DodgeControl>();
        dodgeCtrl.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
