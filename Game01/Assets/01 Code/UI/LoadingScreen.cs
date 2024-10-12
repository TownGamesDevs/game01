using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	[SerializeField] private GameObject _loadScreen;
	[SerializeField] private GameObject[] _bullets;

	const float MAX_WAIT_TIME = 5f;
	private int _currentBullet;
	private float _time;


	public void LoadLevel(int index)
	{
		UIManager.instance.ShowLoadScreen();
		StartCoroutine(StartLoading(index));
		HideLoadScreen();
	}
	


	IEnumerator StartLoading(int index)
	{
        ResetLoadingBullets();
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

		while (!operation.isDone && _time < MAX_WAIT_TIME)
		{
			_time += Time.deltaTime;
			float progress = Mathf.Clamp01(operation.progress / .9f);
			if ((progress*100) % 10 == 0)
				ShowNextBullet();
		}
		yield return null;
	}

	private void ShowNextBullet()
	{
		_currentBullet++;
		if (_currentBullet < _bullets.Length - 1)
			_bullets[_currentBullet].SetActive(true);
	}

	private void ResetLoadingBullets()
	{
		_currentBullet = 0;
		for (int i = 1;  i < _bullets.Length - 1; i++)
			_bullets[i].SetActive(false);
	}

	public void ShowLoadScreen()
	{
		_loadScreen.SetActive(true);
	}

	public void HideLoadScreen()
	{
		_loadScreen.SetActive(false);
	}


}
