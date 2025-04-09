using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float swingDamage = 10f;  // Damage dealt by the weapon
    public float swingSpeed = 5f;    // Speed at which the weapon swings around the player
    public float swingRadius = 2f;   // The radius of the swing path around the player
    public float swingDuration = 1f; // Duration of the swing before the weapon returns

    private bool isSwinging = false;  // To track whether the swing is in progress
    private Vector3 originalPosition; // The starting position of the weapon
    private float swingTimer = 0f;    // Timer to track swing completion
    private Collider2D weaponCollider;

    private Transform player;         // Reference to the player (set in the Inspector)
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer of the weapon

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Get the player object
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the sprite renderer for visual effect
        originalPosition = transform.position;  // Set the original position
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;  // Disable collider initially
    }

    void Update()
    {
        // Trigger the swing when the player clicks the mouse button
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartSwing();
        }

        if (isSwinging)
        {
            PerformSwing();
        }
    }

    // Start the swing by enabling the weapon's collider and initiating the swinging motion
    private void StartSwing()
    {
        isSwinging = true;
        swingTimer = 0f;  // Reset the timer
        weaponCollider.enabled = true;  // Enable the collider to detect hits
    }

    // Perform the swing movement around the player
    private void PerformSwing()
    {
        swingTimer += Time.deltaTime;

        // Calculate the angle to move the weapon in a circular path
        float angle = Mathf.Lerp(0f, 360f, swingTimer / swingDuration);
        float radians = angle * Mathf.Deg2Rad;

        // Calculate the new position of the weapon in the circular path around the player
        float x = player.position.x + Mathf.Cos(radians) * swingRadius;
        float y = player.position.y + Mathf.Sin(radians) * swingRadius;

        // Update the position
        transform.position = new Vector3(x, y, originalPosition.z);

        // Rotate the weapon to align with the direction of the swing (like clock hands)
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Once the swing is completed, return the weapon to its original position
        if (swingTimer >= swingDuration)
        {
            isSwinging = false;
            swingTimer = 0f;  // Reset the swing timer
            weaponCollider.enabled = false;  // Disable the collider
            transform.position = originalPosition;  // Return to the original position
        }
    }

    // This function will be called when the weapon hits an enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Assuming enemies have a "Health" script attached to them
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(swingDamage, gameObject);  // Deal damage to the enemy
            }
        }
    }
}