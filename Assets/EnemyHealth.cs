using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 10f;
    private float _currentHealth = 10f;

    public AudioClip EnemyDeathSoundObject;
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialization();
        audioSource = GetComponent<AudioSource>();
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
        

        // Optionally play the death sound
        if (EnemyDeathSoundObject != null && audioSource != null)
        {
            //Instantiate(EnemyDeathSoundObject, transform.position, transform.rotation);
            audioSource.PlayOneShot(EnemyDeathSoundObject);
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}