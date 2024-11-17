using UnityEngine;

public class TotalKilled : MonoBehaviour
{
    private int _killed;

    private void Start() => ResetTotalKilled();
    public void IncrementTotalKilled()
    {
        _killed++;

        // Update stats
        PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);
        PlayerPrefs.Save();
    }
    public int GetTotalKilled() => _killed;

    public void ResetTotalKilled() => _killed = 0;
}
