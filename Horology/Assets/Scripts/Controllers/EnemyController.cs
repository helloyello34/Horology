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
    public bool isSlowed;
    public bool aggro = false;
    public virtual void Start()
    {
        target = PlayerManager.instance.player.transform;
        weapon = GetComponentInChildren<EnemyWeapon>();

        timeCallback += TimeSlow;
        TimeManager.instance.timeEvent.AddListener(timeCallback);
        baseSpeed = speed;
    }


    void FixedUpdate()
    {
    }

    public void TimeSlow(bool slowed)
    {
        isSlowed = slowed;
        if (slowed)
        {
            speed *= TimeManager.instance.timeFactor;
        }
        else
        {
            speed = baseSpeed;
        }
    }
}
