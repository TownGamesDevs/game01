using System.Collections.Generic;

[System.Serializable]
public class EnemyGroups
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
public class Wave
{
    public List<EnemyGroups> enemyGroups = new List<EnemyGroups>();
    public float waveDelay = 5f;
    public float spawnInterval = 0.5f; // Spawn interval for this wave
}
