using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Load Event Channel")]
public class LoadEventChannel_SO : ScriptableObject
{
    public UnityAction<GameScene_SO, bool> OnLoadingRequested;

    public void RaiseEvent(GameScene_SO locationToLoad, bool showLoadingScreen)
    {
        if(OnLoadingRequested != null)
        {
            OnLoadingRequested.Invoke(locationToLoad, showLoadingScreen);
        }
        else
        {
            Debug.Log("A Scene loading was requested, but nothing picked it up!" +
                "Check why there is no SceneLoader already present, " +
                "and make sure it's listening on this Load Event Channel. ");
        }
    }
}
