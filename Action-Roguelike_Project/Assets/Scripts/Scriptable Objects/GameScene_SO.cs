using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used for containing data about Scenes
/// </summary>
[CreateAssetMenu(fileName = "New Game Scene", menuName = "Game Scene Scriptable Object")]
public class GameScene_SO : ScriptableObject
{
    public enum GameSceneType
    {
        //MainMenu,
        //HubRoom,
        LevelGenerator,
        EventRoom
        //EndRoom
    }

    public GameSceneType sceneType;
    public string sceneName;
}
