using UnityEngine;

public class UIController : MonoBehaviour
{ public static UIController instance;


    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _scoreUI;
    [SerializeField] private GameObject _healthUI;
    [SerializeField] private GameObject _levelCompletedUI;


    private void Awake() => instance ??= this;

    private void Start() => ShowGameUI();
    
    public void ShowLevelCompletedUI()
    {
        _levelCompletedUI.SetActive(true);

        _gameOverUI.SetActive(false);
        _pauseUI.SetActive(false);
        _scoreUI.SetActive(false);
        _healthUI.SetActive(false);
    }
    public void ShowGameOverUI()
    {
        _gameOverUI.SetActive(true);

        _pauseUI.SetActive(false);
        _scoreUI.SetActive(false);
        _healthUI.SetActive(false);
        _levelCompletedUI.SetActive(false);
    }

    public void ShowPauseUI()
    {
        _pauseUI.SetActive(true);

        _gameOverUI.SetActive(false);
        _scoreUI.SetActive(false);
        _healthUI.SetActive(false);
        _levelCompletedUI.SetActive(false);
    }

    public void ShowScoreUI(bool state) => _scoreUI.SetActive(state);
    public void ShowHealthUI(bool state) => _healthUI.SetActive(state);

    public void ShowGameUI()
    {
        _scoreUI.SetActive(true);
        _healthUI.SetActive(true);

        _gameOverUI.SetActive(false);
        _pauseUI.SetActive(false);
        _levelCompletedUI.SetActive(false);
    }

}
