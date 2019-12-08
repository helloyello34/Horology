using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanism : MonoBehaviour
{
    [HideInInspector]
    public float timeSinceShot = 0;

    public float shootInterval;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource shotSound;
    public Sprite muzzleFlash;
    public Sprite bulletSprite;

    public int framesToFlash = 3;
    public float destroyTime = 3;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        PlayerManager.instance.gunTransform = transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        // Time since last frame was called
        timeSinceShot += Time.deltaTime;

        // If R1 is pushed
        if (Input.GetButton("Fire"))
        {
            if (timeSinceShot >= shootInterval)
            {
                Shoot();
            }
        }
    }

    // Shoot projectile
    public virtual void Shoot()
    {
        // Create an instance of a bullet

        var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        spriteRenderer = bullet.GetComponent<SpriteRenderer>();

        StartCoroutine(FlashMuzzleFlash(spriteRenderer));
        //StartCoroutine(TimedDestruction());


        shotSound.Play();

        // Reset timer
        timeSinceShot = 0f;
    }

    public virtual IEnumerator FlashMuzzleFlash(SpriteRenderer s)
    {
        s.sprite = muzzleFlash;
        
        for(int i = 0; i < framesToFlash; i++)
        {
            yield return 0;
        }

        if(s != null)
        {
        s.sprite = bulletSprite;
        }
    }

    IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
