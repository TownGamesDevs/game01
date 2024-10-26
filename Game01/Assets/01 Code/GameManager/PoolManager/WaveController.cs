using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyTypes;

public class WaveController : MonoBehaviour
{
    public static WaveController instance;
    public static event Action OnWaveCompleted;


    [Header("Group Configuration")]
    [SerializeField] private List<WaveGroups> _groups = new List<WaveGroups>();

    private int _totalZombies;
    private int _currentGroup = 0;

    private SpawnPoints _spawn;
    private TotalKilled _enemyKilled;

    private void Awake() => instance ??= this;

    private void Start()
    {
        _spawn = GetComponent<SpawnPoints>();
        _enemyKilled = GetComponent<TotalKilled>();

        _totalZombies = CalculateTotalZombies();
        if (_groups.Count > 0)
            StartCoroutine(SpawnGroup(_currentGroup));
    }

    private int CalculateTotalZombies()
    {
        int total = 0;
        foreach (WaveGroups wave in _groups)
            foreach (EnemyTypes group in wave.enemyGroups)
                total += group.count;

        return total;
    }
    IEnumerator SpawnGroup(int waveIndex)
    {
        if (waveIndex >= _groups.Count)
        {
            Debug.Log("All waves complete!");
            yield break;
        }

        WaveGroups wave = _groups[waveIndex];

        // Spawn all the enemies in the group
        foreach (EnemyTypes group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                // Get a random spawn point
                Transform spawnPoint = _spawn.GetRandomSpawnPoint();

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
    public void CheckAllKilled()
    {
        _enemyKilled.AddKilled();

        // All killed?
        if (_enemyKilled.GetTotalKilled() >= _totalZombies)
            OnWaveCompleted?.Invoke();
    }
    public int GetTotalZombies() => _totalZombies;
}




[System.Serializable]
public class EnemyTypes
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
public class WaveGroups
{
    public List<EnemyTypes> enemyGroups = new List<EnemyTypes>();
    public float _spawnDelay = 0.5f; // Spawn interval for this wave
    public float _nextWaveDelay = 5f;
}