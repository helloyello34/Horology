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
    private void Awake()
    {
        instance = this;
        originalTimeFactor = timeFactor;
        timeEvent = new boolEvent();
    }

    public UnityEvent<bool> timeEvent;

    public float timeFactor = 1;
    public float timeModifier;
    private float originalTimeFactor;
    public bool isSlowed = false;
    public bool isTimebarEmpty;

    private void Update()
    {
        if (Input.GetAxisRaw("Time") == 1 && !isSlowed && !isTimebarEmpty)
        {
            timeFactor *= timeModifier;
            isSlowed = true;
            timeEvent.Invoke(true);
        }
        else if ((Input.GetAxisRaw("Time") == 0 && isSlowed) || isTimebarEmpty)
        {
            timeFactor = originalTimeFactor;
            isSlowed = false;
            timeEvent.Invoke(false);
        }

        if(isSlowed)
        {
            timeEffect.fillAmount = 1;
        } else
        {
            timeEffect.fillAmount = 0;
        }
    }
}
