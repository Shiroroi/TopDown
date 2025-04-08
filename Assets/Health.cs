using UnityEngine;

public class Health : MonoBehaviour
{

    public float MaxHealth = 10f;

    private float _currentHealth = 10f;

    public GameObject EnemyDeathSoundObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float damage, GameObject source)
    {

        //GameObject.Instantiate(EnemyDeathSoundObject, transform.position, transform.rotation);


        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }


    }
    public void Die()
    {

        Destroy(this.gameObject);
    }

}
