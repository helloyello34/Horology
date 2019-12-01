﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 40f;
    public Rigidbody2D rb;
    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENEMY HIT " + collision.name);
        Destroy(gameObject);

        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.Hit(40);
        }
    }
}