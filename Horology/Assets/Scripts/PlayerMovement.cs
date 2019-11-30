using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    Vector3 direction;
    Vector3 looking;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3();
        looking = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        looking.x = Input.GetAxis("ShootHorizontal");
        looking.y = Input.GetAxis("ShootVertical");
        float angle = Mathf.Atan2(looking.y, looking.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
    }

    private void FixedUpdate()
    {
        transform.position += movementSpeed * direction * Time.fixedDeltaTime;
    }
}
