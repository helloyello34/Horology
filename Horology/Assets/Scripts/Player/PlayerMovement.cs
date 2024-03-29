﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float currentSpeed;
    private float currentMaxSpeed;
    public Vector3 direction;
    private UnityAction<float, float> dashCallback;
    private bool isDashing = false;
    private float baseSpeed;
    private float dashDuration = 0;
    private float dashElapsed = 0;
    private Rigidbody2D rb;
    public AudioSource stepSound1;
    public AudioSource stepSound2;
    private bool soundToPlay = true;
    private float stepTimer = 0;
    public float stepInterval;
    public GameObject body;
    public GameObject head;
    private Animator bodyAnimator;
    private Animator headAnimator;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3();
        // baseSpeed = movementSpeed;
        dashCallback += CallbackFunc;
        PlayerManager.instance.player.GetComponent<Player>().dashEvent.AddListener(dashCallback);
        rb = GetComponent<Rigidbody2D>();

        //Fetch Animator component for head and body sprite
        bodyAnimator = body.GetComponent<Animator>();
        headAnimator = head.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isDead || GameManager.instance.isWin)
        {
            direction = new Vector2(0, 0);
            bodyAnimator.speed = 0f;
            headAnimator.speed = 0f;
            return;
        }
        if (!isDashing)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
            if (direction.magnitude > 1f)
            {
                direction = direction.normalized;
            }
        }

        if (currentSpeed <= 0)
        {
            //If player is standing still, cue idle animation
            bodyAnimator.Play("PlayerIdle");
            headAnimator.Play("IdleHair");
        }
        else
        {
            //If player is moving, cue walking animation
            bodyAnimator.Play("Walking");
            headAnimator.Play("WalkingHair");
        }
    }

    private void FixedUpdate()
    {
        stepTimer = direction.magnitude > 0 ? stepTimer + Time.fixedDeltaTime : 0;
        dashElapsed = isDashing ? dashElapsed + Time.fixedDeltaTime : 0;

        if (stepTimer > stepInterval)
        {
            PlaySound();
        }

        if (isDashing)
        {
            isDashing = dashElapsed < dashDuration;
            if (!isDashing)
            {
                currentSpeed = maxSpeed;
            }
            rb.velocity = direction.normalized * currentSpeed;
        }
        else
        {
            currentMaxSpeed = Mathf.Lerp(0, maxSpeed, direction.magnitude);
            currentSpeed += acceleration * direction.magnitude;
            if (currentSpeed > currentMaxSpeed)
            {
                currentSpeed = currentMaxSpeed;
            }
            if (direction.magnitude > 0.2)
            {
                rb.velocity = direction.normalized * currentSpeed;
            }
        }
    }

    public void CallbackFunc(float duration, float speed)
    {
        isDashing = true;
        dashElapsed = 0;
        dashDuration = duration;
        currentMaxSpeed = maxSpeed * speed;
        currentSpeed = currentMaxSpeed;
    }

    private void PlaySound()
    {
        if (soundToPlay)
        {
            stepSound1.Play();
        }
        else
        {
            stepSound2.Play();
        }
        soundToPlay = !soundToPlay;
        stepTimer = 0;
    }
}
