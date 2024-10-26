using UnityEngine;

public class TotalKilled : MonoBehaviour
{
    private int _killed;

    private void Start() => _killed = 0;
    public void AddKilled()
    {
        _killed++;

        // Update stats
        PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);
        PlayerPrefs.Save();
    }
    public int GetTotalKilled() => _killed;
}
