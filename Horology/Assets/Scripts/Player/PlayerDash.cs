using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashEvent : UnityEvent<float, float>
{
    // float = dashDuration
    // float = dashSpeed
}

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed;
    public float dashDuration;
    private bool canDash;
    private bool isDashing;
    private float dashTimeElapsed;
    private PlayerMovement playerMovement;
    private UnityEvent<float, float> dashEvent;


    void Start()
    {
        playerMovement = PlayerManager.instance.player.GetComponent<PlayerMovement>();
        dashEvent = PlayerManager.instance.player.GetComponent<Player>().dashEvent;
    }

    void Update()
    {
        Debug.Log("DASH");
        if ( (Input.GetAxisRaw("Dash") == 1 || Input.GetButtonDown("Cancel")) && !isDashing && canDash)
        {
            Debug.Log("DASH");
            isDashing = true;
            canDash = false;
            dashTimeElapsed = 0;
            dashEvent.Invoke(dashDuration, dashSpeed);
        }

        if (Input.GetAxisRaw("Dash") == 0)
        {
            canDash = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimeElapsed += Time.fixedDeltaTime;

            if (dashTimeElapsed > dashDuration)
            {
                isDashing = false;
                Debug.Log("DASH OFF");
            }
        }
    }
}
