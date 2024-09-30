using TMPro;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{ public static ErrorManager instance;


    [SerializeField] private TextMeshProUGUI errorTxt;
    [SerializeField] private GameObject errorManagerGameObject;


    private void Awake()
    { if (instance == null) instance = this; }

    private void Start()
    { errorManagerGameObject.SetActive(false); }

    public void PrintError(string txt)
    {
        errorManagerGameObject.SetActive(true);
        TimeManager.instance.SetTime0();
        errorTxt.text = "Error: " + txt;
        errorManagerGameObject.SetActive(true);
    }

    public void PrintWarning(string txt)
    {
        errorManagerGameObject.SetActive(true);
        TimeManager.instance.SetTime0();
        errorTxt.text = "Warning: " + txt;
        errorManagerGameObject.SetActive(true);
    }

    public void CloseErrorWindow()
    {
        TimeManager.instance.SetTime1();
        errorManagerGameObject.SetActive(false);
        errorTxt.text = "";
    }


}
