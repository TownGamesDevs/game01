using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    public Transform GetRandomSpawnPoint()
    {
        if (_spawnPoints.Length > 0)
        {
            int rnd = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[rnd];
        }
        return null;
    }
}
