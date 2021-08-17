using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave 
    {
        public int spawnCount;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public CharacterStats goToSpawn_PF;

    Wave currentWave;
    int currentWaveNumber;

    int goRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    private void Start()
    {
        NextWave();
    }

    private void Update()
    {
        if(goRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            goRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            CharacterStats spawnedGO = Instantiate(goToSpawn_PF, transform.position, transform.rotation) as CharacterStats;
            spawnedGO.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if(enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            goRemainingToSpawn = currentWave.spawnCount;
            enemiesRemainingAlive = goRemainingToSpawn;
        }
    }
}
