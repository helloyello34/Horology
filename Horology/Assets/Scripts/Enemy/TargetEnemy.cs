using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : Enemy
{

    public int health = 100;
    int startHealth;
    TimeBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        healthBar = GetComponentInChildren<TimeBar>();
        healthBar.gameObject.SetActive(false);
    }

    public override void Hit(int damage)
    {
        healthBar.gameObject.SetActive(true);
        health -= damage;
        healthBar.SetBar(health, startHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    /*
    public override void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
    */
}
