﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanism : MonoBehaviour
{
    Vector3 looking = new Vector3();
    // Update is called once per frame
    void Update()
    {
        looking.x = Input.GetAxis("ShootHorizontal");
        looking.y = Input.GetAxis("ShootVertical");
        float angle = Mathf.Atan2(looking.y, looking.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
    }
}
