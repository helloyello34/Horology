﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float speed = 40;

    public GameObject bulletPrefab;
    public GameObject firePoint;

    Transform target;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    private void Update()
    {
        Vector3 direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
    }
}