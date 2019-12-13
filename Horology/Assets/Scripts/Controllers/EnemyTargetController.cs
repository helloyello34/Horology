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
    private bool roaming = true;
    private Vector3 randomDirection = new Vector3(0, 0, 0);
    private bool firstPass = true;
    private float startingInterval;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isNotIdle = true;
    public override void Start()
    {
        //Calling base classes start method to initalize variables
        base.Start();

        //Random initial time between mode changes so every enemy doesn't switch at the same time
        timeSinceModeChange = Random.Range(0, 5);
        startingInterval = shootInterval;

        //Get animator component
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        shootInterval = Mathf.Lerp((startingInterval / TimeManager.instance.timeModifier), startingInterval, TimeManager.instance.currentToBaseRatio);
        animator.speed = TimeManager.instance.timeFactor;
        // if (isSlowed && firstPass)
        // {
        //     firstPass = false;
        //     shootInterval /= TimeManager.instance.timeModifier;
        //     //Slow down animation speed by timeFactor
        //     animator.speed = TimeManager.instance.timeFactor;
        // }
        // else if (!isSlowed && !firstPass)
        // {
        //     firstPass = true;
        //     shootInterval = startingInterval;
        //     //Set animation speed to normal pace
        //     animator.speed = 1;
        // }
    }

    void FixedUpdate()
    {
        base.FUpdate();
        //Set enemy animation to walking if he is not idle
        if (isNotIdle)
        {
            animator.Play("TargetEnemyWalking");
        }

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
        isNotIdle = true;

        if (distanceToPlayer <= aggroRadius && GameManager.instance.currentLevel == levelNumber)
        {
            //Permanently make enemy aggressive to player
            aggro = true;
        }

        //If enemy is not aggro, do nothing
        if (aggro)
        {
            //Flip sprite according to player position
            spriteRenderer.flipX = target.position.x < transform.position.x;

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
                    //If no movement is chosen, set bool flag so walking animation will not be triggered
                    //and play idle animation
                    if (randomDirection.magnitude == 0)
                    {
                        isNotIdle = false;
                        animator.Play("TargetEnemyIdle");
                    }
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
                //If no movement is chosen, set bool flag so walking animation will not be triggered
                //and play idle animation
                if (randomDirection.magnitude == 0)
                {
                    isNotIdle = false;
                    animator.Play("TargetEnemyIdle");
                }
            }

            //Flip sprite according to direction
            spriteRenderer.flipX = randomDirection.y < 0;
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
