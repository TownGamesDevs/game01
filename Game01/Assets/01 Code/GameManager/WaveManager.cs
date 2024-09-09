using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public static event Action OnWaveCompleted;

    [SerializeField] private GameObject[] _zombiePrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _initialZombieCount;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _difficultyMultiplier = 1.2f;

    [SerializeField] private int _totalWaves;
    private int _currentWave;
    private int _totalZombies;
    private bool _waveInProgress;


    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        _currentWave = 0;
        StartNextWave();
    }

    private void StartNextWave()
    {
        if (_currentWave <= _totalWaves)
        {
            _currentWave++;
            _waveInProgress = true;

            // Sets total zombies per wave
            _totalZombies = Mathf.RoundToInt(_initialZombieCount * Mathf.Pow(_difficultyMultiplier, _currentWave - 1));
            
            // Spawns zombie after X time
            StartCoroutine(SpawnZombies(_totalZombies));
        }
    }

    IEnumerator SpawnZombies(int count)
    {
        _totalZombies = count;

        for (int i = 0; i < count; i++)
        {
            Transform rnd_spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject rnd_zombie = _zombiePrefabs[Random.Range(0, _zombiePrefabs.Length)];
            Instantiate(rnd_zombie, rnd_spawnPoint.position, rnd_spawnPoint.rotation);

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void ZombieDefeated()
    {
        _totalZombies--;

        if (_totalZombies <= 0 && _waveInProgress)
        {
            _waveInProgress = false;
            OnWaveCompleted?.Invoke();
            print("WAVE COMPLETED!");
        }
    }






}
