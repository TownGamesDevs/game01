using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{ public static ReloadWeapon instance;

    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _reloadTime;
    [SerializeField] private TextMeshProUGUI[] _ammoTxt;

    private Shooting _as;
    private bool _isReloading;
    private int _ammo;

    private void Awake() => instance ??= this;

    private void Start()
    {
        _as = GetComponent<Shooting>();
        _ammo = _maxAmmo;
        _isReloading = false;
        PrintAmmo(_ammoTxt, _ammo.ToString());
    }

    private void Update() => CheckCanReload();


    public void CheckCanReload()
    {
        if (!_isReloading && _ammo <= 0 || (Input.GetKey(KeyCode.R) && _ammo < _maxAmmo && !_isReloading))
        {
            _isReloading = true;
            _as.SetCanShoot(false);
            StartCoroutine(ReloadGun(_reloadTime));
        }
    }

    IEnumerator ReloadGun(float time)
    {
        // Change text and play sound
        PrintAmmo(_ammoTxt, "Reloading...");
        AudioManager.instance.PlayOneShot(AudioManager.Category.Weapons, "Reload", "FullReload");

        // Wait some time before weapon is reloaded
        yield return new WaitForSeconds(time);
        Reload();
        _as.SetCanShoot(true);
        _isReloading = false;
    }

    private void Reload()
    {
        _ammo = _maxAmmo;
        PrintAmmo(_ammoTxt, _ammo.ToString());
    }

    private void PrintAmmo(TextMeshProUGUI[] txt, string ammo)
    {
        for (int i = 0; i < txt.Length; i++)
            txt[i].text = ammo.ToString();
    }


    public bool IsReloading() => _isReloading;
    public void DecreaseAmmo()
    {
        _ammo--;
        PrintAmmo(_ammoTxt, _ammo.ToString());
    }

}
