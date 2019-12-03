using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onEnter;

    private void Awake()
    {
        if (onEnter == null)
        {
            onEnter = new UnityEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            onEnter.Invoke();
        }
    }
}
