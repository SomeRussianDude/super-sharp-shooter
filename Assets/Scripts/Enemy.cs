using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // Config params
    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private GameObject healthPack;
    [SerializeField] private GameObject explosionVFX;

    [SerializeField] private float health = 100;
    [SerializeField] private float minTimeBetweenShots = 0.5f;
    [SerializeField] private float maxTimeBetweenShots = 1.5f;
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField] private float dropSpeed = 2f;

    [SerializeField] private int pointsForHit = 10;
    [SerializeField] private int pointsForKill = 50;

    [SerializeField] private AudioClip shootingSfx;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] [Range(0, 1)] private float sFXVolume = 1f;

    private float shotCounter;
    private GameSession gameSession;
    
    private int currentWave;
    private EnemyManager enemyManager;

    public int CurrentWave
    {
        set => currentWave = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
        
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            AudioSource.PlayClipAtPoint(shootingSfx, Camera.main.transform.position, sFXVolume);
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser =
            Instantiate(enemyLaser, transform.position + new Vector3(0, -0.6f), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        HitProcessing(damageDealer);
    }

    private void HitProcessing(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        gameSession.IncreaseScoreForHit(pointsForHit);
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemyManager.MarkDead(currentWave,this);
        gameSession.IncreaseScoreForKill(pointsForKill);
        GameObject explosion = Instantiate(explosionVFX, transform.position, quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSfx, Camera.current.transform.position, sFXVolume);
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }

    public void HealthDrop()
    {
        GameObject healthPack = Instantiate(this.healthPack, transform.position, quaternion.identity);
        healthPack.GetComponent<Rigidbody2D>().velocity = new Vector2(0, dropSpeed);
    }
}