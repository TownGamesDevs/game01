using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _MainMenu;
    [SerializeField] private GameObject _waveCompleted;

    private void Awake()
    {
        // Subscribes to activate waveCompleted screen when all zombies are eliminated
        WaveManager.OnWaveCompleted += WaveCompleted;
    }

    private void OnDestroy()
    {
        // Destroy subscripiton
        WaveManager.OnWaveCompleted -= WaveCompleted;
    }

    private void WaveCompleted()
    {
        SetWaveCompleted(true);
    }

    private void SetWaveCompleted(bool state)
    {
        if (_waveCompleted.activeSelf != state)
            _waveCompleted.SetActive(state);
    }

    public void UpgradeBTN()
    {
        print("Store UI is open...");
    }

    public void NextWave()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
        print("Next wave...");
    }

    public void StartNewGame()
    {
        print("New game started...");
    }

    public void OptionsUI()
    {
        print("Options UI is open...");
    }
}
