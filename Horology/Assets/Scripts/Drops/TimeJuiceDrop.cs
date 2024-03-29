﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeJuiceDrop : MonoBehaviour
{
    public float amount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            //Modify Time Juice
            PlayerManager.instance.player.GetComponent<PlayerTimePower>().IncreaseMana(amount);
            Destroy(gameObject);
        }
    }
}
