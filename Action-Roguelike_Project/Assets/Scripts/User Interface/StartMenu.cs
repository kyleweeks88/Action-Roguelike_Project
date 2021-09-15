using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.UpdateGameState(GameManager.GameState.LevelGeneration);
    }
}
