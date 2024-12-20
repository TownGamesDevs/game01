using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public static event Action OnWaveCompleted;


    [SerializeField] private int _totalZombies;
    [SerializeField, Range(0.1f, 1f)] private float _spawnDelay;

    private int _counter;
    private float _timer;
    private SpawnPoints _spawnPoint;
    private TotalKilled _killed;
    private bool _canSpawnZombies; // flag

    private void Awake() => instance ??= this;

    private void Start()
    {
        _canSpawnZombies = false;
        _spawnPoint = GetComponent<SpawnPoints>();
        _killed = GetComponent<TotalKilled>();
    }


    private void Update()
    {
        _timer += Time.deltaTime;

        bool spawnTime = _timer >= _spawnDelay;
        bool totalSpawned = _counter < _totalZombies;

        if (spawnTime && totalSpawned)
        {
            _timer = 0;
            if (_canSpawnZombies)
                SpawnZombie();
        }
    }

    public void EnableZombieSpawn() => _canSpawnZombies = true;

    private void SpawnZombie()
    {
        GameObject zombie = PoolManager.instance.Pool(PoolTypes.Type.Walker);
        if (zombie == null) return;

        _counter++;
        zombie.transform.position = _spawnPoint.GetRandomSpawnPoint();
    }


    public void CheckAllKilled()
    {
        _killed.IncrementTotalKilled();

        // All killed?
        if (_killed.GetTotalKilled() >= _totalZombies)
            OnWaveCompleted?.Invoke();
    }
    public int GetTotalZombies() => _totalZombies;
}
