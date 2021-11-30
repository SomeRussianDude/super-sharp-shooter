using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // Config params
    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private AudioClip shootingSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private float health = 100;
    [SerializeField] private float minTimeBetweenShots = 0.5f;
    [SerializeField] private float maxTimeBetweenShots = 1.5f;
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField] [Range (0,1)] private float sFXVolume = 1f;

    private float shotCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
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
            AudioSource.PlayClipAtPoint(shootingSFX,Camera.main.transform.position, sFXVolume);
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaser, transform.position + new Vector3(0,-0.6f), Quaternion.identity) as GameObject;
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
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, sFXVolume);
        Destroy(explosion,1f);
        Destroy(gameObject);
    }
}
