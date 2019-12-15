using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
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
    [HideInInspector]
    public Vector3 startingPosition;
    public float knockbackAmount;
    private float knockInterval;

    void Start()
    {
        PlayerManager.instance.gunTransform = transform;
        startingPosition = transform.localPosition;
        knockInterval = shootInterval;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        ReAdjust();

        // Time since last frame was called
        timeSinceShot += Time.deltaTime;

        Vector2 direction = new Vector2(Input.GetAxis("ShootHorizontal"), Input.GetAxis("ShootVertical"));
        // If R1 is pushed
        if (Input.GetKey("space") ||Input.GetButton("Fire") || (PlayerPrefs.GetString("ShootingMode", "Manual") == "Auto" && direction.magnitude > 0.2))
        {
            if (timeSinceShot >= shootInterval && !(GameManager.instance.isDead || GameManager.instance.isWin))
            {
                Shoot();
                KnockBack();
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


        shotSound.Play();

        // Reset timer
        timeSinceShot = 0f;
    }

    public virtual IEnumerator FlashMuzzleFlash(SpriteRenderer s)
    {
        s.sprite = muzzleFlash;

        for (int i = 0; i < framesToFlash; i++)
        {
            yield return 0;
        }

        if (s != null)
        {
            s.sprite = bulletSprite;
        }

    }



    public virtual void ReAdjust()
    {
        knockInterval += Time.deltaTime;
        float step = Mathf.Lerp(transform.localPosition.x, startingPosition.x, knockInterval / shootInterval);
        transform.localPosition = new Vector3(step, 0, 0);
    }

    public virtual void KnockBack()
    {
        knockInterval = 0f;
        transform.localPosition -= new Vector3(knockbackAmount, 0, 0);
    }
}
