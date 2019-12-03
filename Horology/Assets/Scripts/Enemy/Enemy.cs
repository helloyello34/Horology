using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent onDeath;

    public virtual void Hit(int damage)
    {
    }
    private void Awake()
    {
        if (onDeath == null)
        {
            onDeath = new UnityEvent();
        }
    }


    public virtual void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
