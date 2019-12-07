using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent onDeath;
    public AudioSource deathSound;
    private SpriteRenderer sprite;
    public int levelNumber;

    public virtual void Hit(int damage)
    {
    }

    private void Awake()
    {
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (!sprite.enabled && !deathSound.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Die()
    {
        deathSound.Play();
        sprite.enabled = false;

        EnemyManager.instance.killEnemy(levelNumber);

        RandomLoot lootScript = PlayerManager.instance.GetComponent<RandomLoot>();
        GameObject loot = lootScript.getRandomLoot();

        if (loot != null)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
        GetComponent<Rigidbody2D>().transform.position = new Vector2(10000, 10000);
    }
}
