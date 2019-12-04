﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") || collision.CompareTag("Drop"))
        {
            return;
        }

        // Get the enemy that the bullet hit
        Enemy enemy = collision.GetComponent<Enemy>();
        Destroy(gameObject);
        if (enemy)
        {
            enemy.Hit(40);
        }
    }
}
