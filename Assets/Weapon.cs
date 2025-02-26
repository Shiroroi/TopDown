using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public int CurrentBulletCount
    {
        get { return currentBulletCount; }
    }

    protected int currentBulletCount = 10;

    private void Start()
    {
        currentBulletCount = MaxBulletCount;
    }

    private void Update()
    {
        UpdateReloadCooldown();
        UpdateShootCooldown();
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
        if (Projectile == null)
            return;

        if (SpawnPos == null)
            return;

        if (ReloadCooldown.IsOnCoolDown || ReloadCooldown.CurrentProgress != Cooldown.Progress.Ready)
            return;


        switch (FireMode)
        {
            case FireModes.Auto:
                {
                    AutoFireShoot();
                    break;
                }

            case FireModes.SingleFire:
                {
                    SingleFireShoot();
                    break;
                }

            case FireModes.BurstFire:
                {
                    BurstFireShoot();
                    break;
                }
        }

        void AutoFireShoot()
        {
            if (!_canShoot)
                return;

            if (ShootInterval.CurrentProgress != Cooldown.Progress.Ready)
                return;

            ShootProjectile();

            currentBulletCount--;

            ShootInterval.StartCooldown();

            if (currentBulletCount <= 0 && !ReloadCooldown.IsOnCoolDown)
            {
                ReloadCooldown.StartCooldown();
            }


        }

        void SingleFireShoot()
        {

        }

        void BurstFireShoot()
        {

        }


        void ShootProjectile()
        {
            for (int i = 0; i < ProjectileCount; i++)
            {
                float randomRot = Random.Range(-Spread, Spread);

                GameObject.Instantiate(Projectile, SpawnPos.position, SpawnPos.rotation * Quaternion.Euler(0, 0, randomRot));
            }
        }
    }

    public void StopShoot()
    {
        if (FireMode == FireModes.Auto)
            return;

        _firereset = true;

    }
}



    


    
    

   


