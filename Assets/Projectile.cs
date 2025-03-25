using System.ComponentModel;
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

        Die();
    }

    void Die()
    {
        Lifetime.StopCooldown();
        Destroy(gameObject);
    }  
}
