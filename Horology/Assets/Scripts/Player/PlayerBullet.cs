﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public int damage = 25;
    public GameObject bulletHitEffect;
    public AudioSource hitSound;
    private SpriteRenderer sprite;

    private bool hasHit = false;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trigger") || collision.collider.CompareTag("Drop") || hasHit)
        {
            return;
        }
        hasHit = true;
       

        Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
        // Get the enemy that the bullet hit
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        EnemyController enemyController = collision.collider.GetComponent<EnemyController>();
        if (enemy)
        {
            sprite.enabled = false;
            hitSound.Play();

            //Enemy is aggro if hit
            enemyController.aggro = true;

            enemy.Hit(damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") || collision.CompareTag("Drop"))
        {
            return;
        }

        Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
        // Get the enemy that the bullet hit
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyController enemyController = collision.GetComponent<EnemyController>();
        if (enemy)
        {
            sprite.enabled = false;
            hitSound.Play();

            //Enemy is aggro if hit
            enemyController.aggro = true; 

            enemy.Hit(damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!sprite.enabled && !hitSound.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
