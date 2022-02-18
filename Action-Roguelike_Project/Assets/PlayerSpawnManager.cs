using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Instantiates the character from the GameManager's playerprefab field.
/// sets the player to inactive.
/// </summary>
public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager instance;
    GameManager gameManager;
    public GameObject playerPrefab;
    public GameObject playerObj;

    bool playerSpawned;
    [SerializeField] VoidEventChannel_SO eventChannel;

    // !!! TESTING
    [SerializeField] Transform spawnLoc;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        gameManager = GameManager.instance;
    }

    void Start()
    {
        eventChannel.OnEventRaised += SpawnPlayer;
    }

    void SpawnPlayer()
    {
        if (playerSpawned) { return; }

        playerPrefab = gameManager.playerPrefab;
        playerObj = Instantiate(gameManager.playerPrefab, spawnLoc.position, spawnLoc.rotation);
        playerObj.SetActive(true);
        playerSpawned = true;
    }    
}
