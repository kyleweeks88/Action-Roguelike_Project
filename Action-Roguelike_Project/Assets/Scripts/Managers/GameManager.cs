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
    [SerializeField] GameObject playerObj;

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
                break;
            case GameState.LevelGeneration:
                SceneManager.LoadScene("LvlGen_Test");
                break;
            case GameState.Gameplay_State:
                Transform start = GameObject.FindGameObjectWithTag("Start").transform;
                GameObject player = GameObject.Instantiate(playerObj, start.position, start.rotation);
                break;
            case GameState.Fail_State:
                break;
            case GameState.Success_State:
                break;
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