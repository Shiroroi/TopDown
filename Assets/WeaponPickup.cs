using UnityEngine;

public class WeaponPickup : PickUp
{
    public GameObject weaponPrefab;  // Reference to the melee weapon prefab

    protected override void PickedUp(Collider2D collision)
    {
        if (weaponPrefab == null)
        {
            Debug.LogWarning("No weapon prefab assigned!");
            return;
        }

        // Get the WeaponHandler component from the player
        WeaponHandler weaponHandler = collision.GetComponent<WeaponHandler>();
        if (weaponHandler == null)
            return;

        // Equip the weapon when the player picks it up
        weaponHandler.EquipWeapon(weaponPrefab);

        // Destroy the pickup object (the white cube)
        Destroy(gameObject);
    }
}
