using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedUI;
    [SerializeField] private GameObject _scoreUI;
    [SerializeField] private float _waitTimeLevelCompleted;

    private void Awake() => WaveController.OnWaveCompleted += WaveCompleted;
    

    private void Start() => SetGameUI();
    

    public void SetGameUI()
    {
        _scoreUI.SetActive(true);
        _levelCompletedUI.SetActive(false);
    }

    private void OnDestroy() => WaveController.OnWaveCompleted -= WaveCompleted;
    

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
