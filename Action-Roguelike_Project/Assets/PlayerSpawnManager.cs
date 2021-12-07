using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject playerPrefab;
    public GameObject playerObj;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    void Start()
    {
        playerPrefab = gameManager.playerPrefab;
        playerObj = Instantiate(gameManager.playerPrefab, Vector3.zero, Quaternion.identity);
        playerObj.SetActive(false);
    }
}
