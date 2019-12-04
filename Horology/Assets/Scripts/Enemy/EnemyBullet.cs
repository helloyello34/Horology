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
        Debug.Log("BULLET TIME");
        startingVelocity = transform.right * speed;
        rb.velocity = TimeManager.instance.isSlowed ? startingVelocity * TimeManager.instance.timeFactor : startingVelocity;
        timeCallback += SlowDown;
        TimeManager.instance.timeEvent.AddListener(timeCallback);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") || collision.CompareTag("Drop"))
        {
            return;
        }
        // Instansiating on hit effect animation
        Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
        Player player = collision.GetComponent<Player>();

        if (player && !player.isGod)
        {
            Destroy(gameObject);
            player.Hit(1);
        }
    }

    public void SlowDown(bool isSlowed)
    {
        Debug.Log("RB:(" + rb.velocity.x + ", " + rb.velocity.y + ");");
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
