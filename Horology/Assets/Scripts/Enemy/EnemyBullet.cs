using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 40f;
    public Rigidbody2D rb;
    private Vector2 startingVelocity;
    public UnityAction<bool> timeCallback;
    public GameObject bulletHitEffect;
    private void Start()
    {
        startingVelocity = transform.right * speed;
        rb.velocity = TimeManager.instance.isSlowed ? startingVelocity * TimeManager.instance.timeFactor : startingVelocity;
        timeCallback += SlowDown;
        TimeManager.instance.timeEvent.AddListener(timeCallback);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Trigger") || collision.CompareTag("Drop") || (player != null && player.isGod))
        {
            return;
        }
        // Instansiating on hit effect animation


        if (player)
        {
            player.Hit(1);
        }

        Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }



    public void SlowDown(bool isSlowed)
    {
        if (isSlowed)
        {
            rb.velocity = rb.velocity * TimeManager.instance.timeFactor;
        }
        else
        {
            rb.velocity = startingVelocity;
        }
    }
}
