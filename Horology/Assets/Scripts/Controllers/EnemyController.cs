using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float aggroRadius = 6.0f;
    public float outerRadius = 5.0f;
    public float innerRadius = 3.0f;
    [Space]
    public float speed = 10f;
    private float baseSpeed;
    public float shootInterval;
    public float decisionInterval;
    public float modeInterval;
    public float lookRadius = 4.0f;
    [Space]
    public bool doesSlow = true;
    private UnityAction<bool> timeCallback;

    private float timeSinceShot = 0;
    private float timeSinceLastDecision = 0;
    private float timeSinceModeChange = 0;
    private bool aggro = false;
    private bool roaming = true;
    private Vector3 randomDirection = new Vector3(0, 0, 0);

    Transform target;

    EnemyWeapon weapon;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        weapon = GetComponentInChildren<EnemyWeapon>();

        //Random initial time between mode changes so every enemy doesn't switch at the same time
        timeSinceModeChange = Random.Range(0, 5);

        timeCallback += TimeSlow;
        TimeManager.instance.timeEvent.AddListener(timeCallback);
        baseSpeed = speed;
        //agent = GetComponent<NavMeshAgent>();
    }


    void FixedUpdate()
    {
        EnemyMovement();
    }

    public void TimeSlow(bool isSlowed)
    {
        // Debug.Log("enemy slow called");
        if (isSlowed)
        {
            speed *= TimeManager.instance.timeFactor;
        }
        else
        {
            speed = baseSpeed;
        }
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
            timeSinceShot += dt;
            float movement = dt * speed;

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
            //Do nothing for now, //Idle animation maybe ?
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }
}
