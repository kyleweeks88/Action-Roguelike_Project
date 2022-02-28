using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EventSceneExitManager : LocationExitManager
{
    [SerializeField] VoidEventChannel_SO roomComplete_Channel;
    //[SerializeField] GameScene_SO locationToLoad;
    //[SerializeField] bool showLoadingScreen;

    //[Header("Broadcasting On...")]
    //[SerializeField] LoadEventChannel_SO locationExitLoadChannel;

    //bool isLoaded;

    //private void OnTriggerEnter(Collider col)
    //{
    //    PlayerManager playerMgmt = col.gameObject.GetComponent<PlayerManager>();
    //    if (playerMgmt != null)
    //    {
    //        //interactingEntity = col.gameObject;
    //        HandleEntityInput(playerMgmt, true);
    //    }
    //}

    //private void OnTriggerExit(Collider col)
    //{
    //    PlayerManager playerMgmt = col.gameObject.GetComponent<PlayerManager>();
    //    if (playerMgmt != null)
    //    {
    //        //interactingEntity = col.gameObject;
    //        HandleEntityInput(playerMgmt, false);
    //    }
    //}

    /// <summary>
    /// Subscribes or Unsubscribes this Weapon Pickup object to an event
    /// on the interacting entity's InputManager.
    /// </summary>
    /// <param name="colObj"></param>
    /// <param name="boolVal"></param>
    //void HandleEntityInput(PlayerManager playerMgmt, bool boolVal)
    //{
    //    if (playerMgmt != null)
    //    {
    //        if (boolVal)
    //            playerMgmt.inputMgmt.interactEvent += ExitLocation;

    //        if (!boolVal)
    //            playerMgmt.inputMgmt.interactEvent -= ExitLocation;
    //    }
    //}

    public override void ExitLocation()
    {
        // Deactivate player
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        // Reactivate LvlGen root object
        LevelGenerator.instance.gameObject.SetActive(true);
        // Continute LvlGen generation from EventRoom "exit"
        roomComplete_Channel.RaiseEvent();
        // Unload the current Event Room Scene.
        SceneManager.UnloadSceneAsync("Scene_EventRoom_01");
        // Show loading scene. Wait until LvlGen has placed the next EventRoomConn
        // then raise event?

        // Reactivate and place Player back at "exit" of EventRoom
        // DOING THIS MANUALLY RIGHT NOW HEHEHEHE

        //base.ExitLocation();
    }
}
