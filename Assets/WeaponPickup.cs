using UnityEngine;

public class WeaponPickup : PickUp
{
    public GameObject Weapon;

    protected override void PickedUp(Collider2D collision)
    {
        if (Weapon == null)
        {
            Debug.LogWarning("Missing Weapon");
            return;
        }

        WeaponHandler weaponHandler = collision.GetComponent<WeaponHandler>();

        if (weaponHandler == null)
            return;

        
            
        


        weaponHandler.EquipWeapon(Weapon);
        
    }





}
