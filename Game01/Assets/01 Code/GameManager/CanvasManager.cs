using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedUI;
    [SerializeField] private GameObject _scoreUI;
    //[SerializeField] private GameObject _pauseUI;

    [SerializeField] private float _waitTimeLevelCompleted;

    private void Awake() => WaveManager.OnWaveCompleted += WaveCompleted;
    

    private void Start() => SetGameUI();
    

    public void SetGameUI()
    {
        _scoreUI.SetActive(true);
        _levelCompletedUI.SetActive(false);
        //_pauseUI.SetActive(false);
    }

    private void OnDestroy() => WaveManager.OnWaveCompleted -= WaveCompleted;
    

    private void WaveCompleted() => SetWaveCompleted(true);
   

    private void SetWaveCompleted(bool state) => StartCoroutine(ShowWaveCompletedUI());
    


    IEnumerator ShowWaveCompletedUI()
    {
        yield return new WaitForSeconds(_waitTimeLevelCompleted);
        _levelCompletedUI.SetActive(true);
        LevelCompletedScore.instance.ShowScore();
    }

    public void NextWave()
    {
        SceneManager.LoadScene("SampleScene");
        print("Next wave...");
    }
}
