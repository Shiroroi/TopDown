using UnityEngine;

public class PlayerWeaponHandler : WeaponHandler
{
    protected override void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
            _tryshoot = true;
        if (Input.GetButtonUp("Fire1"))
            _tryshoot = false;
    }
}
