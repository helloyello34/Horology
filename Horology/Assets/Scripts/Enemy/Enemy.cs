using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent onDeath;

    public int lvlNumber;

    private void Start()
    {
        
    }

    public virtual void Hit(int damage)
    {
    }

    private void Awake()
    {
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
    }

    public virtual void Die()
    {
        //onDeath.Invoke();

        EnemyManager.instance.killEnemy(lvlNumber);
        Destroy(gameObject);

        RandomLoot lootScript = PlayerManager.instance.GetComponent<RandomLoot>();
        GameObject loot = lootScript.getRandomLoot();

        if (loot != null)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}
