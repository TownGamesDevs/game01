using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private GameObject _areYouSure;
    [SerializeField] private TextMeshProUGUI[] _totalKilledTxt;
    [SerializeField] private TextMeshProUGUI[] _totalShotsTxt;
    [SerializeField] private TextMeshProUGUI[] _highScore;


    private void Start()
    {
        UpdateAllTexts();
        HideAreYouSure();
    }

    private void OnEnable() => UpdateAllTexts();


    public void UpdateTotalKilled()
    {
        int total = PlayerPrefs.GetInt("ZombiesKilled");
        for (int i =0;  i < _totalKilledTxt.Length; i++)
            _totalKilledTxt[i].text = "Enemies Killed: " + total;
        
    }

    public void UpdateTotalShots()
    {
        int total = PlayerPrefs.GetInt("TotalShots");
        for (int i = 0; i < _totalShotsTxt.Length; i++)
            _totalShotsTxt[i].text = "Total shots: " + total;
    }

    public void UpdateHighscore()
    {
        int total = PlayerPrefs.GetInt("Highscore");
        for (int i = 0; i < _highScore.Length; i++)
            _highScore[i].text = "High score: " + total;
    }

    public void ShowAreYouSure() => _areYouSure.SetActive(true);
    public void HideAreYouSure() => _areYouSure?.SetActive(false);

    public void ResetAllStats()
    {
        PlayerPrefs.SetInt("ZombiesKilled",0);
        PlayerPrefs.SetInt("TotalShots", 0);
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.Save();
        UpdateAllTexts();
    }

    public void UpdateAllTexts()
    {
        UpdateTotalKilled();
        UpdateTotalShots();
        UpdateHighscore();
    }
    


}
