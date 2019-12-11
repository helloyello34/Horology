using System.Collections;
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
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float timeSinceShot = 0;
    private float timeSinceLastDecision = 0;
    private Vector3 randomDirection = new Vector3(0, 0, 0);

    private Rigidbody2D rb;
    public override void Start()
    {
        //Calling base classes start method to initalize variables
        base.Start();
        startingInterval = shootInterval;

        //Get animator component
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isSlowed && firstPass)
        {
            firstPass = false;
            shootInterval *= (1 + TimeManager.instance.timeFactor);
            //Slow down animation speed by timeFactor
            animator.speed = TimeManager.instance.timeFactor;
        }
        else if (!isSlowed && !firstPass)
        {
            firstPass = true;
            shootInterval = startingInterval;
            //Set animation speed to normal pace
            animator.speed = 1;
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
        //Add delta time to counters
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

            //Flip sprite according to player position
            spriteRenderer.flipX = target.position.x < transform.position.x;

            timeSinceShot += dt;

            if (timeSinceShot >= shootInterval)
            {
                timeSinceShot = 0;
                weapon.Shoot();
            }

            if (distanceToPlayer > outerRadius) //If player is outside outerRadius -> move closer
            {
                //Move towards the player
                //transform.position = Vector3.MoveTowards(transform.position, target.position, movement);
                rb.velocity = (target.position - transform.position) * movement;
            }
            else if (distanceToPlayer < innerRadius) //If player is within innerRaddius -> move away
            {
                //Move away from the player
                //transform.position = Vector3.MoveTowards(transform.position, transform.position - target.position, movement);
                rb.velocity = ((transform.position - target.position) - transform.position) * movement;
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
                //transform.Translate(randomDirection * movement);
                rb.velocity = randomDirection * movement;
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
                //transform.Translate(randomDirection * movement);
                rb.velocity = randomDirection * movement;
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
