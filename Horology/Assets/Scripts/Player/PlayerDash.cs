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
    public float dashCooldown;
    private bool canDash = true;
    private bool isDashing = false;
    private float cooldownElapsed = 0;
    private float dashTimeElapsed = 0;
    private PlayerMovement playerMovement;
    private UnityEvent<float, float> dashEvent;
    public TimeBar dashBar;


    void Start()
    {
        playerMovement = PlayerManager.instance.player.GetComponent<PlayerMovement>();
        dashEvent = PlayerManager.instance.player.GetComponent<Player>().dashEvent;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if ((Input.GetAxisRaw("Dash") == 1 || Input.GetButtonDown("Cancel")) && !isDashing && canDash && direction.magnitude > 0.2)
        {
            isDashing = true;
            canDash = false;
            dashTimeElapsed = 0;
            dashEvent.Invoke(dashDuration, dashSpeed);
        }
        else if ((Input.GetAxisRaw("Dash") == 0 || Input.GetButtonUp("Cancel")) && cooldownElapsed > dashCooldown)
        {
            canDash = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            cooldownElapsed += Time.fixedDeltaTime;
            dashBar.SetBar(cooldownElapsed, dashCooldown);
        }
        else
        {
            dashTimeElapsed += Time.fixedDeltaTime;

            if (dashTimeElapsed > dashDuration)
            {
                isDashing = false;
                cooldownElapsed = 0;
            }
        }
    }
}
