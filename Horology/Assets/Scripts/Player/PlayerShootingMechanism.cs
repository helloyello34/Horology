using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanism : MonoBehaviour
{
    Vector3 looking = new Vector3();
    float timeSinceShot = 0;

    public float shootInterval = 0.5f;
    public Transform firePoint;
    public GameObject bulletPrefab;


    private void Start()
    {
        PlayerManager.instance.gunTransform = transform;
    }


    // Update is called once per frame
    void Update()
    {
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
            // Get the angle of the joystick and rotating the object on that angle
            float angle = Mathf.Atan2(looking.y, looking.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
            Debug.Log(angle);
        }
    }

    // Shoot projectile
    void Shoot()
    {
        // Create an instance of a bullet
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        // Reset timer
        timeSinceShot = 0f;
    }
}
