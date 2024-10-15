using TMPro;
using UnityEngine;

public class TotalStarsEarned : MonoBehaviour
{ public static TotalStarsEarned instance;

    [SerializeField] private TextMeshProUGUI[] _txt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        
    }
    public void TotalStars(int num)
    {
        for (int i = 0; i < num; i++)
        {
            _txt[i].text = num.ToString() + " /18";
        }
    }


}
