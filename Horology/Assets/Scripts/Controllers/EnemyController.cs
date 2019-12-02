using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float aggroRadius = 6.0f;
    public float outerRadius = 5.0f;
    public float innerRadius = 3.0f;
    [Space]
    public float speed = 10f;
    public float shootInterval;
    public float decisionInterval;
    public float lookRadius = 4.0f;


    private float timeSinceShot = 0;
    private float timeSinceLastDecision = 0;
    private bool aggro = false;
    private Vector3 randomDirection = new Vector3(0, 0, 0);

    Transform target;
    Rigidbody2D rb;

    EnemyWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<EnemyWeapon>();
        //agent = GetComponent<NavMeshAgent>();

    }


    void FixedUpdate()
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
         
            timeSinceShot += Time.fixedDeltaTime;
            timeSinceLastDecision += Time.fixedDeltaTime;

            float movement = Time.fixedDeltaTime * speed;

            //Remember to implement enemy stop to shoot or slow down
            if (timeSinceShot >= shootInterval)
            {
                timeSinceShot = 0;
                //weapon.Shoot();
            }
            else if(distanceToPlayer > outerRadius) //If player is outside outerRadius -> move closer
            {
                //Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, target.position, movement);
            }
            else if(distanceToPlayer < innerRadius) //If player is within innerRaddius -> move away
            {
                //Move away from the player
                transform.position = Vector3.MoveTowards(transform.position, transform.position - target.position, movement);
            }
            else if(timeSinceLastDecision >= decisionInterval)
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
            //Do nothing for now
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
