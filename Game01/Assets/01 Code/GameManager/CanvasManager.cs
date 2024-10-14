using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private bool _showMainMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject waveCompleted;
    [SerializeField] private float _waitTimeWaveCompleted;

    private void Awake()
    {
        // Subscribes to activate waveCompleted screen when all zombies are eliminated
        WaveManager.OnWaveCompleted += WaveCompleted;
    }

    public void InitializeMainMenu()
    {
        //TimeManager.instance.SetTime0();
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
        StartCoroutine(ShowWaveCompletedUI());
    }


    IEnumerator ShowWaveCompletedUI()
    {
        yield return new WaitForSeconds(_waitTimeWaveCompleted);
        waveCompleted.SetActive(true);
        LevelCompletedScore.instance.ShowScore();
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
