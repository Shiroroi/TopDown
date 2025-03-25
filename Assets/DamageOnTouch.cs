using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    // radio signal
    public delegate void OnHitSomething();
    public OnHitSomething OnHit;


    public float MinDamage = 1f;
    public float MaxDamage = 2f;
    public float PushForce = 10f;

    public LayerMask TargetLayerMask;
    public LayerMask IgnoreLayerMask;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (((IgnoreLayerMask.value & (1 << col.gameObject.layer)) > 0))
            return;

        if (((TargetLayerMask.value & (1 << col.gameObject.layer)) > 0))
            HitDamageable(col);

        else
            HitAnyThing(col);


    }

    private void HitAnyThing(Collider2D col)
    {
        Debug.Log("Hi");
        OnHit?.Invoke();
    }

    private void HitDamageable(Collider2D col)
    {
        Health targetHealth = col.gameObject.GetComponent<Health>();

        if (targetHealth == null) { Debug.LogWarning(gameObject.name + ":  is missing something."); return; }

        Rigidbody2D targetRigidbody = col.gameObject.GetComponent<Rigidbody2D>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddForce((col.transform.position - transform.position).normalized * PushForce);
        }

        TryDamage(targetHealth);



    }

    private void TryDamage(Health targetHealth)
    {
        float damageAmount = Random.Range(MinDamage, MaxDamage);
        targetHealth.Damage(damageAmount, transform.gameObject);

        //? mean send signal to who had subscribe the delegate (radio signal)
        OnHit?.Invoke();

    }
}
