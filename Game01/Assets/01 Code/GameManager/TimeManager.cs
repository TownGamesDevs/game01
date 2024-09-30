using UnityEngine;

public class TimeManager : MonoBehaviour
{ public static TimeManager instance;

    private void Awake()
    { if (instance == null) instance = this; }

    public void SetTime1()
    {
        Time.timeScale = 1f;
    }

    public void SetTime0()
    {
        Time.timeScale = 0f;
    }


}
