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
        // Instansiate unity event if it is null
        if(modifyHearts == null)
        {
            modifyHearts = new UnityEvent();
        }
    }

    public void Hit(int damage)
    {
        // Take damage
        currentHealth -= damage;

        // Emit event to update heart health bar
        modifyHearts.Invoke();

        // Call death function if health equals or goes under 0
        if(currentHealth <= 0)
        {
            Die();
        }

    }


    // Called when player dies
    private void Die()
    {
        // Destroys the player object
        Destroy(gameObject);
    }
}
