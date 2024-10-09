using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{


    public void Play()
    {
        SceneManager.LoadScene("MainLevel");
    }


}
