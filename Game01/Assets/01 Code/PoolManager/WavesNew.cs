using System;
using UnityEngine;

public class WavesNew : MonoBehaviour
{
    public static WavesNew instance;

    [SerializeField] private float _spawnTime; // Time between spawns in a wave
    [SerializeField] private float _waitTime; // Time between spawns in a wave

    private TotalKilled _tk;          // Tracks killed zombies
    private SpawnPoints _spawnPoint;  // Handles spawn positions

    private float _timer;             // Timer for spacing out spawns
    private int _currentWave = 1;     // Current wave number
    private int _zombiesToSpawn;      // Number of zombies in the current wave
    private int _zombiesSpawned;      // Zombies spawned so far in the wave

    private bool _waveActive = false; // Is the current wave active?

    private void Awake() => instance ??= this;

    private void Start()
    {
        _timer = 0f;
        _zombiesSpawned = 0;
        _zombiesToSpawn = _currentWave; // Start with 1 zombie in wave 1

        _spawnPoint = GetComponent<SpawnPoints>();
        _tk = GetComponent<TotalKilled>();

        if (_spawnPoint == null)
            Debug.LogWarning("Spawn points are null");
    }

    private void Update()
    {
        if (_waveActive)
        {
            UpdateSpawnTimer();
            CheckWaveCompletion();
        }
    }



    private void UpdateSpawnTimer()
    {
        if (_zombiesSpawned < _zombiesToSpawn)
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnTime)
            {
                _timer = 0f;
                SpawnZombie();
                _zombiesSpawned++;
            }
        }
    }

    private void SpawnZombie()
    {
        // Get a zombie from the pool
        GameObject zombie = PoolManager.instance.Pool(PoolData.Type.Walker);

        // Handle potential pool failure
        if (zombie == null)
        {
            Debug.LogWarning("Failed to spawn zombie");
            return;
        }

        // Position the zombie at a random spawn point
        if (_spawnPoint != null)
            zombie.transform.position = _spawnPoint.GetRandomSpawnPoint();
    }

    private void CheckWaveCompletion()
    {
        // If all zombies in the wave are killed, start the next wave
        if (_tk.GetTotalKilled() >= _zombiesToSpawn)
        {
            _waveActive = false; // End the current wave
            _currentWave++;      // Increment wave number
            Invoke(nameof(StartNextWave), _waitTime); // Start the next wave after a delay
        }
    }

    public void StartNextWave()
    {
        _zombiesToSpawn = _currentWave; // Increase zombies per wave
        _zombiesSpawned = 0;           // Reset spawned count
        _timer = 0f;                   // Reset the spawn timer
        _tk.ResetTotalKilled();        // Reset the kill counter
        _waveActive = true;            // Activate the wave
    }
}
