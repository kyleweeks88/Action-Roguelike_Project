using System.Collections;
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
    public event UnityAction blockEventStarted;
    public event UnityAction blockEventCancelled;
    public event UnityAction attackEventStarted;
    public event UnityAction attackEventCancelled;
    public event UnityAction rangedAttackEventStarted;
    public event UnityAction rangedAttackEventCancelled;
    // Used when the player interacts with contextual objects in the environment
    public event UnityAction interactEvent;
    public event UnityAction userInterfaceEvent;
    public event UnityAction pauseEvent;

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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (attackEventStarted != null &&
            context.phase == InputActionPhase.Started)
            attackEventStarted.Invoke();

        if (attackEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            attackEventCancelled.Invoke();
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (rangedAttackEventStarted != null &&
            context.phase == InputActionPhase.Started)
            rangedAttackEventStarted.Invoke();

        if (rangedAttackEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            rangedAttackEventCancelled.Invoke();
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

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (rangedAttackEventStarted != null &&
            context.phase == InputActionPhase.Started)
            rangedAttackEventStarted.Invoke();

        if (rangedAttackEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            rangedAttackEventCancelled.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (blockEventStarted != null &&
            context.phase == InputActionPhase.Started)
            blockEventStarted.Invoke();

        if (blockEventCancelled != null &&
            context.phase == InputActionPhase.Canceled)
            blockEventCancelled.Invoke();
    }
}
