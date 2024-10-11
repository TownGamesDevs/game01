using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void SelectLevel(int level)
    {
        if (level <= 0) return;

        switch (level)
        {
            case 1:
                SceneManager.LoadScene("MainLevel");
                break;



            default: print("invalid level inserted");
                break;
        }
    }


}
