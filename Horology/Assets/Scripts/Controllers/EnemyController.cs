using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 4.0f;
    public float speed = 10f;

    Transform target;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        rb = GetComponent<Rigidbody2D>();
        //agent = GetComponent<NavMeshAgent>();

    }


    private void FixedUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            transform.position += speed * Time.fixedDeltaTime * (target.position - transform.position).normalized;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
