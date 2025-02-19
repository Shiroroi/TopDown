using Unity.VisualScripting;
using UnityEngine;

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



}
