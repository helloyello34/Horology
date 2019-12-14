using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public float speed = 10f;
    [Space]
    public bool doesSlow = true;

    [HideInInspector]
    public UnityAction<bool> timeCallback;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public EnemyWeapon weapon;
    [HideInInspector]
    public float baseSpeed;
    private float currentSpeed, minSpeed;
    public bool isSlowed;
    public bool aggro = false;

    [HideInInspector]
    public Rigidbody2D rb;

    public virtual void Start()
    {
        target = PlayerManager.instance.player.transform;
        weapon = GetComponentInChildren<EnemyWeapon>();

        timeCallback += TimeSlow;
        TimeManager.instance.timeEvent.AddListener(timeCallback);
        baseSpeed = speed;
        currentSpeed = speed;
        minSpeed = baseSpeed * TimeManager.instance.timeModifier;
    }

    void ChangeSpeed(float amount)
    {
        currentSpeed += amount;
        if (currentSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
        }
        else if (currentSpeed > baseSpeed)
        {
            currentSpeed = baseSpeed;
        }

        rb = GetComponent<Rigidbody2D>();
    }


    public void FUpdate()
    {
        speed = Mathf.Lerp(minSpeed, baseSpeed, TimeManager.instance.currentToBaseRatio);
    }

    public void TimeSlow(bool slowed)
    {
        isSlowed = slowed;
    }

    public virtual void ReverseMovement() { }


}
