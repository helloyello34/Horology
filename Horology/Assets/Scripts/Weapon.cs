using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Shoot projectile
        Instantiate(bullet, firePoint.position, transform.rotation);
    }
}
