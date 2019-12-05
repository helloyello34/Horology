using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyTargetController : EnemyController
{
    public float aggroRadius = 6.0f;
    public float outerRadius = 5.0f;
    public float innerRadius = 3.0f;
    [Space]
    public float shootInterval;
    public float decisionInterval;
    public float modeInterval;
    [Space]
    private float timeSinceShot = 0;
    private float timeSinceLastDecision = 0;
    private float timeSinceModeChange = 0;
    private bool aggro = false;
    private bool roaming = true;
    private Vector3 randomDirection = new Vector3(0, 0, 0);
    private bool firstPass = true;
    private float startingInterval;

    public override void Start()
    {
        //Calling base classes start method to initalize variables
        base.Start();

        //Random initial time between mode changes so every enemy doesn't switch at the same time
        timeSinceModeChange = Random.Range(0, 5);
        startingInterval = shootInterval;
    }

    private void Update()
    {
        if (isSlowed && firstPass)
        {
            firstPass = false;
            shootInterval /=  TimeManager.instance.timeModifier;
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
        float dt = Time.fixedDeltaTime;
        timeSinceLastDecision += dt;
        float movement = dt * speed;
        int levelNumber = GetComponent<Enemy>().levelNumber;

        if (distanceToPlayer <= aggroRadius && GameManager.instance.currentLevel == levelNumber)
        {
            //Permanently make enemy aggressive to player
            aggro = true;
        }

        //If enemy is not aggro, do nothing
        if (aggro)
        {
            timeSinceShot += dt;

            if (timeSinceShot >= shootInterval)
            {
                timeSinceShot = 0;
                weapon.Shoot();
            }

            if (timeSinceModeChange >= modeInterval)
            {
                //Swaps mode everytime, change this possibly for random chance ???
                roaming = !roaming;
                timeSinceModeChange = 0;
            }

            if (roaming)
            {
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
                //Chase mode
                if (distanceToPlayer < innerRadius)
                {
                    //Move away from the player
                    transform.position = Vector3.MoveTowards(transform.position, transform.position - target.position, movement);
                }
                else
                {
                    //Move towards the player
                    transform.position = Vector3.MoveTowards(transform.position, target.position, movement);
                }
            }
        }
        else
        {
            //Code Duplication, Add to parent class ????
            if (timeSinceLastDecision >= decisionInterval)
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
