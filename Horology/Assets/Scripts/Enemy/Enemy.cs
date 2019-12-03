using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Hit(int damage)
    {

    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
