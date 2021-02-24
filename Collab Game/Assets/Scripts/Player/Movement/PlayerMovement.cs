using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// This script will handle the locomotion of the player character.
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CharacterController charController = null;
    Animator myAnimator;
    [SerializeField] GameObject myCamera;
    [SerializeField] CinemachineFreeLook freeLook;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float turnSpeed = 15f;
    Vector2 previousInput;

    // Gravity-related variables
    float yVelocity = 0;
    float gravity = -9.81f;

    [Header("Jump Settings")]
    [SerializeField] bool isJumping;
    [SerializeField] float jumpVelocity = 5f;

    #region Animator Parameters
    // My Animator parameters turned from costly Strings to cheap Ints
    int isSprintingParam = Animator.StringToHash("isSprinting");
    int isJumpingParam = Animator.StringToHash("isJumping");
    int isGroundedParam = Animator.StringToHash("isGrounded");
    int yVelocityParam = Animator.StringToHash("yVelocity");
    int inputXParam = Animator.StringToHash("InputX");
    int inputYParam = Animator.StringToHash("InputY");
    #endregion
    
    // Input System references
    PlayerControls playerControls;
    PlayerControls PlayerControls
    {
        get
        {
            if(playerControls != null) { return playerControls; }
            return playerControls = new PlayerControls();
        }
    }

    void OnEnable() => PlayerControls.Enable();
    void OnDisable() => PlayerControls.Disable();

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        charController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerControls.Locomotion.Jump.performed += ctx => Jump();
    }

void Update()
    {
        // Applies gravity to player if not grounded
        if(!charController.isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
        }
        else if(yVelocity < 0)
        {
            yVelocity = 0f;
        }

        // If player is currently jumping in the air but heading back towards ground
        // do a raycast to check for ground.
        if(isJumping && yVelocity < 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
            }
        }

        myAnimator.SetBool(isJumpingParam, isJumping);
        myAnimator.SetBool(isGroundedParam, charController.isGrounded);
        myAnimator.SetFloat(yVelocityParam, yVelocity);
        Move();
        UpdateIsSprinting();
    }

    void Jump()
    {
        // MADE CHANGES TO HOW JUMP WORKS!
        
        if(!isJumping)
        {
            isJumping = true;

            yVelocity += jumpVelocity;
        }
    }

    void Move()
    {
        // READS THE INPUT SYSTEMS ACTION
        var movementInput = PlayerControls.Locomotion.Movement.ReadValue<Vector2>();

        // CONVERTS THE INPUT INTO A NORMALIZED VECTOR3
        var movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;
        
        // MAKES THE CHARACTER'S FORWARD AXIS MATCH THE CAMERA'S FORWARD AXIS
        Vector3 rotationMovement = Quaternion.Euler(0,myCamera.transform.rotation.eulerAngles.y, 0)  * movement;
        Vector3 verticalMovement = Vector3.up * yVelocity;

        // MAKES THE CHARACTER MODEL TURN TOWARDS THE CAMERA'S FORWARD AXIS
        float cameraYaw = myCamera.transform.rotation.eulerAngles.y;
        // ... ONLY IF THE PLAYER IS MOVING
        if(movement.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,cameraYaw,0), turnSpeed * Time.deltaTime);
        }

        // HANDLES ANIMATIONS
        myAnimator.SetFloat(inputXParam, movement.x);
        myAnimator.SetFloat(inputYParam, movement.z);

        // MOVES THE PLAYER
        charController.Move((verticalMovement + (rotationMovement * moveSpeed)) * Time.deltaTime);
    }

    void UpdateIsSprinting()
    {
        bool isSprinting = (PlayerControls.Locomotion.Sprint.activeControl != null) ? true : false;
        myAnimator.SetBool(isSprintingParam, isSprinting);

        if(isSprinting)
        {
            moveSpeed = sprintSpeed;
            turnSpeed = 5f;
        }
        else
        {
            moveSpeed = 5f;
            turnSpeed = 15f;
        }
    }
}
