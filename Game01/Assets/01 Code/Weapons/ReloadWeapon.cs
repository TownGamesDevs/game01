using System.Collections;
using TMPro;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{ public static ReloadWeapon instance;

    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _reloadTime;
    [SerializeField] private TextMeshProUGUI[] _ammoTxt;

    private Shooting _shooting;
    private bool _isReloading;
    private int _currentAmmo;

    private void Awake() => instance ??= this;

    private void Start()
    {
        _shooting = GetComponent<Shooting>();
        _currentAmmo = _maxAmmo;
        _isReloading = false;
        PrintAmmo(_ammoTxt, _currentAmmo.ToString());
    }

    private void Update() => CheckCanReload();


    public void CheckCanReload()
    {
        // conditions
        bool noAmmo = _currentAmmo <= 0;
        bool reloadKeyPressed = Input.GetKey(KeyCode.R) && _currentAmmo < _maxAmmo;


        if (noAmmo || reloadKeyPressed && !_isReloading)
        {
            _isReloading = true;
            _shooting.SetCanShoot(false);
            StartCoroutine(Reload(_reloadTime));
        }
    }

    IEnumerator Reload(float time)
    {
        // Change text and play sound
        PrintAmmo(_ammoTxt, "Reloading...");
        AudioManager.instance.PlayOneShot(AudioManager.Category.Weapons, "Reload", "FullReload");

        yield return new WaitForSeconds(time);
        SetMaxAmmo();

        _shooting.SetCanShoot(true);
        _isReloading = false;
    }

    private void SetMaxAmmo()
    {
        _currentAmmo = _maxAmmo;
        PrintAmmo(_ammoTxt, _currentAmmo.ToString());
    }

    private void PrintAmmo(TextMeshProUGUI[] txt, string ammo)
    {
        for (int i = 0; i < txt.Length; i++)
            txt[i].text = ammo.ToString();
    }


    public bool IsReloading() => _isReloading;
    public void DecreaseAmmo()
    {
        _currentAmmo--;
        PrintAmmo(_ammoTxt, _currentAmmo.ToString());
    }

    public void DecreaseReloadTime(float speed) => _reloadTime -= speed;
    public void IncreaseMaxAmmo(int ammo) => _maxAmmo += ammo;

}
