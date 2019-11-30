using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        transform.position += movementSpeed * direction * Time.deltaTime;
    }
}
