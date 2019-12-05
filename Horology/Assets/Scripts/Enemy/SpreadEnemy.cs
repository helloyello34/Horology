using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEnemy : Enemy
{

    public int health = 100;
    public GameObject heartPrefab;
    public GameObject timeJuicePrefab;
    public GameObject[] upgradePrefabs;

    //private float dropChance = 10f; // 1/10

    // Start is called before the first frame update
    void Start()
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

    /*
    public override void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
    */
}
