using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : Enemy
{

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
