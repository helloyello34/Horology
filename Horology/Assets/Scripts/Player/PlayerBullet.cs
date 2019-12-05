using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public GameObject bulletHitEffect;
    public AudioSource hitSound;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
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
        if (enemy)
        {
            Debug.Log("HIT ENEMY");
            sprite.enabled = false;
            hitSound.Play();
            enemy.Hit(40);
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
