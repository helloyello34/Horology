using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    public int amountOfBullet = 4;
    public int spreadAngle = 20;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();


    // Knockback variables
    public float knockbackAmount;

    Vector3 startPosition;


    private void Start()
    {
        startPosition = transform.localPosition;
    }

    public override void Shoot()
    {
        //Instantiate bullets

        //Instantiate bullets and add their SpriteRenderer to a list
        for(int i = 0; i < amountOfBullet; i++)
        {
            Vector3 temp = transform.rotation.eulerAngles;
            var rand = Random.Range(-spreadAngle, spreadAngle);
            temp.z += rand;

            var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(temp));
            spriteRenderers.Add(bullet.GetComponent<SpriteRenderer>());
        }

        //Coroutine to MuzzleFlash for x frames
        foreach (SpriteRenderer s in spriteRenderers)
        {
            StartCoroutine(FlashMuzzleFlash(s));
        }

        shotSound.Play();

        //Reset timer
        timeSinceShot = 0;
        //Clear array
        spriteRenderers.Clear();
        KnockBack();

    }

    public void KnockBack()
    {
        transform.localPosition -= new Vector3(knockbackAmount, 0, 0);
    }


    public override void ReAdjust()
    {
        transform.localPosition += (startPosition - transform.localPosition) * 0.25f;
    }


}
