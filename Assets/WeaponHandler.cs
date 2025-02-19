using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Weapon CurrentWeapon;
    public Transform GunPosition;

    protected bool _tryshoot = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        HandleInput();
        HandleWeapon();

    }

    protected virtual void HandleInput()
    {

    }

    protected virtual void HandleWeapon()
    {
        if (CurrentWeapon == null || GunPosition == null)
            return;

        CurrentWeapon.transform.position = GunPosition.position;
        CurrentWeapon.transform.rotation = GunPosition.rotation;
    }

    public void EquipWeapon(GameObject equipWeapon)
    {
        if (equipWeapon == null) return;

        if (CurrentWeapon != equipWeapon)
        {
            Destroy(CurrentWeapon.gameObject);
        }


        GameObject _weaponGO = GameObject.Instantiate(equipWeapon, GunPosition);
        Weapon weapon = _weaponGO.GetComponent<Weapon>();

        if (weapon == null)
            return;

        CurrentWeapon = weapon;
    }

}
