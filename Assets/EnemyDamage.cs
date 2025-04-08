using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 1f;  // Amount of damage dealt to the player
    public float knockbackForce = 10f;  // Force applied to the player when hit

    // Called when an enemy collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerHealth component on the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Apply damage to the player
                playerHealth.TakeDamage(damageAmount);

                // Apply knockback to the player
                ApplyKnockback(collision.gameObject);

                // Optionally, trigger any visual feedback for the player here (if needed)
            }
        }
    }

    // Apply knockback effect to the player when hit
    private void ApplyKnockback(GameObject player)
    {
        // Get the Rigidbody2D component of the player
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            // Calculate the knockback direction (away from the enemy)
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;

            // Apply the knockback force to the player's Rigidbody2D
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
