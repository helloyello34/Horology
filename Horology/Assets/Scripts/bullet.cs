using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        //direction = transform.right * speed;
        rb.velocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, direction, Time.fixedDeltaTime);

    }
}
