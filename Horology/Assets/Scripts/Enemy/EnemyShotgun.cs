using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : EnemyWeapon
{

    public GameObject bulletPrefab;
    public GameObject firePoint;
    public int bulletAmount;
    public int angle;

    Transform target;

    private float angleBetweenBullets;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        angleBetweenBullets = angle / bulletAmount;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public override void Shoot()
    {
        if (TimeManager.instance.timeFactor != 0)
        {
            for (int i = -bulletAmount / 2; i < bulletAmount/2; i++)
            {
                Vector3 temp = transform.rotation.eulerAngles;
                temp.z += i * angleBetweenBullets;
                Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.Euler(temp));
            }
        }
    }
}
