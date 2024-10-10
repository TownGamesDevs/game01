using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject options;
   //[SerializeField] private GameObject waveUI;

    private void Start()
    { ShowMainMenu(); }

    public void StartGame()
    {
        // Load game scene
    }
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        //waveUI.SetActive(false);
    }
    public void HideMainMenu()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        //waveUI.SetActive(true);
    }
    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        //waveUI.SetActive(false);
    }
    public void BackToMainMenu()
    {
        ShowMainMenu();
    }
}
