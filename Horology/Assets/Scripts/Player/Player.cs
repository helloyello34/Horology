﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int startingHealth = 6;
    public int maxHealth = 6;
    public int currentHealth = 6;

    public UnityEvent modifyHearts;
    public UnityEvent death;


    private void Awake()
    {
        // Instansiate unity event if it is null
        if(modifyHearts == null)
        {
            modifyHearts = new UnityEvent();
        }
        if (death == null)
        {
            death = new UnityEvent();
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

    public void Heal()
    {
        //Function to heal player by 1 heart
        //Called when heart drop is picked up

        //+2 for 2 halfs of a heart
        currentHealth += 2;

        //If health exceeds max health then set current to max
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //Invoke event to change health sprites
        modifyHearts.Invoke();
    }


    // Called when player dies
    private void Die()
    {
        // Destroys the player object
        death.Invoke();
    }
}
