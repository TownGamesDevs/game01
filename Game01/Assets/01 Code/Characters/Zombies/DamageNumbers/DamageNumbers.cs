using System.Collections;
using TMPro;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _damageText;  // Reference to the damage text
    private CanvasGroup _canvasGroup;     // Reference to the CanvasGroup for fading


    private DNSpeed _DN_Speed;
    private DNDuration _DN_Duration;
    private DNFadeOut _DN_FadeOut;
    private float _speed;
    private float _duration;
    private float _fadeOutTime;
    private float _timer;

    private void OnEnable()
    {
        Initialize();
    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _DN_Speed = GetComponent<DNSpeed>();
        _DN_Duration = GetComponent<DNDuration>();
        _DN_FadeOut = GetComponent<DNFadeOut>();
    }
    private void Update() => MoveUp();
    public void SetDM(int damage)
    {
        // Exit if damage is zero or negative
        if (damage <= 0) return;
        
        PrintDamage(damage);

        // Set values
        _speed = _DN_Speed.CalculateSpeed(damage);
        _duration = _DN_Duration.CalculateDuration(damage);
        _fadeOutTime = _DN_FadeOut.CalculateFadeOut(damage);

        // Start the fade and destroy process
        StartCoroutine(FadeAway());
    }



    public void MoveUp()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + _speed * Time.deltaTime);
    }
    private void PrintDamage(int damage)
    {
        for (int i = 0; i < _damageText.Length; i++)
            _damageText[i].text = "+" + damage.ToString();
    }

    private IEnumerator FadeAway()
    {
        // Wait for the duration before fading out
        yield return new WaitForSeconds(_duration);

        _timer = 0f;
        while (_timer < _fadeOutTime)
        {
            _timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, _timer / _fadeOutTime);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
