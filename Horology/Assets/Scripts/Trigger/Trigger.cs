using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onEnter;

    private void Awake()
    {
        // Create new unity event if the event object was not yet initialized
        if (onEnter == null)
        {
            onEnter = new UnityEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player walked over the trigger
        Player player = collision.GetComponent<Player>();
        EnemyController enemy = collision.GetComponent<EnemyController>();

        Debug.Log("Hit " + collision.name);

        if (player)
        {
            onEnter.Invoke();
        } else if (enemy)
        {
            enemy.ReverseMovement();
        }
    }
}
