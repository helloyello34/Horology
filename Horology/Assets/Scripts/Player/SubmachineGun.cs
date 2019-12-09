using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmachineGun : GunBase
{
    public int spreadAngle = 6;
    private SpriteRenderer s;

    public GameObject muzzleFlashPrefab;
    public float muzzleFlashFrames;



    public override void Shoot()
    {
        //Instantiate bullets
        Vector3 temp = transform.rotation.eulerAngles;
        var rand = Random.Range(-spreadAngle, spreadAngle);
        temp.z += rand;

        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(temp));
        s = bullet.GetComponent<SpriteRenderer>();

        var muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, transform.rotation);

        //Coroutine that yields for x frames and then destroys the muzzleflash prefab
        StartCoroutine(SMGMuzzleFlash(muzzleFlash));

        shotSound.Play();

        //Reset timer
        timeSinceShot = 0;
    }

    public virtual IEnumerator SMGMuzzleFlash(GameObject muzzleFlash)
    {

        //Wait x many frames
        for (int i = 0; i < muzzleFlashFrames; i++)
        {
            yield return 0;
        }

        if (muzzleFlash != null)
        {
            Destroy(muzzleFlash);
        }

    }
}
