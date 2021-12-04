using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class Player : MonoBehaviour
{
    // Config params

    [Header("Player")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float padding = 0.5f;
    [SerializeField] private int health = 200;
    [Header("Shooting")]
    [SerializeField] private GameObject playerLaser;
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField] private float laserFrequency = 0.5f;
    [FormerlySerializedAs("deathSFX")] [SerializeField] private AudioClip deathSfx;
    [SerializeField] [Range(0,1)] private float sFXVolume = 1f;

    // Cached references
    private Coroutine firingCoroutine;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    
    // Props
    public int Health => health;

    // Start is called before the first frame update
    void Start()
    {
        MovementBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(laserFrequency);
        }
    }

    private void MovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.75f, 0)).y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
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
        FindObjectOfType<Levels>().LoadGameOver();
        AudioSource.PlayClipAtPoint(deathSfx,Camera.current.transform.position, sFXVolume);
        Destroy(gameObject);
    }
}