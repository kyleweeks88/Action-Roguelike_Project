﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeControl : MonoBehaviour
{
    [SerializeField] PlayerManager playerMgmt = null;

    public float delayBeforeInvulnerable = 0.2f;
    public float invulnerableDuration = 0.5f;

    public float dodgeCooldown = 1f;
    float cooldown;

    public float dodgeVelocity = 3f;

    Vector2 prevDir;

    void Start()
    {
        playerMgmt.inputMgmt.dodgeEvent += Dodge;
        playerMgmt.inputMgmt.moveEvent += DodgeDirection;
    }

    private void FixedUpdate()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if(cooldown < 0)
        {
            cooldown = 0f;
        }
    }

    void DodgeDirection(Vector2 dir)
    {
        // Reads the move input direction to determine dodge direction
        prevDir = dir;
    }

    // MAKE THIS MORE LIKE A DASH WITHOUT I-FRAMES
    public void Dodge()
    {
        // If the player is in the air then exit
        if (!playerMgmt.playerMovement.isGrounded) { return; }
        // If the cooldown isn't ready then exit
        if (cooldown > 0) { return; }

        // If the entity has enough stamina to dodge
        if ((playerMgmt.playerStats.GetCurrentStamina() - 10f) > 0)
        {
            playerMgmt.isInteracting = true;
            playerMgmt.playerStats.DamageStamina(10f);

            // INVULNERABLE FUNCTION CALLED HERE
            playerMgmt.playerStats.Invulnerability();

            // normalizes the dir vector
            Vector3 _dir = new Vector3
            {
                x = prevDir.x,
                z = prevDir.y
            }.normalized;

            // If the player is pressing a direction...
            if (_dir.sqrMagnitude != 0)
            {
                // Get entity's direction/rotation relative to the camera
                Vector3 rotationMovement = Quaternion.Euler(0, playerMgmt.cameraCtrl.myCamera.transform.rotation.eulerAngles.y, 0) * _dir;
                Vector3 verticalMovement = Vector3.up * playerMgmt.myRb.velocity.y;

                // Adds force relative to the camera in a direction
                playerMgmt.myRb.AddForce((verticalMovement + (rotationMovement * dodgeVelocity)), ForceMode.VelocityChange);

            }
            // If the player isn't pressing any direction...
            else
            {
                playerMgmt.myRb.AddForce(-transform.forward * dodgeVelocity/2f, ForceMode.VelocityChange);
            }

            // PLAY DODGE ANIMATION FROM AnimationManager
            playerMgmt.animMgmt.animator.SetTrigger("dodge");

            // Resets the cooldown timer
            cooldown = dodgeCooldown;
        }
    } 

    // DO A DODGE ROLL WITH I-FRAMES IF THE PLAYER DOUBLE TAPS DODGE
}
