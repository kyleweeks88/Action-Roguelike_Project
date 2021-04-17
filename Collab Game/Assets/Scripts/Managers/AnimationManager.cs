using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] PlayerManager playerMgmt;

    #region Animator Parameters
    // My Animator parameters turned from costly Strings to cheap Ints
    [HideInInspector] public int isSprintingParam = Animator.StringToHash("isSprinting");
    int isJumpingParam = Animator.StringToHash("isJumping");
    int isGroundedParam = Animator.StringToHash("isGrounded");
    int yVelocityParam = Animator.StringToHash("yVelocity");
    [HideInInspector] public int inputXParam = Animator.StringToHash("InputX");
    [HideInInspector] public int inputYParam = Animator.StringToHash("InputY");
    int inCombatParam = Animator.StringToHash("inCombat");
    int isInteractingParam = Animator.StringToHash("isInteracting");
    int isSlidingParam = Animator.StringToHash("isSliding");
    int isBlockingParam = Animator.StringToHash("isBlocking");
    int moveAnimSpeedParam = Animator.StringToHash("moveAnimSpeed");
    #endregion


    void Update()
    {
        animator.SetFloat(moveAnimSpeedParam, 1f);
        animator.SetBool(isBlockingParam, playerMgmt.combatMgmt.isBlocking);
        animator.SetBool(isSlidingParam, playerMgmt.playerMovement.isSliding);
        animator.SetBool(isSprintingParam, playerMgmt.playerMovement.isSprinting);
        animator.SetBool(isJumpingParam, playerMgmt.playerMovement.isJumping);
        animator.SetBool(isGroundedParam, playerMgmt.playerMovement.isGrounded);
        animator.SetFloat(yVelocityParam, playerMgmt.myRb.velocity.y);
        animator.SetBool(inCombatParam, playerMgmt.combatMgmt.inCombat);

        if (playerMgmt.combatMgmt.attackInputHeld)
        {
            if (playerMgmt.equipmentMgmt.currentlyEquippedWeapon != null &&
                !playerMgmt.equipmentMgmt.currentlyEquippedWeapon.weaponData.isChargeable)
            {
                MeleeWeapon myWeapon = playerMgmt.equipmentMgmt.currentlyEquippedWeapon as MeleeWeapon;
                if((playerMgmt.playerStats.GetCurrentStamina() - myWeapon.meleeData.staminaCost) > 0)
                    animator.SetTrigger(playerMgmt.combatMgmt.attackAnim);
            }
        }

        if (playerMgmt.combatMgmt.attackInputHeld)
        {
            animator.SetFloat("moveAnimSpeed", 0.5f);
        }
        else
        {
            animator.SetFloat("moveAnimSpeed", 1f);
        }
    }

    /// <summary>
    /// Used to set the determined AnimatorOverrideController
    /// </summary>
    /// <param name="overrideCtrl"></param>
    public void SetAnimation(AnimatorOverrideController overrideCtrl)
    {
        animator.runtimeAnimatorController = overrideCtrl;
    }

    public void MovementAnimation(float xMove, float zMove)
    {
        animator.SetFloat(inputXParam, xMove);
        animator.SetFloat(inputYParam, zMove);
    }

    public void HandleMeleeAttackAnimation(bool boolVal)
    {
        animator.SetBool(playerMgmt.combatMgmt.attackAnim, boolVal);
    }

    public void HandleRangedAttackAnimation(bool boolVal)
    {
        animator.SetBool(playerMgmt.combatMgmt.attackAnim, boolVal);
    }
}
