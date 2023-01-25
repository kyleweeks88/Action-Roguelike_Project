using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{// TEST
    [Header("Component Ref")]
    [SerializeField] PlayerManager playerMgmt = null;
    PhysicMaterial physMat;
    SlideManager slideMgmt;

    [Header("Ground detection")]
    public LayerMask whatIsWalkable;
    public Transform groundColPos;
    public bool isGrounded;

    [Header("Step Climb settings")]
    [SerializeField] Transform[] stepRaysLow;
    [SerializeField] Transform[] stepRaysHigh;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 0.1f;

    Vector3 movement;
    Vector3 rotationMovement;
    Vector2 _previousMovementInput;

    float turnSpeed = 30f;

    [HideInInspector] public bool isSprinting = false;
    [HideInInspector] public bool isJumping = false;
    bool jumpInputHeld = false;
    int currentJumps;

    public bool moveLocked;

    private void OnEnable()
    {
        playerMgmt.inputMgmt.jumpEventStarted += Jump;
        playerMgmt.inputMgmt.jumpEventCancelled += JumpReleased;
        playerMgmt.inputMgmt.sprintEventStarted += SprintPressed;
        playerMgmt.inputMgmt.sprintEventCancelled += SprintReleased;
        playerMgmt.inputMgmt.moveEvent += OnMove;
    }

    private void Awake()
    {
        physMat = gameObject.GetComponent<CapsuleCollider>().material;
        slideMgmt = GetComponent<SlideManager>();

        currentJumps = (int)playerMgmt.playerStats.jumpMultiplier.value;
    }

    private void FixedUpdate()
    {
        // Handles the player's PhysicMaterial to prevent slow-sliding down shallow slopes when standing still.
        // but turns friction to zero when the player is moving.
        if (_previousMovementInput.sqrMagnitude != 0)
        {
            physMat.dynamicFriction = 0f;
        }
        else if (_previousMovementInput.sqrMagnitude == 0 && !slideMgmt.isSliding)
        {
            physMat.dynamicFriction = 1f;
        }

        GroundCheck();
        UpdateIsSprinting();
        Move();
    }

    #region Movement
    void OnMove(Vector2 movement)
    {
        _previousMovementInput = movement;
    }

    public Vector3 GetPrevMovement()
    {
        return _previousMovementInput;
    }

    public void Move()
    {
        if (moveLocked) { return; }

        // CONVERTS THE INPUT INTO A NORMALIZED VECTOR3
        //movement = new Vector3
        //{
        //    x = _previousMovementInput.x,
        //    y = -currentSlideVelocity,
        //    z = _previousMovementInput.y
        //}.normalized;

        // !!! THIS CODE COULD BE CAUSING BUGS OR PERFORMANCE ISSUES !!!
        Vector3 velocity = Vector3.zero;
        movement = Vector3.Lerp(movement, new Vector3
        {
            x = _previousMovementInput.x,
            y = 0,
            z = _previousMovementInput.y
        }.normalized, 10f * Time.deltaTime);

        if (_previousMovementInput.sqrMagnitude <= 0.5f)
            movement = Vector3.zero;

        // MAKES THE CHARACTER'S FORWARD AXIS MATCH THE CAMERA'S FORWARD AXIS
        rotationMovement = Quaternion.Euler(0, playerMgmt.cameraCtrl.myCamera.transform.rotation.eulerAngles.y, 0) * movement;

        // MAKES THE CHARACTER MODEL TURN TOWARDS THE CAMERA'S FORWARD AXIS
        // ... ONLY IF THE PLAYER IS MOVING, BLOCKING OR ATTACKING
        if (movement.sqrMagnitude > 0 || playerMgmt.combatMgmt.inCombat || playerMgmt.combatMgmt.isBlocking)
        {
            float cameraYaw = playerMgmt.cameraCtrl.myCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cameraYaw, 0), turnSpeed * Time.deltaTime);
        }

        // Only allows the player to sprint forwards
        if (isSprinting && movement.z <= 0)
        {
            SprintReleased();
        }

        if (movement.sqrMagnitude > 0.1f && isGrounded)
        {
            StepClimb();
        }

        // HANDLES ANIMATIONS
        playerMgmt.animMgmt.MovementAnimation(movement.x, movement.z);

        // MOVES THE PLAYER
        if (_previousMovementInput.y < 0)
        {
            playerMgmt.myRb.velocity += rotationMovement * (playerMgmt.playerStats.moveSpeed.value * .5f);
        }
        else
        {
            playerMgmt.myRb.velocity += rotationMovement * playerMgmt.playerStats.moveSpeed.value;
        }
    }
    #endregion

    void StepClimb()
    {
        if (slideMgmt.isSliding) { return; }
        Ray myRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit, 10f, whatIsWalkable))
        {
            float slopeAngle = Mathf.Deg2Rad * Vector3.Angle(Vector3.up, hit.normal); 

            if (slopeAngle <= 10 * Mathf.Deg2Rad) 
            {
                for (int i = 0; i < stepRaysLow.Length; i++)
                {
                    RaycastHit hitLower;
                    if (Physics.Raycast(stepRaysLow[i].position, stepRaysLow[i].TransformDirection(Vector3.forward), out hitLower, 0.3f, whatIsWalkable))
                    {
                        if (stepRaysLow[i].name == "step low F")
                        {
                            RaycastHit hitUpper;
                            if (!Physics.Raycast(stepRaysHigh[0].position, stepRaysHigh[0].TransformDirection(Vector3.forward), out hitUpper, 0.4f, whatIsWalkable))
                            {
                                playerMgmt.myRb.position -= new Vector3(0f, -stepSmooth, 0f);
                                return;
                            }
                        }
                        else if (stepRaysLow[i].name == "step low B")
                        {
                            RaycastHit hitUpper;
                            if (!Physics.Raycast(stepRaysHigh[1].position, stepRaysHigh[1].TransformDirection(Vector3.forward), out hitUpper, 0.4f, whatIsWalkable))
                            {
                                playerMgmt.myRb.position -= new Vector3(0f, -stepSmooth, 0f);
                                return;
                            }
                        }
                        else if (stepRaysLow[i].name == "step low L")
                        {
                            RaycastHit hitUpper;
                            if (!Physics.Raycast(stepRaysHigh[2].position, stepRaysHigh[2].TransformDirection(Vector3.forward), out hitUpper, 0.4f, whatIsWalkable))
                            {
                                playerMgmt.myRb.position -= new Vector3(0f, -stepSmooth, 0f);
                                return;
                            }
                        }
                        else if (stepRaysLow[i].name == "step low R")
                        {
                            RaycastHit hitUpper;
                            if (!Physics.Raycast(stepRaysHigh[3].position, stepRaysHigh[3].TransformDirection(Vector3.forward), out hitUpper, 0.4f, whatIsWalkable))
                            {
                                playerMgmt.myRb.position -= new Vector3(0f, -stepSmooth, 0f);
                                return;
                            }
                        }
                        return;
                    }
                }
            }
        }
    }

    void GroundCheck()
    {
        Collider[] groundCollisions = Physics.OverlapSphere(groundColPos.position, 0.25f, whatIsWalkable);

        if (groundCollisions.Length <= 0)
        {
            isGrounded = false;

            // Add the aerialMovementModifier if it isn't already affecting _moveSpeed.
            if (!playerMgmt.playerStats.moveSpeed.StatModifiers.Contains(playerMgmt.playerStats.aerialMovementModifier))
                playerMgmt.playerStats.moveSpeed.AddModifer(playerMgmt.playerStats.aerialMovementModifier);

            // Makes jumping and falling feel better
            if (playerMgmt.myRb.velocity.y < 0f)
            {
                playerMgmt.myRb.velocity += Vector3.up * Physics.gravity.y * (10f - 1f) * Time.deltaTime;
            }
            else if (playerMgmt.myRb.velocity.y > 0f && !jumpInputHeld)
            {
                playerMgmt.myRb.velocity += Vector3.up * Physics.gravity.y * (8f - 1f) * Time.deltaTime;
            }

            // If the player has jumped and is now falling downwards, cast a ray 
            // to check for ground and turn isJumping false if hit.
            if (playerMgmt.myRb.velocity.y < -5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, whatIsWalkable))
                {
                    isJumping = false;
                }
            }
        }
        else
        {
            isGrounded = true;

            // Remove the aerialMovementModifier if it is still affecting _moveSpeed.
            if (playerMgmt.playerStats.moveSpeed.StatModifiers.Contains(playerMgmt.playerStats.aerialMovementModifier))
                playerMgmt.playerStats.moveSpeed.RemoveModifier(playerMgmt.playerStats.aerialMovementModifier);

            // This stops the ground collision from pre-emptively turning isJumping to false when the player jumps.
            if (Mathf.Abs(playerMgmt.myRb.velocity.y) < 0.01f && Mathf.Abs(playerMgmt.myRb.velocity.y) > -0.01f)
                isJumping = false;

            if (isGrounded && !isJumping)
                currentJumps = (int)playerMgmt.playerStats.jumpMultiplier.value;
        }
    }

    #region Sprinting
    public void SprintPressed()
        {
            if (movement.z > 0.1 && playerMgmt.playerStats.GetCurrentStamina()
                - playerMgmt.playerStats.staminaDrainAmount > 0)
            {
                isSprinting = true;
                //playerMgmt.isInteracting = true;

                // adds moveSpeed StatModifier
                playerMgmt.playerStats.moveSpeed.AddModifer(playerMgmt.playerStats.sprintMovementModifier);
            }
        }

    public void SprintReleased()
    {
        isSprinting = false;

        // removes moveSpeed StatModifier
        playerMgmt.playerStats.moveSpeed.RemoveModifier(playerMgmt.playerStats.sprintMovementModifier);
    }

    void UpdateIsSprinting()
    {
        if (isSprinting)
        {
            if (playerMgmt.playerStats.GetCurrentStamina()
                - playerMgmt.playerStats.staminaDrainAmount > 0)
            {
                playerMgmt.playerStats.StaminaDrainOverTime( 
                    playerMgmt.playerStats.staminaDrainAmount,
                    playerMgmt.playerStats.staminaDrainDelay);
            }
            else
            {
                SprintReleased();
                return;
            }
        }
    }
    #endregion

    #region Jumping
    public void Jump()
    {
        if(currentJumps <= 0 && !isGrounded) { return; }
        if (playerMgmt.isInteracting) { return; }
        if (slideMgmt.isSliding) { return; }

        jumpInputHeld = true;

        // Check if player has enough Stamina to perform the jump
        if (playerMgmt.playerStats.GetCurrentStamina() - 10f > 0)
        {
            // if the player is not currently in the air...
            if (!isJumping)
            {
                isJumping = true;
                playerMgmt.playerStats.DamageStamina(10f);
                playerMgmt.myRb.velocity += Vector3.up * playerMgmt.playerStats.jumpForce.value;
                playerMgmt.myRb.velocity += rotationMovement * playerMgmt.playerStats.jumpForce.value/2f;
                currentJumps--;
            }
            // if the player is in the air and jumps...
            else
            {
                playerMgmt.playerStats.DamageStamina(10f);
                playerMgmt.myRb.velocity += Vector3.up * playerMgmt.playerStats.jumpForce.value * 1.25f;
                currentJumps--;
            }
        }
    }

    void JumpReleased()
    {
        jumpInputHeld = false;
    }
    #endregion
}
