using UnityEngine;
using UnityEngine.UI;  // To reference UI elements like Image

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 5;  // Max hearts
    public int currentHearts;  // Current health

    public float knockbackForce = 10f;  // Force for knockback
    public float immunityDuration = 2f;  // Time for immunity after damage
    private bool isImmune = false;  // Flag for immunity
    private float immunityTimer = 0f;  // Timer for immunity duration

    public Image[] heartImages;  // UI images for hearts (assign in the Inspector)
    public Image damageOverlayImage;  // Custom damage overlay image (assign in the Inspector)
    public Color TargetOverlayImageColor = Color.white;

    // Player's sprite renderer to handle blinking effect
    private SpriteRenderer playerSpriteRenderer;
    private Color originalColor;

    // Start is called once before the first frame update
    void Start()
    {
        currentHearts = maxHearts;
        UpdateHealthUI();  // Update the heart UI initially

        // Get the SpriteRenderer component for the player blinking effect
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = playerSpriteRenderer.color;  // Store the original color of the player sprite

        // Ensure damageOverlayImage starts as invisible (fully transparent)
        if (damageOverlayImage != null)
        {
            damageOverlayImage.color = new Color(1f, 1f, 1f, 0f);  // Fully transparent
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune)
        {
            immunityTimer -= Time.deltaTime;

            // Make the player blink by toggling opacity during immunity
            if (immunityTimer % 0.2f < 0.1f)
            {
                playerSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f); // Make player semi-transparent
            }
            else
            {
                playerSpriteRenderer.color = originalColor; // Restore original opacity
            }

            if (immunityTimer <= 0)
            {
                isImmune = false;
                playerSpriteRenderer.color = originalColor;  // Reset player color after immunity ends
            }
        }

        // Update the damage overlay effect based on health
        if (damageOverlayImage != null)
        {
            // Calculate the transparency based on current health
            float healthPercentage = (float)currentHearts / maxHearts;
            float alphaValue = 1 - healthPercentage;  // The lower the health, the more opaque the overlay
            damageOverlayImage.color = new Color(TargetOverlayImageColor.r, TargetOverlayImageColor.g, TargetOverlayImageColor.b, alphaValue);  // Custom image with alpha based on health
        }
    }

    // Function to handle player damage (called by enemy interaction)
    public void TakeDamage(float damageAmount)
    {
        if (isImmune) return;  // Ignore damage if immune

        // Damage by 1 (not halved anymore)
        currentHearts -= Mathf.CeilToInt(damageAmount);  // Reduce health by 1 heart
        currentHearts = Mathf.Max(currentHearts, 0);  // Don't let health go below 0

        UpdateHealthUI();  // Update the health UI

        // Trigger immunity and knockback
        isImmune = true;
        immunityTimer = immunityDuration;

        Knockback();  // Apply knockback when hit

        if (currentHearts <= 0)
        {
            GameOver();  // If no hearts left, game over
        }
    }

    // Update the heart UI to show the current health
    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHearts)
                heartImages[i].enabled = true;  // Show the heart
            else
                heartImages[i].enabled = false;  // Hide the heart (empty)
        }
    }

    // Knockback effect when the player is hit
    void Knockback()
    {
        Vector2 knockbackDirection = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    // Game over logic
    void GameOver()
    {
        // Display a Game Over screen, stop the game, or restart
        Debug.Log("Game Over! No health left.");
        // You can add game over logic here (e.g., load a new scene, show UI)
        Destroy(this.gameObject);
    }
}

