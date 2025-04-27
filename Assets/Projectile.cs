using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public Cooldown Lifetime;
    private Rigidbody2D _rigidBody;
    private DamageOnTouch _damageOnTouch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddRelativeForce(new Vector2(0, Speed));

        _damageOnTouch = GetComponent<DamageOnTouch>();

        if (_damageOnTouch != null)
            _damageOnTouch.OnHit += Die;

        Lifetime.StartCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (Lifetime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        

        Die();  // Destroy the projectile after it expires
    }

    void ApplyOneShotKill()
    {
        // Get all enemies within a small radius around the bullet
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f);  // Small radius to check for enemies

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    // Log to check if the damage is being applied
                    Debug.Log($"Applying 1 shot, 1 kill to {enemy.gameObject.name}. Max Health: {enemyHealth.MaxHealth}");

                    // Apply enough damage to instantly kill the enemy
                    float damage = enemyHealth.MaxHealth;  // Set damage equal to max health (1-shot kill)
                    enemyHealth.Damage(damage, gameObject);  // Apply damage to the enemy and kill it
                    Debug.Log($"Enemy {enemy.gameObject.name} is killed with {damage} damage");
                }
            }
        }
    }

    void Die()
    {
        Lifetime.StopCooldown();
        Destroy(gameObject);  // Destroy the projectile after the effect ends
    }
}