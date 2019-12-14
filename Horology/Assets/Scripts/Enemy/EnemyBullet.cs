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
    private float currentSpeed, startingSpeed, minSpeed;
    private void Start()
    {
        startingSpeed = speed;
        startingVelocity = transform.right * speed;
        rb.velocity = TimeManager.instance.isSlowed ? startingVelocity * TimeManager.instance.timeFactor : startingVelocity;
        currentSpeed = speed;
        minSpeed = startingSpeed * TimeManager.instance.timeModifier;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Trigger") || collision.CompareTag("Drop") || (player != null && player.isGod))
        {
            return;
        }
        // Instantiating on hit effect animation


        if (player)
        {
            player.Hit(1);
        }

        Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        SetSpeed();
        if (currentSpeed > minSpeed && currentSpeed < startingSpeed)
        {
            rb.velocity = rb.velocity.normalized * (currentSpeed);
        }
    }

    private void SetSpeed()
    {
        currentSpeed = Mathf.Lerp(0, startingSpeed, TimeManager.instance.currentToBaseRatio);
    }
}
