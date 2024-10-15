using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyGroups;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public static event Action OnWaveCompleted;

    [SerializeField] private Transform[] _spawnPoints;

    [Header("General Configuration")]
    [SerializeField] private float _spawnInterval;
    private int _totalZombies;  // Now calculated dynamically
    private int _totalKilled;
    private int _tmpKilled;

    [Header("Waves Configuration")]
    public List<Wave> waves = new List<Wave>();
    private int _currentWave = 0;

    [Header("Random Wave Configuration")]
    public RandomWaveConfig randomWaveConfig; // Random wave settings

    private void Awake() => instance ??= this;

    private void Start()
    {
        _totalKilled = 0;
        _totalZombies = 0;
        // Calculate total zombies across all waves
        CalculateTotalZombies();

        // Start with a random function (either spawn wave or random waves)
        if (waves.Count > 0)
            StartCoroutine(RandomSpawn());
    }

    // Method to calculate total zombies
    private void CalculateTotalZombies()
    {
        _totalZombies = 0;  // Reset the count
        foreach (Wave wave in waves)
            foreach (EnemyGroups group in wave.enemyGroups)
                _totalZombies += group.count;  // Add the number of enemies in each group
    }

    // Function that randomly chooses between spawning a wave or generating a random wave
    IEnumerator RandomSpawn()
    {
        while (_currentWave < waves.Count)  // Loop until all waves are spawned
        {
            // Randomly decide which spawn method to use
            if (Random.Range(0, 2) == 0)
            {
                yield return StartCoroutine(SpawnWave(_currentWave));
                _currentWave++;
            }
        }
    }

    IEnumerator SpawnWave(int waveIndex)
    {
        if (waveIndex >= waves.Count)
        {
            Debug.Log("All waves complete!");
            yield break;  // Stop the coroutine when all waves are done
        }

        Wave wave = waves[waveIndex];

        // Spawn all the enemies in the wave
        foreach (var group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                // Get a random spawn point
                Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

                // Spawn the enemy of this type from the pool
                GameObject enemy = PoolManager.instance.Pool(group.enemyType == EnemyType.Walker
                                                   ? PoolData.Type.Walker
                                                   : PoolData.Type.Brute);

                if (enemy != null)
                    enemy.transform.position = spawnPoint.position;
                

                yield return new WaitForSeconds(wave.spawnInterval);  // Use the specific spawn interval for this wave
            }
        }

        // Wait for the delay before the next wave
        yield return new WaitForSeconds(wave.waveDelay);
    }

    // Function to generate and spawn a random wave based on configuration
    IEnumerator SpawnRandomWave()
    {
        int zombiesToSpawn = Random.Range(randomWaveConfig.minZombies, randomWaveConfig.maxZombies);

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            // Randomly select an enemy type
            PoolData.Type enemyType = Random.Range(0, 2) == 0 ? PoolData.Type.Walker : PoolData.Type.Brute;

            // Get a random spawn point
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            // Spawn the enemy from the pool
            GameObject enemy = PoolManager.instance.Pool(enemyType);

            if (enemy != null)
                enemy.transform.position = spawnPoint.position;
            

            // Wait for the configured random spawn interval
            yield return new WaitForSeconds(Random.Range(randomWaveConfig.minSpawnInterval, randomWaveConfig.maxSpawnInterval));
        }
    }

    public void ZombieKilled()
    {
        _totalKilled++;

        if (_totalKilled >= _totalZombies)
        {
            OnWaveCompleted?.Invoke();
            _tmpKilled = _totalKilled;
            _totalKilled = 0;
        }
    }

    public int GetTotalKilled() => _tmpKilled;
    public int GetTotalZombies() => _totalZombies;
}





[Serializable]
public class RandomWaveConfig
{
    [Header("Random Wave Settings")]
    public int minZombies = 3;  // Minimum number of zombies per random wave
    public int maxZombies = 15;  // Maximum number of zombies per random wave
    public float minSpawnInterval = 0.15f;  // Minimum time between spawns in a random wave
    public float maxSpawnInterval = 2f;  // Maximum time between spawns in a random wave
}
