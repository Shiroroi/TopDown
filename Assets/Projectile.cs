using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 10f;
    public Cooldown Lifetime;

    private Rigidbody2D _rigidBody;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        _rigidBody.AddRelativeForce(new Vector2(0, Speed));

        Lifetime.StartCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (Lifetime.CurrentProgress != Cooldown.Progress.Finished)
            return;

        Die();
    }

    void Die()
    {
        Lifetime.StopCooldown();
        Destroy(gameObject);
    }

}
