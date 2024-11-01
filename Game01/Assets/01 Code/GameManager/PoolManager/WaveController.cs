using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyTypes;

public class WaveController : MonoBehaviour
{
    public static WaveController instance; public static event Action OnWaveCompleted;


    [Header("Group Configuration")]
    [SerializeField] private int _maxOnScreen;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _delayAfterKilled;
    [SerializeField] private List<WaveGroups> _groups = new List<WaveGroups>();

    private int _totalSpawned;
    private int _currentInScreen;
    private float _timer;
    private int _totalZombies;
    private int _currentGroup;
    private SpawnPoints _spawn;
    private TotalKilled _enemyKilled;


    private void Awake() => instance ??= this;

    private void Start()
    {
        _spawn = GetComponent<SpawnPoints>();
        _enemyKilled = GetComponent<TotalKilled>();
        _totalZombies = CalculateTotalZombies();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnDelay && _currentInScreen < _maxOnScreen && _totalSpawned < _totalZombies)
        {
            _timer = 0;
            SpawnZombie(_currentGroup);
        }
    }
    private void SpawnZombie(int waveIndex)
    {
        WaveGroups _wave = _groups[waveIndex];

        // Spawn all the enemies in the group
        foreach (EnemyTypes group in _wave.enemyGroups)
        {
            // Try spawn zombie
            GameObject enemy = PoolManager.instance.Pool(group.enemyType == EnemyType.Walker ? PoolData.Type.Walker : PoolData.Type.Brute);
            if (enemy == null) return;

            _currentInScreen++;
            _totalSpawned++;
            enemy.transform.position = _spawn.GetRandomSpawnPoint();

        }
    }

    public void UpdateZombiesOnScreen()
    {
        _currentInScreen--;
        //if (_totalSpawned < _totalZombies)
        //    StartCoroutine(SpawnAfterDeath(_delayAfterKilled));
    }

    IEnumerator SpawnAfterDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnZombie(_currentGroup);
    }



    private int CalculateTotalZombies()
    {
        int total = 0;
        foreach (WaveGroups wave in _groups)
            foreach (EnemyTypes group in wave.enemyGroups)
                total += group.count;

        return total;
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