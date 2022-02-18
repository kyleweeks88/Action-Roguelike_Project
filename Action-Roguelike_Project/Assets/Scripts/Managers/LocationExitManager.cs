using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LocationExitManager : MonoBehaviour
{
    [SerializeField] GameScene_SO locationToLoad;
    [SerializeField] bool showLoadingScreen;

    [Header("Broadcasting On...")]
    [SerializeField] LoadEventChannel_SO locationExitLoadChannel;

    bool isLoaded;

    private void OnTriggerEnter(Collider col)
    {
        PlayerManager playerMgmt = col.gameObject.GetComponent<PlayerManager>();
        if (playerMgmt != null)
        {
            //interactingEntity = col.gameObject;
            HandleEntityInput(playerMgmt, true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        PlayerManager playerMgmt = col.gameObject.GetComponent<PlayerManager>();
        if (playerMgmt != null)
        {
            //interactingEntity = col.gameObject;
            HandleEntityInput(playerMgmt, false);
        }
    }

    /// <summary>
    /// Subscribes or Unsubscribes this Weapon Pickup object to an event
    /// on the interacting entity's InputManager.
    /// </summary>
    /// <param name="colObj"></param>
    /// <param name="boolVal"></param>
    void HandleEntityInput(PlayerManager playerMgmt, bool boolVal)
    {
        if (playerMgmt != null)
        {
            if (boolVal)
                playerMgmt.inputMgmt.interactEvent += ExitLocation;

            if (!boolVal)
                playerMgmt.inputMgmt.interactEvent -= ExitLocation;
        }
    }

    void ExitLocation()
    {
        if (isLoaded) { return; }
        // Show loading scene?
        SceneManager.LoadSceneAsync("Scene_"+locationToLoad.sceneName, LoadSceneMode.Additive);
        isLoaded = true;
        // Deactivate current scene?
        LevelGenerator.instance.gameObject.SetActive(false);
        // Place player in new scene
        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;
    }
}
