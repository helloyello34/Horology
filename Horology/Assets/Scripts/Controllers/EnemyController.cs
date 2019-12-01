using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 4.0f;
    public float speed = 10f;
    float timeSinceShot = 0;
    public float shootInterval;

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


    private void FixedUpdate()
    {
        timeSinceShot += Time.fixedDeltaTime;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            float step = Time.fixedDeltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            
            if (timeSinceShot >= shootInterval)
            {
                timeSinceShot = 0;
                Debug.Log("Shoot");
                weapon.Shoot();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
