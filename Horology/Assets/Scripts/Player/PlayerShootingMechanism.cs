using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanism : MonoBehaviour
{
    Vector3 looking = new Vector3();
    float timeSinceShot = 0;

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

        // Get the R axis on the joystick
        looking.x = Input.GetAxis("ShootHorizontal");
        looking.y = Input.GetAxis("ShootVertical");

        // If R1 is pushed
        if (Input.GetButton("Fire"))
        {
            if (timeSinceShot >= shootInterval)
            {
                Shoot();
            }
        }
        // If the button is not in the deadzone change the rotation
        if (!(looking.x <= 0.2 && looking.x >= -0.2 && looking.y <= 0.2 && looking.y >= -0.2))
        {
            PlayerManager.instance.gunTransform = transform;
            looking = looking.normalized;
            // Get the angle of the joystick and rotating the object on that angle
            float angle = Mathf.Atan2(looking.y, looking.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
    }

    // Shoot projectile
    void Shoot()
    {
        // Create an instance of a bullet

        var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        spriteRenderer = bullet.GetComponent<SpriteRenderer>();

        StartCoroutine(FlashMuzzleFlash());
        //StartCoroutine(TimedDestruction());


        shotSound.Play();

        // Reset timer
        timeSinceShot = 0f;
    }

    IEnumerator FlashMuzzleFlash()
    {
        spriteRenderer.sprite = muzzleFlash;

        for (int i = 0; i < framesToFlash; i++)
        {
            yield return 0;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = bulletSprite;
        }

    }

    IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
