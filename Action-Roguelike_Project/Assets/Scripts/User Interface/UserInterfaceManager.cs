using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] GameObject menuInterface;
    [SerializeField] Canvas menuInt;
    [SerializeField] PlayerManager playerMgmt;
    public LayerMask uiMask;
    int oldMask = 0;

    void Start()
    {
        oldMask = playerMgmt.myCamera.cullingMask;
        playerMgmt.inputMgmt.userInterfaceEvent += OnUserInterface;
        playerMgmt.inputMgmt.pauseEvent += PauseGame;
    }

    void OnUserInterface()
    {
        if(!menuInt.enabled)
        {
            playerMgmt.inputMgmt.EnableUserInterfaceInput();
            playerMgmt.myCamera.cullingMask = uiMask;
            playerMgmt.uiCamera.m_Priority = 11;

            menuInt.enabled = true;
        }
        else
        {
            playerMgmt.inputMgmt.EnableGameplayInput();
            playerMgmt.myCamera.cullingMask = oldMask;
            playerMgmt.uiCamera.m_Priority = 8;

            menuInt.enabled = false;
        }
        //if (!menuInterface.activeInHierarchy)
        //{
        //    playerMgmt.inputMgmt.EnableUserInterfaceInput();
        //    playerMgmt.myCamera.cullingMask = uiMask;
        //    playerMgmt.uiCamera.m_Priority = 11;

        //    menuInterface.SetActive(true);
        //}
        //else
        //{
        //    playerMgmt.inputMgmt.EnableGameplayInput();
        //    playerMgmt.myCamera.cullingMask = oldMask;
        //    playerMgmt.uiCamera.m_Priority = 8;

        //    menuInterface.SetActive(false);
        //}
    }

    void PauseGame()
    {
        // BRING UP A PAUSE MENU UI
        // PAUSE GAME
    }
}
