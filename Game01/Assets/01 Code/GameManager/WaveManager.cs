using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{ public static WaveManager instance;
	public static event Action OnWaveCompleted;

	[SerializeField] private Transform[] _spawnPoints;
	[SerializeField] private int _initialZombieCount;
	[SerializeField] private float _spawnInterval;
	[SerializeField] private float _difficultyMultiplier = 1.2f;
	[SerializeField] private int _totalWaves;

	private int _currentWave;
	private int _totalZombies;
	private bool _waveInProgress;

	private void Awake()
	{ if (instance == null) instance = this; }
	private void Start()
	{
        _currentWave = 0;
		StartNextWave();
	}

    private void Update()
    {
		//print("total: " + _totalZombies);
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
			GameObject zombie;

			// Get random spawn point
			Transform rnd_spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

			// Get random zombie
			int rnd_zombie = Random.Range(0, 2);


			// Pool a zombie
			if (rnd_zombie == 0)
				zombie = PoolManager.instance.PoolBruteZombie();
			else
				zombie = PoolManager.instance.PoolWalkerZombie();


			// Set zombie to spawn position
			if (zombie != null)
				zombie.transform.position = new Vector2(rnd_spawnPoint.position.x, rnd_spawnPoint.position.y);


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
		}
	}

	public void AddTotalZombies()
	{ _totalZombies++; }

	public int GetTotalZombies()
	{
		if (_initialZombieCount > 0)
			return _initialZombieCount;

		return -1;
	}
}
