using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 direction;
    private UnityAction<float, float> dashCallback;
    private bool isDashing = false;
    private float baseSpeed;
    private float dashDuration = 0;
    private float dashElapsed = 0;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3();
        baseSpeed = movementSpeed;
        dashCallback += CallbackFunc;
        PlayerManager.instance.player.GetComponent<Player>().dashEvent.AddListener(dashCallback);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashElapsed += Time.fixedDeltaTime;
            isDashing = dashElapsed < dashDuration;
            if (!isDashing)
            {
                movementSpeed = baseSpeed;
            }
        }
        rb.velocity = direction * movementSpeed;
    }

    public void CallbackFunc(float duration, float speed)
    {
        Debug.Log("CALLBACK");
        isDashing = true;
        dashElapsed = 0;
        dashDuration = duration;
        movementSpeed *= speed;
    }
}
