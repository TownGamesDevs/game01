using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public static event Action OnWaveCompleted;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _totalZombies;
    [SerializeField] private float _spawnInterval;

    private int _totalKilled = 0;

    private void Awake() => instance ??= this;
    private void Start() => StartCoroutine(SpawnZombies());

    IEnumerator SpawnZombies()
    {
        for (int i = 0; i < _totalZombies;)
        {
            // Randomly select a spawn point and zombie type
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            GameObject zombie = Random.Range(0, 2) == 0
                                ? PoolManager.instance.Pool(PoolData.Type.Brute)
                                : PoolManager.instance.Pool(PoolData.Type.Walker);

            // Set zombie to spawn position if pooled successfully
            if (zombie != null)
            {
                zombie.transform.position = spawnPoint.position;
                i++;
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }


    public void ZombieKilled()
    {
        _totalKilled++;

        if (_totalKilled >= _totalZombies)
            OnWaveCompleted?.Invoke();
    }

    public int GetTotalKilled() => _totalKilled;
    public int GetTotalZombies() => _totalZombies;


}
