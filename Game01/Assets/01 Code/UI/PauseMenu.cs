using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool _isPaused = false;
    private bool _canPause = true;

    private void Awake() => WaveController.OnWaveCompleted += DisablePause;
    private void Start()
    {
        pauseMenu.SetActive(false);
        _canPause = true;
        ResumeGame();
    }
    private void OnDestroy() => WaveController.OnWaveCompleted -= DisablePause;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _canPause)
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void DisablePause() => _canPause = false;
    

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;  // Pauses the game
        _isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;  // Resumes the game
        _isPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.StopAll();
        SceneManager.LoadScene(0);
        UIManager.instance.ShowMainMenu();
        Time.timeScale = 1f;
        _isPaused = false;
    }
}
