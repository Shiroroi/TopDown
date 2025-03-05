using UnityEngine;
using UnityEngine.UI;

public class UIreloadIndicator : MonoBehaviour
{
    private Image _reloadBar;
    private WeaponHandler playerWeaponHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _reloadBar = GetComponent<Image>();

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");

        if (playerGO == null)
            return;

        playerWeaponHandler = playerGO.GetComponent<WeaponHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWeaponHandler == null)
            return ;

        if (playerWeaponHandler.CurrentWeapon == null)
            return;

        if (playerWeaponHandler.CurrentWeapon.CurrentBulletCount > 0)
        {
            float currentBulletCount = playerWeaponHandler.CurrentWeapon.CurrentBulletCount;
            float maxBulletCount = playerWeaponHandler.CurrentWeapon.MaxBulletCount;

            float bulletLeftFill = currentBulletCount / maxBulletCount;
            float bulletRightFill = 

            if (_reloadBar != null)
            {
                _reloadBar.fillAmount = bulletLeftFill;
                
            }

            if (_reloadBar == null)
            {
                
            }

        }

       
    }
}
