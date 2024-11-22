using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    public Vector3 GetRandomSpawnPoint()
    {
        if (_spawnPoints.Length > 0)
        {
            int rnd = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[rnd].position;
        }
        return new Vector3(0, 0, 0);
    }
}
