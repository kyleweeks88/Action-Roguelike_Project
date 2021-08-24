using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;

    private void Start()
    {
        UpdateGameState(GameState.MainMenu_State);
    }

    public void UpdateGameState(GameState _newGameState_)
    {
        gameState = _newGameState_;

        switch (_newGameState_)
        {
            case GameState.MainMenu_State:
                break;
            case GameState.Gameplay_State:
                SceneManager.LoadScene("Scene_Test");
                break;
            case GameState.Fail_State:
                break;
            case GameState.Success_State:
                break;
        }

        OnGameStateChanged?.Invoke(_newGameState_);
    }

    public enum GameState
    {
    MainMenu_State,
    Gameplay_State,
    Fail_State,
    Success_State
    }
}