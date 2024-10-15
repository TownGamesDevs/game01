using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{ 

    [SerializeField] private GameObject[] _bullets;
    const float _delay = 0.3f;


    private void Start()
    {
        // Don't destroy the loading screen when switching scenes
        DontDestroyOnLoad(gameObject);

        // Ensure all bullet images are inactive at the start
        foreach (GameObject bullet in _bullets)
            bullet.SetActive(false);
    }


    public void LoadLevel(int index)
    {   // Called with UI button
        StartCoroutine(StartLoading(index));
    }


    IEnumerator StartLoading(int index)
    {
        // Show the loading screen
        UIManager.instance.ShowLoadingScreen();

        // Begin loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;  // Prevent scene activation until fully ready

        while (!operation.isDone)
        {
            // The progress goes from 0.0 to 0.9, so we map it to 100% by dividing by 0.9
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            int bulletsToActivate = Mathf.FloorToInt(progress * 10);  // Each 10% activates one bullet

            // Activate bullets based on progress
            for (int i = 0; i < bulletsToActivate; i++)
            {
                _bullets[i].SetActive(true);
            }

            // When the progress reaches 90% (0.9 in AsyncOperation), allow the scene activation
            if (operation.progress >= 0.9f)
            {
                // Activate all bullets just before scene activation
                for (int i = 0; i < _bullets.Length; i++)
                {
                    _bullets[i].SetActive(true);
                }

                // Add a slight delay to show the full loading UI, then activate the scene
                yield return new WaitForSeconds(_delay);  // Optional: Adjust as needed
                operation.allowSceneActivation = true;
            }

            yield return null;  // Continue in the next frame
        }

        // Hide the loading screen after the scene has fully loaded
        UIManager.instance.HideLoadingScreen();
    }

}
