using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public UnityEvent onDeath;

    private void Awake()
    {
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
