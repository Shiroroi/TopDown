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

    // For feedback (like a damage flash)
    public GameObject damageFeedbackPrefab;

    // Start is called once before the first frame update
    void Start()
    {
        currentHearts = maxHearts;
        UpdateHealthUI();  // Update the heart UI initially
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune)
        {
            immunityTimer -= Time.deltaTime;
            if (immunityTimer <= 0)
            {
                isImmune = false;
                ResetHeartUI();  // Reset heart visuals after immunity ends
            }
        }
    }

    // Function to handle player damage (called by enemy interaction)
    public void TakeDamage(float damageAmount)
    {
        if (isImmune) return;  // Ignore damage if immune

        currentHearts -= Mathf.CeilToInt(damageAmount / 2);  // Reduce health by half a heart
        currentHearts = Mathf.Max(currentHearts, 0);  // Don't let health go below 0

        UpdateHealthUI();  // Update the health UI

        // Trigger immunity and knockback
        isImmune = true;
        immunityTimer = immunityDuration;

        // Visual feedback for damage (e.g., a quick flash of red)
        if (damageFeedbackPrefab != null)
            Instantiate(damageFeedbackPrefab, transform.position, Quaternion.identity);

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

    // Reset heart UI after immunity ends (remove blinking effect)
    void ResetHeartUI()
    {
        foreach (Image heart in heartImages)
        {
            heart.color = Color.white;  // Reset heart color to white
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
    }
}

