using UnityEngine;

public class GameOver : MonoBehaviour
{ public static GameOver instance;

	[SerializeField] private GameObject _player;
	private void Awake() => instance ??= this;
	
	public void GameOverScreen()
	{
		_player.SetActive(false);
		UIController.instance.ShowGameOverUI();
		Time.timeScale = 0;
	}

}
