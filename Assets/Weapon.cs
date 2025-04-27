using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;


public class Weapon : MonoBehaviour
{
    public enum FireModes
    {
        Auto, // = 0
        SingleFire, // = 1
        BurstFire, // = 2
    }



    public FireModes FireMode;

    public float Spread = 0f;
    public int BurstFireAmount = 3;
    public float BurstFireInterval = 0.1f;
    public int ProjectileCount = 1;
    public float FireRate = 0.07f;

    public GameObject Projectile;

    public GameObject[] Feedbacks;
    public GameObject[] ReloadFeedbacks;

    public Transform SpawnPos;
    public Cooldown ShootInterval;

    private float _timer = 0f;
    private bool _canShoot = true;
    private bool _firereset = true;

    public Cooldown ReloadCooldown;
    public int MaxBulletCount = 10;

    public AudioClip shootSound;  // Drag the shooting sound here in the inspector
    private AudioSource audioSource;

    private float shotCooldownTimer = 0f;


    public int CurrentBulletCount
    {
        get { return currentBulletCount; }
    }

    protected int currentBulletCount = 10;

    private void Start()
    {
        currentBulletCount = MaxBulletCount;
        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component
    }

    private void Update()
    {
        UpdateReloadCooldown();
        UpdateShootCooldown();
        HandleShotCooldown();
    }

    private void HandleShotCooldown()
    {
        if (shotCooldownTimer > 0)
        {
            shotCooldownTimer -= Time.deltaTime;  // Decrease cooldown timer
        }
    }

    private void UpdateReloadCooldown()
    {
        if (ReloadCooldown.CurrentProgress != Cooldown.Progress.Finished)
            return;

        if (ReloadCooldown.CurrentProgress == Cooldown.Progress.Finished)
        {
            currentBulletCount = MaxBulletCount;
        }

        ReloadCooldown.CurrentProgress = Cooldown.Progress.Ready;
    }

    private void UpdateShootCooldown()
    {
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Finished)
            return;

        ShootInterval.CurrentProgress = Cooldown.Progress.Ready;
    }
    public void Shoot()
    {
        if (shootSound != null && audioSource != null)
        {
            // Play the shooting sound only once per click
            if (!audioSource.isPlaying)  // Only play if the sound isn't already playing
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
        if (Projectile == null || SpawnPos == null) return;
        if (ReloadCooldown.IsOnCoolDown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready) return;

        if (shotCooldownTimer <= 0)
        {
            switch (FireMode)
            {
                case FireModes.Auto:
                    AutoFireShoot();
                    break;

                case FireModes.SingleFire:
                    SingleFireShoot();
                    break;

                case FireModes.BurstFire:
                    BurstFireShoot();
                    break;
            }
        }
    }

    void AutoFireShoot()
    {
        if (!_canShoot || ShootInterval.CurrentProgress != Cooldown.Progress.Ready) return;

        ShootProjectile();
        currentBulletCount--;
        ShootInterval.StartCooldown();

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
            ReloadCooldown.StartCooldown();
    }

    void SingleFireShoot()
    {
        if (!_canShoot || ShootInterval.CurrentProgress != Cooldown.Progress.Ready) return;

        ShootProjectile();
        currentBulletCount--;
        ShootInterval.StartCooldown();

        shotCooldownTimer = 0.5f;

        _canShoot = false; // Prevent continuous fire
        StartCoroutine(ResetCanShoot()); // Re-enable after button release
    }

    IEnumerator ResetCanShoot()
    {
        yield return new WaitUntil(() => !Input.GetMouseButton(0)); // Wait until button is released
        _canShoot = true;
    }

    void BurstFireShoot()
    {

        if (!_canShoot)
            return;
        if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
            return;

        

        StartCoroutine("BurstShoot");
        currentBulletCount--;

        



        ShootInterval.StartCooldown();

        if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
        {
            ReloadCooldown.StartCooldown();
            
        }


    }

    //Use coroutine for Brust fire mode
    protected IEnumerator BurstShoot()
    {
        for (int i = 0; i < BurstFireAmount; ++i)
        {

            float randomRot = Random.Range(-Spread, Spread);


            GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0, 0, randomRot));



            //wait 0.07 second (Fire rate) and continue to do the for loop
            yield return new WaitForSeconds(FireRate);


        }
    }

    void ShootProjectile()
    {
        for (int i = 0; i < ProjectileCount; i++)
        {
            float randomRot = Random.Range(-Spread, Spread);
            GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0, 0, randomRot));
        }
    }


    public void StopShoot()
    {
        if (FireMode == FireModes.Auto)
            return;

        _firereset = true;

    }
}



    


    
    

   


