using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour /*Singleton<GameManager>*/
{
    public static GameManager instance;

    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;

    // TESTING
    public GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

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
                SceneManager.LoadSceneAsync("StartGame", LoadSceneMode.Additive);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Gameplay_State:
                SceneManager.UnloadSceneAsync("StartGame");
                SceneManager.LoadSceneAsync("Scene_GlobalGameplay", LoadSceneMode.Additive);
                // This will eventually load the hub scene before the level gen scene
                SceneManager.LoadSceneAsync("LvlGen_Test", LoadSceneMode.Additive);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            //case GameState.LevelGeneration:
            //    break;
            //case GameState.Fail_State:
            //    break;
            //case GameState.Success_State:
            //    break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_newGameState_), _newGameState_, null);
        }

        OnGameStateChanged?.Invoke(_newGameState_);
    }

    public enum GameState
    {
        MainMenu_State,
        Gameplay_State,
        Fail_State,
        Success_State,
        LevelGeneration
    }
}