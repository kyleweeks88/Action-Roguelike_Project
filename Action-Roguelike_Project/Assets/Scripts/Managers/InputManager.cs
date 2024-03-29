﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour, PlayerControls.IPlayerActions, PlayerControls.IUserInterfaceActions
{
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction sprintEventStarted;
    public event UnityAction sprintEventCancelled;
    public event UnityAction jumpEventStarted;
    public event UnityAction jumpEventCancelled;
    public event UnityAction dodgeEvent;
    public event UnityAction combatContextEventStarted;
    public event UnityAction combatContextEventCancelled;
    public event UnityAction heavyAttackStartedEvent;
    public event UnityAction heavyAttackCancelledEvent;
    public event UnityAction lightAttackStartedEvent;
    public event UnityAction lightAttackCancelledEvent;
    // Used when the player interacts with contextual objects in the environment
    public event UnityAction interactEvent;
    public event UnityAction userInterfaceEvent;
    public event UnityAction pauseEvent;

    public event UnityAction swapMainWeaponEvent;
    public event UnityAction swapSecondaryWeaponEvent;
    public event UnityAction dropWeaponEvent;

    // FOR TESTING!!!
    public event UnityAction testEvent;
    [HideInInspector] public Vector2 lookDelta;

    PlayerControls controls;
    public PlayerControls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new PlayerControls();
        }
    }

    private void OnEnable()
    {
        Controls.Player.SetCallbacks(this);
        EnableGameplayInput();
    }

    void OnDisable() => DisableAllInput();

    public void EnableGameplayInput()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Controls.UserInterface.Disable();
        Controls.Player.Enable();
        Controls.Player.SetCallbacks(this);
    }

    public void EnableUserInterfaceInput()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Controls.Player.Disable();
        Controls.UserInterface.Enable();
        Controls.UserInterface.SetCallbacks(this);
    }

    public void DisableAllInput()
    {
        controls.Player.Disable();
        controls.UserInterface.Disable();
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (heavyAttackStartedEvent != null &&
            context.phase == InputActionPhase.Started)
            heavyAttackStartedEvent.Invoke();

        if (heavyAttackCancelledEvent != null &&
            context.phase == InputActionPhase.Canceled)
            heavyAttackCancelledEvent.Invoke();
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (lightAttackStartedEvent != null &&
            context.phase == InputActionPhase.Started)
            lightAttackStartedEvent.Invoke();

        if (lightAttackCancelledEvent != null &&
            context.phase == InputActionPhase.Canceled)
            lightAttackCancelledEvent.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (dodgeEvent != null &&
            context.phase == InputActionPhase.Performed)
            dodgeEvent.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (interactEvent != null &&
            context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (jumpEventStarted != null &&
            context.phase == InputActionPhase.Started)
            jumpEventStarted.Invoke();

        if (jumpEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            jumpEventCancelled.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // USE FOR DIFFERENT FORMS OF MOUSE MOVEMENT EVENTUALLY...
        // I.E. UI MOVEMENT etc.
        lookDelta = context.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (moveEvent != null)
        {
            moveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (sprintEventStarted != null &&
            context.phase == InputActionPhase.Started)
            sprintEventStarted.Invoke();

        if (sprintEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            sprintEventCancelled.Invoke();
    }

    public void OnUserInterface(InputAction.CallbackContext context)
    {
        if (userInterfaceEvent != null &&
            context.phase == InputActionPhase.Started)
            userInterfaceEvent.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (pauseEvent != null &&
            context.phase == InputActionPhase.Started)
            pauseEvent.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (combatContextEventStarted != null &&
            context.phase == InputActionPhase.Started)
            combatContextEventStarted.Invoke();

        if (combatContextEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            combatContextEventCancelled.Invoke();
    }

    public void OnSwapMainWeapon(InputAction.CallbackContext context)
    {
        if (swapMainWeaponEvent != null &&
            context.phase == InputActionPhase.Started)
            swapMainWeaponEvent.Invoke();
    }

    public void OnSwapSecondaryWeapon(InputAction.CallbackContext context)
    {
        if (swapSecondaryWeaponEvent != null &&
                    context.phase == InputActionPhase.Started)
            swapSecondaryWeaponEvent.Invoke();
    }

    public void OnDropWeapon(InputAction.CallbackContext context)
    {
        if (dropWeaponEvent != null &&
                    context.phase == InputActionPhase.Started)
            dropWeaponEvent.Invoke();
    }

    public void OnTEST(InputAction.CallbackContext context)
    {
        if (testEvent != null &&
            context.phase == InputActionPhase.Started)
            testEvent.Invoke();
    }
}
