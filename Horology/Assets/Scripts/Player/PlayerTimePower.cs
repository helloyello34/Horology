using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTimePower : MonoBehaviour
{
    public float startingMana = 100;
    public float maxMana = 100;
    public float currentMana;
    public float manaDepletionSpeed = 10f;
    private bool isDepleting = false;
    private UnityAction<bool> usingMana;
    public GameObject manaBar;

    void Start()
    {
        // Set up current power and add listener to time event
        currentMana = startingMana;
        usingMana += UseMana;
        TimeManager.instance.timeEvent.AddListener(usingMana);
    }

    public void UseMana(bool use)
    {
        // Indicate that this should be called again in fixed update since event only happens on button press
        isDepleting = use;
        // If the player has mana
        if (currentMana != 0)
        {
            // Indicate that the bar is not empty
            TimeManager.instance.isTimebarEmpty = false;
            // Depletes the mana by depletion speed per second, if it goes below zero subtract that negative amount to get 0
            currentMana -= use && currentMana > 0 ? manaDepletionSpeed * Time.fixedDeltaTime : 0;
            if (currentMana < 0)
            {
                currentMana -= currentMana;
            }
            // Interpolate the bar size based on current/max mana
            manaBar.GetComponent<TimeBar>().SetBar(currentMana, maxMana);
        }
        else
        {
            // Indicate that the bar has been depleted, Tells time manager to invoke normal speed
            TimeManager.instance.isTimebarEmpty = true;
        }
    }

    public void IncreaseMana(float amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaBar.GetComponent<TimeBar>().SetBar(currentMana, maxMana);
    }

    private void FixedUpdate()
    {
        if (isDepleting)
        {
            UseMana(true);
        }

        // Tells the TimeManager that it can slow again after bar was depleted and player picked up more power
        if (Input.GetAxisRaw("Time") == 1 && currentMana > 0)
        {
            TimeManager.instance.isTimebarEmpty = false;
        }

        // For testing
        // if (Input.GetButton("Cancel"))
        // {
        //     IncreaseMana(30 * Time.fixedDeltaTime);
        // }
    }
}
