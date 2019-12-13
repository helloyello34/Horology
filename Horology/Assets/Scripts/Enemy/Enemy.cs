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

    [Header("Damage Effect variables")]
    [HideInInspector]
    public float timeSinceShot;

    public SpriteRenderer spriteRenderer;
    public float timeOfDamageEffect;
    public Color RegularColor;
    public Color DamageColor;

    [Space]
    public GameObject[] bloodSplatters;

    private bool isDead = false;


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

    private void Start()
    {
        timeSinceShot = timeOfDamageEffect;
    }

    public void Update()
    {
        timeSinceShot += Time.deltaTime;
        if (!sprite.enabled && !deathSound.isPlaying)
        {
            Destroy(gameObject);
        }
        if (timeSinceShot >= timeOfDamageEffect)
        {
            RegularEffect();
        }
    }

    public virtual void Die()
    {
        if( isDead )
        {
            return;
        }
        isDead = true;

        deathSound.Play();
        sprite.enabled = false;

        EnemyManager.instance.killEnemy(levelNumber);

        RandomLoot lootScript = PlayerManager.instance.GetComponent<RandomLoot>();
        GameObject loot = lootScript.getRandomLoot();

        Instantiate(bloodSplatters[Random.Range(0, bloodSplatters.Length)], transform.position, Quaternion.identity);
        //Instantiate(bloodSplatters[2], transform.position, Quaternion.identity);
        if (loot != null)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
        transform.position = new Vector2(10000, 10000);
    }

    public void DamageEffect()
    {
        spriteRenderer.color = DamageColor;
    }

    public void RegularEffect()
    {
        spriteRenderer.color = RegularColor;
    }
}
