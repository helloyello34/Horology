using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.Hit(40);
        }
    }
}
