using UnityEngine;

public class ShowTutorial : MonoBehaviour
{ public static ShowTutorial instance;


	[SerializeField] private GameObject _tutorialPref;
	private bool _canShow;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
	{
		HideTutorialScreen();
		_canShow = true;
	}

	private void OnEnable() => HideTutorialScreen();
	
	public void ShowTutorialScreen()
	{
		if (_canShow)
		{
			// Shows tutorial window only once
			_tutorialPref.SetActive(true);
			_canShow = false;
		}
		else
		{
			// Loads level 1
			UIManager.instance.ShowLoadingScreen();
			LoadingScreen.instance.LoadLevel(1);
		}
	}
	

	public void CanShowTutorial(bool state) => _canShow = state;
	

	public void HideTutorialScreen() => _tutorialPref.SetActive(false);

}
