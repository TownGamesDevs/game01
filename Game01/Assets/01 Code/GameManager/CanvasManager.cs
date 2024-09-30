using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private bool _showMainMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject waveCompleted;


    private void Awake()
    {
        // Subscribes to activate waveCompleted screen when all zombies are eliminated
        WaveManager.OnWaveCompleted += WaveCompleted;
    }

    private void Start()
    { 
        if (_showMainMenu)
            InitializeMainMenu();
    }

    public void InitializeMainMenu()
    {
        TimeManager.instance.SetTime0();
        mainMenu.SetActive(true);
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
        if (waveCompleted.activeSelf != state)
            waveCompleted.SetActive(state);
    }

    public void UpgradeBTN()
    {
        print("Store UI is open...");
    }

    public void NextWave()
    {
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
