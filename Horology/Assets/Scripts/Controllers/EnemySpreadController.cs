﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpreadController : EnemyController
{
    public float aggroRadius = 6.0f;
    public float outerRadius = 5.0f;
    public float innerRadius = 3.0f;
    [Space]
    public float shootInterval;
    public float decisionInterval;
    private float startingInterval;
    private bool firstPass = true;


    private float timeSinceShot = 0;
    private float timeSinceLastDecision = 0;
    private bool aggro = false;
    private Vector3 randomDirection = new Vector3(0, 0, 0);

    public override void Start()
    {
        //Calling base classes start method to initalize variables
        base.Start();
        startingInterval = shootInterval;
    }

    private void Update()
    {
        if (isSlowed && firstPass)
        {
            firstPass = false;
            shootInterval *= (1 + TimeManager.instance.timeFactor);
        }
        else if (!isSlowed && !firstPass)
        {
            firstPass = true;
            shootInterval = startingInterval;
        }
    }


    void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        // Distance to the player
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);


        if (distanceToPlayer <= aggroRadius)
        {
            //Permanently make enemy aggressive to player
            aggro = true;
        }

        //If enemy is not aggro, do nothing
        if (aggro)
        {
            //Add delta time to counters
            float dt = Time.fixedDeltaTime;
            timeSinceLastDecision += dt;
            float movement = dt * speed;
            timeSinceShot += dt;

            if (timeSinceShot >= shootInterval)
            {
                timeSinceShot = 0;
                weapon.Shoot();
            }

            if (distanceToPlayer > outerRadius) //If player is outside outerRadius -> move closer
            {
                //Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, target.position, movement);
            }
            else if (distanceToPlayer < innerRadius) //If player is within innerRaddius -> move away
            {
                //Move away from the player
                transform.position = Vector3.MoveTowards(transform.position, transform.position - target.position, movement);
            }
            else if (timeSinceLastDecision >= decisionInterval)
            {
                // Make movement decision
                //Pick a random direction or stay still
                randomDirection = Random.Range(0, 10) == 0 ? new Vector3(0, 0, 0) : (Vector3)Random.insideUnitCircle.normalized;
                //Reset variable to start counting down to next decision
                timeSinceLastDecision = 0;
            }
            else
            {
                // Move in decided direction
                transform.Translate(randomDirection * movement);
            }


        }
        else
        {
            //Do nothing for now, //Idle animation maybe ?
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }
}
