using UnityEngine;

public class TotalKilled : MonoBehaviour
{ public static TotalKilled instance;

    private int _killed;


    private void Awake() => instance ??= this;
    private void Start() => ResetTotalKilled();
    public void IncrementTotalKilled() => _killed++;
    public int GetTotalKilled() => _killed;
    public void ResetTotalKilled() => _killed = 0;

    // Update stats
    //PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);
    //PlayerPrefs.Save();
}
