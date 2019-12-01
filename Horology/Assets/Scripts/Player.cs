using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int startingHealth = 6;
    public int maxHealth = 6;
    public int currentHealth = 6;

    public UnityEvent modifyHearts;


    private void Awake()
    {
        Debug.Log("Emit");
        if(modifyHearts == null)
        {
            modifyHearts = new UnityEvent();
        }
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;

        //Emit event to update heart health bar
        modifyHearts.Invoke();

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
