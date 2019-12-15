using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class boolEvent : UnityEvent<bool>
{
}

public class TimeManager : MonoBehaviour
{

    public float alphaValue;
    public Image timeEffect;
    public static TimeManager instance;
    private bool toggle;
    private bool isActive;
    private bool once = true;

    public UnityEvent<bool> timeEvent;

    public float timeFactor = 1;
    public float timeModifier;
    public float timeIncrementer;
    public float originalTimeFactor;
    public float currentToBaseRatio;
    public bool isSlowed = false;
    public bool isTimebarEmpty;

    private void Awake()
    {
        instance = this;
        originalTimeFactor = timeFactor;
        timeEvent = new boolEvent();
        currentToBaseRatio = timeFactor / originalTimeFactor;
    }

    private void InputHandler()
    {
        if (GameManager.instance.isDead || GameManager.instance.isWin)
        {
            isActive = false;
            return;
        }
        if (PlayerPrefs.GetString("TimeJuiceMode", "Hold") == "Toggle")
        {
            if (Input.GetAxisRaw("Time") == 1 && once)
            {
                isActive = !isActive;
                once = false;
            }
            else if (Input.GetAxisRaw("Time") == 0)
            {
                once = true;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Time") == 1)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
        }
    }

    private void TimeChanger(float amount)
    {
        timeFactor += amount * Time.fixedDeltaTime;
        if (timeFactor < originalTimeFactor * timeModifier)
        {
            timeFactor = originalTimeFactor * timeModifier;
        }
        else if (timeFactor > originalTimeFactor)
        {
            timeFactor = originalTimeFactor;
        }
        currentToBaseRatio = timeFactor / originalTimeFactor;
    }

    private void FixedUpdate()
    {
        TimeChanger(isSlowed ? -timeIncrementer : timeIncrementer);
    }

    private void Update()
    {
       
        InputHandler();
        if (isActive && !isSlowed && !isTimebarEmpty)
        {
            // timeFactor *= timeModifier;
            isSlowed = true;
            timeEvent.Invoke(true);
        }
        else if ((!isActive && isSlowed) || isTimebarEmpty)
        {
            // timeFactor = originalTimeFactor;
            isSlowed = false;
            timeEvent.Invoke(false);
        }

        // if(isSlowed)
        // {
        //     timeEffect.fillAmount = 1;
        // } else
        // {
        //     timeEffect.fillAmount = 0;
        // }
    }
}
