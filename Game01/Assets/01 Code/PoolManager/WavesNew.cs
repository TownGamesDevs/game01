using UnityEngine;

public class WavesNew : MonoBehaviour
{
    public static WavesNew instance;

    [SerializeField] private float _spawnTime; 
    [SerializeField] private float _waitTime = 0.7f;

    private TotalKilled _tk;          
    private SpawnPoints _spawnPoint;  

    private float _timer;            
    private int _currentWave = 1;    
    private int _zombiesToSpawn;    
    private int _zombiesSpawned;

    private bool _waveActive = false;

    private void Awake() => instance ??= this;

    private void Start()
    {
        _timer = 0f;
        _zombiesSpawned = 0;
        _zombiesToSpawn = _currentWave;

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
        // Ensure we haven't reached the maximum zombies on screen
        if (_zombiesSpawned < _zombiesToSpawn)
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnTime)
            {
                _timer = 0f;
                SpawnZombie();
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
        {
            zombie.transform.position = _spawnPoint.GetRandomSpawnPoint();
        }

        _zombiesSpawned++;
    }

    private void CheckWaveCompletion()
    {
        // If all zombies in the wave are killed, start the next wave
        if (_tk.GetTotalKilled() >= _zombiesToSpawn)
        {
            _waveActive = false; 
            _currentWave++;
            IncreaseAbilities.instance.Increase();
            Invoke(nameof(StartNextWave), _waitTime);
        }
    }

    public void StartNextWave()
    {
        _zombiesToSpawn = _currentWave; 
        _zombiesSpawned = 0;            
        _timer = 0f;                  
        _tk.ResetTotalKilled();        
        _waveActive = true;           
    }
}
