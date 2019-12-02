using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanism : MonoBehaviour
{
    Vector3 looking = new Vector3();

    public Transform firePoint;
    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        looking.x = Input.GetAxis("ShootHorizontal");
        looking.y = Input.GetAxis("ShootVertical");
        float angle = Mathf.Atan2(looking.y, looking.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        Debug.Log(transform.rotation);

        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Shoot projectile
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}
