using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Gameplay_State);
    }
}
