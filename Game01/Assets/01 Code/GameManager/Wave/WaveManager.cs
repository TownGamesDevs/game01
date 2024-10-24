using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyGroups;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{ public static WaveManager instance;

    public static event Action OnWaveCompleted;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] _spawnPoints;
    [Header("Waves Configuration")]
    [SerializeField] private List<Wave> _groups = new List<Wave>();

    private int _totalZombies;
    private int _totalKilled;
    private int _tmpKilled;
    private int _currentWave = 0;

    private void Awake() => instance ??= this;

    private void Start()
    {
        _totalKilled = 0;
        _totalZombies = CalculateTotalZombies();

        if (_groups.Count > 0)
            StartCoroutine(SpawnWave(_currentWave));
    }

    private int CalculateTotalZombies()
    {
        int total = 0;
        foreach (Wave wave in _groups)
            foreach (EnemyGroups group in wave.enemyGroups)
                total += group.count;

        return total;
    }

    IEnumerator SpawnWave(int waveIndex)
    {
        if (waveIndex >= _groups.Count)
        {
            Debug.Log("All waves complete!");
            yield break;
        }

        Wave wave = _groups[waveIndex];

        // Spawn all the enemies in the group
        foreach (EnemyGroups group in wave.enemyGroups)
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

                // Use the specific spawn interval for this wave
                yield return new WaitForSeconds(wave._spawnDelay);
            }
        }

        // Wait for the delay before the next wave
        yield return new WaitForSeconds(wave._nextWaveDelay);
    }

    public void ZombieKilled()
    {
        _totalKilled++;

        // Total zombies killed
        PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);
        PlayerPrefs.Save();


        // All killed?
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

[System.Serializable]
public class EnemyGroups
{
    public enum EnemyType
    {
        Walker,
        Brute
    }

    public EnemyType enemyType;
    public int count;
}

[System.Serializable]
public class Wave
{
    public List<EnemyGroups> enemyGroups = new List<EnemyGroups>();
    public float _spawnDelay = 0.5f; // Spawn interval for this wave
    public float _nextWaveDelay = 5f;
}