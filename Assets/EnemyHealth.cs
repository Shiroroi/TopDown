using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 10f;
    private float _currentHealth = 10f;

    public GameObject EnemyDeathSoundObject;

    // Reference to SandevistanGauge to register kills
    private SandevistanGauge sandevistanGauge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialization();

        // Find the SandevistanGauge component (either on the player or a manager object)
        sandevistanGauge = Object.FindFirstObjectByType<SandevistanGauge>();  // Using Object.FindFirstObjectByType for better performance
    }

    private void Initialization()
    {
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // You can add extra logic here if needed for updating health or other behaviors
    }

    // Damage the enemy
    public void Damage(float damage, GameObject source)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    // Handle enemy death
    public void Die()
    {
        // Register a kill in the SandevistanGauge when the enemy dies
        if (sandevistanGauge != null)
        {
            sandevistanGauge.RegisterKill();  // Increment the kill count to fill the gauge
        }

        // Optionally play the death sound
        if (EnemyDeathSoundObject != null)
        {
            Instantiate(EnemyDeathSoundObject, transform.position, transform.rotation);
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}